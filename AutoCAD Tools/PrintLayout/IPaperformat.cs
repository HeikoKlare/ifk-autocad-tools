
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Defines a paperformat with the viewports and borders extends and insertion point.
    /// The values are defined through a specified viewport in model space.
    /// </summary>
    /// <typeparam name="T">the type that is returned when changing the size, should be a subclass of this</typeparam>
    public abstract class IPaperformat<T>
    {
        /// <summary>
        /// The maximum size of this paperformat.
        /// </summary>
        protected static readonly Size MAX_VIEWPORT_SIZE;

        private Size viewportSizeModel;
        /// <summary>
        /// Gets or sets the size of the viewport in model space.
        /// </summary>
        /// <value>
        /// The size of the viewport in model space.
        /// </value>
        public Size ViewportSizeModel
        {
            get { return viewportSizeModel; }
            protected set { viewportSizeModel = value; }
        }

        /// <summary>
        /// Gets the size of the viewport in layout space.
        /// </summary>
        /// <value>
        /// The size of the viewport in layout space.
        /// </value>
        public abstract Size ViewportSizeLayout
        {
            get;
        }

        /// <summary>
        /// Gets the viewport base point in layout space.
        /// </summary>
        /// <value>
        /// The viewport base point.
        /// </value>
        public abstract Point ViewportBasePoint
        {
            get;
        }

        /// <summary>
        /// Gets the size of the border.
        /// </summary>
        /// <value>
        /// The size of the border.
        /// </value>
        public abstract Size BorderSize
        {
            get;
        }

        /// <summary>
        /// Gets the border base point.
        /// </summary>
        /// <value>
        /// The border base point.
        /// </value>
        public abstract Point BorderBasePoint
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IPaperformat{T}"/> class with the initial size Zero.
        /// </summary>
        public IPaperformat()
        {
            viewportSizeModel = Size.Zero;
        }

        /// <summary>
        /// Changes the size of the viewport according to the specified value. The paperformat that fits the size is returned.
        /// </summary>
        /// <param name="size">The size of the viewport in model space.</param>
        /// <returns>
        /// The paperformat that fits the specified viewport size
        /// </returns>
        public abstract T ChangeSize(Size size);

    }
}
