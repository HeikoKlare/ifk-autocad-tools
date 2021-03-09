
using System;
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Specifies a two-dimensional Size.
    /// </summary>
    public class Size
    {
        #region Fields

        private readonly double width;
        /// <summary>
        /// The width of the size instance.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width
        {
            get { return width; }
        }

        private readonly double height;
        /// <summary>
        /// The height of the size instance.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height
        {
            get { return height; }
        }

        private static readonly Size zero = new Size(0, 0);
        /// <summary>
        /// The zero size.
        /// </summary>
        /// <value>
        /// The zero size.
        /// </value>
        public static Size Zero
        {
            get
            {
                return zero;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Size(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> class as a copy of the specified one.
        /// </summary>
        /// <param name="size">The size to copy.</param>
        public Size(Size size)
        {
            this.width = size.width;
            this.height = size.height;
        }

        /// <summary>
        /// Returns a size with x and y size swapped.
        /// </summary>
        /// <returns>a size with x and y value swapped</returns>
        public Size Rotate()
        {
            return new Size(this.height, this.width);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this contains the specified size.
        /// </summary>
        /// <param name="size">The size to check.</param>
        /// <returns><c>true</c> if this size contains the specified one, <c>false</c> otherwise</returns>
        public bool Contains(Size size)
        {
            return size.width <= this.width + 0.001 && size.height <= this.height + 0.001;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator + as the component-wise sum of two sizes.
        /// </summary>
        /// <param name="s1">The first size.</param>
        /// <param name="s2">The seconds size.</param>
        /// <returns>
        /// The component-wise sum of the two sizes.
        /// </returns>
        public static Size operator +(Size s1, Size s2)
        {
            return new Size(s1.width + s2.width, s1.height + s2.height);
        }

        /// <summary>
        /// Implements the operator - as the component-wise difference of two sizes.
        /// </summary>
        /// <param name="s1">The first size.</param>
        /// <param name="s2">The seconds size.</param>
        /// <returns>
        /// The component-wise difference of the two sizes.
        /// </returns>
        public static Size operator -(Size s1, Size s2)
        {
            return new Size(s1.width - s2.width, s1.height - s2.height);
        }

        /// <summary>
        /// Implements the operator * as the component-wise scaled size by the specified scalar.
        /// </summary>
        /// <param name="scalar">The scalar to multiply the size with.</param>
        /// <param name="size">The size to scale.</param>
        /// <returns>
        /// The component-wise scaled Size.
        /// </returns>
        public static Size operator *(double scalar, Size size) {
            return new Size(size.width * scalar, size.height * scalar);
        }

        /// <summary>
        /// Implements the operator ==. Is true if sizes are component-wise equal.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>
        /// <c>true</c> if both sizes are component wise equal, <c>false</c> otherweise.
        /// </returns>
        public static bool operator ==(Size left, Size right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=. Is true if sizes are unequal in at least one component.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>
        /// <c>true</c> if both sizes are unequal in at least one component, <c>false</c> otherweise.
        /// </returns>
        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }

        #endregion

        #region Standard Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// Two sizes are equal if they are equal in all components.
        /// </summary>
        /// <param name="other">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object other)
        {
            Size otherSize = other as Size;
            if (otherSize == null)
            {
                return false;
            }
            return otherSize.width == width && otherSize.height == height;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance in the format: WIDTH, HEIGHT.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return width + ", " + height;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (int)(this.width * (int)this.width * this.height);
        }

        #endregion

    }
}
