using AutoCADTools.Tools;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This class represent a specification for the layout creation.
    /// </summary>
    public class LayoutCreationSpecification : INotifyPropertyChanged
    {
        #region Enums

        /// <summary>
        /// Specifies an orientation of paper.
        /// </summary>
        public enum PaperOrientation
        {
            /// <summary>
            /// Portrait format.
            /// </summary>
            /// 
            Portrait,
            /// <summary>
            /// Landscape format.
            /// </summary>
            Landscape
        }

        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        // From: https://docs.microsoft.com/en-us/dotnet/desktop/winforms/how-to-implement-the-inotifypropertychanged-interface?view=netframeworkdesktop-4.8&redirectedfrom=MSDN
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly Document document;
        /// <summary>
        /// The document the layout is created in.
        /// </summary>
        public Document Document => document;

        private string layoutName;
        /// <summary>
        /// The name of the layout to create.
        /// </summary>
        public string LayoutName
        {
            get => layoutName;
            set
            {
                if (layoutName != value)
                {
                    layoutName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal drawingUnit;
        /// <summary>
        /// The drawing unit (millimeters represented by a unit in the drawing).
        /// </summary>
        public decimal DrawingUnit
        {
            get => drawingUnit;
            set
            {
                if (drawingUnit != value)
                {
                    drawingUnit = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double scale;
        /// <summary>
        /// The scale factor the extract shell be printed with.
        /// </summary>
        public double Scale
        {
            get => scale;
            set {
                if (scale != value)
                {
                    scale = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool useTextfield;
        /// <summary>
        /// Whether the layout uses a textfield or not. Can only be set to true if the document <see cref="CanUseTextfield"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">if set to true but no textfield is present</exception>
        public bool UseTextfield
        {
            get => useTextfield;
            set
            {
                if (!CanUseTextfield)
                {
                    throw new InvalidOperationException("cannot use textfield as it is not present in the document");
                }
                if (useTextfield != value)
                {
                    useTextfield = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Returns whether the necessary data to use a textfield is present in the <see cref="Document"/>.
        /// </summary>
        public bool CanUseTextfield
        {
            get
            {
                using (var transaction = document.Database.TransactionManager.StartOpenCloseTransaction())
                {
                    var blockTable = transaction.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    return blockTable.Has(PaperformatTextfieldA4.TEXTFIELD_BLOCK_NAME) && blockTable.Has(PaperformatTextfieldFullTextfield.TEXTFIELD_BLOCK_NAME);
                }
            }
        }

        /// <summary>
        /// Whether the drawing uses an old version of the textfield.
        /// </summary>
        private bool UsesOldTextfield
        {
            get
            {
                var drawingData = document.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
                return drawingData?.Version < 2;
            }
        }

        /// <summary>
        /// The calculated paperformat for the defined viewport size. Is <code>null</code> if <see cref="Size"/> is not set.
        /// </summary>
        public Paperformat Paperformat
        {
            get
            {
                if (DrawingArea.Size == null)
                {
                    return null;
                }
                Size viewportSize = Scale * (double)DrawingUnit * DrawingArea.Size;
                if (UseTextfield)
                {
                    return PaperformatFactory.GetPaperformatTextfield(viewportSize, UsesOldTextfield);
                }
                else
                {
                    return PaperformatFactory.GetPlainPaperformat(viewportSize);
                }
            }
        }

        private PrinterPaperformat printerformat;
        /// <summary>
        /// The printer paperformat used for printing.
        /// </summary>
        public PrinterPaperformat Printerformat
        {
            get => printerformat;
            set => printerformat = value;
        }

        /// <summary>
        /// Whether the viewport has to be rotated.
        /// </summary>
        public bool RotateViewport => Paperformat != null && Paperformat is PaperformatTextfieldA4Horizontal;

        /// <summary>
        /// The orientation for the paper.
        /// </summary>
        public PaperOrientation Orientation => RotateViewport || Paperformat is PaperformatA4Vertical || Paperformat is PaperformatTextfieldA4Vertical ? PaperOrientation.Portrait : PaperOrientation.Landscape;

        /// <summary>
        /// Whether there is a predefined drawing area and the required textfield is available (<see cref="CanUseTextfield"/>) that can be loaded by calling <see cref="LoadDataForPredefinedDrawingArea"/>.
        /// </summary>
        public bool HasPredefinedDrawingArea => (document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper).DrawingArea.IsValid && CanUseTextfield;

        /// <summary>
        /// Defines a frame by one point and its size.
        /// </summary>
        public class Frame
        {
            /// <summary>
            /// The point at the lower right edge of the frame.
            /// </summary>
            public Point LowerRightPoint { get; set; }

            /// <summary>
            /// The size of the frame.
            /// </summary>
            public Size Size { get; set; }
        }

        /// <summary>
        /// The area in the model to be printed.
        /// </summary>
        public Frame DrawingArea { get; }

        /// <summary>
        /// Whether the layout is valid, i.e., all properties have been set.
        /// </summary>
        public bool IsValid => DrawingUnit != 0 &&
                    Scale != 0 &&
                    DrawingArea.LowerRightPoint != null &&
                    Paperformat != null &&
                    Printerformat != null &&
                    !String.IsNullOrEmpty(LayoutName);

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutCreationSpecification"/> class with the paperformat <see cref="PaperformatTextfieldA4Vertical"/>
        /// and the default layout name <see cref="Properties.Settings.Default"/>.
        /// </summary>
        public LayoutCreationSpecification()
        {
            document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            LayoutName = Properties.Settings.Default.DefaultLayoutName;
            DrawingArea = new Frame();
            Scale = 1.0;
            DrawingUnit = 1;
        }

        #endregion

        #region Functionality

        /// <summary>
        /// Loads the data from the predefined drawing area. This affects <see cref="DrawingUnit"/>, <see cref="Scale"/> and <see cref="DrawingArea"/>.
        /// Ensure before calling that there is a predefined drawing area by calling <see cref="HasPredefinedDrawingArea"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">is thrown if there is not predefined drawing area</exception>
        public void LoadDataForPredefinedDrawingArea()
        {
            if (!HasPredefinedDrawingArea)
            {
                throw new InvalidOperationException("drawing area can only be loaded if there is a drawing area");
            }
            DrawingArea drawingArea = (document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper).DrawingArea;

            var drawingData = document.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            DrawingUnit = drawingData.DrawingUnit;
            DrawingArea.Size = 1 / drawingArea.Scale * drawingArea.Format.ViewportSizeModel;
            using (Transaction trans = document.TransactionManager.StartTransaction())
            {
                var point = (drawingArea.DrawingAreaId.GetObject(OpenMode.ForRead) as BlockReference).Position;
                DrawingArea.LowerRightPoint = new Point(point.X, point.Y);
            }
            Scale = drawingArea.Scale / (double)DrawingUnit;
            UseTextfield = true;
        }

        #endregion

    }
}
