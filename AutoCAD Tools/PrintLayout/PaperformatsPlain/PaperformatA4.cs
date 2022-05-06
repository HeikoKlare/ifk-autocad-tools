
using AutoCADTools.Utils;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// The paperformat for A4 paper.
    /// </summary>
    public abstract class PaperformatA4 : PaperformatPlain
    {
        /// <summary>
        /// The maximum possible viewport size.
        /// </summary>
        public static new Size MaximumViewportSize
        {
            get { return new Size(297, 210); }
        }

        /// <inheritdoc />
        public override Size ViewportSizeLayout
        {
            get
            {
                return MaximumViewportSize;
            }
        }

        /// <inheritdoc />
        public override Size BorderSize
        {
            get
            {
                return MaximumViewportSize;
            }
        }

        /// <inheritdoc />
        public override Point ViewportBasePoint
        {
            get
            {
                return Point.Origin;
            }
        }

        /// <inheritdoc />
        public override Point BorderBasePoint
        {
            get
            {
                return Point.Origin;
            }
        }

        /// <inheritdoc/>
        public override Printer GetDefaultPrinter()
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterA4];
        }

        /// <inheritdoc/>
        public override PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedPaperformats, IProgressMonitor progressMonitor)
        {
            return PaperformatPrinterMapping.GetFittingPaperformat(printer, optimizedPaperformats, 4, progressMonitor);
        }
    }
}
