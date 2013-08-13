using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace AutoCADTools.Tools
{
    class TrussImport
    {
        #region Enum Types

        /// <summary>
        /// The different layers that can be imported
        /// </summary>
        public enum Layer { Member, Bracings, Bearings, Dimensions, Plates };

        /// <summary>
        /// The Directions the imported truss can have
        /// </summary>
        public enum Direction { LeftRotate, RightRotate, Standard };


        #endregion

        #region Attributes

        private string fileName;

        /// <summary>
        /// The name of the file to import.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string layerPrefix = "Schnitt";

        /// <summary>
        /// The prefix of the resulting layers.
        /// </summary>
        public string LayerPrefix
        {
            get { return layerPrefix; }
            set { layerPrefix = value; }
        }

        private Dictionary<Layer, bool> layersChecked;

        /// <summary>
        /// Sets a layer checked for import.
        /// </summary>
        /// <param name="layer">the layer to check</param>
        /// <param name="activated">true if it shell be imported, false if not</param>
        public void setLayerChecked(Layer layer, bool activated)
        {
            layersChecked[layer] = activated;
        }
        /// <summary>
        /// Return is a layer is checked for import.
        /// </summary>
        /// <param name="layer">the layer to check</param>
        /// <returns>true if the layer is checked for import, false if not</returns>
        public bool isLayerChecked(Layer layer)
        {
            return layersChecked[layer];
        }

        private Direction rotation = Direction.LeftRotate;

        /// <summary>
        /// The Rotation of the imported truss.
        /// </summary>
        public Direction Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private Dictionary<string, Layer> originalLayerNameMap;
        private Dictionary<Layer, string> newLayerNameMap;

        /// <summary>
        /// Returns the name of the resulting layer.
        /// </summary>
        /// <param name="layer">the layer to return the name for</param>
        /// <returns>the name of the resulting layer</returns>
        private string getNewLayerName(Layer layer)
        {
            return layerPrefix + " - " + newLayerNameMap[layer];
        }

        private string dummyBlockName = "TrussPreventFromDuplicateBlockName";

        private static TrussImport instance;

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes the class
        /// </summary>
        private TrussImport()
        {
            initialiseLayerNames();
            initialiseLayerChecks();
        }

        /// <summary>
        /// Initialises the checked layers
        /// </summary>
        private void initialiseLayerChecks()
        {
            layersChecked = new Dictionary<Layer, bool>();
            layersChecked[Layer.Bearings] = true;
            layersChecked[Layer.Bracings] = true;
            layersChecked[Layer.Dimensions] = false;
            layersChecked[Layer.Member] = true;
            layersChecked[Layer.Plates] = false;
        }

        /// <summary>
        /// Initialises the layer names
        /// </summary>
        private void initialiseLayerNames() {
            originalLayerNameMap = new Dictionary<string, Layer>();
            // TrussCon Layers
            originalLayerNameMap.Add("QUERSCHNITTE", Layer.Member);
            originalLayerNameMap.Add("AUSSTEIFUNG", Layer.Bracings);
            originalLayerNameMap.Add("AUFLAGER", Layer.Bearings);
            originalLayerNameMap.Add("MAßLINIEN", Layer.Dimensions);
            originalLayerNameMap.Add("PLATTE", Layer.Plates);
            // MiTek Layers
            originalLayerNameMap.Add("MEMBERS", Layer.Member);
            originalLayerNameMap.Add("WEB_BRACING", Layer.Bracings);
            originalLayerNameMap.Add("BEARINGS", Layer.Bearings);
            originalLayerNameMap.Add("DIMENSIONS", Layer.Dimensions);
            originalLayerNameMap.Add("PLATES", Layer.Plates);

            // New Layer names
            newLayerNameMap = new Dictionary<Layer, string>();
            newLayerNameMap[Layer.Bearings] = LocalData.TrussImportSuffixBearings;
            newLayerNameMap[Layer.Bracings] = LocalData.TrussImportSuffixBracings;
            newLayerNameMap[Layer.Dimensions] = LocalData.TrussImportSuffixDimensions;
            newLayerNameMap[Layer.Member] = LocalData.TrussImportSuffixMembers;
            newLayerNameMap[Layer.Plates] = LocalData.TrussImportSuffixPlates;
        }

        /// <summary>
        /// Returns the singleton instance for this class.
        /// </summary>
        /// <returns>the one and only instance</returns>
        public static TrussImport getInstance()
        {
            if (instance == null)
            {
                lock (typeof(TrussImport))
                {
                    if (instance == null)
                    {
                        instance = new TrussImport();
                    }
                }
            }
            return instance;
        }

        #endregion

        #region Import

        /// <summary>
        /// Imports the objects on the selected layers of the specified drawing to the currently opened drawing.
        /// The user is asked to place the imported objects.
        /// </summary>
        public bool Import()
        {
            // Look if selected file exists, otherwise show error and return
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File: " + fileName + " not found");
            }

            // Get the documents of the current one and the one to copy from (open the second one)
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Document acImportDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Open(fileName, true);

            // Save min and max value of the imported objects
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double minXall = double.MaxValue;
            double minYall = double.MaxValue;
            bool noWood = true;

            // Create a collection for the objects to copy
            ObjectIdCollection acObjIdColl = new ObjectIdCollection();
            
            // Lock the current document
            using (acImportDoc.LockDocument())
            {
                // Start a transaction
                using (Transaction acTrans = acImportDoc.Database.TransactionManager.StartTransaction())
                {
                    // Open the Block table for read
                    BlockTable acBlkTbl = acTrans.GetObject(acImportDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Open layer table for creating new layers
                    LayerTable acLyrTbl = acTrans.GetObject(acImportDoc.Database.LayerTableId, OpenMode.ForRead) as LayerTable;
                    acLyrTbl.UpgradeOpen();

                    // Run through the objects and copy them if wanted
                    foreach (ObjectId obj in acBlkTblRec)
                    {
                        try
                        {
                            Entity ent = (Entity)acTrans.GetObject(obj, OpenMode.ForRead);

                            // If object is wood, save its values too
                            if (originalLayerNameMap.ContainsKey(ent.Layer) && originalLayerNameMap[ent.Layer] == Layer.Member)
                            {
                                noWood = false;
                                if (ent.Bounds.Value.MinPoint.X < minX)
                                {
                                    minX = ent.Bounds.Value.MinPoint.X;
                                }
                                if (ent.Bounds.Value.MinPoint.Y < minY)
                                {
                                    minY = ent.Bounds.Value.MinPoint.Y;
                                }
                            }

                            // Look if entity is on a layer that shell be copied
                            if (originalLayerNameMap.ContainsKey(ent.Layer) && layersChecked[originalLayerNameMap[ent.Layer]])
                            {
                                string newLayerName = getNewLayerName(originalLayerNameMap[ent.Layer]);
                                if (!acLyrTbl.Has(newLayerName))
                                {
                                    // Create the new layer
                                    using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                                    {
                                        acLyrTblRec.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                                        acLyrTblRec.Name = newLayerName;
                                        acLyrTbl.Add(acLyrTblRec);
                                        acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                                    }
                                }

                                ent.UpgradeOpen();
                                ObjectId layer = acLyrTbl[newLayerName];
                                ent.LayerId = layer;

                                // add object to collection, look if new min value
                                acObjIdColl.Add(ent.ObjectId);
                                if (ent.Bounds.Value.MinPoint.X < minXall) 
                                {
                                    minXall = ent.Bounds.Value.MinPoint.X;
                                }
                                if (ent.Bounds.Value.MinPoint.Y < minYall) 
                                {
                                    minYall = ent.Bounds.Value.MinPoint.Y;
                                }
                            }

                            
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    acTrans.Commit();
                }
            }

            // If there was wood, take its min values for positioning
            if (!noWood)
            {
                minXall = minX;
                minYall = minY;
            }

            // Set the current document active again
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument = acDoc;
            
            // Lock the new document
            using (acDoc.LockDocument())
            {
                // Start a transaction in the new database
                using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
                {
                    // Open the Block table for read
                    BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Open the Block table record Model space for read
                    using (BlockTableRecord importBlock = new BlockTableRecord())
                    {
                        // Name the import block and add it to the table record
                        importBlock.Name = dummyBlockName;
                        acBlkTbl.UpgradeOpen();
                        acBlkTbl.Add(importBlock);

                        // Add the block to the database
                        acTrans.AddNewlyCreatedDBObject(importBlock, true);

                        // Clone the objects to the current database
                        using (IdMapping acIdMap = new IdMapping())
                        {
                            acDoc.Database.WblockCloneObjects(acObjIdColl, importBlock.ObjectId, acIdMap, DuplicateRecordCloning.Ignore, false);
                        }
                        
                        // Set origin to the minimum values of the imported objects
                        importBlock.Origin = new Point3d(minXall, minYall, 0);

                        // Try to create new reference to the import block
                        BlockReference newBlock = null;
                        try
                        {
                            // Create a new reference of the block to add to model space and create the jig
                            newBlock = new BlockReference(new Point3d(0, 0, 0), acTrans.GetObject(acBlkTbl[dummyBlockName], OpenMode.ForRead).ObjectId);
                            newBlock.Color = Autodesk.AutoCAD.Colors.Color.FromColor(System.Drawing.Color.Black);

                            // Get the wanted rotation and apply to the new block reference
                            double rotate = 0;
                            if (rotation == Direction.LeftRotate)
                            {
                                rotate = Math.PI / 2;
                            }
                            else if (rotation == Direction.RightRotate)
                            {
                                rotate = -Math.PI / 2;
                            }
                            newBlock.TransformBy(Matrix3d.Rotation(rotate, acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, new Point3d(0, 0, 0)));

                            // Scale the objects
                            newBlock.TransformBy(Matrix3d.Scaling(0.001, new Point3d(0, 0, 0)));

                            // Create a jig to place the copied objects
                            BlockJig jig = new BlockJig(newBlock);
                            if (acDoc.Editor.Drag(jig).Status == PromptStatus.OK)
                            {
                                // Create new object collection, explode the block reference in it and add the new objects to the database
                                using (DBObjectCollection acDbObjColl = new DBObjectCollection())
                                {
                                    newBlock.Explode(acDbObjColl);
                                    foreach (Entity acEnt in acDbObjColl)
                                    {
                                        acBlkTblRec.AppendEntity(acEnt);
                                        acTrans.AddNewlyCreatedDBObject(acEnt, true);
                                    }
                                }
                            }
                            
                            importBlock.Erase();
                            newBlock.Dispose();

                            // Save the copied objects to the database
                            acTrans.Commit();
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
            }

            acObjIdColl.Dispose();
            // Close the document the objects were imported from
            acImportDoc.CloseAndDiscard();
            acDoc.Editor.Regen();

            return true;
        }

        #endregion

        #region BlockJig

        /// <summary>
        /// This is a Jig helper class.
        /// It can be used to place a block reference at some point.
        /// </summary>
        class BlockJig : EntityJig
        {
            private Point3d insertionPoint;
            
            /// <summary>
            /// Initiates a new BlockJig for the given BlockReference.
            /// </summary>
            /// <param name="br">the BlockReference to be positioned (and sized)</param>
            public BlockJig(BlockReference br) : base(br)
            {
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
