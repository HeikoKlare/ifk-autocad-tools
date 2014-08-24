
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A paperformat for A4 paper with textfield and horizontal viewport.
    /// </summary>
    public class PaperformatTextfieldA4Horizontal : PaperformatTextfieldA4
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static new readonly Size MAX_VIEWPORT_SIZE = PaperformatTextfieldA4.MAX_VIEWPORT_SIZE;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfieldA4Horizontal"/> class with the maximum possible size.
        /// </summary>
        /// <param name="oldTextfieldSize">if set to <c>true</c> the old textfield size is used.</param>
        public PaperformatTextfieldA4Horizontal(bool oldTextfieldSize)
            : base(oldTextfieldSize)
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
        }

        /// <inheritdoc/>
        public override PaperformatTextfield ChangeSize(Size size)
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
            if (!MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatTextfieldA4Vertical(OldTextfieldSize).ChangeSize(size);
            }
            return this;
        }
    }
}
