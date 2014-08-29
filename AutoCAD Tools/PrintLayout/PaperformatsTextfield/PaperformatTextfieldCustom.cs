
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
        public static new readonly Size MAX_VIEWPORT_SIZE = new Size(1140.0, 810.0);
        private static readonly Size MIN_VIEWPORT_SIZE = new Size(395, 287.0);

        /// <summary>
        /// The threshold for increasing the size to the next fold size
        /// </summary>
        private static double foldThreshold = 20.0;
        public  static readonly Point foldPeriod = new Point(190, 297);
        private static readonly Point foldMargin = new Point(185, 287);

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperformatTextfieldCustom"/> class with the maximum possible size.
        /// </summary>
        /// <param name="oldTextfieldSize">specified if the old textfield size shell be used</param>
        public PaperformatTextfieldCustom(bool oldTextfieldSize)
            : base(oldTextfieldSize)
        {
            this.ViewportSizeModel = MAX_VIEWPORT_SIZE;
            this.offset = new Size(10.0, 10.0);
        }

        /// <inheritdoc/>
        public override PaperformatTextfield ChangeSize(Size size)
        {
            this.ViewportSizeModel = size;
            IncreaseToNextBiggerFormat();
            Crop();

            if (PaperformatTextfieldA3.MAX_VIEWPORT_SIZE.Contains(size))
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

            width = MAX_VIEWPORT_SIZE.Width < ViewportSizeModel.Width ? MAX_VIEWPORT_SIZE.Width : width;
            width = MIN_VIEWPORT_SIZE.Width > ViewportSizeModel.Width ? MIN_VIEWPORT_SIZE.Width : width;

            height = MAX_VIEWPORT_SIZE.Height < ViewportSizeModel.Height ? MAX_VIEWPORT_SIZE.Height : height;
            height = MIN_VIEWPORT_SIZE.Height > ViewportSizeModel.Height ? MIN_VIEWPORT_SIZE.Height : height;

            this.ViewportSizeModel = new Size(width, height);
        }

        /// <summary>
        /// The model viewport dimensions are increased to the next bigger format in each dimension, if the value is
        /// less than a threshold away from the next bigger formants size.
        /// </summary>
        public void IncreaseToNextBiggerFormat()
        {
            Size result = new Size(ViewportSizeModel.Width, ViewportSizeModel.Height);
            if ((ViewportSizeModel.Width - foldMargin.X) % foldPeriod.X > (foldPeriod.X - foldThreshold))
            {
                result = new Size(((int)(ViewportSizeModel.Width - foldMargin.X) / (int)foldPeriod.X + 1) * foldPeriod.X + foldMargin.X, result.Height);
            }
            if ((ViewportSizeModel.Height - foldMargin.Y) % foldPeriod.Y > (foldPeriod.Y - foldThreshold))
            {
                result = new Size(result.Width, ((int)(ViewportSizeModel.Height - foldMargin.Y) / (int)foldPeriod.Y + 1) * foldPeriod.Y + foldMargin.Y);
            }
            ViewportSizeModel = result;
        }
    }
}
