using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Drawing;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This class implements the basic layout creation process. It can be extended by implementing the hook DrawLayoutAdditions.
    /// </summary>
    public abstract class LayoutCreation
    {
        #region Enums

        /// <summary>
        /// Specifies an orientation of paper.
        /// </summary>
        public enum PaperOrientation
        {
            /// <summary>
            /// Portrait format.
            /// </summary>
            /// 
            Portrait,
            /// <summary>
            /// Landscape format.
            /// </summary>
            Landscape
        }

        #endregion

        #region Attributes

        private Paperformat paperformat;
        /// <summary>
        /// Gets the paperformat specified in the constructor.
        /// </summary>
        /// <value>
        /// The paperformat.
        /// </value>
        public Paperformat Paperformat
        {
            get { return paperformat; }
        }

        private double drawingUnit;
        /// <summary>
        /// Gets or sets the drawing unit (millimeters represented by a unit in the drawing).
        /// </summary>
        /// <value>
        /// The drawing unit (millimeters represented by a unit in the drawing).
        /// </value>
        public double DrawingUnit
        {
            get { return drawingUnit; }
            set { drawingUnit = value; }
        }

        private PrinterPaperformat printerformat;
        /// <summary>
        /// Gets or sets the format used for printing.
        /// </summary>
        /// <value>
        /// The format used for printing.
        /// </value>
        public PrinterPaperformat Printerformat
        {
            get { return printerformat; }
            set { printerformat = value; }
        }

        private Point extractLowerRightPoint;
        /// <summary>
        /// Gets or sets the lower right point of the extract to create the layout for.
        /// </summary>
        /// <value>
        /// The lower right point of the extract to create the layout for.
        /// </value>
        public Point ExtractLowerRightPoint
        {
            get { return extractLowerRightPoint; }
            set { extractLowerRightPoint = value; }
        }

        private string layoutName;
        /// <summary>
        /// Gets or sets the name of the layout to create.
        /// </summary>
        /// <value>
        /// The name of the layout to create.
        /// </value>
        public string LayoutName
        {
            get { return layoutName; }
            set { layoutName = value; }
        }

        private double scale;
        /// <summary>
        /// Gets or sets the scale factor the extract shell be printed with.
        /// </summary>
        /// <value>
        /// The scale factor the extract shell be printed with.
        /// </value>
        public double Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Specifies if the viewport has to be rotated.
        /// </summary>
        protected bool rotateViewport;

        private Document document;
        /// <summary>
        /// Gets the document the layout is created in.
        /// </summary>
        /// <value>
        /// The document the layout is created in.
        /// </value>
        public Document Document
        {
            get { return document; }
        }

        private PaperOrientation orientation;
        /// <summary>
        /// Gets or sets the orientation for the paper.
        /// </summary>
        /// <value>
        /// The orientation for the paper.
        /// </value>
        public PaperOrientation Orientation
        {
            get { return orientation; }
            set { orientation = rotateViewport ? PaperOrientation.Portrait : value; }
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutCreation"/> class for the specified paperformat.
        /// </summary>
        /// <param name="paperformat">The paperformat to create the layout for.</param>
        public LayoutCreation(Paperformat paperformat)
        {
            this.paperformat = paperformat;
            rotateViewport = paperformat is PaperformatTextfieldA4Horizontal;
            orientation = rotateViewport ? PaperOrientation.Portrait : PaperOrientation.Landscape;
            this.document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        }

        #endregion

        #region Creation

        /// <summary>
        /// Creates the layout with the specified attributes. If a layout with the specified name already exists, it is removed.
        /// </summary>
        /// <returns><c>true</c> if the layout was successfully created, <c>false</c> otherwise.</returns>
        public bool CreateLayout()
        {
            ValidateProperties();
                        
            using (document.LockDocument())
            {
                var oldCos = document.Editor.CurrentUserCoordinateSystem;
                // Set the coordinate system to WCS
                if (!(document.Editor.CurrentUserCoordinateSystem.Equals(Matrix3d.Identity)))
                {
                    document.Editor.CurrentUserCoordinateSystem = Matrix3d.Identity;
                }

                if (!LayoutManager.Current.GetLayoutId(layoutName).IsNull)
                {
                    LayoutManager.Current.DeleteLayout(layoutName);
                }

                // Create the new layout and activate it
                LayoutManager.Current.CreateLayout(layoutName);
                LayoutManager.Current.CurrentLayout = layoutName;

                // Start the transaction
                using (Transaction trans = document.Database.TransactionManager.StartTransaction())
                {
                    // Get BlockTable and the BlockTableRecord of the active layout
                    BlockTable blockTable = trans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord layoutRecord = trans.GetObject(document.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

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
                    psv.SetPlotConfigurationName(layout, printerformat.Printer.Name + ".pc3", printerformat.FormatName);

                    // Set the plot settings and validate them
                    psv.RefreshLists(layout);
                    if (printerformat.Printer.Name == "PNG")
                    {
                        psv.SetPlotPaperUnits(layout, PlotPaperUnit.Pixels);
                        psv.SetCustomPrintScale(layout, new CustomScale(drawingUnit * 10, 1));
                    }
                    else
                    {
                        psv.SetPlotPaperUnits(layout, PlotPaperUnit.Millimeters);
                        psv.SetCustomPrintScale(layout, new CustomScale(drawingUnit, 1));
                    }
                    psv.SetPlotType(layout, Autodesk.AutoCAD.DatabaseServices.PlotType.Layout);

                    // Get size and margins of the layout
                    Double width = layout.PlotPaperSize.X;
                    Double height = layout.PlotPaperSize.Y;
                    Size margin = new Size(layout.PlotPaperMargins.MinPoint.X / drawingUnit, layout.PlotPaperMargins.MinPoint.Y / drawingUnit);

                    // Get the right rotation and switch width and height if needed
                    if ((width < height && orientation == PaperOrientation.Portrait) ||
                        (height < width && orientation == PaperOrientation.Landscape))
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

                    Viewport PVport = new Viewport();
                    PVport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                    PVport.LayerId = document.Database.LayerZero;
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
                    if (paperformat is PaperformatTextfieldA4)
                    {
                        viewport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.White);
                        PVport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.White);
                    }

                    if (rotateViewport)
                    {
                        PVport.TwistAngle = Math.PI / 2;
                    }

                    if (!DrawLayoutAdditions(margin, layoutRecord))
                    {
                        trans.Abort();
                        return false;
                    }

                    // Turn the viewport on
                    PVport.On = true;

                    // Swap end if turned VP
                    if (rotateViewport)
                    {
                        // Take care of the turned viewport, x-axis is the original y and the y-axis is the inverted original x
                        PVport.ViewCenter = new Point2d(-(extractLowerRightPoint.Y + paperformat.ViewportSizeModel.Height / drawingUnit / scale / 2), extractLowerRightPoint.X - paperformat.ViewportSizeModel.Width / drawingUnit / scale / 2);
                        PVport.ViewHeight = paperformat.ViewportSizeModel.Width / drawingUnit / scale;
                    }
                    else
                    {
                        // Set the view of the viewport
                        PVport.ViewCenter = new Point2d(extractLowerRightPoint.X - paperformat.ViewportSizeModel.Width / drawingUnit / scale / 2, extractLowerRightPoint.Y + paperformat.ViewportSizeModel.Height / drawingUnit / scale / 2);
                        PVport.ViewHeight = paperformat.ViewportSizeModel.Height / drawingUnit / scale;
                    }

                    //PVport.StandardScale = StandardScaleType.CustomScale;
                    PVport.CustomScale = scale;

                    // Switch to modelspace and back to prevent unwanted situations
                    LayoutManager.Current.CurrentLayout = "Model";
                    LayoutManager.Current.CurrentLayout = layoutName;

                    // Set the annotation scale for the viewport
                    ObjectContextCollection occ = document.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
                    AnnotationScale annoScale = occ.GetContext("1:" + (1.0 / scale).ToString()) as AnnotationScale;
                    if (annoScale != null)
                    {
                        PVport.AnnotationScale = annoScale;
                    }
                    else
                    {
                        PVport.AnnotationScale = document.Database.Cannoscale;
                    }

                    // Dispose the used objects
                    PVport.Dispose();
                    viewport.Dispose();

                    document.Editor.CurrentUserCoordinateSystem = oldCos;

                    // Close everything and commit changes
                    trans.Commit();
                }

                var oldEcho = Application.GetSystemVariable("CMDECHO");
                Application.SetSystemVariable("CMDECHO", 0);
                document.Editor.Command("_.ZOOM", "G");
                document.Editor.Command("_.ZOOM", ".8x");
                Application.SetSystemVariable("CMDECHO", oldEcho); 
            }

            return true;
        }

        /// <summary>
        /// Validates the specified properties.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Drawing Unit or scale must not be null
        /// or
        /// Layout name must not be null or empty
        /// </exception>
        /// <exception cref="System.ArgumentNullException">No argument should be null</exception>
        private void ValidateProperties()
        {
            if (drawingUnit == 0)
            {
                throw new ArgumentException("Drawing Unit must not be null");
            }
            else if (scale == 0)
            {
                throw new ArgumentException("Scale must not be null");
            }
            if (extractLowerRightPoint == null || printerformat == null)
            {
                throw new ArgumentNullException();
            }
            if (String.IsNullOrWhiteSpace(layoutName))
            {
                throw new ArgumentException("Layout name must not be null or empty");
            }
        }
        
        /// <summary>
        /// Draws additional parts in the layout.
        /// </summary>
        /// <param name="margin">The margin of the used paper.</param>
        /// <param name="layoutRecord">The layout record of the created layout.</param>
        /// <returns></returns>
        protected abstract bool DrawLayoutAdditions(Size margin, BlockTableRecord layoutRecord);

        /// <summary>
        /// Creates the polyline to use as the viewport.
        /// </summary>
        /// <param name="margin">The margin of the paper used.</param>
        /// <returns>The created polyline.</returns>
        private Polyline CreateViewportPolyline(Size margin)
        {
            Polyline viewport = new Polyline();
            viewport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
            viewport.LayerId = document.Database.LayerZero;

            // Set the vertices of the viewport poly
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d(paperformat.ViewportBasePoint.X / drawingUnit - margin.Width,
                    paperformat.ViewportBasePoint.Y / drawingUnit - margin.Height), 0, 0, 0);
            if (paperformat is PaperformatTextfieldFullTextfield)
            {
                PaperformatTextfield ppTextfield = paperformat as PaperformatTextfield;
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d(ppTextfield.TextfieldBasePoint.X / drawingUnit - margin.Width,
                     ppTextfield.ViewportBasePoint.Y / drawingUnit - margin.Height), 0, 0, 0);
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d(ppTextfield.TextfieldBasePoint.X / drawingUnit - margin.Width,
                        ppTextfield.TextfieldBasePoint.Y / drawingUnit - margin.Height), 0, 0, 0);
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d((ppTextfield.TextfieldBasePoint.X + ppTextfield.TextfieldSize.Width) / drawingUnit - margin.Width,
                        ppTextfield.TextfieldBasePoint.Y / drawingUnit - margin.Height), 0, 0, 0);
            }
            else
            {
                viewport.AddVertexAt(viewport.NumberOfVertices,
                   new Point2d((paperformat.ViewportBasePoint.X + paperformat.ViewportSizeLayout.Width) / drawingUnit - margin.Width,
                       paperformat.ViewportBasePoint.Y / drawingUnit - margin.Height), 0, 0, 0);
            }
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d((paperformat.ViewportBasePoint.X + paperformat.ViewportSizeLayout.Width) / drawingUnit - margin.Width,
                    (paperformat.ViewportBasePoint.Y + paperformat.ViewportSizeLayout.Height) / drawingUnit - margin.Height), 0, 0, 0);
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d(paperformat.ViewportBasePoint.X / drawingUnit - margin.Width,
                    (paperformat.ViewportBasePoint.Y + paperformat.ViewportSizeLayout.Height) / drawingUnit - margin.Height), 0, 0, 0);
            viewport.Closed = true;
            viewport.LineWeight = LineWeight.LineWeight050;

            return viewport;
        }

        #endregion
    }
}
