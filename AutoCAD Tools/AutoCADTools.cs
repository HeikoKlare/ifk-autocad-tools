using System;
using System.IO;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using AutoCADTools.Tools;

[assembly: CLSCompliant(true)]
[assembly: CommandClass(typeof(AutoCADTools.AcadTools))]

namespace AutoCADTools
{
    /// <summary>
    /// The class Tools handles all the commands that are added to AutoCAD by this library.
    /// </summary>
    public static class AcadTools
    {
        #region Attributes
        
        /// <summary>
        /// A string used as transfer for creating details
        /// </summary>
        private static String fileToOpen = "";

        /// <summary>
        /// A string used as transfer for creating details
        /// </summary>
        public static String FileToOpen
        {
            get { return AcadTools.fileToOpen; }
            set { AcadTools.fileToOpen = value; }
        }

        #endregion

        #region Management

        /// <summary>
        /// Opens a dialog for changing settings especially on database.
        /// </summary>
        [CommandMethod("Settings")]
        public static void Settings()
        {
            using (Form settings = new Management.Settings())
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
            using (Form management = new Management.ManageEmployers())
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
            using (Form management = new Management.ManageProjects())
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
            using (Form management = new Management.ManageAnnotationCategories())
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
            using (Form management = new Management.Annotations())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
        }

        /// <summary>
        /// Opens a dialog for managing detail categories.
        /// </summary>
        [CommandMethod("ManageDetailCategories")]
        public static void ManageDetailCategories()
        {
            using (Form management = new Management.ManageDetailCategories())
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
            using (Form management = new Management.ManageDetails())
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
            using (Form details = new Tools.Details())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(details);
            }
            if (!String.IsNullOrEmpty(fileToOpen))
            {
                Autodesk.AutoCAD.ApplicationServices.Document open = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Add(fileToOpen);
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument = open;
                fileToOpen = "";
            }
        }

        /// <summary>
        /// Opens a dialog for using annotations.
        /// </summary>
        [CommandMethod("Annotations")]
        public static void Annotations()
        {
            Form annotations = new Tools.Annotations();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(annotations);
        }

        /// <summary>
        /// DrawingSettings opens a UserForm to edit the drawing properties for the active document.
        /// These information are used for the textfields of layouts.
        /// </summary>
        [CommandMethod("DrawingSettings")]
        public static void DrawingSettings()
        {
            using (Form settings = new Tools.DrawingSettings())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(settings);
            }
        }
        
        /// <summary>
        /// NeueRispe is a command to create a new Rispe in the drawing. It opens a UserForm to input a position 
        /// number and description and then lets the user draw Rispen by selecting start and end point. Pressing 
        /// RETURN after drawing one Rispe lets the next one begin at the end of the old one.
        /// </summary>
        [CommandMethod("NeueRispe", CommandFlags.NoPaperSpace)]
        public static void NeueRispe()
        {
            using (Form UFRispe = new Panicle())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(UFRispe);
            }
        }

        /// <summary>
        /// VerbandErstellen is a command to create a new bracing. It opend a UserForm to input the needed data and
        /// define the extends.
        /// </summary>
        [CommandMethod("VerbandErstellen", CommandFlags.NoPaperSpace)]
        public static void VerbandErstellen()
        {
            using (Form UFVerband = new UFVerband())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(UFVerband);
            }
        }
        
        /// <summary>
        /// LayoutErstellen opens a UserForm with options and methods to define a new layout. There is the
        /// possibility to create simple layouts or layouts with textfields and borders using the data
        /// of the drawing properties.
        /// </summary>
        [CommandMethod("LayoutErstellen", CommandFlags.NoPaperSpace)]
        public static void LayoutErstellen()
        {
            using (Form UFLayoutErstellen = new UFLayoutErstellen())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(UFLayoutErstellen);
            }
        }

        /// <summary>
        /// TrussImport opens a dialog to select a file and some layers to import to the current document.
        /// </summary>
        [CommandMethod("TrussImport")]
        public static void TrussImport()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(new TrussImportUI());
        }

        /// <summary>
        /// Enables the user to define a consecutive dimension step by step with optimizing attributes automatically
        /// </summary>
        [CommandMethod("ConsecutiveDimension")]
        public static void ConsecutiveDimension()
        {
            Tools.ConsecutiveDimension.Execute();
        }

        #endregion

        #region DrawingArea

        /// <summary>
        /// ZeichenbereichA4Hochformat allows to insert a vertical A4 layout in meter-units at a user defined
        /// position.
        /// </summary>
        [CommandMethod("DrawingAreaA4Portrait", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA4Portrait()
        {
            DrawingArea.Create(DrawingArea.CA4, DrawingArea.CVERTICAL);
        }

        /// <summary>
        /// ZeichenbereichA4Querformat allows to insert a horizontal A4 layout in meter-units at a user defined
        /// position.
        /// </summary>
        [CommandMethod("DrawingAreaA4Landscape", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA4Landscape()
        {
            DrawingArea.Create(DrawingArea.CA4, DrawingArea.CHORIZONTAL);
        }

        /// <summary>
        /// ZeichenbereichA3 allows to insert a A3 layout in meter-units at a user defined position.
        /// </summary>
        [CommandMethod("DrawingAreaA3", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaA3()
        {
            DrawingArea.Create(DrawingArea.CA3, DrawingArea.CHORIZONTAL);
        }

        /// <summary>
        /// ZeichenbereichCustom allows to manually create a drawing frame. First the input position is defined
        /// and then the size has to be set interactively. Minumum sizes for the drawing frame are
        /// automatically applied.
        /// </summary>
        [CommandMethod("DrawingAreaCustom", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaCustom()
        {
            DrawingArea.Create(DrawingArea.CAX, DrawingArea.CHORIZONTAL);
        }

        /// <summary>
        /// ZeichenbereichAuto tries to automatically create a drawing frame by analyzing the object currently
        /// in the docuemnt's database and calculating their extends.
        /// </summary>
        [CommandMethod("DrawingAreaAuto", CommandFlags.NoPaperSpace)]
        public static void DrawingAreaAuto()
        {
            DrawingArea.CreateAuto();
        }

        /// <summary>
        /// Allows the user to modify the current drawing area.
        /// </summary>
        [CommandMethod("ModifyDrawingAreaSize", CommandFlags.NoPaperSpace)]
        public static void ModifyDrawingAreaSize()
        {
            DrawingArea.ModifySize();
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
            prop.Add("AE1Name", " ");
            prop.Add("AE1Datum", " ");
            prop.Add("AE1Vermerk", " ");
            prop.Add("AE2Name", " ");
            prop.Add("AE2Datum", " ");
            prop.Add("AE2Vermerk", " ");
            String unit = "1000";
            prop.Add("Zeichnungseinheit", unit);
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.SummaryInfo
                = dbSumBuilder.ToDatabaseSummaryInfo();
        }

        #endregion

    }
}