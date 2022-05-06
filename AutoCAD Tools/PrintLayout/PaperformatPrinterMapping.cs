
using AutoCADTools.Utils;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This class maps paperformats to default printers as well as paperformats to the best available formats using a specified printer.
    /// </summary>
    public static class PaperformatPrinterMapping
    {
        #region Fitting Paperformats

        /// <summary>
        /// Helper method for getting the best fitting printer paperformat for the specified printer and format.
        /// </summary>
        /// <param name="printer">The printer.</param>
        /// <param name="optimizedFormats">if set to <c>true</c> optimized formats are used.</param>
        /// <param name="formatNumber">The format number, e.g. 4 when using an A4 format.</param>
        /// <param name="progressMonitor">The monitor to get informed about the initialization process, may be <code>null</code>.</param>
        /// <returns>The best fitting printer paperformat for the specified format.</returns>
        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, int formatNumber, IProgressMonitor progressMonitor)
        {
            PrinterPaperformat result = null;
            int bestFormatFound = -1;
            foreach (var format in printer.InitializeAndGetPaperformats(optimizedFormats, progressMonitor))
            {
                char lastSymbol = format.Name[format.Name.Length - 1];
                if (char.IsDigit(lastSymbol))
                {
                    int number = int.Parse(lastSymbol.ToString());
                    if (number <= formatNumber && number > bestFormatFound)
                    {
                        bestFormatFound = number;
                        result = format;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Determines whether the specified printer paperformat fits the specified paperformat.
        /// </summary>
        /// <param name="printerformat">The printerformat to check for fitting the paperformat.</param>
        /// <param name="paperformat">The paperformat to check against.</param>
        /// <param name="progressMonitor">The monitor to get informed about the initialization process, may be <code>null</code>.</param>
        /// <returns><c>true</c> if the printer paperformat fits the specified paperformat, <c>false</c> otherwise</returns>
        public static bool IsFormatFitting(PrinterPaperformat printerformat, Paperformat paperformat, IProgressMonitor progressMonitor)
        {
            if (printerformat == null || paperformat == null) return false;
            var fittingFormat = paperformat.GetFittingPaperformat(printerformat.Printer, false, progressMonitor);
            if (fittingFormat == null) return false;
            char fittingFormatChar = fittingFormat.Name[fittingFormat.Name.Length - 1];
            char currentFormatChar = printerformat.Name[printerformat.Name.Length - 1];
            return char.IsDigit(fittingFormatChar) && char.IsDigit(currentFormatChar) && int.Parse(fittingFormatChar.ToString()) >= int.Parse(currentFormatChar.ToString());
        }

        #endregion
    }
}
