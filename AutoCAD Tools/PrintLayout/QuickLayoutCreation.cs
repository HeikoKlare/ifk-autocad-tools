using AutoCADTools.Tools;
using AutoCADTools.Utils;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Windows.Forms;

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
            var creationSpecification = new LayoutCreationSpecification();
            if (!creationSpecification.HasPredefinedDrawingArea)
            {
                creationSpecification.Document.Editor.WriteMessage(Environment.NewLine + LocalData.NoDrawingAreaMessage);
                MessageBox.Show(AutocadMainWindow.Instance, LocalData.NoDrawingAreaMessage, "Layout");
                return false;
            }          
            creationSpecification.LoadDataForPredefinedDrawingArea();

            var printer = creationSpecification.Paperformat.GetDefaultPrinter();
            if (printer == null)
            {
                creationSpecification.Document.Editor.WriteMessage(Environment.NewLine + LocalData.DefaultPrinterInvalid);
                MessageBox.Show(AutocadMainWindow.Instance, LocalData.DefaultPrinterInvalid, "Layout");
                return false;
            }

            using (var progressDialog = new ProgressDialog())
            {
                creationSpecification.Printerformat = creationSpecification.Paperformat.GetFittingPaperformat(printer, true, progressDialog);
            }
            if (creationSpecification.Printerformat != null)
            {
                return new LayoutCreator(creationSpecification).CreateLayout();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a PNG layout based in the specified <see cref="DrawingArea"/> in this drawing.
        /// </summary>
        /// <returns><c>true</c> if the layout was created successfully, <c>false</c> otherwise</returns>
        public static bool CreatePngLayout()
        {
            var creationSpecification = new LayoutCreationSpecification()
            {
                LayoutName = "PNG"
            };
            if (!creationSpecification.HasPredefinedDrawingArea)
            {
                creationSpecification.Document.Editor.WriteMessage(Environment.NewLine + LocalData.NoDrawingAreaMessage);
                MessageBox.Show(AutocadMainWindow.Instance, LocalData.NoDrawingAreaMessage, "Layout");
                return false;
            }
            creationSpecification.LoadDataForPredefinedDrawingArea();

            Printer printer = PrinterRepository.Instance["PNG"];
            if (creationSpecification.Paperformat is PaperformatTextfieldCustom || printer == null)
            {
                return false;
            }
            creationSpecification.Printerformat = creationSpecification.Paperformat.GetFittingPaperformat(printer, true, new ProgressDialog());

            return new LayoutCreator(creationSpecification).CreateLayout();
        }

    }
}
