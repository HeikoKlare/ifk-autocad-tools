using AutoCADTools.Tools;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoCADTools.PrintLayout
{
    public static class QuickLayoutCreation
    {
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

        private static DrawingArea GetDrawingArea()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            return drawingAreaWrapper.DrawingArea;
        }

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
