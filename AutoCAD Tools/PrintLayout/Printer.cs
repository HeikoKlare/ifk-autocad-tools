using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoCADTools.Utils;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Represents a printer with its name and paperformats.
    /// </summary>
    public class Printer
    {
        /// <summary>
        /// File extension for printer configuration files
        /// </summary>
        public const string printerConfigurationFileExtension = ".pc3";

        /// <summary>
        /// Returns whether the printer has been initialized.
        /// </summary>
        public bool Initialized
        {
            get;
            private set;
        }


        private readonly IEnumerable<string> ordinaryPaperFormats = new List<string>() { "A0", "A1", "A2", "A3", "A4" };
        private readonly Dictionary<string, string> ordinaryPaperFormatsToPng = new Dictionary<string, string> { { "A4", "UserDefinedRaster (2100.00 x 2970.00Pixel)" }, { "A3", "UserDefinedRaster (2970.00 x 4200.00Pixel)" } };

        /// <summary>
        /// Gets the name of the printer.
        /// </summary>
        /// <value>
        /// The name of the printer.
        /// </value>
        public String Name { get; }

        private readonly IList<PrinterPaperformat> paperformats = new List<PrinterPaperformat>();

        /// <summary>
        /// Initializes and gets the optimized or unoptimized paperformats for this printer, depending on the specified parameter. Formats are optimized if their margins
        /// are all nearly 4 mm.
        /// </summary>
        /// <param name="optimized">if set to <c>true</c> optimized formats are returned.</param>
        /// <returns>A list the (un)optmized paperformats for this printer</returns>
        public IReadOnlyList<PrinterPaperformat> InitializeAndGetPaperformats(bool optimized)
        {
            if (!InitializePaperformats())
            {
                MessageBox.Show(LocalData.LoadPaperformatsErrorTitle, LocalData.LoadPaperformatsErrorMessage + " " + Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return GetPaperformats(optimized);
        }

        /// <summary>
        /// Gets the optimized or unoptimized paperformats for this printer, depending on the specified parameter. Formats are optimized if their margins
        /// are all nearly 4 mm.
        /// If paperformats have not been initialized calling <see cref="InitializePaperformats"/>, an empty list will be returned.
        /// Initialization can be checked accessing <see cref="Initialized"/>.
        /// </summary>
        /// <param name="optimized">if set to <c>true</c> optimized formats are returned.</param>
        /// <returns>A list the (un)optmized paperformats for this printer</returns>
        public IReadOnlyList<PrinterPaperformat> GetPaperformats(bool optimized)
        {
            return optimized ? paperformats.Where(format => format.Optimal).ToList().AsReadOnly() : paperformats.ToList().AsReadOnly();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Printer"/> class with the specified name.
        /// <see cref="Printer.InitializePaperformats()"/> has to be called afterwards to load the available paperformats.
        /// </summary>
        /// <param name="name">The name of the printer.</param>
        /// <exception cref="System.ArgumentException">Is thrown if the name is null, empty or if the specified printer does not exist.</exception>
        public Printer(String name)
        {
            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            if (String.IsNullOrEmpty(name) || !psv.GetPlotDeviceList().Contains(name + printerConfigurationFileExtension))
            {
                throw new System.ArgumentException(LocalData.PrinterNameException + name);
            }

            this.Name = name;
        }

        /// <summary>
        /// Initializes the paperformats. They are seperated into optimized and unoptimized formats.
        /// During execution, the plot settings validator may not be changed (by creating a new document, closing the application or the like).
        /// Otherwise, access violation exception will occur, which then need to be handled by the caller.
        /// If the paperformats have already been initialized, nothing is done.
        /// Returns whether initialization was successful.
        /// </summary>
        [HandleProcessCorruptedStateExceptions]
        public bool InitializePaperformats()
        {
            if (Initialized)
            {
                return true;
            }
            try
            {
                using (UFProgress progressWindow = new UFProgress())
                {
                    var numberOfOrdinaryPaperFormats = ordinaryPaperFormats.Count();
                    progressWindow.setName(LocalData.LoadPaperformatsTitle);
                    progressWindow.setMain(LocalData.LoadPaperformatsText);
                    progressWindow.setDescription(LocalData.LoadPaperformatsInitialization);
                    progressWindow.setProgress(0);
                    progressWindow.Update();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(progressWindow);

                    PlotSettingsValidator psv = PlotSettingsValidator.Current;
                    using (PlotSettings acPlSet = new PlotSettings(true))
                    {
                        psv.SetPlotConfigurationName(acPlSet, Name + printerConfigurationFileExtension, null);
                        psv.RefreshLists(acPlSet);
                        var paperFormats = psv.GetCanonicalMediaNameList(acPlSet).Cast<string>();

                        var paperFormatNumber = 0;
                        foreach (var ordinaryPaperformat in ordinaryPaperFormats)
                        {
                            var isOptimal = false;
                            String foundFormat = null;
                            var candidateFormats = paperFormats.Where(format => psv.GetLocaleMediaName(acPlSet, format).Contains(ordinaryPaperformat));

                            if (ordinaryPaperFormatsToPng.ContainsKey(ordinaryPaperformat) && candidateFormats.Contains(ordinaryPaperFormatsToPng[ordinaryPaperformat]))
                            {
                                isOptimal = true;
                                foundFormat = ordinaryPaperFormatsToPng[ordinaryPaperformat];
                            }
                            else
                            {
                                var numberOfCandidateFormats = candidateFormats.Count();
                                var candidateNumber = 0;
                                foreach (var candidate in candidateFormats)
                                {
                                    var progress = (100 * paperFormatNumber + 100 * candidateNumber / numberOfCandidateFormats) / numberOfOrdinaryPaperFormats;
                                    UpdateProgressWindow(progressWindow, ordinaryPaperformat, progress);
                                    foundFormat = candidate;
                                    psv.SetCanonicalMediaName(acPlSet, candidate);
                                    Extents2d margins = acPlSet.PlotPaperMargins;
                                    if (Math.Abs(margins.MinPoint.X) < 4.2 && Math.Abs(margins.MinPoint.Y) < 4.2
                                        && Math.Abs(margins.MaxPoint.X) < 4.2 && Math.Abs(margins.MaxPoint.Y) < 4.2
                                        && Math.Abs(margins.MinPoint.X) > 3.8 && Math.Abs(margins.MinPoint.Y) > 3.8
                                        && Math.Abs(margins.MaxPoint.X) > 3.8 && Math.Abs(margins.MaxPoint.Y) > 3.8)
                                    {
                                        isOptimal = true;
                                        break;
                                    }
                                    candidateNumber++;
                                }
                            }
                            if (foundFormat != null)
                            {
                                paperformats.Add(new PrinterPaperformat(ordinaryPaperformat, foundFormat, this, isOptimal));
                            }
                            paperFormatNumber++;
                            UpdateProgressWindow(progressWindow, ordinaryPaperformat, 100 * paperFormatNumber / numberOfOrdinaryPaperFormats);
                        }
                        progressWindow.Hide();
                    }
                }
            }
            catch (Exception)
            {
                // There may be different reasons for the initialization to fail. In particular,
                // changing the document in between will lead to access violation errors, such that
                // we abort and return false to enfore restart initialization.
                return false;
            }
            Initialized = true;
            return true;
        }

        private static void UpdateProgressWindow(UFProgress progressWindow, string paperFormatName, int progress)
        {
            progressWindow.setDescription(String.Format("{0} {1} ({2} %)", LocalData.LoadPaperformatsDescription, paperFormatName, progress));
            progressWindow.setProgress(progress);
            progressWindow.Update();
        }
    }

    /// <summary>
    /// Defines a paperformat for a printer, defined by a readable name and the format name.
    /// </summary>
    public class PrinterPaperformat
    {
        /// <summary>
        /// Gets the readable name of the format.
        /// </summary>
        /// <value>
        /// The readable name of the format.
        /// </value>
        public String Name { get; }

        /// <summary>
        /// Gets the name of the format as it is used to adress the format.
        /// </summary>
        /// <value>
        /// The original name of the format.
        /// </value>
        public String FormatName { get; }

        /// <summary>
        /// Gets the printer this format belongs to.
        /// </summary>
        /// <value>
        /// The printer.
        /// </value>
        public Printer Printer { get; }

        /// <summary>
        /// Returns whether the printer paperformat is optimal
        /// </summary>
        public bool Optimal { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrinterPaperformat"/> class with the specified readable name, the original format name, 
        /// and whether it is optimal or not.
        /// </summary>
        /// <param name="name">The readable name.</param>
        /// <param name="formatName">Original name of the format.</param>
        /// <param name="printer">The printer this format belongs to.</param>
        /// <param name="optimal">Whether the format is optimal.</param>
        public PrinterPaperformat(String name, String formatName, Printer printer, bool optimal)
        {
            this.Name = name;
            this.FormatName = formatName;
            this.Printer = printer;
            this.Optimal = optimal;
        }
    }

}
