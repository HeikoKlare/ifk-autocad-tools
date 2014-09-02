
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// The paperformat for A4 paper with horizontal viewport.
    /// </summary>
    public class PaperformatA4Horizontal : PaperformatA4
    {
        /// <summary>
        /// The maximum possible viewport size.
        /// </summary>
        public static new readonly Size MAX_VIEWPORT_SIZE = PaperformatA4.MAX_VIEWPORT_SIZE;

        /// <inheritdoc />
        public override PaperformatPlain ChangeSize(Size size)
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
            if (!MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatA4Vertical().ChangeSize(size);
            }
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatA4Horizontal"/> class with the maximum possible size.
        /// </summary>
        public PaperformatA4Horizontal()
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
        }
    }
}
