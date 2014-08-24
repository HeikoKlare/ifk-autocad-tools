
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A plain paperformat for custom sized viewports.
    /// </summary>
    public class PaperformatCustom : PaperformatPlain
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static new readonly Size MAX_VIEWPORT_SIZE = new Size(1160, 825);

        /// <inheritdoc />
        public override Size ViewportSizeLayout {
            get
            {
                return ViewportSizeModel;
            }
        }

        /// <inheritdoc />
        public override Size BorderSize
        {
            get
            {
                return ViewportSizeModel;
            }
        }

        /// <inheritdoc />
        public override Point ViewportBasePoint
        {
            get
            {
                return Point.Origin + new Size(10, 10);
            }
        }

        /// <inheritdoc />
        public override Point BorderBasePoint
        {
            get
            {
                return Point.Origin + new Size(10, 10);
            }
        }

        /// <inheritdoc />
        public override PaperformatPlain ChangeSize(Size size)
        {
            this.ViewportSizeModel = size;
            
            if (PaperformatA3.MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatA3().ChangeSize(size);
            }
            if (!MAX_VIEWPORT_SIZE.Contains(size))
            {
                this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
            }

            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatCustom"/> class with the maximum possible size.
        /// </summary>
        public PaperformatCustom()
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
        }
    }
}
