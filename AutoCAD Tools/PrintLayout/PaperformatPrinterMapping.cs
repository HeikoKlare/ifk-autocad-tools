
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This class maps paperformats to default printers as well as paperformats to the best available formats using a specified printer.
    /// </summary>
    public static class PaperformatPrinterMapping
    {
        #region Default Printer

        /// <summary>
        /// Gets the default printer for the specified paperformat or <c>null</c> if there is none.
        /// </summary>
        /// <param name="paperformat">The paperformat to get the default printer for.</param>
        /// <returns>The default printer for the specified paperformat or <c>null</c> if there is none.</returns>
        public static Printer GetDefaultPrinter(PaperformatA4 paperformat)
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterA4];
        }

        /// <summary>
        /// Gets the default printer for the specified paperformat or <c>null</c> if there is none.
        /// </summary>
        /// <param name="paperformat">The paperformat to get the default printer for.</param>
        /// <returns>The default printer for the specified paperformat or <c>null</c> if there is none.</returns>
        public static Printer GetDefaultPrinter(PaperformatA3 paperformat)
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterA3];
        }

        /// <summary>
        /// Gets the default printer for the specified paperformat or <c>null</c> if there is none.
        /// </summary>
        /// <param name="paperformat">The paperformat to get the default printer for.</param>
        /// <returns>The default printer for the specified paperformat or <c>null</c> if there is none.</returns>
        public static Printer GetDefaultPrinter(PaperformatCustom paperformat)
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterCustom];
        }

        /// <summary>
        /// Gets the default printer for the specified paperformat or <c>null</c> if there is none.
        /// </summary>
        /// <param name="paperformat">The paperformat to get the default printer for.</param>
        /// <returns>The default printer for the specified paperformat or <c>null</c> if there is none.</returns>
        public static Printer GetDefaultPrinter(PaperformatTextfieldA4 paperformat)
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterA4];
        }

        /// <summary>
        /// Gets the default printer for the specified paperformat or <c>null</c> if there is none.
        /// </summary>
        /// <param name="paperformat">The paperformat to get the default printer for.</param>
        /// <returns>The default printer for the specified paperformat or <c>null</c> if there is none.</returns>
        public static Printer GetDefaultPrinter(PaperformatTextfieldA3 paperformat)
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterA3];
        }

        /// <summary>
        /// Gets the default printer for the specified paperformat or <c>null</c> if there is none.
        /// </summary>
        /// <param name="paperformat">The paperformat to get the default printer for.</param>
        /// <returns>The default printer for the specified paperformat or <c>null</c> if there is none.</returns>
        public static Printer GetDefaultPrinter(PaperformatTextfieldCustom paperformat)
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterCustom];
        }

        #endregion

        #region Fitting Paperformats

        /// <summary>
        /// Helper method for getting the best fitting printer paperformat for the specified printer and format.
        /// </summary>
        /// <param name="printer">The printer.</param>
        /// <param name="optimizedFormats">if set to <c>true</c> optimized formats are used.</param>
        /// <param name="formatNumber">The format number, e.g. 4 when using an A4 format.</param>
        /// <returns>The best fitting printer paperformat for the specified format.</returns>
        private static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, int formatNumber)
        {
            PrinterPaperformat result = null;
            int bestFormatFound = -1;
            foreach (var format in printer.GetPaperformats(optimizedFormats))
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
        /// Gets the best fitting printer paperformat for the specified printer and paperformat combination.
        /// </summary>
        /// <param name="printer">The printer to get the format for.</param>
        /// <param name="optimizedFormats">if set to <c>true</c> optimized formats are used.</param>
        /// <param name="paperformat">The paperformat to get the fitting paperformat of the printer for.</param>
        /// <returns>The best fitting printer paperformat of the specified printer and paperformat combination.</returns>
        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatA4 paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 4);
        }

        /// <summary>
        /// Gets the best fitting printer paperformat for the specified printer and paperformat combination.
        /// </summary>
        /// <param name="printer">The printer to get the format for.</param>
        /// <param name="optimizedFormats">if set to <c>true</c> optimized formats are used.</param>
        /// <param name="paperformat">The paperformat to get the fitting paperformat of the printer for.</param>
        /// <returns>The best fitting printer paperformat of the specified printer and paperformat combination.</returns>
        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatTextfieldA4 paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 4);
        }

        /// <summary>
        /// Gets the best fitting printer paperformat for the specified printer and paperformat combination.
        /// </summary>
        /// <param name="printer">The printer to get the format for.</param>
        /// <param name="optimizedFormats">if set to <c>true</c> optimized formats are used.</param>
        /// <param name="paperformat">The paperformat to get the fitting paperformat of the printer for.</param>
        /// <returns>The best fitting printer paperformat of the specified printer and paperformat combination.</returns>
        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatA3 paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 3);
        }

        /// <summary>
        /// Gets the best fitting printer paperformat for the specified printer and paperformat combination.
        /// </summary>
        /// <param name="printer">The printer to get the format for.</param>
        /// <param name="optimizedFormats">if set to <c>true</c> optimized formats are used.</param>
        /// <param name="paperformat">The paperformat to get the fitting paperformat of the printer for.</param>
        /// <returns>The best fitting printer paperformat of the specified printer and paperformat combination.</returns>
        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatTextfieldA3 paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 3);
        }

        /// <summary>
        /// Gets the best fitting printer paperformat for the specified printer and paperformat combination.
        /// </summary>
        /// <param name="printer">The printer to get the format for.</param>
        /// <param name="optimizedFormats">if set to <c>true</c> optimized formats are used.</param>
        /// <param name="paperformat">The paperformat to get the fitting paperformat of the printer for.</param>
        /// <returns>The best fitting printer paperformat of the specified printer and paperformat combination.</returns>
        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatCustom paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 0);
        }

        /// <summary>
        /// Gets the best fitting printer paperformat for the specified printer and paperformat combination.
        /// </summary>
        /// <param name="printer">The printer to get the format for.</param>
        /// <param name="optimizedFormats">if set to <c>true</c> optimized formats are used.</param>
        /// <param name="paperformat">The paperformat to get the fitting paperformat of the printer for.</param>
        /// <returns>The best fitting printer paperformat of the specified printer and paperformat combination.</returns>
        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatTextfieldCustom paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 0);
        }

        /// <summary>
        /// Determines whether the specified printer paperformat fits the specified paperformat.
        /// </summary>
        /// <param name="printerformat">The printerformat to check for fitting the paperformat.</param>
        /// <param name="paperformat">The paperformat to check against.</param>
        /// <returns><c>true</c> if the printer paperformat fits the specified paperformat, <c>false</c> otherwise</returns>
        public static bool IsFormatFitting(PrinterPaperformat printerformat, Paperformat paperformat)
        {
            if (printerformat == null || paperformat == null) return false;
            var fittingFormat = paperformat.GetFittingPaperformat(printerformat.Printer, false);
            if (fittingFormat == null) return false;
            char fittingFormatChar = fittingFormat.Name[fittingFormat.Name.Length - 1];
            char currentFormatChar = printerformat.Name[printerformat.Name.Length - 1];
            return char.IsDigit(fittingFormatChar) && char.IsDigit(currentFormatChar) && int.Parse(fittingFormatChar.ToString()) >= int.Parse(currentFormatChar.ToString());
        }

        #endregion
    }
}
