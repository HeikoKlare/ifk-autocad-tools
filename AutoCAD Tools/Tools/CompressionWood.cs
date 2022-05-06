using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;

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
                PromptIntegerOptions intOpts = new PromptIntegerOptions(Environment.NewLine + LocalData.CompressionWoodWidth)
                {
                    UseDefaultValue = true,
                    DefaultValue = WIDTH
                };
                var widthResult = acDoc.Editor.GetInteger(intOpts);
                if (widthResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }
                WIDTH = widthResult.Value;

                // Get height from user
                intOpts.Message = Environment.NewLine + LocalData.CompressionWoodHeight;
                intOpts.DefaultValue = HEIGHT;
                var heightResult = acDoc.Editor.GetInteger(intOpts);
                if (heightResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }
                HEIGHT = heightResult.Value;

                // Get angle from user
                PromptAngleOptions angleOpts = new PromptAngleOptions(Environment.NewLine + LocalData.CompressionWoodAngle)
                {
                    UseDefaultValue = true,
                    DefaultValue = 0.0
                };
                var angleResult = acDoc.Editor.GetAngle(angleOpts);
                if (angleResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }

                // Get the block or create it
                BlockTable blockTable = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForWrite) as BlockTable;

                var poly = new Polyline();
                poly.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                poly.AddVertexAt(1, new Point2d(0, HEIGHT * 0.01), 0, 0, 0);
                poly.AddVertexAt(2, new Point2d(WIDTH * 0.01, HEIGHT * 0.01), 0, 0, 0);
                poly.AddVertexAt(3, new Point2d(0, 0), 0, 0, 0);
                poly.AddVertexAt(4, new Point2d(WIDTH * 0.01, 0), 0, 0, 0);
                poly.AddVertexAt(5, new Point2d(0, HEIGHT * 0.01), 0, 0, 0);
                poly.AddVertexAt(6, new Point2d(WIDTH * 0.01, HEIGHT * 0.01), 0, 0, 0);
                poly.AddVertexAt(7, new Point2d(WIDTH * 0.01, 0), 0, 0, 0);
                poly.TransformBy(Matrix3d.Rotation(angleResult.Value, Vector3d.ZAxis, new Point3d(0, 0, 0)));

                // Get the user insertion point input
                PolylineJig jig = new PolylineJig(poly);
                var res = acDoc.Editor.Drag(jig);

                // If everything is okay, fix it
                if (res.Status == PromptStatus.OK)
                {
                    BlockTableRecord modelSpace = acTrans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    modelSpace.AppendEntity(poly);
                    acTrans.AddNewlyCreatedDBObject(poly, true);
                    acTrans.Commit();
                }
                else
                {
                    acTrans.Abort();
                }
            }
        }

        #endregion

        #region Jig

        /// <summary>
        /// This is a Jig helper class.
        /// It can be used to place a polyline at some point.
        /// </summary>
        class PolylineJig : EntityJig
        {
            // Set variables for points, the phase counter, width, height and scale of the block
            private Point3d insertionPoint;
            private Point3d oldPoint;

            /// <summary>
            /// Initiates a new PolylineJig for the given Polyline.
            /// </summary>
            /// <param name="poly">the Polyline to be positioned (and sized)</param>
            public PolylineJig(Polyline poly) : base(poly)
            {
                // Set the insertionPoint at a position
                insertionPoint = poly.StartPoint;
                oldPoint = poly.StartPoint;
            }


            /// <summary>
            /// Samples the current jig status.
            /// Therefore the user is asked to input the insertion point and the current mouse
            /// position and user inputs are analyzed to update the Polyline and see wheter the
            /// input has ended.
            /// </summary>
            /// <param name="prompts">the JigPrompts to use</param>
            /// <returns>the current SamplerStatus, will be NoChange or OK</returns>
            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                if (prompts == null) return SamplerStatus.Cancel;

                // Get the insertionPoint
                PromptPointResult getPointResult = prompts.AcquirePoint(Environment.NewLine + LocalData.TrussImportInputPoint);
                Point3d oldPoint0 = insertionPoint;
                Point3d currentPoint = getPointResult.Value;

                // Return NoChange if difference is to low to avoid flimmering
                if (currentPoint.DistanceTo(oldPoint0) < 0.001)
                {
                    return SamplerStatus.NoChange;
                }

                insertionPoint = currentPoint;

                // Otherwise return OK
                return SamplerStatus.OK;
            }


            /// <summary>
            /// Updates this PolylineJig.
            /// Changes the insertionPoint according to the current user input.
            /// </summary>
            /// <returns>true if everything is okay</returns>
            protected override bool Update()
            {
                if (insertionPoint.DistanceTo(oldPoint) >= 0.001)
                {
                    ((Polyline)this.Entity).TransformBy(Matrix3d.Displacement(oldPoint.GetVectorTo(insertionPoint)));
                    oldPoint = insertionPoint;
                }

                // Return that everything is fine
                return true;
            }

        }

        #endregion
    }
}
