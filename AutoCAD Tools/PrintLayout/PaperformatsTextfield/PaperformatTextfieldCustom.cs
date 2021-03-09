
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This type defines a paperformat with custom size, bigger than A3.
    /// </summary>
    public class PaperformatTextfieldCustom : PaperformatTextfieldFullTextfield
    {
        /// <summary>
        /// The maximum viewport size
        /// </summary>
        public static new Size MaximumViewportSize
        {
            get { return new Size(1140.0, 810.0); }
        }

        private static Size MinimumViewportSize
        {
            get { return new Size(395, 287.0); }
        }

        /// <summary>
        /// The threshold for increasing the size to the next fold size
        /// </summary>
        private const double foldThreshold = 20.0;

        /// <summary>
        /// The fold sizes in each dimension. After the specified value the paper is folded.
        /// </summary>
        public static Point FoldPeriod
        {
            get { return new Point(190, 297); }
        }

        private static Point FoldMargin
        {
            get { return new Point(185, 287); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfieldCustom"/> class with the maximum possible size.
        /// </summary>
        /// <param name="oldTextfieldSize">specified if the old textfield size shell be used</param>
        public PaperformatTextfieldCustom(bool oldTextfieldSize)
            : base(oldTextfieldSize)
        {
            this.ViewportSizeModel = MaximumViewportSize;
            this.offset = new Size(10.0, 10.0);
        }

        /// <inheritdoc/>
        public override PaperformatTextfield ChangeSize(Size size)
        {
            this.ViewportSizeModel = size;
            IncreaseToNextBiggerFormat();
            Crop();

            if (PaperformatTextfieldA3.MaximumViewportSize.Contains(size))
            {
                return new PaperformatTextfieldA3(OldTextfieldSize).ChangeSize(size);
            }
            return this;
        }

        /// <summary>
        /// Crops the viewport size, so it is neither to big nor to small.
        /// </summary>
        private void Crop()
        {
            double width = ViewportSizeModel.Width;
            double height = ViewportSizeModel.Height;

            width = MaximumViewportSize.Width < ViewportSizeModel.Width ? MaximumViewportSize.Width : width;
            width = MinimumViewportSize.Width > ViewportSizeModel.Width ? MinimumViewportSize.Width : width;

            height = MaximumViewportSize.Height < ViewportSizeModel.Height ? MaximumViewportSize.Height : height;
            height = MinimumViewportSize.Height > ViewportSizeModel.Height ? MinimumViewportSize.Height : height;

            this.ViewportSizeModel = new Size(width, height);
        }

        /// <summary>
        /// The model viewport dimensions are increased to the next bigger format in each dimension, if the value is
        /// less than a threshold away from the next bigger formants size.
        /// </summary>
        public void IncreaseToNextBiggerFormat()
        {
            Size result = new Size(ViewportSizeModel.Width, ViewportSizeModel.Height);
            if ((ViewportSizeModel.Width - FoldMargin.X) % FoldPeriod.X > (FoldPeriod.X - foldThreshold))
            {
                result = new Size(((int)(ViewportSizeModel.Width - FoldMargin.X) / (int)FoldPeriod.X + 1) * FoldPeriod.X + FoldMargin.X, result.Height);
            }
            if ((ViewportSizeModel.Height - FoldMargin.Y) % FoldPeriod.Y > (FoldPeriod.Y - foldThreshold))
            {
                result = new Size(result.Width, ((int)(ViewportSizeModel.Height - FoldMargin.Y) / (int)FoldPeriod.Y + 1) * FoldPeriod.Y + FoldMargin.Y);
            }
            ViewportSizeModel = result;
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
