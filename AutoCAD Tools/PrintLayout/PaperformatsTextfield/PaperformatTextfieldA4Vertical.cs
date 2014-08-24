
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A paperformat for A4 paper with textfield and vertical viewport.
    /// </summary>
    public class PaperformatTextfieldA4Vertical : PaperformatTextfieldA4
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static new readonly Size MAX_VIEWPORT_SIZE = PaperformatTextfieldA4.MAX_VIEWPORT_SIZE.Rotate();

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfieldA4Vertical"/> class with the maximum possible viewport isze.
        /// </summary>
        /// <param name="oldTextfieldSize">if set to <c>true</c> the old textfield size is used.</param>
        public PaperformatTextfieldA4Vertical(bool oldTextfieldSize)
            : base(oldTextfieldSize)
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
        }

        /// <inheritdoc/>
        public override PaperformatTextfield ChangeSize(Size size)
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;

            if (PaperformatTextfieldA4Horizontal.MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatTextfieldA4Horizontal(OldTextfieldSize).ChangeSize(size);
            }
            if (!MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatTextfieldA3(OldTextfieldSize).ChangeSize(size);
            }

            return this;
        }
    }
}
