using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
[assembly: PerDocumentClass(typeof(AutoCADTools.PrintLayout.DrawingAreaDocumentWrapper))]
 

namespace AutoCADTools.PrintLayout
{
    public class DrawingAreaDocumentWrapper
    {
        public static string DICTIONARY_NAME = "DrawingAreaWrapper";
        private DrawingArea drawingArea;

        public DrawingArea DrawingArea
        {
            get { return drawingArea; }
        }

        public DrawingAreaDocumentWrapper(Document doc)
        {
            string saveLayout = LayoutManager.Current.CurrentLayout;
            LayoutManager.Current.CurrentLayout = "Model";
            try
            {
                this.drawingArea = DrawingArea.FindDrawingArea(doc);
            }
            catch (System.Exception)
            {
                System.Windows.Forms.MessageBox.Show("Error while finding the drawing area.");
            }
            doc.UserData.Add(DICTIONARY_NAME, this);
            LayoutManager.Current.CurrentLayout = saveLayout;
        }

        public bool Create(SpecificFormat format) {
            var createdArea = drawingArea.Create(format);
            if (createdArea != null)
            {
                this.drawingArea = createdArea;
            }
            return createdArea != null;
        }

        public bool Resize(bool oldTextfieldUsed = false) {
            if (!this.drawingArea.IsValid) this.drawingArea = DrawingArea.FindDrawingArea(drawingArea.Document);
            return this.drawingArea.Resize(oldTextfieldUsed);
        }

        public bool CreateAndResize(SpecificFormat format)
        {
            using (var trans = drawingArea.Document.TransactionManager.StartTransaction())
            {
                var createdArea = drawingArea.Create(format);
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

        public static DrawingAreaDocumentWrapper Create(Document doc)
        {
            var pdd = new DrawingAreaDocumentWrapper(doc);
            
            return pdd;

        }
    
    }
}
