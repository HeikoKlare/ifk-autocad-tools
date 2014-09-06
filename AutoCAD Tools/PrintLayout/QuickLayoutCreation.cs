using AutoCADTools.Tools;
using Autodesk.AutoCAD.DatabaseServices;
using System;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This class provides functionalities for a quick creation of layouts using default values and a specified drawing area.
    /// </summary>
    public static class QuickLayoutCreation
    {
        /// <summary>
        /// Creates a layout based in the specified <see cref="DrawingArea"/> in this drawing using default values for layout name and printers.
        /// </summary>
        /// <returns><c>true</c> if the layout was created successfully, <c>false</c> otherwise</returns>
        public static bool CreateDefaultLayout()
        {
            var drawingArea = GetDrawingArea();
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            
            if (!drawingArea.IsValid)
            {
                doc.Editor.WriteMessage(Environment.NewLine + LocalData.NoDrawingAreaMessage);
                return false;
            }
            
            var paperformat = drawingArea.Format;
            if (paperformat.GetDefaultPrinter() == null)
            {
                doc.Editor.WriteMessage(Environment.NewLine + LocalData.DefaultPrinterInvalid);
                return false;
            }

            var creation = new LayoutTextfield(paperformat);
            SetDrawingArea(creation, drawingArea);
            creation.LayoutName = Properties.Settings.Default.DefaultLayoutName;
            creation.Printerformat = paperformat.GetFittingPaperformat(paperformat.GetDefaultPrinter(), true);
            
            return creation.CreateLayout();
        }

        /// <summary>
        /// Creates a PNG layout based in the specified <see cref="DrawingArea"/> in this drawing.
        /// </summary>
        /// <returns><c>true</c> if the layout was created successfully, <c>false</c> otherwise</returns>
        public static bool CreatePngLayout()
        {
            var drawingArea = GetDrawingArea();
            Printer printer = PrinterCache.Instance["PNG"];
            if (!drawingArea.IsValid || drawingArea.Format is PaperformatTextfieldCustom || printer == null)
            {
                return false;
            }

            var paperformat = drawingArea.Format;
            var creation = new LayoutTextfield(paperformat);
            SetDrawingArea(creation, drawingArea);

            creation.LayoutName = "PNG";
            creation.Printerformat = paperformat.GetFittingPaperformat(printer, true);

            return creation.CreateLayout();
        }

        /// <summary>
        /// Gets the drawing area in the drawing.
        /// </summary>
        /// <returns>The drawing area in the drawing.</returns>
        private static DrawingArea GetDrawingArea()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            return drawingAreaWrapper.DrawingArea;
        }

        /// <summary>
        /// Sets the default values for the specified drawing area in the given creation process.
        /// </summary>
        /// <param name="creation">The creation process object.</param>
        /// <param name="drawingArea">The drawing area.</param>
        /// <exception cref="System.ArgumentNullException">Is thrown if an argument is null</exception>
        private static void SetDrawingArea(LayoutCreation creation, DrawingArea drawingArea)
        {
            if (creation == null || drawingArea == null || !drawingArea.IsValid)
            {
                throw new ArgumentNullException();
            }

            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingData = doc.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;

            using (Transaction trans = doc.TransactionManager.StartOpenCloseTransaction())
            {
                var point = (trans.GetObject(drawingArea.DrawingAreaId, OpenMode.ForRead) as BlockReference).Position;
                creation.ExtractLowerRightPoint = new Point(point.X, point.Y);
            }

            creation.DrawingUnit = drawingData.DrawingUnit;
            creation.Scale = drawingArea.Scale / drawingData.DrawingUnit;
            creation.Orientation = creation.Paperformat is PaperformatTextfieldA4Vertical ? LayoutCreation.PaperOrientation.Portrait : LayoutCreation.PaperOrientation.Landscape;
        }

    }
}
