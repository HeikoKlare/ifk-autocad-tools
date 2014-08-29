using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCADTools.PrintLayout
{
    public abstract class LayoutCreation
    {
        protected Paperformat paperformat;
        protected double unit;
        protected PrinterPaperformat printerformat;
        protected Point lowerLeftPoint;
        protected string name;
        protected double scale;
        protected bool rotateViewport;
        protected Document document;

        public LayoutCreation(string name, Paperformat paperformat, Point lowerLeftPoint, PrinterPaperformat printerformat, double unit, double scale, bool rotateViewport)
        {
            this.paperformat = paperformat;
            this.unit = unit;
            this.printerformat = printerformat;
            this.lowerLeftPoint = lowerLeftPoint;
            this.name = name;
            this.scale = scale;
            this.rotateViewport = rotateViewport;
            this.document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        }

        public void CreateLayout()
        {
            //acDoc.Database.LayoutDictionaryId.            
            // Create the new layout and activate it
            LayoutManager.Current.CreateLayout(name);
            LayoutManager.Current.CurrentLayout = name;
            
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
                    psv.SetCustomPrintScale(layout, new CustomScale(unit * 10, 1));
                }
                else
                {
                    psv.SetPlotPaperUnits(layout, PlotPaperUnit.Millimeters);
                    psv.SetCustomPrintScale(layout, new CustomScale(unit, 1));
                }
                psv.SetPlotType(layout, Autodesk.AutoCAD.DatabaseServices.PlotType.Layout);

                // Get size and margins of the layout
                Double width = layout.PlotPaperSize.X;
                Double height = layout.PlotPaperSize.Y;
                Size margin = new Size(layout.PlotPaperMargins.MinPoint.X / unit, layout.PlotPaperMargins.MinPoint.Y / unit);

                // Get the right rotation and switch width and height if needed
                if ((width < height && (paperformat is PaperformatTextfieldA4 || paperformat is PaperformatA4Vertical)) || 
                    (height < width && !(paperformat is PaperformatTextfieldA4 || paperformat is PaperformatA4Vertical)))
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

                DrawLayout(margin, layoutRecord);

                // Turn the viewport on
                PVport.On = true;

                // Swap end if turned VP
                if (rotateViewport)
                {
                    // Take care of the turned viewport, x-axis is the original y and the y-axis is the inverted original x
                    PVport.ViewCenter = new Point2d(-(lowerLeftPoint.Y + paperformat.ViewportSizeModel.Height / scale / unit / 2), lowerLeftPoint.X - paperformat.ViewportSizeModel.Width / scale / unit / 2);
                    PVport.ViewHeight = paperformat.ViewportSizeModel.Width / scale / unit;
                }
                else
                {
                    // Set the view of the viewport
                    PVport.ViewCenter = new Point2d(lowerLeftPoint.X - paperformat.ViewportSizeModel.Width / scale / unit / 2, lowerLeftPoint.Y + paperformat.ViewportSizeModel.Height / scale / unit / 2);
                    PVport.ViewHeight = paperformat.ViewportSizeModel.Height / scale / unit;
                }

                PVport.CustomScale = scale;

                // Auto-Zoom in paperspace
                if (printerformat.Printer.Name != "PNG")
                {
                    document.SendStringToExecute("_.ZOOM _E ", true, false, true);
                    document.SendStringToExecute("_.ZOOM .8x ", true, false, true);
                }

                // Switch to modelspace and back to prevent unwanted situations
                LayoutManager.Current.CurrentLayout = "Model";
                LayoutManager.Current.CurrentLayout = name;

                // Set the annotation scale for the viewport
                ObjectContextCollection occ = document.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
                AnnotationScale annoScale = occ.GetContext("1:" + (1.0 / scale).ToString()) as AnnotationScale;
                if (annoScale != null)
                {
                    PVport.AnnotationScale = annoScale;
                }
                else
                {
                    MessageBox.Show("Keine passende Skalierung gefunden!");
                }

                // Dispose the used objects
                PVport.Dispose();
                viewport.Dispose();

                // using acPlSet

                // Close everything and commit changes
                trans.Commit();
            }
        }


        protected abstract void DrawLayout(Size margin, BlockTableRecord layoutRecord);

        protected Polyline CreateViewportPolyline(Size margin)
        {
            Polyline viewport = new Polyline();
            viewport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
            viewport.LayerId = document.Database.LayerZero;
            
            // Set the vertices of the viewport poly
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d(paperformat.ViewportBasePoint.X / unit - margin.Width,
                    paperformat.ViewportBasePoint.Y/ unit - margin.Height), 0, 0, 0);
            if (paperformat is PaperformatTextfieldFullTextfield)
            {
                PaperformatTextfield ppTextfield = paperformat as PaperformatTextfield;
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d(ppTextfield.TextfieldBasePoint.X / unit - margin.Width,
                     ppTextfield.ViewportBasePoint.Y / unit - margin.Height), 0, 0, 0);
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d(ppTextfield.TextfieldBasePoint.X / unit - margin.Width,
                        ppTextfield.TextfieldBasePoint.Y / unit - margin.Height), 0, 0, 0);
                viewport.AddVertexAt(viewport.NumberOfVertices,
                    new Point2d((ppTextfield.TextfieldBasePoint.X + ppTextfield.TextfieldSize.Width) / unit - margin.Width,
                        ppTextfield.TextfieldBasePoint.Y / unit - margin.Height), 0, 0, 0);
            }
            else
            {
                viewport.AddVertexAt(viewport.NumberOfVertices,
                   new Point2d((paperformat.ViewportBasePoint.X + paperformat.ViewportSizeLayout.Width) / unit - margin.Width,
                       paperformat.ViewportBasePoint.Y / unit - margin.Height), 0, 0, 0);
            }
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d((paperformat.ViewportBasePoint.X + paperformat.ViewportSizeLayout.Width) / unit - margin.Width,
                    (paperformat.ViewportBasePoint.Y + paperformat.ViewportSizeLayout.Height) / unit - margin.Height), 0, 0, 0);
            viewport.AddVertexAt(viewport.NumberOfVertices,
                new Point2d(paperformat.ViewportBasePoint.X / unit - margin.Width,
                    (paperformat.ViewportBasePoint.Y + paperformat.ViewportSizeLayout.Height) / unit - margin.Height), 0, 0, 0);
            viewport.Closed = true;
            viewport.LineWeight = LineWeight.LineWeight050;

            return viewport;
        }


    }
}
