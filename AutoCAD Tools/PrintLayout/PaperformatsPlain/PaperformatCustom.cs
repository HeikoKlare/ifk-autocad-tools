
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A plain paperformat for custom sized viewports.
    /// </summary>
    public class PaperformatCustom : PaperformatPlain
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static new Size MaximumViewportSize
        {
            get { return new Size(1160, 825); }
        }

        /// <inheritdoc />
        public override Size ViewportSizeLayout
        {
            get
            {
                return ViewportSizeModel;
            }
        }

        /// <inheritdoc />
        public override Size BorderSize
        {
            get
            {
                return ViewportSizeModel;
            }
        }

        /// <inheritdoc />
        public override Point ViewportBasePoint
        {
            get
            {
                return Point.Origin + new Size(10, 10);
            }
        }

        /// <inheritdoc />
        public override Point BorderBasePoint
        {
            get
            {
                return Point.Origin + new Size(10, 10);
            }
        }

        /// <inheritdoc />
        public override PaperformatPlain ChangeSize(Size size)
        {
            this.ViewportSizeModel = size;

            if (PaperformatA3.MaximumViewportSize.Contains(size))
            {
                return new PaperformatA3().ChangeSize(size);
            }
            if (!MaximumViewportSize.Contains(size))
            {
                this.ViewportSizeModel = MaximumViewportSize;
            }

            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatCustom"/> class with the maximum possible size.
        /// </summary>
        public PaperformatCustom()
        {
            this.ViewportSizeModel = MaximumViewportSize;
        }

        /// <inheritdoc/>
        public override Printer GetDefaultPrinter()
        {
            return PrinterRepository.Instance[Properties.Settings.Default.DefaultPrinterCustom];
        }

        /// <inheritdoc/>
        public override PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedPaperformats)
        {
            return PaperformatPrinterMapping.GetFittingPaperformat(printer, optimizedPaperformats, 0);
        }
    }
}
