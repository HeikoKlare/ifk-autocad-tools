
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A plain A3 paperformat.
    /// </summary>
    public class PaperformatA3 : PaperformatPlain
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static new readonly Size MAX_VIEWPORT_SIZE = new Size(420, 297);

        /// <inheritdoc />
        public override Size ViewportSizeLayout {
            get
            {
                return MAX_VIEWPORT_SIZE;
            }
        }

        /// <inheritdoc />
        public override Size BorderSize
        {
            get
            {
                return MAX_VIEWPORT_SIZE;
            }
        }

        /// <inheritdoc />
        public override Point ViewportBasePoint
        {
            get
            {
                return Point.Origin;
            }
        }

        /// <inheritdoc />
        public override Point BorderBasePoint
        {
            get
            {
                return Point.Origin;
            }
        }

        /// <inheritdoc />
        public override PaperformatPlain ChangeSize(Size size)
        {
            this.ViewportSizeModel = size;
            
            if (PaperformatA4Vertical.MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatA4Vertical().ChangeSize(size);
            }
            if (!MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatCustom().ChangeSize(size);
            }

            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatA3"/> class with the maximum possible size.
        /// </summary>
        public PaperformatA3()
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
        }
    }
}
