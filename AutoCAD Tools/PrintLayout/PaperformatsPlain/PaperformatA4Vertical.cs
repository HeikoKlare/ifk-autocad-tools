
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// The paperformat for A4 paper with a vertical viewport.
    /// </summary>
    public class PaperformatA4Vertical : PaperformatA4
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static new readonly Size MAX_VIEWPORT_SIZE = PaperformatA4.MAX_VIEWPORT_SIZE.Rotate();

        /// <inheritdoc />
        public override Size ViewportSizeLayout
        {
            get
            {
                return base.ViewportSizeLayout.Rotate();
            }
        }

        /// <inheritdoc />
        public override Size BorderSize
        {
            get
            {
                return base.BorderSize.Rotate();
            }
        }

        /// <inheritdoc />
        public override Point BorderBasePoint
        {
            get
            {
                return base.BorderBasePoint.Rotate();
            }
        }

        /// <inheritdoc />
        public override Point ViewportBasePoint
        {
            get
            {
                return base.ViewportBasePoint.Rotate();
            }
        }

        /// <inheritdoc />
        public override PaperformatPlain ChangeSize(Size size)
        {
            this.ViewportSizeModel = size;
            
            if (PaperformatA4Horizontal.MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatA4Horizontal().ChangeSize(size);
            }
            if (!MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatA3().ChangeSize(size);
            }

            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatA4Vertical"/> class with the maximum possible size.
        /// </summary>
        public PaperformatA4Vertical()
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
        }
    }
}
