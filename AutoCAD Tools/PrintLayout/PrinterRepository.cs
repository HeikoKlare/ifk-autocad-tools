using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using System.Threading.Tasks;
using System.Linq;
using static AutoCADTools.PrintLayout.Printer;
using System.Threading;
using System.Windows.Forms;
using ApplicationServices = Autodesk.AutoCAD.ApplicationServices;
using System.Runtime.ExceptionServices;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A cache for printers so the formats are loaded only once.
    /// </summary>
    public class PrinterRepository
    {
        private static readonly IReadOnlyList<string> excludedNames = new List<string>() { "Default" };

        private static readonly PrinterRepository instance = new PrinterRepository();

        public bool Initialized
        {
            get;
            private set;
        }

        private bool initializing = false;
    
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
                var defaultPrinters = new List<string> { Properties.Settings.Default.DefaultPrinterCustom, Properties.Settings.Default.DefaultPrinterA3, Properties.Settings.Default.DefaultPrinterA4 };
                return psv.GetPlotDeviceList().Cast<string>()
                       .Where(device => device.EndsWith(Printer.printerConfigurationFileExtension))
                       .Select(device => device.Replace(Printer.printerConfigurationFileExtension, ""))
                       .Where(device => excludedNames.ToList().TrueForAll(name => !device.Contains(name)))
                       .OrderByDescending(name => defaultPrinters.IndexOf(name))
                       .ToList().AsReadOnly();
            }
        }

        private PrinterRepository() { }

        /// <summary>
        /// Gets the <see cref="Printer"/> with the specified name.
        /// Requries that the repository has been initialized, otherwise throws an <see cref="InvalidOperationException"/>.
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
                if (!Initialized)
                {
                    throw new InvalidOperationException("Printer repository has not been initialized yet");
                }

                if (!printer.ContainsKey(name))
                {
                    return null;
                }
                return printer[name];
            }
        }

        private void InitializePrinter(string name)
        {
            var newprinter = new Printer(name);
            newprinter.InitializePaperformats();
            printer.Add(name, newprinter);
        }

        /// <summary>
        /// Initializes the repository with available printers.
        /// </summary>
        [HandleProcessCorruptedStateExceptions]
        public IEnumerable<string> Initialize(CancellationToken cancellationToken)
        {
            if (initializing || Initialized)
            {
                return Enumerable.Empty<string>();
            }

            initializing = true;
            var failedPrinters = new List<string>();
            foreach (string device in PrinterNames)
            {
                try
                {
                    InitializePrinter(device);
                }
                catch (Exception)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return Enumerable.Empty<string>();
                    }
                    failedPrinters.Add(device);
                }
            }
            Initialized = true;
            return failedPrinters;
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
