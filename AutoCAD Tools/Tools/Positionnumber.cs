using AutoCADTools.Utils;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class provides functionality to create position number with a circle around the position.
    /// </summary>
    public static class Positionnumber
    {
        private static string DefaultPosition = "1";

        /// <summary>
        /// Executes the command by getting user input to define the position.
        /// </summary>
        public static void Execute()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                PromptStringOptions numberOpts = new PromptStringOptions(Environment.NewLine + LocalData.PositionnumberPrompt);
                numberOpts.UseDefaultValue = true;
                numberOpts.DefaultValue = DefaultPosition;
                PromptResult positionResult = acDoc.Editor.GetString(numberOpts);
                if (positionResult.Status != PromptStatus.OK || String.IsNullOrEmpty(positionResult.StringResult))
                {
                    acTrans.Abort();
                    return;
                }

                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                MText text = new MText();
                text.Annotative = AnnotativeStates.True;
                text.Attachment = AttachmentPoint.MiddleCenter;
                text.Contents = positionResult.StringResult;
                text.Location = Point3d.Origin;
                acBlkTblRec.AppendEntity(text);

                double radius = Math.Max(text.ActualHeight, text.ActualWidth) * 0.5 + text.ActualHeight * 0.45;
                Circle circle = new Circle(Point3d.Origin, new Vector3d(0, 0, 1), radius);
                acBlkTblRec.AppendEntity(circle);

                List<Entity> entities = new List<Entity>();
                entities.Add(text);
                entities.Add(circle);
                EntitiesJig jig = new EntitiesJig(entities, Point3d.Origin, LocalData.InsertionPoint);

                PromptResult dragResult = acDoc.Editor.Drag(jig);
                if (dragResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }

                DefaultPosition = positionResult.StringResult;

                acTrans.AddNewlyCreatedDBObject(circle, true);
                acTrans.AddNewlyCreatedDBObject(text, true);
                acTrans.Commit();
                circle.Dispose();
                text.Dispose();
            }
        }

        
    }
}
