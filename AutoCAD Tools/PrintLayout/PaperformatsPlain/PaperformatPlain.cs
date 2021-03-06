
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Base class for plain paperformats without any textfields or borders. Layout size is equal to viewport size in model space.
    /// </summary>
    public abstract class PaperformatPlain : Paperformat
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static Size MaximumViewportSize
        {
            get { return new Size(1160, 825); }
        }

        /// <summary>
        /// Changes the size of the viewport according to the specified value. The paperformat that fits the size is returned.
        /// </summary>
        /// <param name="size">The size of the viewport in model space.</param>
        /// <returns>
        /// The paperformat that fits the specified viewport size
        /// </returns>
        public abstract PaperformatPlain ChangeSize(Size size);
    }
}
