using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Drawing;
using static AutoCADTools.PrintLayout.LayoutCreationSpecification;

namespace AutoCADTools.PrintLayout
{
    class LayoutCreator
    {
        private readonly LayoutCreationSpecification _specification;

        private Document Document { get { return _specification.Document; } }
        private string LayoutName { get { return _specification.LayoutName; } }
        private double DrawingUnit { get { return _specification.DrawingUnit; } }
        private double Scale { get { return _specification.Scale; } }
        private PaperOrientation Orientation { get { return _specification.Orientation; } }
        private PrinterPaperformat PrinterPaperformat { get { return _specification.Printerformat; } }
        private Paperformat Paperformat { get { return _specification.Paperformat; } }
        private Point LowerRightPoint { get { return _specification.PrintArea.LowerRightPoint; } }
        private bool RotateViewport { get { return _specification.RotateViewport; } }

        public LayoutCreator(LayoutCreationSpecification specification)
        {
            this._specification = specification;
        }

        /// <summary>
        /// Creates the layout with the specified attributes. If a layout with the specified name already exists, it is removed.
        /// </summary>
        /// <returns><c>true</c> if the layout was successfully created, <c>false</c> otherwise.</returns>
        public bool CreateLayout()
        {
            if (!_specification.IsValid)
            {
                return false;
            }

            using (Document.LockDocument())
            {
                var oldCos = Document.Editor.CurrentUserCoordinateSystem;
                // Set the coordinate system to WCS
                if (!(Document.Editor.CurrentUserCoordinateSystem.Equals(Matrix3d.Identity)))
                {
                    Document.Editor.CurrentUserCoordinateSystem = Matrix3d.Identity;
                }

                if (!LayoutManager.Current.GetLayoutId(LayoutName).IsNull)
                {
                    LayoutManager.Current.DeleteLayout(LayoutName);
                }

                // Create the new layout and activate it
                LayoutManager.Current.CreateLayout(LayoutName);
                LayoutManager.Current.CurrentLayout = LayoutName;

                // Start the transaction
                using (Transaction trans = Document.Database.TransactionManager.StartTransaction())
                {
                    // Get BlockTable and the BlockTableRecord of the active layout
                    BlockTable blockTable = trans.GetObject(Document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord layoutRecord = trans.GetObject(Document.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                    // Get the current layout and create new plot settings copied from current layouts one
                    Layout layout = trans.GetObject(LayoutManager.Current.GetLayoutId(LayoutManager.Current.CurrentLayout),
                        OpenMode.ForWrite) as Layout;

                    // Erase the existing object in the new layout (especially the automatically created viewport)
                    foreach (ObjectId id in layoutRecord)
                    {
                        trans.GetObject(id, OpenMode.ForWrite).Erase();
                    }

                    // Get current PlotSettingsValidator and set printer and format
                    PlotSettingsValidator psv = PlotSettingsValidator.Current;
                    psv.SetPlotConfigurationName(layout, PrinterPaperformat.Printer.Name + Printer.PrinterConfigurationFileExtension, PrinterPaperformat.FormatName);

                    // Set the plot settings and validate them
                    psv.RefreshLists(layout);
                    if (PrinterPaperformat.Printer.Name == "PNG")
                    {
                        psv.SetPlotPaperUnits(layout, PlotPaperUnit.Pixels);
                        psv.SetCustomPrintScale(layout, new CustomScale(DrawingUnit * 10, 1));
                    }
                    else
                    {
                        psv.SetPlotPaperUnits(layout, PlotPaperUnit.Millimeters);
                        psv.SetCustomPrintScale(layout, new CustomScale(DrawingUnit, 1));
                    }
                    psv.SetPlotType(layout, Autodesk.AutoCAD.DatabaseServices.PlotType.Layout);

                    // Get size and margins of the layout
                    Double width = layout.PlotPaperSize.X;
                    Double height = layout.PlotPaperSize.Y;
                    Size margin = new Size(layout.PlotPaperMargins.MinPoint.X / DrawingUnit, layout.PlotPaperMargins.MinPoint.Y / DrawingUnit);

                    // Get the right rotation and switch width and height if needed
                    if ((width < height && Orientation == PaperOrientation.Portrait) ||
                        (height < width && Orientation == PaperOrientation.Landscape))
                    {
                        psv.SetPlotRotation(layout, PlotRotation.Degrees000);
                    }
                    else
                    {
                        psv.SetPlotRotation(layout, PlotRotation.Degrees090);
                        Double temp = width;
                        width = height;
                        height = temp;
                        margin = margin.Rotate();
                    }

                    Viewport PVport = new Viewport
                    {
                        Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black),
                        LayerId = Document.Database.LayerZero
                    };
                    layoutRecord.AppendEntity(PVport);
                    trans.AddNewlyCreatedDBObject(PVport, true);

                    Polyline viewport = CreateViewportPolyline(margin);
                    layoutRecord.AppendEntity(viewport);
                    trans.AddNewlyCreatedDBObject(viewport, true);

                    // Make viewport non rectangular and set lineweight
                    PVport.NonRectClipEntityId = viewport.ObjectId;
                    PVport.NonRectClipOn = true;
                    PVport.LineWeight = LineWeight.LineWeight050;

                    // If A4 format set viewport borders white
                    if (Paperformat is PaperformatTextfieldA4)
                    {
                        viewport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.White);
                        PVport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.White);
                    }

                    if (RotateViewport)
                    {
                        PVport.TwistAngle = Math.PI / 2;
                    }

                    if (Paperformat is PaperformatTextfield && !DrawLayoutAdditions(Paperformat as PaperformatTextfield, margin, layoutRecord))
                    {
                        trans.Abort();
                        return false;
                    }

                    // Turn the viewport on
                    PVport.On = true;

                    // Swap end if turned VP
                    if (RotateViewport)
                    {
                        // Take care of the turned viewport, x-axis is the original y and the y-axis is the inverted original x
                        PVport.ViewCenter = new Point2d(-(LowerRightPoint.Y + Paperformat.ViewportSizeModel.Height / DrawingUnit / Scale / 2), LowerRightPoint.X - Paperformat.ViewportSizeModel.Width / DrawingUnit / Scale / 2);
                        PVport.ViewHeight = Paperformat.ViewportSizeModel.Width / DrawingUnit / Scale;
                    }
                    else
                    {
                        // Set the view of the viewport
                        PVport.ViewCenter = new Point2d(LowerRightPoint.X - Paperformat.ViewportSizeModel.Width / DrawingUnit / Scale / 2, LowerRightPoint.Y + Paperformat.ViewportSizeModel.Height / DrawingUnit / Scale / 2);
                        PVport.ViewHeight = Paperformat.ViewportSizeModel.Height / DrawingUnit / Scale;
                    }

                    //PVport.StandardScale = StandardScaleType.CustomScale;
                    PVport.CustomScale = Scale;

                    // Switch to modelspace and back to prevent unwanted situations
                    LayoutManager.Current.CurrentLayout = "Model";
                    LayoutManager.Current.CurrentLayout = LayoutName;

                    // Set the annotation Scale for the viewport
                    ObjectContextCollection occ = Document.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
                    AnnotationScale annoScale = occ.GetContext("1:" + (1.0 / Scale).ToString()) as AnnotationScale;
                    if (annoScale != null)
                    {
                        PVport.AnnotationScale = annoScale;
                    }
                    else
                    {
                        PVport.AnnotationScale = Document.Database.Cannoscale;
                    }

                    // Dispose the used objects
                    PVport.Dispose();
                    viewport.Dispose();

                    Document.Editor.CurrentUserCoordinateSystem = oldCos;

                    // Close everything and commit changes
                    trans.Commit();
                }

                var oldEcho = Application.GetSystemVariable("CMDECHO");
                Application.SetSystemVariable("CMDECHO", 0);
                Document.Editor.Command("_.ZOOM", "G");
                Document.Editor.Command("_.ZOOM", ".8x");
                Application.SetSystemVariable("CMDECHO", oldEcho);
            }

            return true;
        }

        /// <summary>
        /// Draws additional parts in the layout: textfield, borders and fold lines.
        /// </summary>
        /// <param name="margin">The margin of the used paper.</param>
        /// <param name="layoutRecord">The layout record of the created layout.</param>
        /// <returns></returns>
        private bool DrawLayoutAdditions(PaperformatTextfield paperformat, Size margin, BlockTableRecord layoutRecord)
        {
            using (Transaction trans = Document.Database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = trans.GetObject(Document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                if (!blockTable.Has(paperformat.TextfieldBlockName))
                {
                    trans.Abort();
                    return false;
                }

                // Create polyline and add the vertices
                Polyline pBorder = new Polyline
                {
                    Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black)
                };

                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d(Paperformat.BorderBasePoint.X / DrawingUnit - margin.Width,
                        Paperformat.BorderBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d((Paperformat.BorderBasePoint.X + Paperformat.BorderSize.Width) / DrawingUnit - margin.Width,
                        Paperformat.BorderBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d((Paperformat.BorderBasePoint.X + Paperformat.BorderSize.Width) / DrawingUnit - margin.Width,
                        (Paperformat.BorderBasePoint.Y + Paperformat.BorderSize.Height) / DrawingUnit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d(Paperformat.BorderBasePoint.X / DrawingUnit - margin.Width,
                    (Paperformat.BorderBasePoint.Y + Paperformat.BorderSize.Height) / DrawingUnit - margin.Height), 0, 0, 0);
                pBorder.Closed = true;

                // Append polyline and tell transaction about it
                layoutRecord.AppendEntity(pBorder);
                trans.AddNewlyCreatedDBObject(pBorder, true);
                pBorder.Dispose();

                // Create a line for the nippel and set start- and endpoint depending on format
                Line halfSizeMark = new Line
                {
                    Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black),
                    LayerId = Document.Database.LayerZero
                };

                if (Paperformat is PaperformatTextfieldA4)
                {
                    halfSizeMark.StartPoint = new Point3d((Paperformat.BorderBasePoint.X - 10.0) / DrawingUnit - margin.Width,
                        (Paperformat.BorderBasePoint.Y + Paperformat.BorderSize.Height / 2) / DrawingUnit - margin.Height, 0);
                }
                else
                {
                    double remainingHeight = Paperformat.BorderSize.Height;
                    int counter = 0;
                    while (remainingHeight > PaperformatTextfieldCustom.FoldPeriod.Y)
                    {
                        counter++;
                        using (Line foldLine = new Line())
                        {
                            foldLine.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                            foldLine.LayerId = Document.Database.LayerZero;
                            foldLine.StartPoint = new Point3d(Paperformat.BorderBasePoint.X / DrawingUnit - margin.Width,
                                    (Paperformat.BorderBasePoint.Y + counter * PaperformatTextfieldCustom.FoldPeriod.Y) / DrawingUnit - margin.Height, 0);
                            foldLine.EndPoint = foldLine.StartPoint.Add(new Vector3d((Paperformat.ViewportBasePoint.X - Paperformat.BorderBasePoint.X) / DrawingUnit, 0, 0));
                            layoutRecord.AppendEntity(foldLine);
                            trans.AddNewlyCreatedDBObject(foldLine, true);
                        }
                        remainingHeight -= PaperformatTextfieldCustom.FoldPeriod.Y;
                    }
                    halfSizeMark.StartPoint = new Point3d((Paperformat.ViewportBasePoint.X - 10.0) / DrawingUnit - margin.Width,
                        (Paperformat.BorderBasePoint.Y + PaperformatTextfieldCustom.FoldPeriod.Y / 2) / DrawingUnit - margin.Height, 0);
                }

                halfSizeMark.EndPoint = halfSizeMark.StartPoint.Add(new Vector3d(10.0 / DrawingUnit, 0, 0));

                // Append nippel polyline and tell transaction about it
                layoutRecord.AppendEntity(halfSizeMark);
                trans.AddNewlyCreatedDBObject(halfSizeMark, true);
                halfSizeMark.Dispose();

                // Create a new BlockReference of the right textfield and get the record of the block
                BlockTableRecord textfieldBlock = trans.GetObject(blockTable[paperformat.TextfieldBlockName], OpenMode.ForRead) as BlockTableRecord;
                BlockReference textfield = new BlockReference(new Point3d(paperformat.TextfieldBasePoint.X / DrawingUnit - margin.Width,
                    paperformat.TextfieldBasePoint.Y / DrawingUnit - margin.Height, 0), blockTable[paperformat.TextfieldBlockName])
                {
                    Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black),
                    LayerId = Document.Database.LayerZero
                };

                // Set right Scalefactor (should be 0.01)
                textfield.ScaleFactors = new Scale3d(textfield.UnitFactor);
                if (paperformat.OldTextfieldSize)
                {
                    textfield.ScaleFactors = new Scale3d(10 / DrawingUnit);
                    textfield.Position = textfield.Position.Add(new Vector3d(paperformat.TextfieldSize.Width / DrawingUnit, -paperformat.TextfieldSize.Height / DrawingUnit, 0));
                }

                // Append the textfield and tell transaction about it
                layoutRecord.AppendEntity(textfield);
                trans.AddNewlyCreatedDBObject(textfield, true);

                // Go through object in record and add a reference of the attributes to the BlockReference
                foreach (ObjectId id in textfieldBlock)
                {
                    AttributeDefinition attDef = trans.GetObject(id, OpenMode.ForRead) as AttributeDefinition;
                    if (attDef != null)
                    {
                        using (AttributeReference attRef = new AttributeReference())
                        {
                            // Position the attribute reference in the block reference
                            attRef.SetAttributeFromBlock(attDef, textfield.BlockTransform);
                            attRef.Position = attDef.Position.TransformBy(textfield.BlockTransform);

                            // Set Scale or date in the textfield
                            if (attRef.Tag == "MAßSTÄBE")
                            {
                                attRef.TextString = "1:" + Math.Round(1.0 / Scale).ToString();
                            }

                            // Append the attribute and tell transaction about it
                            textfield.AttributeCollection.AppendAttribute(attRef);
                            trans.AddNewlyCreatedDBObject(attRef, true);
                        }

                        attDef.Dispose();
                    }
                }

                textfield.Dispose();
                trans.Commit();
            }
            return true;
        }

        /// <summary>
        /// Creates the polyline to use as the viewport.
        /// </summary>
        /// <param name="margin">The margin of the paper used.</param>
        /// <returns>The created polyline.</returns>
        private Polyline CreateViewportPolyline(Size margin)
        {
            Polyline viewport = new Polyline
            {
                Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black),
                LayerId = Document.Database.LayerZero
            };

            // Set the vertices of the viewport poly
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d(Paperformat.ViewportBasePoint.X / DrawingUnit - margin.Width,
                    Paperformat.ViewportBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
            if (Paperformat is PaperformatTextfieldFullTextfield)
            {
                PaperformatTextfield ppTextfield = Paperformat as PaperformatTextfield;
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d(ppTextfield.TextfieldBasePoint.X / DrawingUnit - margin.Width,
                     ppTextfield.ViewportBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d(ppTextfield.TextfieldBasePoint.X / DrawingUnit - margin.Width,
                        ppTextfield.TextfieldBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d((ppTextfield.TextfieldBasePoint.X + ppTextfield.TextfieldSize.Width) / DrawingUnit - margin.Width,
                        ppTextfield.TextfieldBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
            }
            else
            {
                viewport.AddVertexAt(viewport.NumberOfVertices,
                   new Point2d((Paperformat.ViewportBasePoint.X + Paperformat.ViewportSizeLayout.Width) / DrawingUnit - margin.Width,
                       Paperformat.ViewportBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
            }
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d((Paperformat.ViewportBasePoint.X + Paperformat.ViewportSizeLayout.Width) / DrawingUnit - margin.Width,
                    (Paperformat.ViewportBasePoint.Y + Paperformat.ViewportSizeLayout.Height) / DrawingUnit - margin.Height), 0, 0, 0);
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d(Paperformat.ViewportBasePoint.X / DrawingUnit - margin.Width,
                    (Paperformat.ViewportBasePoint.Y + Paperformat.ViewportSizeLayout.Height) / DrawingUnit - margin.Height), 0, 0, 0);
            viewport.Closed = true;
            viewport.LineWeight = LineWeight.LineWeight050;

            return viewport;
        }
    }
}
