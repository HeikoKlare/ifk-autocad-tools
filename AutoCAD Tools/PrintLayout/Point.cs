
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Specifies a two-dimensional point.
    /// </summary>
    public class Point
    {
        #region Fields

        private double x;
        /// <summary>
        /// The x value of the point.
        /// </summary>
        /// <value>
        /// The x-value of the point.
        /// </value>
        public double X
        {
            get { return x; }
        }

        private double y;
        /// <summary>
        /// The y value of the point.
        /// </summary>
        /// <value>
        /// The y-value of the point.
        /// </value>
        public double Y
        {
            get { return y; }
        }

        private static Point origin = new Point(0, 0);
        /// <summary>
        /// The origin (0, 0).
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public static Point Origin
        {
            get
            {
                return origin;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class with the specified coordinates
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class as a copy of the specified one.
        /// </summary>
        /// <param name="point">The point to copy</param>
        public Point(Point point)
        {
            this.x = point.x;
            this.y = point.y;
        }

        /// <summary>
        /// Returns a point with x- and y-value swapped.
        /// </summary>
        /// <returns>
        /// A point with x- and y-value swapped.
        /// </returns>
        public Point Rotate()
        {
            return new Point(this.y, this.x);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Returns a point with the specified size component-wise added.
        /// </summary>
        /// <param name="point">The point to add the size to.</param>
        /// <param name="size">The size to add to the point.</param>
        /// <returns>
        /// A point added with the specified size.
        /// </returns>
        public static Point operator +(Point point, Size size)
        {
            return new Point(point.x + size.Width, point.y + size.Height);
        }

        /// <summary>
        /// Returns a point with the specified size component-wise subtracted.
        /// </summary>
        /// <param name="point">The point to subtract the size from.</param>
        /// <param name="size">The size to subtract from the point.</param>
        /// <returns>
        /// A point subtracted with the specified size.
        /// </returns>
        public static Point operator -(Point point, Size size)
        {
            return new Point(point.x - size.Width, point.y - size.Height);
        }

        /// <summary>
        /// Returns the size as the difference between the specified points.
        /// </summary>
        /// <param name="point1">The point to subtract the other one from.</param>
        /// <param name="point2">The point to subtract from the first one.</param>
        /// <returns>
        /// The size representing the difference between the specified points.
        /// </returns>
        public static Size operator -(Point point1, Point point2)
        {
            return new Point(point1.x - point2.x, point1.y - point2.y);
        }

        #endregion

    }
}
