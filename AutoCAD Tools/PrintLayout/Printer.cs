using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.PlottingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;

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
        /// Gets the optimized or unoptimized paperformats for this printer, depending on the specified parameter. Formats are optimized if their margins
        /// are all nearly 4 mm.
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
        /// </summary>
        /// <exception cref="InvalidOperationException">When a memory access error occurred, such that the printer could not initialized properly. This is usually the case when the application has been closed during execution.</exception>
        [HandleProcessCorruptedStateExceptions]
        public void InitializePaperformats()
        {
            try
            {
                PlotSettingsValidator psv = PlotSettingsValidator.Current;
                using (PlotSettings acPlSet = new PlotSettings(true))
                {
                    psv.SetPlotConfigurationName(acPlSet, Name + printerConfigurationFileExtension, null);
                    psv.RefreshLists(acPlSet);

                    var paperFormats = psv.GetCanonicalMediaNameList(acPlSet).Cast<string>();
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
                            foreach (var candidate in candidateFormats)
                            {
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
                            }
                        }
                        if (foundFormat != null)
                        {
                            paperformats.Add(new PrinterPaperformat(ordinaryPaperformat, foundFormat, this, isOptimal));
                        }
                    }
                }
            }
            catch (AccessViolationException)
            {
                paperformats.Clear();
                throw new InvalidOperationException("A memory access error occurred when initializing paperformats for printer " + Name);
            }
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
