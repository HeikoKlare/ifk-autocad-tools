using AutoCAD;
using AutoCADTools.Properties;
using System;
using System.Windows.Forms;
using static System.IO.Path;

namespace AutoCADTools
{
    /// <summary>
    /// This class encapsulated initialization work when AutoCAD is loaded.
    /// </summary>
    public class ToolsInitialization : Autodesk.AutoCAD.Runtime.IExtensionApplication
    {
        private const string AssemblyName = "AutoCAD Tools";
        private const string PlotterFolder = "Plotters";
        private const string PlotterPmpFolder = "PMP Files";
        private const string CustomizationFolder = "Customization";
        private const string CustomizationFile = "ifk";
        private const string TemplateFolder = "Templates";
        private const string TemplateFile = "ifk.dwt";
        private const string MenuGroupName = "IFK";
        private const string DefaultDatabasePath = "SERVER_URL";

        private readonly string roamingFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + DirectorySeparatorChar + AssemblyName + DirectorySeparatorChar;

        /// <summary>
        /// Sets the defaults paths for the tools when starting AutoCAD. Sets printer configurations, UI customization and template files.
        /// </summary>
        public void Initialize()
        {
            SetPrinterConfigPath();
            SetPrinterPmpPath();
            SetCustomizationFile();
            SetTemplateFile();
            InitializeDatabaseAfterInstallation();
        }

        private void InitializeDatabaseAfterInstallation()
        {
            if (Settings.Default.SqlConnectionPath == DefaultDatabasePath)
            {
                if (MessageBox.Show(LocalData.FirstStartupText, LocalData.FirstStatupTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    AcadTools.Settings();
                }
            }
        }

        private void SetPrinterConfigPath()
        {
            AcadPreferences prefs = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            string plotterConfigPaths = prefs.Files.PrinterConfigPath;
            string plotter = (roamingFolder + PlotterFolder);
            bool printerFound = false;
            foreach (var path in plotterConfigPaths.Split(';'))
            {
                if (path.Trim().ToLower().Equals(plotter.ToLower()))
                {
                    printerFound = true;
                    break;
                }
            }
            if (!printerFound) prefs.Files.PrinterConfigPath = plotterConfigPaths + ";" + plotter;
        }

        private void SetPrinterPmpPath()
        {
            AcadPreferences prefs = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            string plotterDescPaths = prefs.Files.PrinterDescPath;
            string plotterDesc = (roamingFolder + PlotterFolder + DirectorySeparatorChar + PlotterPmpFolder);
            bool printerFound = false;
            foreach (var path in plotterDescPaths.Split(';'))
            {
                if (path.Trim().ToLower().Equals(plotterDesc.ToLower()))
                {
                    printerFound = true;
                    break;
                }
            }
            if (!printerFound) prefs.Files.PrinterDescPath = plotterDescPaths + ";" + plotterDesc;
        }

        private void SetCustomizationFile()
        {
            AcadPreferences prefs = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            bool found = false;
            for (int i = 0; i < prefs.Application.MenuGroups.Count; i++)
            {
                if (prefs.Application.MenuGroups.Item(i).Name == MenuGroupName)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                prefs.Application.MenuGroups.Load(roamingFolder + CustomizationFolder + DirectorySeparatorChar + CustomizationFile);
                var group = prefs.Application.MenuGroups.Item(MenuGroupName);
                for (int i = 0; i < group.Toolbars.Count; i++)
                {
                    group.Toolbars.Item(i).Visible = true;
                }
            }

        }

        private void SetTemplateFile()
        {
            AcadPreferences prefs = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            if (prefs.Files.QNewTemplateFile != (roamingFolder + TemplateFolder + DirectorySeparatorChar + TemplateFile).ToLower())
            {
                prefs.Files.QNewTemplateFile = (roamingFolder + TemplateFolder + DirectorySeparatorChar + TemplateFile).ToLower();
            }
        }

        /// <summary>
        /// Cleans up when AutoCAD is closed. Nothing is done.
        /// </summary>
        public void Terminate()
        {
            // Empty
        }
    }
}
