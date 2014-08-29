
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
        protected static new readonly Size MAX_VIEWPORT_SIZE = new Size(250.5, 171.37);

        /// <inheritdoc/>
        public override Size ViewportSizeLayout
        {
            get { return MAX_VIEWPORT_SIZE.Rotate() + new Size(-0.342, -0.5); }
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

        /// <inheritdoc/>
        public override string TextfieldBlockName
        {
            get { return "Textfeld A4"; }
        }

    }
}
