using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCAD;

namespace AutoCADTools
{
    public class ToolsInitialization : Autodesk.AutoCAD.Runtime.IExtensionApplication
    {
        private const string AssemblyName = "AutoCAD Tools";
        private const string PlotterFolder = "Plotters";
        private const string PlotterPmpFolder = "PMP Files";
        private const string CustomizationFolder = "Customization";
        private const string CustomizationFile = "ifk.cuix";
        private const string TemplateFolder = "Templates";
        private const string TemplateFile = "ifk.dwt";

        private readonly string roamingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + AssemblyName + "\\";

        public void Initialize()
        {
            //SetPrinterConfigPath();
            //SetPrinterPmpPath();
            //SetCustomizationFile();
            //SetTemplateFile();
        }

        private void SetPrinterConfigPath()
        {
            AcadPreferences prefs = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            string plotterConfigPaths = prefs.Files.PrinterConfigPath;
            string plotter = (roamingFolder + PlotterFolder).ToLower();
            bool printerFound = false;
            foreach (var path in plotterConfigPaths.Split(';'))
            {
                if (path.Trim().ToLower().Equals(plotter))
                {
                    printerFound = true;
                    break;
                }
            }
            if (!printerFound) prefs.Files.PrinterConfigPath = (plotterConfigPaths + "; " + plotter).ToLower();
        }

        private void SetPrinterPmpPath()
        {
            AcadPreferences prefs = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            string plotterDescPaths = prefs.Files.PrinterDescPath;
            string plotterDesc = (roamingFolder + PlotterFolder + "\\" + PlotterPmpFolder).ToLower();
            bool printerFound = false;
            foreach (var path in plotterDescPaths.Split(';'))
            {
                if (path.Trim().ToLower().Equals(plotterDesc))
                {
                    printerFound = true;
                    break;
                }
            }
            if (!printerFound) prefs.Files.PrinterDescPath = (plotterDescPaths + "; " + plotterDesc).ToLower();
        }

        private void SetCustomizationFile()
        {
            AcadPreferences prefs = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            prefs.Files.EnterpriseMenuFile = (roamingFolder + CustomizationFolder + "\\" + CustomizationFile).ToLower();
        }

        private void SetTemplateFile()
        {
            AcadPreferences prefs = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            prefs.Files.QNewTemplateFile = (roamingFolder + TemplateFolder + "\\" + TemplateFile).ToLower();
        }

        public void Terminate()
        {
            // Empty
        }
    }
}
