
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A paperformat with a textfield and borders.
    /// </summary>
    public abstract class PaperformatTextfield : Paperformat
    {
        #region Fields

        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static Size MaximumViewportSize
        {
            get { return new Size(1140, 810); }
        }

        /// <summary>
        /// Gets the size of the textfield.
        /// </summary>
        /// <value>
        /// The size of the textfield.
        /// </value>
        public abstract Size TextfieldSize { get; }

        /// <summary>
        /// Gets the base point of the textfield for insertion in layout space.
        /// </summary>
        /// <value>
        /// The base point of the textfield.
        /// </value>
        public abstract Point TextfieldBasePoint { get; }

        private bool oldTextfieldSize;
        /// <summary>
        /// Gets or sets a value indicating whether the old textfield size is used or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if old textfield size is used; otherwise, <c>false</c>.
        /// </value>
        public bool OldTextfieldSize
        {
            get { return oldTextfieldSize; }
            set { oldTextfieldSize = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the full textfield is used (A3+) or not (A4).
        /// </summary>
        /// <value>
        ///   <c>true</c> if full textfield is used; otherwise, <c>false</c>.
        /// </value>
        public abstract bool FullTextfieldUsed
        {
            get;
        }

        /// <summary>
        /// Gets the name of the textfield block.
        /// </summary>
        /// <value>
        /// The name of the textfield block.
        /// </value>
        public abstract string TextfieldBlockName
        {
            get;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfield"/> class.
        /// </summary>
        /// <param name="oldTextfieldSize">if set to <c>true</c> the old textfield size is used.</param>
        public PaperformatTextfield(bool oldTextfieldSize)
        {
            this.oldTextfieldSize = oldTextfieldSize;
        }

        #endregion

        /// <summary>
        /// Changes the size of the viewport according to the specified value. The paperformat that fits the size is returned.
        /// </summary>
        /// <param name="size">The size of the viewport in model space.</param>
        /// <returns>
        /// The paperformat that fits the specified viewport size
        /// </returns>
        public abstract PaperformatTextfield ChangeSize(Size size);
    }
}
