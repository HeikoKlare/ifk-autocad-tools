﻿
namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Defines a paperformat with the viewports and borders extends and insertion point.
    /// The values are defined through a specified viewport in model space.
    /// </summary>
    public abstract class Paperformat
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
        /// Gets the default printer for this paperformat.
        /// </summary>
        /// <returns>The default printer for this paperformat or <c>null</c> if none is found.</returns>
        public abstract Printer GetDefaultPrinter();

        /// <summary>
        /// Gets the fitting paperformat for the specified printer.
        /// </summary>
        /// <param name="printer">The printer to get the paperformat for.</param>
        /// <param name="optimizedPaperformats">if set to <c>true</c> only optimized paperformats are used.</param>
        /// <returns>The paperformat for the specified printer fitting this paperformat or <c>null</c> if none is found.</returns>
        public abstract PrinterPaperformat GetFittingPaperformat(Printer printer, bool optimizedPaperformats);

        /// <summary>
        /// Initializes a new instance of the <see cref="Paperformat"/> class with the initial size Zero.
        /// </summary>
        public Paperformat()
        {
            viewportSizeModel = Size.Zero;
        }
    }
}
