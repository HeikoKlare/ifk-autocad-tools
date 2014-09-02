using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCADTools.PrintLayout
{
    public class PaperformatPrinterMapping
    {
        public static Printer GetDefaultPrinter(PaperformatA4 paperformat)
        {
            return PrinterCache.Instance[Properties.Settings.Default.DefaultPrinterA4];
        }

        public static Printer GetDefaultPrinter(PaperformatA3 paperformat)
        {
            return PrinterCache.Instance[Properties.Settings.Default.DefaultPrinterA3];
        }

        public static Printer GetDefaultPrinter(PaperformatCustom paperformat)
        {
            return PrinterCache.Instance[Properties.Settings.Default.DefaultPrinterCustom];
        }

        public static Printer GetDefaultPrinter(PaperformatTextfieldA4 paperformat)
        {
            return PrinterCache.Instance[Properties.Settings.Default.DefaultPrinterA4];
        }

        public static Printer GetDefaultPrinter(PaperformatTextfieldA3 paperformat)
        {
            return PrinterCache.Instance[Properties.Settings.Default.DefaultPrinterA3];
        }

        public static Printer GetDefaultPrinter(PaperformatTextfieldCustom paperformat)
        {
            return PrinterCache.Instance[Properties.Settings.Default.DefaultPrinterCustom];
        }

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

        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatA4 paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 4);
        }

        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatTextfieldA4 paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 4);
        }

        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatA3 paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 3);
        }

        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatTextfieldA3 paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 3);
        }

        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatCustom paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 0);
        }

        public static PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedFormats, PaperformatTextfieldCustom paperformat)
        {
            return GetFittingPaperformat(printer, optimizedFormats, 0);
        }

        public static bool IsFormatFitting(PrinterPaperformat printerformat, Paperformat paperformat)
        {
            var fittingFormat = paperformat.GetFittingPaperformat(printerformat.Printer, false);
            char fittingFormatChar = fittingFormat.Name[fittingFormat.Name.Length - 1];
            char currentFormatChar = printerformat.Name[printerformat.Name.Length - 1];
            return char.IsDigit(fittingFormatChar) && char.IsDigit(currentFormatChar) && int.Parse(fittingFormatChar.ToString()) >= int.Parse(currentFormatChar.ToString());
        }
    }
}
