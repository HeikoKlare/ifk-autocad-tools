﻿
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
            return PaperformatPrinterMapping.GetDefaultPrinter(this);
        }

        /// <inheritdoc/>
        public override PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedPaperformats)
        {
            return PaperformatPrinterMapping.GetFittingPaperformat(printer, optimizedPaperformats, this);
        }
    }
}
