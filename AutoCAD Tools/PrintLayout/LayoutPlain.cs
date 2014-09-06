using Autodesk.AutoCAD.DatabaseServices;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Creates a plain layout with the specified attributes. There is just the defined extract as a viewport in the layout.
    /// </summary>
    public class LayoutPlain : LayoutCreation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutPlain"/> class for the specified paperformat.
        /// </summary>
        /// <param name="paperformat">The paperformat to create the layout for.</param>
        public LayoutPlain(Paperformat paperformat)
            : base(paperformat)
        {

        }

        /// <summary>
        /// Draws nothing.
        /// </summary>
        /// <param name="margin">The margin of the used paper.</param>
        /// <param name="layoutRecord">The layout record of the created layout.</param>
        /// <returns><c>true</c> because nothing to do.</returns>
        protected override bool DrawLayoutAdditions(Size margin, BlockTableRecord layoutRecord)
        {
            return true;
        }
    }
}
