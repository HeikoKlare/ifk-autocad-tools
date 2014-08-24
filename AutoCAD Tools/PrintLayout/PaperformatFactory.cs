
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// A factory for creating paperformats.
    /// </summary>
    public static class PaperformatFactory
    {
        /// <summary>
        /// Gets a paperformat with textfield according to the specified viewport size.
        /// </summary>
        /// <param name="viewportSize">Size of the viewport.</param>
        /// <param name="oldTextfieldSize">if set to <c>true</c> the old textfield size is used.</param>
        /// <returns>
        /// A paperformat with textfield according to the specified viewport size.
        /// </returns>
        public static PaperformatTextfield GetPaperformatTextfield(Size viewportSize, bool oldTextfieldSize = false)
        {
            return new PaperformatTextfieldA4Horizontal(oldTextfieldSize).ChangeSize(viewportSize);
        }

        /// <summary>
        /// Gets a plain paperformat according to the specified viewport size.
        /// </summary>
        /// <param name="viewportSize">Size of the viewport.</param>
        /// <returns>
        /// A plain paperformat according to the specified viewport size.
        /// </returns>
        public static PaperformatPlain GetPlainPaperformat(Size viewportSize)
        {
            return new PaperformatA4Horizontal().ChangeSize(viewportSize);
        }
    }
}
