
using AutoCADTools.Utils;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A paperformat for A4 paper with textfield and borders.
    /// </summary>
    public abstract class PaperformatTextfieldA4 : PaperformatTextfield
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        protected static new Size MaximumViewportSize
        {
            get { return new Size(250.5, 171.37); }
        }

        /// <inheritdoc/>
        public override Size ViewportSizeLayout
        {
            get { return MaximumViewportSize.Rotate() + new Size(-0.342, -0.5); }
        }

        private readonly Point viewportBasePoint = new Point(26.986, 11.0);
        /// <inheritdoc/>
        public override Point ViewportBasePoint
        {
            get { return viewportBasePoint; }
        }

        private readonly Size borderSize = new Size(175.0, 281.0);
        /// <inheritdoc/>
        public override Size BorderSize
        {
            get { return borderSize; }
        }

        private readonly Point borderBasePoint = new Point(25.0, 9.0);
        /// <inheritdoc/>
        public override Point BorderBasePoint
        {
            get { return borderBasePoint; }
        }

        private readonly Size textfieldSize = new Size(0.0, 0.0);
        /// <inheritdoc/>
        public override Size TextfieldSize
        {
            get { return textfieldSize; }
        }

        private readonly Point textfieldBasePoint = new Point(25.0, 290.0);
        /// <inheritdoc/>
        public override Point TextfieldBasePoint
        {
            get { return textfieldBasePoint; }
        }

        /// <inheritdoc/>
        public override bool FullTextfieldUsed
        {
            get { return false; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfieldA4"/> class.
        /// </summary>
        /// <param name="oldTextfieldSize">if set to <c>true</c> the old textfield size is used.</param>
        public PaperformatTextfieldA4(bool oldTextfieldSize)
            : base(oldTextfieldSize)
        {
        }

        /// <summary>
        /// The textfield block name for A4 paper
        /// </summary>
        public static readonly string TEXTFIELD_BLOCK_NAME = "Textfeld A4";

        /// <inheritdoc/>
        public override string TextfieldBlockName
        {
            get { return TEXTFIELD_BLOCK_NAME; }
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
