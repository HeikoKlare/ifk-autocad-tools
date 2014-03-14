using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace AutoCADTools.Tools
{
    class ReinforcingBond
    {

        #region Attributes

        private int halfFieldCount;

        /// <summary>
        /// The number of half field of the reinforcing bond
        /// </summary>
        public int HalfFieldCount
        {
            get { return halfFieldCount; }
            set { halfFieldCount = value; }
        }

        private string position;

        /// <summary>
        /// The position text for this reinforcing bond
        /// </summary>
        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        private double distanceToEave;

        /// <summary>
        /// The distance of the vertical member to the eave
        /// </summary>
        public double DistanceToEave
        {
            get { return distanceToEave; }
            set { distanceToEave = value; }
        }

        private double distanceToRidge;

        /// <summary>
        /// The distance of the vertical member to the ridge
        /// </summary>
        public double DistanceToRidge
        {
            get { return distanceToRidge; }
            set { distanceToRidge = value; }
        }

        private bool drawVerticalMembers;

        /// <summary>
        /// Specifies if vertical members at eave and ridge shell be drawn
        /// </summary>
        public bool DrawVerticalMembers
        {
            get { return drawVerticalMembers; }
            set { drawVerticalMembers = value; if (!drawVerticalMembers) drawChords = false; }
        }

        private bool drawChords;

        /// <summary>
        /// Specifies if chords shell be drawn. On setting true DrawVerticalMembers is also set to true
        /// </summary>
        public bool DrawChords
        {
            get { return drawChords; }
            set { drawChords = value; if (drawChords) drawVerticalMembers = true; }
        }

        private static ReinforcingBond instance;

        /// <summary>
        /// Returns the resulting block name for the text block
        /// </summary>
        private string BlockName {
            get { return BLOCK_PREFIX + position.Length.ToString() + Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CANNOSCALEVALUE").ToString() 
                + Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.Clayer; }
        }

        #endregion

        #region Constants

        private const string POSITION_TAG = "POSITION";
        private const string BLOCK_PREFIX = "ReinforcingBond";
        private static readonly Plane plane = new Plane(new Point3d(0, 0, 0), Vector3d.ZAxis);
        
        #endregion

        #region Bounds Struct

        private struct Bounds
        {
            public Vector2d bondDirection;
            public Vector2d bondDirectionNormalized;
            public Vector2d bondNormal;
            public Point2d firstEavePoint;
            public Point2d secondEavePoint;
            public Point2d firstRidgePoint;
            public Point2d secondRidgePoint;

            public Bounds(Point2d firstEavePoint, Point2d secondEavePoint, Point2d ridgePoint, double distanceToEave, double distanceToRidge)
            {
                bondDirection = firstEavePoint.GetVectorTo(ridgePoint);
                bondDirection = bondDirection.Subtract(bondDirection.MultiplyBy((distanceToEave + distanceToRidge) / bondDirection.Length));
                bondDirectionNormalized = bondDirection.DivideBy(bondDirection.Length);
                bondNormal = firstEavePoint.GetVectorTo(secondEavePoint);
                this.firstEavePoint = firstEavePoint.Add(bondDirectionNormalized.MultiplyBy(distanceToEave));
                this.secondEavePoint = secondEavePoint.Add(bondDirectionNormalized.MultiplyBy(distanceToEave));
                firstRidgePoint = ridgePoint.Subtract(bondDirectionNormalized.MultiplyBy(distanceToRidge));
                secondRidgePoint = ridgePoint.Subtract(bondDirectionNormalized.MultiplyBy(distanceToRidge)).Add(bondNormal);
            }
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes the class
        /// </summary>
        private ReinforcingBond()
        {
            distanceToEave = 0.1;
            distanceToRidge = 0.1;
            halfFieldCount = 4;
            position = "2";
            drawVerticalMembers = true;
            drawChords = false;
        }

        /// <summary>
        /// Returns the singleton instance for this class.
        /// </summary>
        /// <returns>the one and only instance</returns>
        public static ReinforcingBond getInstance()
        {
            if (instance == null)
            {
                lock (typeof(ReinforcingBond))
                {
                    if (instance == null)
                    {
                        instance = new ReinforcingBond();
                    }
                }
            }
            return instance;
        }

        #endregion

        #region Execution

        /// <summary>
        /// Draws the bond using the specified data via user input.
        /// </summary>
        /// <returns>true if everything worked ok, false otherwise</returns>
        public bool Draw()
        {
            // Get the active Document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            // Start transaction
            using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord modelSpace = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                // Get first eave point
                PromptPointOptions pointOpts = new PromptPointOptions("\n" + LocalData.ReinforcingBondFirstEave + ": ");
                var firstEavePointResult = acDoc.Editor.GetPoint(pointOpts);
                if (firstEavePointResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return false;
                }
                Point2d firstEavePoint = firstEavePointResult.Value.Convert2d(plane);

                // Get second eave point
                pointOpts.Message = "\n" + LocalData.ReinforcingBondSecondEave + ": ";
                pointOpts.UseBasePoint = true;
                pointOpts.BasePoint = firstEavePointResult.Value;
                var secondEavePointResult = acDoc.Editor.GetPoint(pointOpts);
                if (secondEavePointResult.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return false;
                }
                Point2d secondEavePoint = secondEavePointResult.Value.Convert2d(plane);
                
                // Create the polyline
                Polyline poly = new Polyline();
                int numberOfVertices = halfFieldCount + 1;
                if (drawVerticalMembers) numberOfVertices += 2;
                if (drawChords) numberOfVertices += 3;
                for (int i = 0; i < numberOfVertices; i++)
                {
                    poly.AddVertexAt(i, new Point2d(0, 0), 0, 0, 0);
                }
                
                // Start the jig
                var jig = new ReinforcingBondJig(poly, firstEavePoint, secondEavePoint);

                if (acDoc.Editor.Drag(jig).Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return false;
                }

                // Append resulting polyline and finalise the block (create text)
                modelSpace.AppendEntity(poly);
                acTrans.AddNewlyCreatedDBObject(poly, true);
                FinaliseBond(firstEavePoint, secondEavePoint, jig.RidgePoint.Convert2d(plane));

                // Commit the changes
                acTrans.Commit();
            }

            return true;
        }

        
        /// <summary>
        /// Updates the polyline according the specified points
        /// </summary>
        /// <param name="poly">the polyline to update</param>
        /// <param name="firstEavePoint">the first eave point</param>
        /// <param name="secondEavePoint">the second eave point</param>
        /// <param name="ridgePoint">the ridge point</param>
        private void updatePolyline(Polyline poly, Point2d firstEavePoint, Point2d secondEavePoint, Point2d ridgePoint)
        {
            Bounds bounds = new Bounds(firstEavePoint, secondEavePoint, ridgePoint, distanceToEave, distanceToRidge);
            
            // Calculate first index of the cross bars depending on drawing vertical members
            int firstIndex = 1;
            if (drawVerticalMembers) firstIndex++;

            // Draw the vertical member at eave if specified
            if (DrawVerticalMembers) poly.SetPointAt(0, bounds.secondEavePoint);
            poly.SetPointAt(firstIndex - 1, bounds.firstEavePoint);

            // Draw one half of cross bars
            for (int i = 0; i < halfFieldCount / 2 + 1; i++)
            {
                Point2d newPoint = bounds.secondEavePoint.Add(bounds.bondDirection.MultiplyBy((i * 2 + 1) / (double)halfFieldCount));
                poly.SetPointAt(firstIndex + i * 2, newPoint);
            }

            // Draw second half of cross bars
            for (int i = 0; i < halfFieldCount / 2; i++)
            {
                Point2d newPoint = bounds.firstEavePoint.Add(bounds.bondDirection.MultiplyBy(((i + 1) * 2) / (double)halfFieldCount));
                poly.SetPointAt(firstIndex + 1 + i * 2, newPoint);
            }

            // If vertical members shell be drawn, do so accoring to even or odd number of cross bars
            if (drawVerticalMembers)
            {
                if (halfFieldCount % 2 == 1)
                {
                    poly.SetPointAt(2 + halfFieldCount, bounds.firstRidgePoint);
                    if (drawChords)
                    {
                        poly.SetPointAt(3 + halfFieldCount, bounds.firstEavePoint);
                        poly.SetPointAt(4 + halfFieldCount, bounds.secondEavePoint);
                        poly.SetPointAt(5 + halfFieldCount, bounds.secondRidgePoint);
                    }
                }
                else
                {
                    poly.SetPointAt(2 + halfFieldCount, bounds.secondRidgePoint);
                    if (drawChords)
                    {
                        poly.SetPointAt(3 + halfFieldCount, bounds.secondEavePoint);
                        poly.SetPointAt(4 + halfFieldCount, bounds.firstEavePoint);
                        poly.SetPointAt(5 + halfFieldCount, bounds.firstRidgePoint);
                    }
                }
            }

        }

        /// <summary>
        /// Finalises the bond by drawing the text block.
        /// </summary>
        /// <param name="firstEavePoint">the first eave point</param>
        /// <param name="secondEavePoint">the second eave point</param>
        /// <param name="ridgePoint">the ridge point</param>
        private void FinaliseBond(Point2d firstEavePoint, Point2d secondEavePoint, Point2d ridgePoint)
        {
            Bounds bounds = new Bounds(firstEavePoint, secondEavePoint, ridgePoint, distanceToEave, distanceToRidge);
            
            // Get the active Document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable blockTable = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord modelSpace = acTrans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                // Look if block definition for current position text length exists
                if (!blockTable.Has(BlockName))
                {
                    // Create the new block
                    BlockTableRecord newBlock = new BlockTableRecord();
                    newBlock.Name = BlockName;
                    blockTable.UpgradeOpen();
                    blockTable.Add(newBlock);
                    acTrans.AddNewlyCreatedDBObject(newBlock, true);

                    // Dummy text for dimensions
                    MText dummyText = new MText();
                    dummyText.Annotative = AnnotativeStates.True;
                    dummyText.Contents = position;
                    modelSpace.AppendEntity(dummyText);
                    acTrans.AddNewlyCreatedDBObject(dummyText, true);

                    // If position is specified, create the block
                    if (!String.IsNullOrEmpty(position))
                    {
                        // Create the attribute definition
                        using (AttributeDefinition attrPos = new AttributeDefinition())
                        {
                            attrPos.Annotative = AnnotativeStates.True;
                            attrPos.Justify = AttachmentPoint.MiddleCenter;
                            attrPos.AlignmentPoint = new Point3d(0, 0, 0);
                            attrPos.Tag = POSITION_TAG;
                            attrPos.LockPositionInBlock = false;

                            newBlock.AppendEntity(attrPos);
                            acTrans.AddNewlyCreatedDBObject(attrPos, true);
                        }

                        // Create the circle
                        double radius = 0.0;
                        switch (position.Length)
                        {
                            case 1: radius = dummyText.ActualHeight * 0.9; break;
                            case 2: radius = dummyText.ActualHeight * 1.3; break;
                            case 3: radius = dummyText.ActualHeight * 1.5; break;
                        }

                        using (Circle circle = new Circle(new Point3d(0, 0, 0), Vector3d.ZAxis, radius))
                        {
                            circle.Linetype = "Continuous";
                            newBlock.AppendEntity(circle);
                            acTrans.AddNewlyCreatedDBObject(circle, true);
                        }
                    }

                    dummyText.Erase();
                    dummyText.Dispose();
                }

                // Look if text should be placed right or left
                double horizontalFactor = halfFieldCount % 4 < 2 ? 0.65 : 0.35;

                // Calculate text position
                Point2d textPos = firstEavePoint.Add(bounds.bondNormal.MultiplyBy(horizontalFactor)).Add(firstEavePoint.GetVectorTo(ridgePoint).MultiplyBy(0.5));
                if (halfFieldCount % 2 == 1)
                {
                    textPos = textPos.Subtract(bounds.bondDirection.MultiplyBy(0.5 / halfFieldCount));
                }

                // Add a block reference
                using (BlockReference blockRef = new BlockReference(new Point3d(textPos.X, textPos.Y, 0), blockTable[BlockName]))
                {
                    modelSpace.AppendEntity(blockRef);
                    acTrans.AddNewlyCreatedDBObject(blockRef, true);

                    // Update attributes
                    foreach (ObjectId id in (BlockTableRecord)acTrans.GetObject(blockTable[BlockName], OpenMode.ForRead))
                    {
                        DBObject obj = id.GetObject(OpenMode.ForRead);
                        AttributeDefinition attDef = obj as AttributeDefinition;
                        if ((attDef != null) && (!attDef.Constant))
                        {
                            //This is a non-constant AttributeDefinition
                            //Create a new AttributeReference
                            using (AttributeReference attRef = new AttributeReference())
                            {
                                attRef.SetAttributeFromBlock(attDef, blockRef.BlockTransform);
                                if (attRef.Tag == POSITION_TAG)
                                {
                                    attRef.TextString = position;
                                }

                                //Add the AttributeReference to the BlockReference
                                blockRef.AttributeCollection.AppendAttribute(attRef);
                                acTrans.AddNewlyCreatedDBObject(attRef, true);
                            }
                            obj.Dispose();
                            attDef.Dispose();
                        }
                    }
                }

                acTrans.Commit();
            }
        }

        #endregion
        
        #region Jig

        /// <summary>
        /// This is a Jig helper class.
        /// It can be used to strech the reinforcing bond polyline to some ridge point
        /// </summary>
        class ReinforcingBondJig : EntityJig
        {
            private Point2d firstEavePoint;
            private Point2d secondEavePoint;
            private Point3d ridgePoint;

            /// <summary>
            /// The ridge point specified by this jig
            /// </summary>
            public Point3d RidgePoint
            {
                get { return ridgePoint; }
                set { ridgePoint = value; }
            }
            private ReinforcingBond bond;
            private Polyline poly;

            /// <summary>
            /// Initiates a new Jig for the given polyline belonging to a reinforcing bond
            /// </summary>
            /// <param name="poly">the polyline the ridge shell be defined for</param>
            /// <param name="firstEavePoint">the first eave point of the reinforcing bond</param>
            /// <param name="secondEavePoint">the second eave point of the reinforcing bond</param>
            public ReinforcingBondJig(Polyline poly, Point2d firstEavePoint, Point2d secondEavePoint) : base(poly)
            {
                this.poly = poly;
                this.firstEavePoint = firstEavePoint;
                this.secondEavePoint = secondEavePoint;
                // Set the insertionPoint at a position
                ridgePoint = new Point3d(secondEavePoint.X, secondEavePoint.Y, 0);
                bond = ReinforcingBond.getInstance();

                //MessageBox.Show(bond.HalfFieldCount + "");
            }


            /// <summary>
            /// Samples the current jig status.
            /// Therefore the user is asked to input the ridge point.
            /// </summary>
            /// <param name="prompts">the JigPrompts to use</param>
            /// <returns>the current SamplerStatus, will be NoChange or OK</returns>
            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                if (prompts == null) return SamplerStatus.Cancel;

                // Get the insertionPoint
                PromptPointResult getPointResult = prompts.AcquirePoint("\n" + LocalData.ReinforcingBondRidge + ": ");
                Point3d oldPoint0 = ridgePoint;
                ridgePoint = getPointResult.Value;

                // Return NoChange if difference is to low to avoid flimmering
                if (ridgePoint.DistanceTo(oldPoint0) < 0.001)
                {
                    return SamplerStatus.NoChange;
                }

                // Otherwise return OK
                return SamplerStatus.OK;
            }


            /// <summary>
            /// Updates this Jig.
            /// Changes the reinforcing bond according to the current ridge point.
            /// </summary>
            /// <returns>true if everything is okay</returns>
            protected override bool Update()
            {
                bond.updatePolyline(poly, firstEavePoint, secondEavePoint, ridgePoint.Convert2d(plane));
            
                // Return that everything is fine
                return true;
            }

        }

        #endregion
    }
}
