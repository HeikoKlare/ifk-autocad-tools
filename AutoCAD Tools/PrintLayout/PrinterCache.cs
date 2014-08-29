using System;
using System.Collections.Generic;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A cache for printers so the formats are loaded only once.
    /// </summary>
    public class PrinterCache
    {
        private static readonly PrinterCache instance = new PrinterCache();

        /// <summary>
        /// Gets the singleton instance of the cache.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static PrinterCache Instance
        {
            get { return PrinterCache.instance; }
        }

        private Dictionary<string, Printer> printer;

        private PrinterCache() {
            this.printer = new Dictionary<string, Printer>();
        }

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
                        var newprinter = new Printer(name);
                        newprinter.InitializePaperformats();
                        printer.Add(name, newprinter);
                        return newprinter;
                    }
                    catch (ArgumentException)
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether the specified printer name has been cached.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <returns><c>true</c> if the specified printer is cached yet, <c>false</c> otherweise</returns>
        public bool HasCached(string printerName)
        {
            return printer.ContainsKey(printerName);
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
