using System;

namespace AutoCADTools.PrintLayout.Deprecated
{
    #region Enums and Structs

    /// <summary>
    /// Defines the used paperformat
    /// </summary>
    public enum Paperformat
    {
        /// <summary>
        /// A4 Paperformat
        /// </summary>
        A4,

        /// <summary>
        /// A3 Paperformat
        /// </summary>
        A3,

        /// <summary>
        /// Maximum possible Paperformat
        /// </summary>
        AMAX
    }

    /// <summary>
    /// Defines the used orientation
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// Horizontally oriented paper (width bigger height)
        /// </summary>
        HORIZONTAL,

        /// <summary>
        /// Vertically oriented paper (height bigger width)
        /// </summary>
        VERTICAL
    }

/*    /// <summary>
    /// A 2D Point
    /// </summary>
    public struct Point {
        private readonly double x;
        /// <summary>
        /// The X value of the point
        /// </summary>
        public double X {get { return x; }}

        private double y;
        /// <summary>
        /// The Y value of the point
        /// </summary>
        public double Y {get { return y; }}

        /// <summary>
        /// Instantiates a new point with specified x and y coordinate
        /// </summary>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        public Point(double x, double y) {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Swaps x and y coordinate and returns a new point with those values
        /// </summary>
        /// <returns>a point with the x and y value swapped</returns>
        public Point Swap() {
            return new Point(y, x);
        }
    }*/

    #endregion

    #region General Format

    /// <summary>
    /// This call specifies a format with the size of viewport in model, layout space, the borders and the insertion points for viewport and borders
    /// </summary>
    public class Format
    {
        private Point viewportModel;

        /// <summary>
        /// The size of the viewport in model space
        /// </summary>
        public virtual Point ViewportModel
        {
            get { return viewportModel; }
            internal set { viewportModel = value; }
        }
        
        private Point viewportLayout;
        /// <summary>
        /// The size of the viewport in layout space
        /// </summary>
        public virtual Point ViewportLayout
        {
            get { return viewportLayout; }
            internal set { viewportLayout = value; }
        }

        private Point border;
        /// <summary>
        /// The size of the border
        /// </summary>
        public virtual Point Border
        {
            get { return border; }
            set { border = value; }
        }

        private Point insertionViewport;
        /// <summary>
        /// The insertion point of the viewport in layout space
        /// </summary>
        public virtual Point InsertionViewport
        {
            get { return insertionViewport; }
            set { insertionViewport = value; }
        }

        private Point insertionBorder;
        /// <summary>
        /// The insertion point of the border in layout space
        /// </summary>
        public virtual Point InsertionBorder
        {
            get { return insertionBorder; }
            set { insertionBorder = value; }
        }

    }

    #endregion

    #region Specific Format

    /// <summary>
    /// This class provides special predefined formats and further functionality.
    /// Data for these formats are returned depending on the state, like the formats orientation.
    /// Furthermore proper viewports and viewports that fit good can be determined depending on given width and height of a viewport.
    /// </summary>
    public class SpecificFormat : Format
    {
        #region Predefined Formats and Constants

        private static readonly Format[] formats;
        
        static SpecificFormat()
        {
            formats = new Format[(int)Paperformat.AMAX+1];
            formats[(int)Paperformat.A4] = new Format()
            {
                ViewportModel = new Point(250.5, 171.37),
                ViewportLayout = new Point(250.0, 171.028),
                Border = new Point(281.0, 175.0),
                InsertionViewport = new Point(11.0, 26.986),
                InsertionBorder = new Point(9.0, 25.0)
            };
            formats[(int)Paperformat.A3] = new Format()
            {
                ViewportModel = new Point(395.0, 287.0),
                ViewportLayout = new Point(395.0, 287.0),
                Border = new Point(420.0, 297.0),
                InsertionViewport = new Point(20.0, 5.0),
                InsertionBorder = new Point(0.0, 0.0)
            };
            formats[(int)Paperformat.AMAX] = new Format()
            {
                ViewportModel = new Point(1160.0, 825.0),
                ViewportLayout = new Point(1160.0, 825.0),
                Border = new Point(1185.0, 835.0),
                InsertionViewport = new Point(30.0, 15.0),
                InsertionBorder = new Point(10.0, 10.0)
            };
        }

        private static readonly Point foldPeriod = new Point(190, 297);
        private static readonly Point foldMargin = new Point(185, 287);
        private static readonly double fangThreshold = 20.0;
        private static readonly Point textfieldSize = new Point(185.0, 57.5);
        private static readonly Point textfieldSizeOld = new Point(185.0, 77.0);
        
        #endregion
                        
        #region Members

        private Orientation orientation;
        /// <summary>
        /// The current orientation (horizontal/vertical)
        /// </summary>
        public Orientation Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        private Paperformat format;
        /// <summary>
        /// The format (A4/A3/..)
        /// </summary>
        public Paperformat Format
        {
            get { return format; }
            set { format = value; }
        }

        private bool withBorder;
        /// <summary>
        /// Defines if a border shell be used (viewport smaller than paper)
        /// </summary>
        public bool WithBorder
        {
            get { return withBorder; }
            set { withBorder = value; }
        }

        /// <summary>
        /// The viewport size in model space, depending on current format and orientation
        /// </summary>
        public override Point ViewportModel
        {
            get { return ApplyOrientation(formats[(int)Format].ViewportModel); }
        }

        /// <summary>
        /// The viewport size in layout space, depending on current format and orientation
        /// </summary>
        public override Point ViewportLayout
        {
            get { return ApplyOrientation(formats[(int)Format].ViewportLayout); }
        }

        /// <summary>
        /// The border size depending on current format and orientation
        /// </summary>
        public override Point Border
        {
            get { return ApplyOrientation(formats[(int)Format].Border); }
        }

        /// <summary>
        /// The insertion point for the viewport in layout space depending on current format and orientation
        /// </summary>
        public override Point InsertionViewport
        {
            get { return ApplyOrientation(formats[(int)Format].InsertionViewport); }
        }

        /// <summary>
        /// The insertion point of the border in layout space depending on the current format and orientation
        /// </summary>
        public override Point InsertionBorder
        {
            get { return ApplyOrientation(formats[(int)Format].InsertionBorder); }
        }

        private bool oldTextfieldUsed;
        /// <summary>
        /// Specifies if the old (bigger) textfield size is used.
        /// </summary>
        public bool OldTextfieldUsed
        {
            get { return oldTextfieldUsed; }
            set { oldTextfieldUsed = value; }
        }

        /// <summary>
        /// Returns the textfield size depending on wheter the old, bigger one shell be used or not
        /// </summary>
        public Point TextfieldSize
        {
            get { return oldTextfieldUsed ? textfieldSizeOld : textfieldSize; }
        }

        /// <summary>
        /// Helper method that swaps x and y value of a point if orientation is not horizontal
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Point ApplyOrientation(Point p)
        {
            if (Orientation == Orientation.VERTICAL) p = p.Rotate();
            return p;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a format depending on the given parameters.
        /// </summary>
        /// <param name="format">the paper format to be used</param>
        /// <param name="orient">the orientation to be used</param>
        /// <param name="withBorder">specifies if a border shell be printed or not (viewport size unequal to paper size)</param>
        /// <param name="oldTextfieldUsed">Specifies if the old, bigger text field shell be used</param>
        public SpecificFormat(Paperformat format = Paperformat.AMAX, Orientation orient = Orientation.HORIZONTAL, bool withBorder = true, bool oldTextfieldUsed = false)
        {
            this.format = format;
            this.orientation = orient;
            this.withBorder = withBorder;
            this.oldTextfieldUsed = oldTextfieldUsed;
        }

        #endregion

        #region ViewportSizeApproximation

        /// <summary>
        /// Returns a proper viewport with paper size and orientation depeding on the given width and height.
        /// Values must almost equal the values of one of the specified formats. If no format fits, horizontal AMAX is returned.
        /// </summary>
        /// <param name="width">the width of the viewport</param>
        /// <param name="height">the height of hte viewport</param>
        /// <returns>a format definition that fits the specified viewport dimensions</returns>
        public static SpecificFormat GetProperViewportFormat(double width, double height)
        {
            SpecificFormat result = new SpecificFormat();
            // Case A4 vertical
            if (Math.Abs(width - formats[(int)Paperformat.A4].ViewportModel.Y) < 0.01
                && Math.Abs(height - formats[(int)Paperformat.A4].ViewportModel.X) < 0.01)
            {
                result.format = Paperformat.A4;
                result.orientation = Orientation.VERTICAL;
            }
            // Case A4 horizontal
            else if (Math.Abs(width - formats[(int)Paperformat.A4].ViewportModel.X) < 0.01
                && Math.Abs(height - formats[(int)Paperformat.A4].ViewportModel.Y) < 0.01)
            {
                result.format = Paperformat.A4;
                result.orientation = Orientation.HORIZONTAL;
            }
            // Case A3
            else if (Math.Abs(width - formats[(int)Paperformat.A3].ViewportModel.X) < 0.01
                && Math.Abs(height - formats[(int)Paperformat.A3].ViewportModel.Y) < 0.01)
            {
                result.format = Paperformat.A3;
                result.orientation = Orientation.HORIZONTAL;
            }
            // Case bigger than A3
            else
            {
                result.format = Paperformat.AMAX;
                result.orientation = Orientation.HORIZONTAL;
            }

            return result;
        }

        /// <summary>
        /// Returns the paper format that is the next bigger one of the specified format, according to the given width and height of the viewport.
        /// Horizontal A4 is prefered before vertical A4 if both fit the input.
        /// </summary>
        /// <param name="width">the width of the viewport</param>
        /// <param name="height">the height of the viewpoert</param>
        /// <returns>the format of the next bigger format that fits the specified viewport</returns>
        public SpecificFormat GetNextBiggerFormat(double width, double height)
        {
            SpecificFormat result = new SpecificFormat(Paperformat.AMAX, Orientation.HORIZONTAL, true);
            if (width <= formats[(int)Paperformat.A4].ViewportModel.X
                && height <= formats[(int)Paperformat.A4].ViewportModel.Y)
            {
                result.Format = Paperformat.A4;
            }
            else if (width <= formats[(int)Paperformat.A4].ViewportModel.Y
                && height <= formats[(int)Paperformat.A4].ViewportModel.X)
            {
                result.Format = Paperformat.A4;
                result.Orientation = Orientation.VERTICAL;
            }
            else if (width <= formats[(int)Paperformat.A3].ViewportModel.X
                && height <= formats[(int)Paperformat.A3].ViewportModel.Y)
            {
                result.Format = Paperformat.A3;
            }
            result.oldTextfieldUsed = oldTextfieldUsed;

            return result;
        }

        /// <summary>
        /// The given viewport dimensions are increased to the next bigger format in each dimension, if the value is
        /// less than a threshold away from the next bigger formants size.
        /// </summary>
        /// <param name="width">the width of the viewport</param>
        /// <param name="height">the height of the viewport</param>
        /// <returns>the size of the viewport, when increasing the given dimensions to the next bigger format if lower than a threshold away</returns>
        public static Point IncreaseToNextBiggerFormat(double width, double height)
        {
            Point result = new Point(width, height);
            if ((width - foldMargin.X) % foldPeriod.X > (foldPeriod.X - fangThreshold))
            {
                result = new Point(((int)(width - foldMargin.X) / (int)foldPeriod.X + 1) * foldPeriod.X + foldMargin.X, result.Y); 
            }
            if ((height - foldMargin.Y) % foldPeriod.Y > (foldPeriod.Y - fangThreshold)) {
                result = new Point(result.X, ((int)(height - foldMargin.Y) / (int)foldPeriod.Y + 1) * foldPeriod.Y + foldMargin.Y);
            }
            return result;
        }

        #endregion

    }

    #endregion

}
