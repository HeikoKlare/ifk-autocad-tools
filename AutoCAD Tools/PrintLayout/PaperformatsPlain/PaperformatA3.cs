
using AutoCADTools.Utils;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A plain A3 paperformat.
    /// </summary>
    public class PaperformatA3 : PaperformatPlain
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static new Size MaximumViewportSize
        {
            get { return new Size(420, 297); }
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

        /// <inheritdoc />
        public override PaperformatPlain ChangeSize(Size size)
        {
            this.ViewportSizeModel = MaximumViewportSize;

            if (PaperformatA4Vertical.MaximumViewportSize.Contains(size))
            {
                return new PaperformatA4Vertical().ChangeSize(size);
            }
            if (!MaximumViewportSize.Contains(size))
            {
                return new PaperformatCustom().ChangeSize(size);
            }

            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatA3"/> class with the maximum possible size.
        /// </summary>
        public PaperformatA3()
        {
            this.ViewportSizeModel = MaximumViewportSize;
        }

        /// <inheritdoc/>
        public override Printer GetDefaultPrinter()
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterA3];
        }

        /// <inheritdoc/>
        public override PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedPaperformats, IProgressMonitor progressMonitor)
        {
            return PaperformatPrinterMapping.GetFittingPaperformat(printer, optimizedPaperformats, 3, progressMonitor);
        }
    }
}
