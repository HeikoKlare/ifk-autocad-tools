using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoCADTools.Utils;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;
using AutoCADTools.Properties;
using Autodesk.AutoCAD.Geometry;

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

        private const double optimizedFormatMarginMinimum = 3.8;
        private const double optimizedFormatMarginMaximum = 4.2;

        private static readonly IEnumerable<string> targetPaperformatsNames = Settings.Default.OrdinaryPaperformats.Cast<string>();
        private static readonly Dictionary<string, string> targetPaperformatsNamesToPng = new Dictionary<string, string> { { "A4", "UserDefinedRaster (2100.00 x 2970.00Pixel)" }, { "A3", "UserDefinedRaster (2970.00 x 4200.00Pixel)" } };

        /// <summary>
        /// Returns whether the printer has been initialized.
        /// </summary>
        public bool Initialized
        {
            get;
            private set;
        }

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
            using (IProgressMonitor progressMonitor = new ProgressDialog())
            {
                progressMonitor.Title = LocalData.LoadPaperformatsTitle + " " + Name;
                progressMonitor.MainText = LocalData.LoadPaperformatsText;
                progressMonitor.CurrentActionDescription = LocalData.LoadPaperformatsInitialization;
                try
                {
                    using (PlotSettings plotSettings = new PlotSettings(true))
                    {
                        var paperformatExtractionState = new PaperformatExtractionState(PlotSettingsValidator.Current, plotSettings, progressMonitor);
                        IEnumerable<PaperformatDescription> printerPaperformatDescriptions = FindAvailablePaperformatsOfPrinter(paperformatExtractionState);
                        var paperFormatNumber = 0;
                        foreach (var targetPaperformatName in targetPaperformatsNames)
                        {
                            var candidatePrinterPaperformatDescriptions = printerPaperformatDescriptions.Where(format => format.LocaleName.Contains(targetPaperformatName));
                            var foundPaperformatDescription = FindAndMarkOptimizedPaperformat(paperformatExtractionState, targetPaperformatName, candidatePrinterPaperformatDescriptions);
                            if (foundPaperformatDescription == null)
                            {
                                foundPaperformatDescription = candidatePrinterPaperformatDescriptions.Last();
                            }
                            if (foundPaperformatDescription != null)
                            {
                                this.paperformats.Add(new PrinterPaperformat(targetPaperformatName, foundPaperformatDescription.CanonicalName, this, foundPaperformatDescription.Optimal));
                            }
                            paperFormatNumber++;
                            progressMonitor.Progress = (double)paperFormatNumber / targetPaperformatsNames.Count();
                        }
                    }
                }
                catch (Exception)
                {
                    // There may be different reasons for the initialization to fail. In particular,
                    // changing the document in between will lead to access violation errors, such that
                    // we abort and return false to enfore restart initialization.
                    this.paperformats.Clear();
                    return false;
                }
            }
            Initialized = true;
            return true;
        }

        private IEnumerable<PaperformatDescription> FindAvailablePaperformatsOfPrinter(PaperformatExtractionState paperformatExtractionState)
        {
            var plotSettingsValidator = paperformatExtractionState.PlotSettingsValidator;
            var plotSettings = paperformatExtractionState.PlotSettings;
            plotSettingsValidator.SetPlotConfigurationName(plotSettings, Name + printerConfigurationFileExtension, null);
            plotSettingsValidator.RefreshLists(plotSettings);
            return plotSettingsValidator.GetCanonicalMediaNameList(plotSettings).Cast<string>().Select(formatName =>
                new PaperformatDescription(formatName, plotSettingsValidator.GetLocaleMediaName(plotSettings, formatName)));
        }

        private static PaperformatDescription FindAndMarkOptimizedPaperformat(PaperformatExtractionState paperformatExtractionState, string targetPaperformatName, IEnumerable<PaperformatDescription> candidatePaperformatDescriptions)
        {
            var foundFormat = FindAndMarkOptimizedPngPaperformat(paperformatExtractionState, targetPaperformatName, candidatePaperformatDescriptions);
            if (foundFormat == null)
            {
                foundFormat = FindAndMarkOptimizedNonPngPaperformat(paperformatExtractionState, targetPaperformatName, candidatePaperformatDescriptions);
            }
            return foundFormat;
        }

        private static PaperformatDescription FindAndMarkOptimizedPngPaperformat(PaperformatExtractionState paperformatExtractionState, string targetPaperformatName, IEnumerable<PaperformatDescription> candidatePaperformatDescriptions)
        {
            if (targetPaperformatsNamesToPng.ContainsKey(targetPaperformatName))
            {
                var paperformatName = targetPaperformatsNamesToPng[targetPaperformatName];
                var potentialResultPaperformatDescriptions = candidatePaperformatDescriptions.Where(description => description.CanonicalName == paperformatName);
                if (potentialResultPaperformatDescriptions.Any())
                {
                    var resultPaperformatDescription = potentialResultPaperformatDescriptions.First();
                    resultPaperformatDescription.Optimal = true;
                    return resultPaperformatDescription;
                }
            }
            return null;
        }

        private static PaperformatDescription FindAndMarkOptimizedNonPngPaperformat(PaperformatExtractionState paperformatExtractionState, string targetPaperformatName, IEnumerable<PaperformatDescription> candidatePaperformatDescriptions)
        {
            foreach (var candidatePaperformatDescription in candidatePaperformatDescriptions)
            {
                UpdateProgressMonitor(paperformatExtractionState.ProgressMonitor, targetPaperformatName,
                    1.0 / candidatePaperformatDescriptions.Count() / targetPaperformatsNames.Count());
                paperformatExtractionState.PlotSettingsValidator.SetCanonicalMediaName(paperformatExtractionState.PlotSettings, candidatePaperformatDescription.CanonicalName);
                Extents2d margins = paperformatExtractionState.PlotSettings.PlotPaperMargins;
                if (IsOptimizedPaperformatMargin(margins.MinPoint) && IsOptimizedPaperformatMargin(margins.MaxPoint))
                {
                    candidatePaperformatDescription.Optimal = true;
                    return candidatePaperformatDescription;
                }
            }
            return null;
        }

        private static bool IsOptimizedPaperformatMargin(Point2d marginSize)
        {
            return Math.Abs(marginSize.X) >= optimizedFormatMarginMinimum && Math.Abs(marginSize.X) <= optimizedFormatMarginMaximum &&
                Math.Abs(marginSize.Y) >= optimizedFormatMarginMinimum && Math.Abs(marginSize.Y) <= optimizedFormatMarginMaximum;
        }

        private static void UpdateProgressMonitor(IProgressMonitor progressMonitor, string paperFormatName, double addedProgress)
        {
            progressMonitor.Progress += addedProgress;
            progressMonitor.CurrentActionDescription = String.Format("{0} {1})", LocalData.LoadPaperformatsDescription, paperFormatName);
        }

        private class PaperformatDescription
        {
            public string CanonicalName { get; }
            public string LocaleName { get; }

            public bool Optimal { get; set; }

            public PaperformatDescription(string canonicalName, string localeName)
            {
                this.CanonicalName = canonicalName;
                this.LocaleName = localeName;
            }
        }

        private class PaperformatExtractionState
        {
            public PlotSettingsValidator PlotSettingsValidator { get; }
            public PlotSettings PlotSettings { get; }
            public IProgressMonitor ProgressMonitor { get; }

            public PaperformatExtractionState(PlotSettingsValidator plotSettingsValidator, PlotSettings plotSettings, IProgressMonitor progressMonitor)
            {
                this.PlotSettingsValidator = plotSettingsValidator;
                this.PlotSettings = plotSettings;
                this.ProgressMonitor = progressMonitor;
            }
        }

    }

    /// <summary>
    /// Defines a paperformat for a printer, defined by a readable name and the format name.
    /// </summary>
    public class PrinterPaperformat
    {
        /// <summary>
        /// The readable name of the format.
        /// </summary>
        /// <value>
        /// The readable name of the format.
        /// </value>
        public String Name { get; }

        /// <summary>
        /// The canonical name of the format as it is used to adress the format.
        /// </summary>
        /// <value>
        /// The original name of the format.
        /// </value>
        public String FormatName { get; }

        /// <summary>
        /// The printer this format belongs to.
        /// </summary>
        /// <value>
        /// The printer.
        /// </value>
        public Printer Printer { get; }

        /// <summary>
        /// Whether the printer paperformat is optimal, i.e., has margins of almost 4 mm.
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
