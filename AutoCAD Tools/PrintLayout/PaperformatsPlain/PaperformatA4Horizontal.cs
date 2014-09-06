
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
        public static new Size MaximumViewportSize
        {
            get { return PaperformatA4.MaximumViewportSize; }
        }

        /// <inheritdoc />
        public override PaperformatPlain ChangeSize(Size size)
        {
            this.ViewportSizeModel = MaximumViewportSize;
            if (!MaximumViewportSize.Contains(size))
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
            this.ViewportSizeModel = MaximumViewportSize;
        }
    }
}
