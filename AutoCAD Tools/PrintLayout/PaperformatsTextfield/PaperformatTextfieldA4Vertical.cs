
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
        public static new Size MaximumViewportSize
        {
            get { return PaperformatTextfieldA4.MaximumViewportSize.Rotate(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfieldA4Vertical"/> class with the maximum possible viewport isze.
        /// </summary>
        /// <param name="oldTextfieldSize">if set to <c>true</c> the old textfield size is used.</param>
        public PaperformatTextfieldA4Vertical(bool oldTextfieldSize)
            : base(oldTextfieldSize)
        {
            this.ViewportSizeModel = MaximumViewportSize;
        }

        /// <inheritdoc/>
        public override PaperformatTextfield ChangeSize(Size size)
        {
            this.ViewportSizeModel = MaximumViewportSize;

            if (PaperformatTextfieldA4Horizontal.MaximumViewportSize.Contains(size))
            {
                return new PaperformatTextfieldA4Horizontal(OldTextfieldSize).ChangeSize(size);
            }
            if (!MaximumViewportSize.Contains(size))
            {
                return new PaperformatTextfieldA3(OldTextfieldSize).ChangeSize(size);
            }

            return this;
        }
    }
}
