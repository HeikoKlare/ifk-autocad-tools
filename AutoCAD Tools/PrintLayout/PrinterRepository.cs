using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;
using System.Linq;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A cache for printers so the formats are loaded only once.
    /// </summary>
    public class PrinterRepository
    {
        private static readonly IReadOnlyList<string> excludedNames = new List<string>() { "Default" };

        private static readonly PrinterRepository instance = new PrinterRepository();
        /// <summary>
        /// Gets the singleton instance of the cache.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static PrinterRepository Instance
        {
            get { return PrinterRepository.instance; }
        }

        private readonly Dictionary<string, Printer> printerNamesToPrinters = new Dictionary<string, Printer>();
        /// <summary>
        /// The printers in this repository.
        /// </summary>
        public IReadOnlyList<Printer> Printers => printerNamesToPrinters.Values.ToList();


        private readonly IReadOnlyList<string> preferenceOrderedPrinterNames;
        /// <summary>
        /// The names of the available printers.
        /// </summary>
        public IReadOnlyList<string> PreferenceOrderedPrinterNames => preferenceOrderedPrinterNames;

        private PrinterRepository() {
            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            var defaultPrinters = new List<string> { Properties.Settings.Default.DefaultPrinterCustom, Properties.Settings.Default.DefaultPrinterA3, Properties.Settings.Default.DefaultPrinterA4 };
            preferenceOrderedPrinterNames = psv.GetPlotDeviceList().Cast<string>()
                   .Where(device => device.EndsWith(Printer.PrinterConfigurationFileExtension))
                   .Select(device => device.Replace(Printer.PrinterConfigurationFileExtension, ""))
                   .Where(device => excludedNames.ToList().TrueForAll(name => !device.Contains(name)))
                   .OrderByDescending(name => defaultPrinters.IndexOf(name)).ToList().AsReadOnly();
            Clear();
        }

        /// <summary>
        /// Gets the <see cref="Printer"/> with the specified name.
        /// If there is no printer with the specified name available, <c>null</c> is returned.
        /// </summary>
        /// <value>
        /// The <see cref="Printer"/>.
        /// </value>
        /// <param name="name">The name of the printer.</param>
        /// <returns>The printer with the specified name.</returns>
        public Printer this[string name]
        {
            get
            {
                if (printerNamesToPrinters.ContainsKey(name))
                {
                    return printerNamesToPrinters[name];
                } else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void Clear()
        {
            printerNamesToPrinters.Clear();
            foreach (var printerName in preferenceOrderedPrinterNames)
            {
                printerNamesToPrinters.Add(printerName, new Printer(printerName));
            }
        }

    }
}
