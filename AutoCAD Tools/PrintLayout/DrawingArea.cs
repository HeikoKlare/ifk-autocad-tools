using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using AutoCADTools.PrintLayout;
using AutoCADTools.Tools;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// This class provides methods to create drawing areas for different formats.
    /// There are also methods to draw a custom drawing area which take care of the
    /// minumum extends of possible drawing areas.
    /// </summary>
    public class DrawingArea
    {
        #region Constants

        /// <summary>
        /// The drawing area's name
        /// </summary>
        public const string NAME = "Zeichenbereich";

        /// <summary>
        /// Constant for the Xrecord name that the new textfield is used
        /// </summary>
        public const string ATTRIBUTE_NEW_TEXTFIELD_USED = "NewTextfieldSizeUsed";

        /// <summary>
        /// Constant for the Xrecord name that specified the scale value
        /// </summary>
        public const string ATTRIBUTE_SCALE = "Scale";

        #endregion

        #region Attributes

        /// <summary>
        /// The scale depending on annotation scale and the paperunit
        /// </summary>
        private double scale;

        /// <summary>
        /// The scale depending on annotation scale and paperunit
        /// </summary>
        public double Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// The saved ID for the line of the drawing frame block
        /// </summary>
        private ObjectId lineId;

        /// <summary>
        /// The saved ID of the drawing frame
        /// </summary>
        private ObjectId drawingAreaId;

        /// <summary>
        /// Getter/Setter access to the saved ID for the drawing frame
        /// </summary>
        public ObjectId DrawingAreaId
        {
            get { return drawingAreaId; }
            set { drawingAreaId = value; }
        }

        private SpecificFormat format;

        /// <summary>
        /// The format of this drawing area, includes data about papersize, orientation, etc.
        /// </summary>
        public SpecificFormat Format
        {
            get { return format; }
            set { format = value; }
        }

        #endregion

        #region Singleton Specification

        private static DrawingArea instance;

        /// <summary>
        /// The singleton instance of the drawing area. If there is no drawing frame, null is returned!
        /// </summary>
        public static DrawingArea Instance
        {
            get
            {
                instance = FindDrawingArea();
                return instance;
            }
            set { instance = value; }
        }

        private DrawingArea() { }

        #endregion

        /// <summary>
        /// Finds the current drawing frame (if existing) and returns it.
        /// <returns>the drawing area if found, null otherwise</returns>
        /// </summary>
        private static DrawingArea FindDrawingArea()
        {
            DrawingArea drawingArea = new DrawingArea();
            bool found = false;

            // Get the current document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock acLock = acDoc.LockDocument())
            {
                // Save layout and set to modelspace
                String saveLayout = LayoutManager.Current.CurrentLayout;
                LayoutManager.Current.CurrentLayout = "Model";

                // Start the Transaction
                using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
                {
                    // Get active BlockTable and BlockTableRecord
                    BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord modelRecord = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                    
                    // Look if block does alread exist
                    if (acBlkTbl.Has(DrawingArea.NAME))
                    {
                        BlockTableRecord drawingAreaRecord = acTrans.GetObject(acBlkTbl[DrawingArea.NAME], OpenMode.ForRead) as BlockTableRecord;
                        foreach (ObjectId objId in modelRecord)
                        {
                            BlockReference block = acTrans.GetObject(objId, OpenMode.ForRead) as BlockReference;
                            try
                            {
                                if (block != null && block.Name == DrawingArea.NAME)
                                    drawingArea.drawingAreaId = objId;
                                found = true;

                                double scale;
                                bool oldTextfieldUsed = false;
                                // Get the scale
                                if (block.ExtensionDictionary.IsNull)
                                {
                                    // If block has no data, get the current scale
                                    if (!DrawingArea.CalculateScale(out scale)) return null;
                                    drawingArea.scale = scale;
                                    oldTextfieldUsed = true;
                                }
                                else
                                {
                                    // If the block has data, get its scale
                                    DBDictionary dict = acTrans.GetObject(block.ExtensionDictionary, OpenMode.ForRead) as DBDictionary;
                                    Xrecord xRecScale = acTrans.GetObject(dict.GetAt(ATTRIBUTE_SCALE), OpenMode.ForRead) as Xrecord;
                                    TypedValue[] valuesScale = xRecScale.Data.AsArray();
                                    Xrecord xRecTextField = acTrans.GetObject(dict.GetAt(ATTRIBUTE_NEW_TEXTFIELD_USED), OpenMode.ForRead) as Xrecord;
                                    TypedValue[] valuesTextField = xRecTextField.Data.AsArray();
                                    try
                                    {
                                        scale = double.Parse(valuesScale[0].Value.ToString());
                                        drawingArea.scale = scale;
                                        oldTextfieldUsed = !bool.Parse(valuesTextField[0].Value.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        if (!DrawingArea.CalculateScale(out scale)) return null;
                                        drawingArea.scale = scale;
                                        oldTextfieldUsed = true;
                                    }
                                }

                                Double width = block.Bounds.Value.MaxPoint.X - block.Bounds.Value.MinPoint.X;
                                Double height = block.Bounds.Value.MaxPoint.Y - block.Bounds.Value.MinPoint.Y;

                                SpecificFormat calculatedFormat = SpecificFormat.GetProperViewportFormat(width * scale, height * scale);
                                drawingArea.format = calculatedFormat;
                                drawingArea.format.OldTextfieldUsed = oldTextfieldUsed;
                            }
                            catch (Exception)
                            {
                                // Just catch an exception for all the other objects
                            }
                        }
                        foreach (ObjectId objId in drawingAreaRecord)
                        {
                            Polyline line = acTrans.GetObject(objId, OpenMode.ForRead) as Polyline;
                            try
                            {
                                if (line != null)
                                {
                                    drawingArea.lineId = objId;
                                }
                            }
                            catch (Exception)
                            {
                                // Just catch an exception for possible other objects
                            }
                        }
                    }
                }

                // Return to old layout
                LayoutManager.Current.CurrentLayout = saveLayout;
            }

            if (!found) drawingArea = null;
            return drawingArea;
        }


        /// <summary>
        /// Calculates the scale based on annotation scale and unit and saves it to the static variable
        /// </summary>
        /// <returns>true, if successfully calculated scale</returns>
        private static bool CalculateScale(out double scale)
        {
            // Look if the drawing unit is already set or otherwise as to it. If not doing return
            bool found = false;
            scale = 0.0d;
            do
            {
                System.Collections.IDictionaryEnumerator enumer = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.
                    Database.SummaryInfo.CustomProperties;
                while (enumer.MoveNext())
                {
                    if (enumer.Key.ToString() == "Zeichnungseinheit")
                    {
                        found = true;
                        // set the scale
                        scale = double.Parse(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CANNOSCALEVALUE").ToString())
                            * int.Parse(enumer.Value.ToString());
                    }
                }
                if (!found)
                {
                    if (MessageBox.Show(LocalData.DrawingAreaUnitFirstText, LocalData.DrawingAreaUnitFirstTitle,
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (Form settings = new DrawingSettings())
                        {
                            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(settings);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            } while (!found);
            return true;
        }




        /// <summary>
        /// Create an entry in the specified dictionary with the specified scale
        /// </summary>
        /// <param name="dictionary">the dictionary to create the entry in</param>
        /// <param name="scale">the scale to be put in the dictionary</param>
        /// <returns>the record added to the dictionary</returns>
        private static Xrecord CreateScaleRecord(DBDictionary dictionary, double scale)
        {
            Xrecord xRec = new Xrecord();
            using (ResultBuffer data = new ResultBuffer())
            {
                data.Add(new TypedValue((int)TypeCode.Int32, scale));
                xRec.Data = data;
                dictionary.SetAt(ATTRIBUTE_SCALE, xRec);
            }
            return xRec;
        }

        /// <summary>
        /// Create an entry in the specified dictionary which specifies if the new text field size is used
        /// </summary>
        /// <param name="dictionary">the dictionary to create the entry in</param>
        /// <param name="newTextFieldUsed">specifies if the new text field is used</param>
        /// <returns>the record added to the dictionary</returns>
        private static Xrecord CreateTextfieldRecord(DBDictionary dictionary, bool newTextFieldUsed)
        {
            Xrecord xRec = new Xrecord();
            using (ResultBuffer data = new ResultBuffer())
            {
                data.Add(new TypedValue((int)TypeCode.Boolean, newTextFieldUsed));
                xRec.Data = data;
                dictionary.SetAt(ATTRIBUTE_NEW_TEXTFIELD_USED, xRec);
            }
            return xRec;
        }

        /// <summary>
        /// Creates a drawing frame according to the specified format.
        /// </summary>
        /// <param name="format">the format the drawing area shell be created for</param>
        /// <returns>the created drawing area</returns>
        public static DrawingArea Create(SpecificFormat format)
        {
            DrawingArea drawingArea = new DrawingArea();
            // Calculate the scale
            double scale;
            if (!DrawingArea.CalculateScale(out scale))
            {
                return null;
            }
            drawingArea.scale = scale;
            drawingArea.format = format;
            
            // Get Document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            // Delete the current drawing frame
            if (Instance != null) Instance.Delete();

            // Get the needed drawing frame to the BlockTable
            drawingArea.CreateBlock();

            // Start the Transaction
            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                using (BlockReference newBlock = new BlockReference(new Point3d(0, 0, 0), acTrans.GetObject(acBlkTbl[DrawingArea.NAME], OpenMode.ForRead).ObjectId))
                {
                    newBlock.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                    newBlock.LayerId = acDoc.Database.LayerZero;
                    newBlock.Linetype = "Continuous";
                    newBlock.LineWeight = LineWeight.ByLineWeightDefault;

                    var jig = new DrawingAreaInsertionJig(newBlock, drawingArea);
                    PromptResult getPromptResult;
                    object oldCrossWidth = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CURSORSIZE");
                    Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", 100);
                    getPromptResult = acDoc.Editor.Drag(jig);
                    Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", oldCrossWidth);
                    // Handle Cancel or Error
                    if (getPromptResult.Status == PromptStatus.Cancel || getPromptResult.Status == PromptStatus.Error)
                    {
                        return null;
                    }

                    // Append the new block to the TableRecord and tell the transaction
                    acBlkTblRec.AppendEntity(newBlock);
                    acTrans.AddNewlyCreatedDBObject(newBlock, true);

                    // Save ID and format + direction
                    drawingArea.drawingAreaId = newBlock.ObjectId;

                    // Add an xRec with the scale
                    newBlock.CreateExtensionDictionary();
                    DBDictionary extensionDict = acTrans.GetObject(newBlock.ExtensionDictionary, OpenMode.ForWrite) as DBDictionary;
                    acTrans.AddNewlyCreatedDBObject(CreateScaleRecord(extensionDict, scale), true);
                    acTrans.AddNewlyCreatedDBObject(CreateTextfieldRecord(extensionDict, !format.OldTextfieldUsed), true);
                }

                // Close transaction
                acTrans.Commit();
                instance = drawingArea;
            }
            return drawingArea;
        }

        
        /// <summary>
        /// Defines a block for this drawing area accord to the specified parameters.
        /// Indicies of the polyline are as follows:
        /// 5-----------4
        /// |           |
        /// |      2----3
        /// |      |
        /// 0------1
        /// </summary>
        private void CreateBlock()
        {
            // Get Document and Database
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            // Start the Transaction
            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                // Get active BlockTable and BlockTableRecord
                BlockTable blockTable = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForWrite) as BlockTable;

                if (blockTable.Has(DrawingArea.NAME))
                {
                    acTrans.GetObject(blockTable[DrawingArea.NAME], OpenMode.ForWrite).Erase();
                }

                // Generate the new block
                using (BlockTableRecord newBlockDef = new BlockTableRecord())
                {
                    newBlockDef.Name = DrawingArea.NAME;

                    // Add the new block to the BlockTable
                    blockTable.Add(newBlockDef);
                    acTrans.AddNewlyCreatedDBObject(newBlockDef, true);

                    // Get some data: width and height
                    double width = format.ViewportModel.X;
                    double height = format.ViewportModel.Y;

                    // Create a polyline as the drawing frame
                    using (Polyline poly = new Polyline())
                    {
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(-width / scale, 0), 0, 0, 0);
                        if (format.Format == Paperformat.A4)
                        {
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, 0), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, 0), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, 0), 0, 0, 0);
                        }
                        else
                        {
                            Point textfieldSize = Format.TextfieldSize;
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(-textfieldSize.X / scale, 0), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(-textfieldSize.X / scale, textfieldSize.Y / scale), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, textfieldSize.Y / scale), 0, 0, 0);
                        }
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, height / scale), 0, 0, 0);
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(-width / scale, height / scale), 0, 0, 0);
                        poly.Closed = true;
                        poly.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                        poly.Linetype = "Continuous";
                        poly.LineWeight = LineWeight.ByLineWeightDefault;
                        poly.LayerId = acDoc.Database.LayerZero;

                        // Append the polyline to the block
                        newBlockDef.AppendEntity(poly);
                        acTrans.AddNewlyCreatedDBObject(poly, true);

                        // Save the object id of the polyline to get access later
                        lineId = poly.ObjectId;
                    }
                }
                // Close the transaction
                acTrans.Commit();
            }
        }


        /// <summary>
        /// Deletes the current drawing frame.
        /// </summary>
        public void Delete()
        {
            // Get the current document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            // Start the Transaction
            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                if (drawingAreaId.IsValid && !drawingAreaId.IsErased)
                {
                    acTrans.GetObject(drawingAreaId, OpenMode.ForWrite).Erase();
                    acTrans.TransactionManager.QueueForGraphicsFlush();
                }

                if (acBlkTbl.Has(DrawingArea.NAME))
                {
                    acTrans.GetObject(acBlkTbl[DrawingArea.NAME], OpenMode.ForWrite).Erase();
                }

                acTrans.TransactionManager.QueueForGraphicsFlush();

                acTrans.Commit();
            }
        }

        /// <summary>
        /// This function allows the user to change the size of the current drawing area using a jig.
        /// If there is no drawing area, nothing is done.
        /// </summary>
        /// <param name="oldTextfieldSize">specifies if the old (bigger) text field size shell be used</param>
        public static void ModifySize(bool oldTextfieldSize = false)
        {
            // Get the old drawing area, if there is none, return
            DrawingArea drawingArea = Instance;
            if (drawingArea == null)
            {
                return;
            }
            SpecificFormat oldFormat = drawingArea.format;
            drawingArea.Format.OldTextfieldUsed = oldTextfieldSize;

            // Get Document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            // Start the Transaction
            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                // Get active BlockTable and BlockTableRecord
                BlockReference blockReference = acTrans.GetObject(drawingArea.drawingAreaId, OpenMode.ForRead) as BlockReference;
                
                DrawingAreaModificationJig jig = new DrawingAreaModificationJig(blockReference, drawingArea);
                PromptResult getPromptResult;

                // Set the phase counter of the jig and get it started
                getPromptResult = acDoc.Editor.Drag(jig);
                // Handle Cancel or Error
                if (getPromptResult.Status == PromptStatus.Cancel || getPromptResult.Status == PromptStatus.Error)
                {
                    drawingArea.format = oldFormat;
                    acTrans.Abort();
                    return;
                }

                // Close transaction
                acTrans.Commit();
            }
        }


        class DrawingAreaInsertionJig : EntityJig
        {
            private DrawingArea drawingArea;
            private Point3d insertionPoint;

            public DrawingAreaInsertionJig(BlockReference br, DrawingArea drawingArea)
                : base(br)
            {
                this.drawingArea = drawingArea;
                this.insertionPoint = br.Position;
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                if (prompts == null) return SamplerStatus.Cancel;

                // Get the insertionPoint
                PromptPointResult getPointResult = prompts.AcquirePoint("\n" + LocalData.DrawingAreaInsertionText + ": ");
                Point3d oldPoint = insertionPoint;
                insertionPoint = getPointResult.Value;

                // If there is a format with a textfield, change the pick point to upper left of the textfield
                if (drawingArea.Format.Format != Paperformat.A4)
                {
                    insertionPoint = insertionPoint.Add(new Vector3d(drawingArea.Format.TextfieldSize.X / drawingArea.Scale, -drawingArea.Format.TextfieldSize.Y / drawingArea.Scale, 0));
                }

                // Return NoChange if difference is to low to avoid flimmering
                if (insertionPoint.DistanceTo(oldPoint) < 0.001)
                {
                    return SamplerStatus.NoChange;
                }

                // Otherwise return OK
                return SamplerStatus.OK;
            }

            protected override bool Update()
            {
                ((BlockReference)this.Entity).Position = insertionPoint;
                return true;
            }

        }

        class DrawingAreaModificationJig : EntityJig
        {
            private DrawingArea drawingArea;
            private Point3d insertionPoint;
            private Point3d targetPoint;
            private BlockReference reference;
            public DrawingAreaModificationJig(BlockReference br, DrawingArea drawingArea)
                : base(br)
            {
                this.drawingArea = drawingArea;
                this.insertionPoint = br.Position;
                this.reference = br;
                
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
                {
                    Polyline poly = acTrans.GetObject(drawingArea.lineId, OpenMode.ForRead) as Polyline;
                    targetPoint = poly.GetPoint3dAt(5);
                }
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                if (prompts == null) return SamplerStatus.Cancel;

                PromptPointResult getPointResult = prompts.AcquirePoint("\n" + LocalData.DrawingAreaModificationText + ": ");
                Point3d oldPoint = targetPoint;
                targetPoint = getPointResult.Value;
                
                // Return NoChange if difference is to low to avoid flimmering
                if (targetPoint.DistanceTo(oldPoint) < 0.001)
                {
                    return SamplerStatus.NoChange;
                }

                double width = insertionPoint.X - targetPoint.X;
                double height = targetPoint.Y - insertionPoint.Y;

                SpecificFormat format = drawingArea.Format.GetNextBiggerFormat(width * drawingArea.Scale, height * drawingArea.Scale);
                SpecificFormat formatA3 = new SpecificFormat(Paperformat.A3);
                drawingArea.format = format;

                // Make a lot of decisions to show the right minimum size of the drawing frame
                if (format.Format != Paperformat.AMAX)
                {
                    targetPoint = new Point3d(
                        insertionPoint.X - format.ViewportModel.X / drawingArea.Scale,
                        insertionPoint.Y + format.ViewportModel.Y / drawingArea.Scale, 0);
                }
                else if (height < formatA3.ViewportModel.Y / drawingArea.Scale)
                {
                    targetPoint = new Point3d(targetPoint.X,
                        insertionPoint.Y + formatA3.ViewportModel.Y / drawingArea.Scale, 0);
                }
                else if (width < formatA3.ViewportModel.X / drawingArea.Scale)
                {
                    targetPoint = new Point3d(insertionPoint.X - formatA3.ViewportModel.X / drawingArea.Scale,
                        targetPoint.Y, 0);
                }

                if (width > format.ViewportModel.X / drawingArea.Scale)
                {
                    targetPoint = new Point3d(insertionPoint.X - format.ViewportModel.X / drawingArea.Scale, targetPoint.Y, 0);
                }
                if (height > format.ViewportModel.Y / drawingArea.Scale)
                {
                    targetPoint = new Point3d(targetPoint.X, insertionPoint.Y + format.ViewportModel.Y / drawingArea.Scale, 0);
                }

                width = insertionPoint.X - targetPoint.X;
                height = targetPoint.Y - insertionPoint.Y;

                if (format.Format == Paperformat.AMAX)
                {
                    Point newPoint = SpecificFormat.IncreaseToNextBiggerFormat(width * drawingArea.Scale, height * drawingArea.Scale);
                    targetPoint = insertionPoint.Add(new Vector3d(-newPoint.X / drawingArea.Scale, newPoint.Y / drawingArea.Scale, 0));
                }

                // Return that everything is fine
                return SamplerStatus.OK;
            }

            protected override bool Update()
            {
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
                {
                    Polyline poly = acTrans.GetObject(drawingArea.lineId, OpenMode.ForWrite) as Polyline;
                    poly.SetPointAt(0, new Point2d(targetPoint.X - insertionPoint.X, 0));
                    poly.SetPointAt(4, new Point2d(0, targetPoint.Y - insertionPoint.Y));
                    poly.SetPointAt(5, new Point2d(targetPoint.X - insertionPoint.X,
                        targetPoint.Y - insertionPoint.Y));
                    if (drawingArea.Format.Format == Paperformat.A4)
                    {
                        poly.SetPointAt(3, new Point2d(0, 0));
                        poly.SetPointAt(2, new Point2d(0, 0));
                        poly.SetPointAt(1, new Point2d(0, 0));
                    }
                    else
                    {
                        poly.SetPointAt(3, new Point2d(0, drawingArea.Format.TextfieldSize.Y / drawingArea.Scale));
                        poly.SetPointAt(2, new Point2d(-drawingArea.Format.TextfieldSize.X / drawingArea.Scale, drawingArea.Format.TextfieldSize.Y / drawingArea.Scale));
                        poly.SetPointAt(1, new Point2d(-drawingArea.Format.TextfieldSize.X / drawingArea.Scale, 0));
                    }
                    BlockTable blockTable = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord drawingAreaRecord = acTrans.GetObject(blockTable[DrawingArea.NAME], OpenMode.ForRead) as BlockTableRecord;
                    reference.UpgradeOpen();
                    reference.BlockTableRecord = drawingAreaRecord.ObjectId;
                    acTrans.TransactionManager.QueueForGraphicsFlush();
                    acTrans.Commit();
                }

                return true;
            }
        }

    }
}