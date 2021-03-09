using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using System.Threading.Tasks;
using System.Linq;
using static AutoCADTools.PrintLayout.Printer;

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

        private readonly Dictionary<string, Printer> printer = new Dictionary<string, Printer>();

        /// <summary>
        /// Gets the names of the available printers.
        /// </summary>
        public IReadOnlyList<string> PrinterNames
        {
            get
            {
                PlotSettingsValidator psv = PlotSettingsValidator.Current;
                return psv.GetPlotDeviceList().Cast<string>()
                       .Where(device => device.EndsWith(Printer.printerConfigurationFileExtension))
                       .Select(device => device.Replace(Printer.printerConfigurationFileExtension, ""))
                       .Where(device => excludedNames.ToList().TrueForAll(name => !device.Contains(name)))
                       .ToList().AsReadOnly();
            }
        }

        private PrinterRepository() { }

        /// <summary>
        /// Gets the <see cref="Printer"/> with the specified name. If it is not loaded it is done now (beware of the delay!).
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
                if (printer.ContainsKey(name))
                {
                    return printer[name];
                }
                else
                {
                    try
                    {
                        lock (this)
                        {
                            var newprinter = new Printer(name);
                            newprinter.InitializePaperformats();
                            printer.Add(name, newprinter);
                            return newprinter;
                        }
                    }
                    catch (ArgumentException)
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the repository with available printers
        /// </summary>
        public void Initialize()
        {
            lock (this)
            {
                PlotSettingsValidator psv = PlotSettingsValidator.Current;
                foreach (string device in PrinterNames)
                {
                    _ = PrinterRepository.Instance[device];
                }
            }
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void Clear()
        {
            printer.Clear();
        }

    }
}
