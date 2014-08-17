using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
[assembly: PerDocumentClass(typeof(AutoCADTools.PrintLayout.DrawingAreaDocumentWrapper))]
 

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This is a per-document wrapper for the drawing area management. One instance is created per opened document, giving access to the drawing areas
    /// </summary>
    public class DrawingAreaDocumentWrapper
    {
        /// <summary>
        /// The per-document dictionary name the wrapper can be found at
        /// </summary>
        public static string DICTIONARY_NAME = "DrawingAreaWrapper";
        private DrawingArea drawingArea;

        /// <summary>
        /// The drawing area that is managed by this this wrapper
        /// </summary>
        public DrawingArea DrawingArea
        {
            get { return drawingArea; }
        }

        /// <summary>
        /// Instanciates a new wrapper for the specified document. An existing drawing area is searched and converted if it is to old.
        /// </summary>
        /// <param name="doc">the document to create the wrapper for</param>
        public DrawingAreaDocumentWrapper(Document doc)
        {
            string saveLayout = LayoutManager.Current.CurrentLayout;
            LayoutManager.Current.CurrentLayout = "Model";
            try
            {
                this.drawingArea = DrawingArea.FindDrawingArea(doc, 0);
            }
            catch (System.Exception)
            {
                MessageBox.Show(LocalData.DrawingAreaFindErrorMessage, LocalData.DrawingAreaFindErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            doc.UserData.Add(DICTIONARY_NAME, this);
            LayoutManager.Current.CurrentLayout = saveLayout;
        }

        /// <summary>
        /// Creates a new drawing area with the specified format in the document this wrapper belongs to. The insertion point is specified by used input.
        /// </summary>
        /// <param name="format">the format the new drawing area should have</param>
        /// <returns>true if the drawing area was successfully created, false otherwise</returns>
        public bool Create(SpecificFormat format) {
            var createdArea = drawingArea.Create(format, 0);
            if (createdArea != null)
            {
                this.drawingArea = createdArea;
            }
            return createdArea != null;
        }

        /// <summary>
        /// Resizes the drawing area in the document this wrapper belongs to. The resizing is done through used input. If there is no drawing area, nothing is done.
        /// </summary>
        /// <param name="oldTextfieldUsed">specified if the old textfield size shell be used for ne resized drawing area</param>
        /// <returns>true if the drawing area was resized, false otherwise</returns>
        public bool Resize(bool oldTextfieldUsed = false) {
            if (!this.drawingArea.IsValid) this.drawingArea = DrawingArea.FindDrawingArea(drawingArea.Document, 0);
            return this.drawingArea.Resize(oldTextfieldUsed);
        }

        /// <summary>
        /// Creates a new drawing area in the document this wrapper belongs to. The given format specifies the initial format that is shown while selecting the insertion point, 
        /// specified through used input. Afterwards the initial frame is resized through used input.
        /// </summary>
        /// <param name="format">the format the new drawing area should have initially</param>
        /// <returns>true if the drawing area was successfully created, false otherwise</returns>
        public bool CreateAndResize(SpecificFormat format)
        {
            using (var trans = drawingArea.Document.TransactionManager.StartTransaction())
            {
                var createdArea = drawingArea.Create(format, 0);
                if (createdArea != null && createdArea.Resize(format.OldTextfieldUsed))
                {
                    this.drawingArea = createdArea;
                    trans.Commit();
                    return true;
                }
                else
                {
                    trans.Abort();
                }
            }
            return false;
        }

        /// <summary>
        /// Creates a new document wrapper for the specified document.
        /// </summary>
        /// <param name="doc">the document the wrapper shell be created for</param>
        /// <returns>the created document wrapper</returns>
        public static DrawingAreaDocumentWrapper Create(Document doc)
        {
            var dadw = new DrawingAreaDocumentWrapper(doc);
            return dadw;
        }
    
    }
}
