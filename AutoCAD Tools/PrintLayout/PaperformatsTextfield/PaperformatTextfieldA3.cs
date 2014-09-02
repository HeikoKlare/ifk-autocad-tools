
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This paperformat is for A3 paper with a textfield and borders.
    /// </summary>
    public class PaperformatTextfieldA3 : PaperformatTextfieldFullTextfield
    {
        /// <summary>
        /// The maximum viewport size.
        /// </summary>
        public static new readonly Size MAX_VIEWPORT_SIZE = new Size(395.0, 287.0);

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfieldA3"/> class with the maximum possible size.
        /// </summary>
        /// <param name="oldTextfieldSize">if set to <c>true</c> the old textfield size is used.</param>
        public PaperformatTextfieldA3(bool oldTextfieldSize)
            : base(oldTextfieldSize)
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
            this.offset = Size.Zero;
        }

        /// <inheritdoc />
        public override PaperformatTextfield ChangeSize(Size size)
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;

            if (PaperformatTextfieldA4Horizontal.MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatTextfieldA4Horizontal(OldTextfieldSize).ChangeSize(size);
            }
            if (PaperformatTextfieldA4Vertical.MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatTextfieldA4Vertical(OldTextfieldSize).ChangeSize(size);
            }
            if (!MAX_VIEWPORT_SIZE.Contains(size))
            {
                return new PaperformatTextfieldCustom(OldTextfieldSize).ChangeSize(size);
            }

            return this;
        }

        /// <inheritdoc/>
        public override Printer GetDefaultPrinter()
        {
            return PaperformatPrinterMapping.GetDefaultPrinter(this);
        }

        /// <inheritdoc/>
        public override PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedPaperformats)
        {
            return PaperformatPrinterMapping.GetFittingPaperformat(printer, optimizedPaperformats, this);
        }
    }
}
