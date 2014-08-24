
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This type is a definition for paperformats with the full (A3+) textfield
    /// </summary>
    public abstract class PaperformatTextfieldFullTextfield : PaperformatTextfield
    {
        /// <summary>
        /// The offset of the whole layout in layout space from origin.
        /// </summary>
        protected Size offset;

        /// <summary>
        /// Gets the size of the textfield depending wheter the old textfield size is used or not.
        /// </summary>
        /// <value>
        /// The size of the textfield.
        /// </value>
        public override Size TextfieldSize
        {
            get { return OldTextfieldSize ? new Size(185.0, 75.0) : new Size(185.0, 57.5); }
        }

        /// <inheritdoc />
        public override Point TextfieldBasePoint
        {
            get { return BorderBasePoint + BorderSize + new Size(-TextfieldSize.Width, TextfieldSize.Height) + new Size(-5.0, 5.0) + offset; }
        }

        /// <inheritdoc />
        public override Size ViewportSizeLayout
        {
            get { return ViewportSizeModel; }
        }

        /// <inheritdoc />
        public override Point ViewportBasePoint
        {
            get { return BorderBasePoint + new Size(20.0, 5.0); }
        }

        /// <inheritdoc />
        public override Size BorderSize
        {
            get { return ViewportSizeLayout + new Size(25.0, 10.0); }
        }

        /// <inheritdoc />
        public override Point BorderBasePoint
        {
            get { return Point.Origin + offset; }
        }

        /// <summary>
        /// True, because the full textfield (A3+) is used.
        /// </summary>
        /// <value>
        ///   <c>true</c> because the full textfield size is used.
        /// </value>
        public override bool FullTextfieldUsed
        {
            get { return true; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfieldFullTextfield"/> class.
        /// </summary>
        /// <param name="oldTextfieldSize">if set to <c>true</c> the old textfield size is used.</param>
        public PaperformatTextfieldFullTextfield(bool oldTextfieldSize) : base(oldTextfieldSize) { }
    }
}
