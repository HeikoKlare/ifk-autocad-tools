﻿
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
            return PaperformatPrinterMapping.GetDefaultPrinter(this);
        }

        /// <inheritdoc/>
        public override PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedPaperformats)
        {
            return PaperformatPrinterMapping.GetFittingPaperformat(printer, optimizedPaperformats, this);
        }
    }
}
