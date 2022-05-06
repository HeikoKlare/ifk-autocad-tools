using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using AutoCADTools.Tools;
using AutoCADTools.PrintLayout;

[assembly: CLSCompliant(true)]
[assembly: CommandClass(typeof(AutoCADTools.AcadTools))]


namespace AutoCADTools
{
    /// <summary>
    /// The class Tools handles all the commands that are added to AutoCAD by this library.
    /// </summary>
    public static class AcadTools
    {
        #region Management

        /// <summary>
        /// Opens a dialog for changing settings especially on database.
        /// </summary>
        [CommandMethod("Settings")]
        public static void Settings()
        {
            using (Form settings = new Management.FrmSettings())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(settings);
            }
        }

        /// <summary>
        /// Opens a dialog for managing employers.
        /// </summary>
        [CommandMethod("ManageEmployers")]
        public static void ManageEmployers()
        {
            using (Form management = new Management.FrmManageEmployers())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
        }

        /// <summary>
        /// Opens a dialog for managing projects and employers.
        /// </summary>
        [CommandMethod("ManageProjects")]
        public static void ManageProjects()
        {
            using (Form management = new Management.FrmManageProjects())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
        }

        /// <summary>
        /// Opens a dialog for managing annotation categories.
        /// </summary>
        [CommandMethod("ManageAnnotationCategories")]
        public static void ManageAnnotationCategories()
        {
            using (Form management = new Management.FrmManageAnnotationCategories())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
        }

        /// <summary>
        /// Opens a dialog for managing annotations.
        /// </summary>
        [CommandMethod("ManageAnnotations")]
        public static void ManageAnnotations()
        {
            //using (Form management = new Management.FrmManageAnnotations())
            //{
            Form management = new Management.FrmManageAnnotations();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(management);
            //}
        }

        /// <summary>
        /// Opens a dialog for managing detail categories.
        /// </summary>
        [CommandMethod("ManageDetailCategories")]
        public static void ManageDetailCategories()
        {
            using (Form management = new Management.FrmManageDetailCategories())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
        }

        /// <summary>
        /// Opens a dialog for managing details.
        /// </summary>
        [CommandMethod("ManageDetails")]
        public static void ManageDetails()
        {
            using (Form management = new Management.FrmManageDetails())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
        }

        #endregion

        #region Tools

        /// <summary>
        /// Opens a dialog for opening details.
        /// </summary>
        [CommandMethod("Details")]
        public static void Details()
        {
            var fileToOpen = "";
            using (FrmDetails details = new FrmDetails())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(details);
                fileToOpen = details.SelectedFileToOpen;
            }
            if (!String.IsNullOrEmpty(fileToOpen))
            {
                Autodesk.AutoCAD.ApplicationServices.Document open = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Add(fileToOpen);
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument = open;
            }
        }

        /// <summary>
        /// Opens a dialog for using annotations.
        /// </summary>
        [CommandMethod("Annotations")]
        public static void Annotations()
        {
            Form annotations = new Tools.FrmAnnotations();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(annotations);
        }

        /// <summary>
        /// DrawingSettings opens a UserForm to edit the drawing properties for the active document.
        /// These information are used for the textfields of layouts.
        /// </summary>
        [CommandMethod("DrawingSettings")]
        public static void DrawingSettings()
        {
            using (Form settings = new Tools.FrmDrawingSettings())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(settings);
            }
        }
        
        /// <summary>
        /// Panicle is a command to create a new panicle in the drawing. It opens a UserForm to input a position 
        /// number and description and then lets the user draw panicles by selecting start and end point. Pressing 
        /// RETURN after drawing one panicle lets the next one begin at the end of the old one.
        /// </summary>
        [CommandMethod("DiagonalBracing", CommandFlags.NoPaperSpace)]
        public static void DiagonalBracing()
        {
            using (Form Form = new FrmDiagonalBracing())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Form);
            }
        }

        /// <summary>
        /// ReinforcingBond is a command to create a new bracing. It opend a UserForm to input the needed data and
        /// define the extends.
        /// </summary>
        [CommandMethod("ReinforcingBond", CommandFlags.NoPaperSpace)]
        public static void ReinforcingBond()
        {
            using (Form Form = new FrmReinforcingBond())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Form);
            }
        }
        
        /// <summary>
        /// Opens a dialog for creating a layout using the LayoutUI dialog.
        /// </summary>
        [CommandMethod("CreateLayout", CommandFlags.NoPaperSpace)]
        public static void CreateLayout()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingData = doc.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            using (Form createLayout = new FrmLayout(drawingData.Version < 2))
            {

                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(createLayout);
            }
        }

        /// <summary>
        /// Creates a layout based on the drawing area and the default printer.
        /// </summary>
        [CommandMethod("CreateLayoutQuick", CommandFlags.NoPaperSpace)]
        public static void CreateLayoutQuick()
        {
            QuickLayoutCreation.CreateDefaultLayout();
        }

        /// <summary>
        /// TrussImport opens a dialog to select a file and some layers to import to the current document.
        /// </summary>
        [CommandMethod("TrussImport")]
        public static void TrussImport()
        {
            bool import = false;
            using (Form trussImportUi = new FrmTrussImport()) {
                import = Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(trussImportUi) == DialogResult.OK;
            }
            if (import) Tools.TrussImport.Instance.Import();
        }

        /// <summary>
        /// Enables the user to define a consecutive dimension step by step with optimizing attributes automatically.
        /// </summary>
        [CommandMethod("ConsecutiveDimension")]
        public static void ConsecutiveDimension()
        {
            Tools.ConsecutiveDimension.Execute();
        }

        /// <summary>
        /// Enables the user to define a compression wood step by step.
        /// </summary>
        [CommandMethod("CompressionWood")]
        public static void CompressionWood()
        {
            Tools.CompressionWood.Execute();
        }

        /// <summary>
        /// Enables the user to define a positionnumber.
        /// </summary>
        [CommandMethod("Positionnumber")]
        public static void Positionnumber()
        {
            Tools.Positionnumber.Execute();
        }

        /// <summary>
        /// Enables the user to define a simple ground plan.
        /// </summary>
        [CommandMethod("GroundPlan")]
        public static void GroundPlan()
        {
            Tools.GroundPlan.Execute();
        }

        /// <summary>
        /// Enables the user to select a text and create a frame around it.
        /// </summary>
        [CommandMethod("TextFrame")]
        public static void TextFrame()
        {
            Tools.TextFrame.Execute();
        }

        #endregion

        #region DrawingArea

        /// <summary>
        /// DrawingAreaA4Portrait allows to insert a vertical A4 layout in meter-units at a user defined
        /// position.
        /// </summary>
        [CommandMethod("DrawingAreaA4Portrait", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA4Portrait()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            var drawingData = doc.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            drawingAreaWrapper.Create(new PaperformatTextfieldA4Vertical(drawingData.Version < 2));
        }

        /// <summary>
        /// DrawingAreaA4Landscape allows to insert a horizontal A4 layout in meter-units at a user defined
        /// position.
        /// </summary>
        [CommandMethod("DrawingAreaA4Landscape", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA4Landscape()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            var drawingData = doc.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            drawingAreaWrapper.Create(new PaperformatTextfieldA4Horizontal(drawingData.Version < 2));
        }

        /// <summary>
        /// DrawingAreaA3 allows to insert a A3 layout in meter-units at a user defined position.
        /// </summary>
        [CommandMethod("DrawingAreaA3", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA3()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            var drawingData = doc.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            drawingAreaWrapper.Create(new PaperformatTextfieldA3(drawingData.Version < 2));
        }

        /// <summary>
        /// DrawingAreaCustom allows to manually create a drawing frame. First the input position is defined
        /// and then the size has to be set interactively. Minumum sizes for the drawing frame are
        /// automatically applied.
        /// </summary>
        [CommandMethod("DrawingAreaCustom", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaCustom()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            var drawingData = doc.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            drawingAreaWrapper.CreateAndResize(new PaperformatTextfieldA3(drawingData.Version < 2));
        }

        /// <summary>
        /// Allows the user to modify the current drawing area.
        /// </summary>
        [CommandMethod("ModifyDrawingAreaSize", CommandFlags.NoPaperSpace)]
        public static void ModifyDrawingAreaSize()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            var drawingData = doc.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            drawingAreaWrapper.Resize(drawingData.Version < 2);
        }

        #endregion

        #region DrawingAreaLegacy

        /// <summary>
        /// DrawingAreaA4PortraitOld allows to insert a vertical A4 layout in meter-units at a user defined
        /// position.
        /// </summary>
        [CommandMethod("DrawingAreaA4PortraitOld", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA4PortraitOld()
        {

            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            drawingAreaWrapper.Create(new PaperformatTextfieldA4Vertical(true));
        }

        /// <summary>
        /// DrawingAreaA4LandscapeOld allows to insert a horizontal A4 layout in meter-units at a user defined
        /// position.
        /// </summary>
        [CommandMethod("DrawingAreaA4LandscapeOld", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA4LandscapeOld()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            drawingAreaWrapper.Create(new PaperformatTextfieldA4Horizontal(true));
        }

        /// <summary>
        /// DrawingAreaA3Old allows to insert a A3 layout in meter-units at a user defined position.
        /// </summary>
        [CommandMethod("DrawingAreaA3Old", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA3Old()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            drawingAreaWrapper.Create(new PaperformatTextfieldA3(true));
        }

        /// <summary>
        /// DrawingAreaCustomOld allows to manually create a drawing frame. First the input position is defined
        /// and then the size has to be set interactively. Minumum sizes for the drawing frame are
        /// automatically applied.
        /// </summary>
        [CommandMethod("DrawingAreaCustomOld", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaCustomOld()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            drawingAreaWrapper.CreateAndResize(new PaperformatTextfieldA3(true));
        }

        /// <summary>
        /// Allows the user to modify the current drawing area.
        /// </summary>
        [CommandMethod("ModifyDrawingAreaSizeOld", CommandFlags.NoPaperSpace)]
        public static void ModifyDrawingAreaSizeOld()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = doc.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            drawingAreaWrapper.Resize(true);
        }

        #endregion

        #region Helper Tools
        
        /// <summary>
        /// Hidden command. Resets all properties to an almost empty string (just contains a whitespace)
        /// </summary>
        [CommandMethod("ClearProperties")]
        public static void ClearProperties()
        {
            Autodesk.AutoCAD.DatabaseServices.DatabaseSummaryInfoBuilder dbSumBuilder = new Autodesk.AutoCAD.DatabaseServices.DatabaseSummaryInfoBuilder();
            System.Collections.IDictionary prop = dbSumBuilder.CustomPropertyTable;
            prop.Add("Version", "2");
            prop.Add("Auftraggeber", " ");
            prop.Add("BV1", " ");
            prop.Add("BV2", " ");
            prop.Add("BV3", " ");
            prop.Add("BV4", " ");
            prop.Add("BVK", " ");
            prop.Add("Erstellungsdatum", " ");
            prop.Add("Statiknummer", " ");
            prop.Add("Bauteil", " ");
            prop.Add("Plannummer", " ");
            String unit = "1000";
            prop.Add("Zeichnungseinheit", unit);
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.SummaryInfo
                = dbSumBuilder.ToDatabaseSummaryInfo();
        }

        /// <summary>
        /// Hidden command. Clears the printer repository, such that printer are initialized anew.
        /// </summary>
        [CommandMethod("ClearPrinterRepository")]
        public static void ClearPrinterRepository()
        {
            PrinterRepository.Instance.Clear();
        }

        #endregion

    }
}