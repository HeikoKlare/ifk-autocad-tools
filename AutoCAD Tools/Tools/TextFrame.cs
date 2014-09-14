using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class provides functionality to create a border around a textfield by selecting it.
    /// </summary>
    public static class TextFrame
    {
        private static double Margin = 0.003;
        /// <summary>
        /// Executes the command by letting the user select a text and create the border around it.
        /// </summary>
        public static void Execute()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                        
            PromptSelectionOptions opts = new PromptSelectionOptions();
            opts.MessageForAdding = LocalData.TextBorderSelectPrompt;
            opts.SingleOnly = true;

            ObjectId id = ObjectId.Null;

            while (id == ObjectId.Null)
            {
                PromptSelectionResult res = acDoc.Editor.GetSelection(opts);
                if (res.Status != PromptStatus.OK || res.Value.Count < 1)
                {
                    return;
                }
                else
                {
                    using (Transaction trans = acDoc.Database.TransactionManager.StartOpenCloseTransaction())
                    {
                        MText text = trans.GetObject(res.Value[0].ObjectId, OpenMode.ForRead) as MText;
                        if (text != null)
                        {
                            id = res.Value[0].ObjectId;
                        }
                    }
                }
            }

            using (Transaction trans = acDoc.Database.TransactionManager.StartTransaction())
            {
                MText text = trans.GetObject(id, OpenMode.ForWrite) as MText;
               
                double margin = Margin / acDoc.Database.Cannoscale.Scale;

                Point3d startPoint = text.Location + new Vector3d(-margin, margin, 0);
                Point3d endPoint = startPoint + new Vector3d(text.ActualWidth + 2 * margin, -text.ActualHeight - 2 * margin, 0);

                Polyline poly = new Polyline();
                poly.SetDatabaseDefaults();
                poly.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y), 0, 0, 0);
                poly.AddVertexAt(1, new Point2d(startPoint.X, endPoint.Y), 0, 0, 0);
                poly.AddVertexAt(2, new Point2d(endPoint.X, endPoint.Y), 0, 0, 0);
                poly.AddVertexAt(3, new Point2d(endPoint.X, startPoint.Y), 0, 0, 0);
                poly.Closed = true;
                poly.TransformBy(Matrix3d.Rotation(text.Rotation, Vector3d.ZAxis, text.Location));

                BlockTable acBlkTbl = trans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord acBlkTblRec = trans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                acBlkTblRec.AppendEntity(poly);
                trans.AddNewlyCreatedDBObject(poly, true);

                trans.Commit();
            }

        }
    }
}
