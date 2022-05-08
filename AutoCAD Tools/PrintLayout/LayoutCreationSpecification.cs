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
        public Document Document
        {
            get { return document; }
        }

        private string layoutName;
        /// <summary>
        /// The name of the layout to create.
        /// </summary>
        public string LayoutName
        {
            get { return layoutName; }
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
            get { return drawingUnit; }
            set {
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
            get { return scale; }
            set { scale = value; }
        }

        private Paperformat paperformat;
        /// <summary>
        /// The paperformat for the layout.
        /// </summary>
        public Paperformat Paperformat
        {
            get { return paperformat; }
            set { paperformat = value; }
        }

        private PrinterPaperformat printerformat;
        /// <summary>
        /// The printer paperformat used for printing.
        /// </summary>
        public PrinterPaperformat Printerformat
        {
            get { return printerformat; }
            set { printerformat = value; }
        }

        /// <summary>
        /// Whether the viewport has to be rotated.
        /// </summary>
        public bool RotateViewport
        {
            get { return paperformat is PaperformatTextfieldA4Horizontal; }
        }

        /// <summary>
        /// The orientation for the paper.
        /// </summary>
        public PaperOrientation Orientation
        {
            get { return RotateViewport || Paperformat is PaperformatA4Vertical || Paperformat is PaperformatTextfieldA4Vertical ? PaperOrientation.Portrait : PaperOrientation.Landscape; }
        }

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
        public Frame PrintArea { get; }

        /// <summary>
        /// Whether the layout is valid, i.e., all properties have been set.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return DrawingUnit != 0 &&
                    Scale != 0 &&
                    PrintArea.LowerRightPoint != null &&
                    Paperformat != null &&
                    Printerformat != null &&
                    !String.IsNullOrEmpty(LayoutName);
            }
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutCreationSpecification"/> class with the paperformat <see cref="PaperformatTextfieldA4Vertical"/>
        /// and the default layout name <see cref="Properties.Settings.Default"/>.
        /// </summary>
        public LayoutCreationSpecification()
        {
            document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingData = document.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            paperformat = new PaperformatTextfieldA4Vertical(drawingData.Version < 2);
            PrintArea = new Frame();
            layoutName = Properties.Settings.Default.DefaultLayoutName;
            scale = 1.0;
            drawingUnit = 1;
        }

        #endregion

    }
}
