
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// The paperformat for A4 paper.
    /// </summary>
    public abstract class PaperformatA4 : PaperformatPlain
    {
        /// <summary>
        /// The maximum possible viewport size.
        /// </summary>
        public static new readonly Size MAX_VIEWPORT_SIZE = new Size(297, 210);

        /// <inheritdoc />
        public override Size ViewportSizeLayout
        {
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

    }
}
