using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AutoCADTools.Tools
{
    class CompressionWood
    {
        #region Attributes

        private static int WIDTH = 8;
        private static int HEIGHT = 5;

        #endregion

        #region Execution

        public static void Execute()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                // Get width from user
                PromptIntegerOptions intOpts = new PromptIntegerOptions("Breite: ");
                intOpts.UseDefaultValue = true;
                intOpts.DefaultValue = WIDTH;
                var widthResult = acDoc.Editor.GetInteger(intOpts);
                if (widthResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }
                WIDTH = widthResult.Value;

                // Get height from user
                intOpts.Message = "Höhe: ";
                intOpts.DefaultValue = HEIGHT;
                var heightResult = acDoc.Editor.GetInteger(intOpts);
                if (heightResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }
                HEIGHT = heightResult.Value;

                // Get angle from user
                var angleResult = acDoc.Editor.GetAngle("Winkel: ");
                if (angleResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }

                // Get the block or create it
                BlockTable blockTable = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord newBlock = null;
                if (!blockTable.Has("CompressionWood " + WIDTH.ToString() + "-" + HEIGHT.ToString())) {
                    newBlock = new BlockTableRecord();
                    newBlock.Name = "CompressionWood " + WIDTH.ToString() + "-" + HEIGHT.ToString();
                    blockTable.Add(newBlock);
                    acTrans.AddNewlyCreatedDBObject(newBlock, true);
                    var poly = new Polyline();
                    poly.AddVertexAt(0, new Point2d(0, 0), 0 ,0 ,0);
                    poly.AddVertexAt(1, new Point2d(0, HEIGHT * 0.01), 0, 0, 0);
                    poly.AddVertexAt(2, new Point2d(WIDTH * 0.01, HEIGHT * 0.01), 0, 0, 0);
                    poly.AddVertexAt(3, new Point2d(0, 0), 0, 0, 0);
                    poly.AddVertexAt(4, new Point2d(WIDTH * 0.01, 0), 0, 0, 0);
                    poly.AddVertexAt(5, new Point2d(0, HEIGHT * 0.01), 0, 0, 0);
                    poly.AddVertexAt(6, new Point2d(WIDTH * 0.01, HEIGHT * 0.01), 0, 0, 0);
                    poly.AddVertexAt(7, new Point2d(WIDTH * 0.01, 0), 0, 0, 0);
                    newBlock.AppendEntity(poly);
                    acTrans.AddNewlyCreatedDBObject(poly, true);
                } else {
                    newBlock = acTrans.GetObject(blockTable["CompressionWood " + WIDTH + "-" + HEIGHT], OpenMode.ForRead) as BlockTableRecord;
                }

                // Get a block reference and rotate it
                BlockReference blockRef = new BlockReference(new Point3d(0, 0, 0), newBlock.ObjectId);
                blockRef.TransformBy(Matrix3d.Rotation(angleResult.Value, Vector3d.ZAxis, new Point3d(0, 0, 0)));

                // Get the user insertion point input
                BlockJig jig = new BlockJig(blockRef);
                var res = acDoc.Editor.Drag(jig);

                // If everything is okay, fix it
                if (res.Status == PromptStatus.OK)
                {
                    BlockTableRecord modelSpace = acTrans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    modelSpace.AppendEntity(blockRef);
                    acTrans.AddNewlyCreatedDBObject(blockRef, true);
                }
                
                acTrans.Commit();
            }
        }

        #endregion

        #region Jig

        /// <summary>
        /// This is a Jig helper class.
        /// It can be used to place a block reference at some point.
        /// </summary>
        class BlockJig : EntityJig
        {
            // Set variables for points, the phase counter, width, height and scale of the block
            private Point3d insertionPoint;
            
            /// <summary>
            /// Initiates a new BlockJig for the given BlockReference.
            /// </summary>
            /// <param name="br">the BlockReference to be positioned (and sized)</param>
            public BlockJig(BlockReference br) : base(br)
            {
                // Get the document
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                // Set the insertionPoint at a position
                insertionPoint = br.Position;
            }


            /// <summary>
            /// Samples the current jig status.
            /// Therefore the user is asked to input the insertion point and the current mouse
            /// position and user inputs are analyzed to update the BlockReference and see wheter the
            /// input has ended.
            /// </summary>
            /// <param name="prompts">the JigPrompts to use</param>
            /// <returns>the current SamplerStatus, will be NoChange or OK</returns>
            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                if (prompts == null) return SamplerStatus.Cancel;

                // Get the insertionPoint
                PromptPointResult getPointResult = prompts.AcquirePoint("\n" + LocalData.TrussImportInputPoint + ": ");
                Point3d oldPoint0 = insertionPoint;
                insertionPoint = getPointResult.Value;

                // Return NoChange if difference is to low to avoid flimmering
                if (insertionPoint.DistanceTo(oldPoint0) < 0.001)
                {
                    return SamplerStatus.NoChange;
                }

                // Otherwise return OK
                return SamplerStatus.OK;
            }


            /// <summary>
            /// Updates this BlockJig.
            /// Changes the insertionPoint according to the current user input.
            /// </summary>
            /// <returns>true if everything is okay</returns>
            protected override bool Update()
            {
                ((BlockReference)this.Entity).Position = insertionPoint;

                // Return that everything is fine
                return true;
            }

        }

        #endregion
    }
}
