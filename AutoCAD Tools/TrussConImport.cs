using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace AutoCADTools
{
    /// <summary>
    /// This class provides the graphical user interface and the functions to import a TrussCon- or MiTeK- Truss-Drawing
    /// to the currently open drawing.
    /// The rotation and the layers to import can be selected
    /// </summary>
    public partial class UfTrussConImport : Form
    {
        /// <summary>
        /// The name of the layers in the imported drawing.
        /// </summary>
        private readonly string[] LAYER_WOOD = {"QUERSCHNITTE", "MEMBERS"};
        private readonly string[] LAYER_BRACING = { "AUSSTEIFUNG", "WEB_BRACING" };
        private readonly string[] LAYER_BEARING = { "AUFLAGER", "BEARINGS" };
        private readonly string[] LAYER_DIMENSIONS = { "MAßLINIEN", "DIMENSIONS" };
        private readonly string[] LAYER_PLATES = { "PLATTE", "PLATES" };


        /// <summary>
        /// Initializes the graphical user interface for the TrussCon-Truss-Drawing import function
        /// </summary>
        public UfTrussConImport()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Opens a dialog to select the file to import the truss from.
        /// </summary>
        /// <param name="sender">the object sending invoke to execute this function</param>
        /// <param name="e">the event arguments</param>
        private void CbSuchenClick(object sender, EventArgs e)
        {
            if (ofdQuelle.ShowDialog() == DialogResult.OK)
            {
                tbQuelle.Text = ofdQuelle.FileName;
            }
        }


        /// <summary>
        /// Imports the objects on the selected layers of the specified drawing to the currently opened drawing.
        /// The user is asked to place the imported objects.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbImportierenClick(object sender, EventArgs e)
        {
            // Look if selected file exists, otherwise show error and return
            if (!File.Exists(tbQuelle.Text))
            {
                MessageBox.Show("Datei nicht gefunden.", "Unbekannte Datei", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();

            string LAYER_NEW = tbLayer.Text;

            // Get the documents of the current one and the one to copy from (open the second one)
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Document acImportDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Open(tbQuelle.Text, true);

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

                    // Run through the objects and copy them if wanted
                    foreach (ObjectId obj in acBlkTblRec)
                    {
                        try
                        {
                            Entity ent = (Entity)acTrans.GetObject(obj, OpenMode.ForRead);
                            // Look if entity is on a layer that shell be copied
                            if (LAYER_BEARING.Contains(ent.Layer) && cbAuflager.Checked
                                || LAYER_BRACING.Contains(ent.Layer) && cbAussteifung.Checked
                                || LAYER_DIMENSIONS.Contains(ent.Layer) && cbMasse.Checked
                                || LAYER_WOOD.Contains(ent.Layer) && CbHolz.Checked
                                || LAYER_PLATES.Contains(ent.Layer) && cbPlatten.Checked)
                            {
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
                            // If object is wood, save its values too
                            if (LAYER_WOOD.Contains(ent.Layer))
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
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ein Fehler trat beim Kopieren der Objekte auf.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        importBlock.Name = "TrussPreventFromDuplicateName";
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
                            newBlock = new BlockReference(new Point3d(0, 0, 0), acTrans.GetObject(acBlkTbl["TrussPreventFromDuplicateName"], OpenMode.ForRead).ObjectId);
                            newBlock.Color = Autodesk.AutoCAD.Colors.Color.FromColor(System.Drawing.Color.Black);

                            // Create new layer "TrussCon" if not existing
                            LayerTable acLyrTbl = acTrans.GetObject(acDoc.Database.LayerTableId, OpenMode.ForRead) as LayerTable;
                            if (!acLyrTbl.Has(LAYER_NEW))
                            {
                                // Create the new layer
                                using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                                {
                                    acLyrTblRec.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                                    acLyrTblRec.Name = LAYER_NEW;
                                    acLyrTbl.UpgradeOpen();
                                    acLyrTbl.Add(acLyrTblRec);
                                    acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                                }
                            }

                            // Get the wanted rotation and apply to the new block reference
                            double rotate = 0;
                            if (rbRotateLeft.Checked)
                            {
                                rotate = Math.PI / 2;
                            }
                            else if (rbRotateRight.Checked)
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
                                        acEnt.Layer = LAYER_NEW;
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
                            MessageBox.Show("Ein Fehler trat beim Kopieren der Objekte auf.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            acObjIdColl.Dispose();
            // Close the document the objects were imported from
            acImportDoc.CloseAndDiscard();

            // Close this dialog
            this.Close();
        }



        /// <summary>
        /// This is a Jig helper class.
        /// This can be used to select the position or in case of format bigger than A3 the size of the
        /// drawing frame to create.
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
            /// Therefore the user is asked to input insertion point or size and the current mouse
            /// position and user inputs are analyzed to update the BlockReference and see wheter the
            /// input has ended.
            /// </summary>
            /// <param name="prompts">the JigPrompts to use</param>
            /// <returns>the current SamplerStatus, will be NoChange or OK</returns>
            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                if (prompts == null) return SamplerStatus.Cancel;

                // Get the insertionPoint
                PromptPointResult getPointResult0 = prompts.AcquirePoint("\nEinfügepunkt angeben: ");
                Point3d oldPoint0 = insertionPoint;
                insertionPoint = getPointResult0.Value;

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
            /// Changes the insertionPoint or the position of the polyline-vertices according to the
            /// current user input.
            /// </summary>
            /// <returns>true if everything is okay</returns>
            protected override bool Update()
            {
                ((BlockReference)this.Entity).Position = insertionPoint;

                // Return that everything is fine
                return true;
            }

        }
        

        /// <summary>
        /// Handles the escape and enter keypress to exit the window or start drawing Rispen.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void UFTrussConImport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Hide();
            }
        }

    }

}
