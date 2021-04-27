using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using System.Linq;
using System.Threading;
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
        
        /// <summary>
        /// Returns whether the repository has been initialized, such that printers can be accessed.
        /// </summary>
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

        [HandleProcessCorruptedStateExceptions]
        private bool InitializePrinter(string name)
        {
            try
            {
                var newprinter = new Printer(name);
                newprinter.InitializePaperformats();
                printer.Add(name, newprinter);
                return true;
            }
            catch (Exception)
            {
                // There may be different reasons for the initialization to fail. In particular,
                // changing the document in between will lead to access violation errors, such that
                // we abort and return false to enfore restart initialization.
                return false;
            }
        }

        /// <summary>
        /// Initializes the repository with available printers.
        /// </summary>
        public void Initialize(CancellationToken cancellationToken)
        {
            lock (this)
            {
                if (initializing || Initialized)
                {
                    return;
                }

                initializing = true;
                var remainingPrinters = new List<string>();
                remainingPrinters.AddRange(PrinterNames);
                while (remainingPrinters.Any())
                {
                    var nextPrinter = remainingPrinters.First();
                    if (InitializePrinter(nextPrinter))
                    {
                        remainingPrinters.Remove(nextPrinter);
                    }
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                }
                Initialized = true;
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
