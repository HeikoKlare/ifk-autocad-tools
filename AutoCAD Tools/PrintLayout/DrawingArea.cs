using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
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
        /// The old drawing area's name
        /// </summary>
        public const string OLD_NAME = "Zeichenbereich";

        /// <summary>
        /// The new drawing area's name
        /// </summary>
        private const string NAME = "DrawingArea";

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

        private bool isValid;
        /// <summary>
        /// Specifies if the data of this drawing area are currently valid
        /// </summary>
        public bool IsValid
        {
            get { return isValid && !drawingAreaId.IsErased && drawingAreaId.IsValid; }
            set { isValid = value; }
        }
        
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

        private Document document;

        /// <summary>
        /// The document this drawing area belongs to
        /// </summary>
        public Document Document
        {
            get { return document; }
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

        private PaperformatTextfield format;

        /// <summary>
        /// The format of this drawing area, includes data about papersize, orientation, etc.
        /// </summary>
        public PaperformatTextfield Format
        {
            get { return format; }
            set { format = value; }
        }

        /// <summary>
        /// The id of this drawing area, unique per document
        /// </summary>
        private int id;

        /// <summary>
        /// The Name of this drawing area, concatenated from a generic name and the id
        /// </summary>
        public string Name
        {
            get { return NAME + id.ToString(); }
        }
                
        #endregion

        #region Constructors

        private DrawingArea(Document doc, int id)
        {
            this.isValid = false;
            this.document = doc;
            this.drawingAreaId = ObjectId.Null;
            this.format = PaperformatFactory.GetPaperformatTextfield(new Size(0, 0));
            this.lineId = ObjectId.Null;
            this.id = id;
        }

        #endregion

        /// <summary>
        /// Finds the current drawing frame in the specified document (if existing) and returns it.
        /// <param name="doc">the document to search for the drawing area in</param>
        /// <param name="id">the id of the drawing frame to be found</param>
        /// <returns>the drawing area if found, null otherwise</returns>
        /// </summary>
        public static DrawingArea FindDrawingArea(Document doc, int id)
        {
            if (LayoutManager.Current.CurrentLayout != "Model") return null;

            DrawingArea drawingArea = new DrawingArea(doc, id);

            using (DocumentLock acLock = doc.LockDocument())
            {
                // Start the Transaction
                using (Transaction acTrans = doc.TransactionManager.StartTransaction())
                {
                    // Get active BlockTable and BlockTableRecord
                    BlockTable acBlkTbl = acTrans.GetObject(doc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord modelRecord = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                    if (acBlkTbl.Has(DrawingArea.OLD_NAME))
                    {
                        ConvertOldArea(doc);
                    }

                    // Look if block does alread exist
                    if (acBlkTbl.Has(drawingArea.Name))
                    {
                        BlockTableRecord drawingAreaRecord = acTrans.GetObject(acBlkTbl[drawingArea.Name], OpenMode.ForRead) as BlockTableRecord;
                        foreach (ObjectId objId in modelRecord)
                        {
                            if (objId.ObjectClass == RXClass.GetClass(typeof(BlockReference))) {
                                BlockReference block = acTrans.GetObject(objId, OpenMode.ForRead) as BlockReference;
                                if (block != null && block.Name == drawingArea.Name)
                                {
                                    drawingArea.drawingAreaId = objId;
                                    drawingArea.isValid = true;

                                    double scale;
                                    bool oldTextfieldUsed = false;
                                    // Get the scale
                                    if (block.ExtensionDictionary.IsNull)
                                    {
                                        // If block has no data, get the current scale
                                        if (!drawingArea.CalculateScale(out scale)) return null;
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
                                        catch (System.Exception)
                                        {
                                            if (!drawingArea.CalculateScale(out scale)) return null;
                                            drawingArea.scale = scale;
                                            oldTextfieldUsed = true;
                                        }
                                    }

                                    Double width = block.Bounds.Value.MaxPoint.X - block.Bounds.Value.MinPoint.X;
                                    Double height = block.Bounds.Value.MaxPoint.Y - block.Bounds.Value.MinPoint.Y;

                                    drawingArea.format = PaperformatFactory.GetPaperformatTextfield(new Size(width * scale, height * scale), oldTextfieldUsed);
                                }
                            }
                        }
                        // Get the polyline if there is a drawing area, otherwise clear the block definition
                        if (drawingArea.IsValid)
                        {
                            foreach (ObjectId objId in drawingAreaRecord)
                            {
                                if (objId.ObjectClass == RXObject.GetClass(typeof(Polyline)))
                                {
                                    drawingArea.lineId = objId;
                                }
                            }
                        }
                        else
                        {
                            drawingAreaRecord.UpgradeOpen();
                            drawingAreaRecord.Erase();
                        }
                    }
                    acTrans.Commit();
                }
            }

            return drawingArea;
        }

        private static void ConvertOldArea(Document document) {
            using (DocumentLock acLock = document.LockDocument())
            {
                using (Transaction acTrans = document.TransactionManager.StartTransaction())
                {
                    // Get active BlockTable and BlockTableRecord
                    BlockTable acBlkTbl = acTrans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord modelRecord = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Look if block does alread exist
                    if (acBlkTbl.Has(DrawingArea.OLD_NAME))
                    {
                        DrawingArea drawingArea = new DrawingArea(document, 0);
                        Point3d insertionPoint = Point3d.Origin;
                        Size dimension = Size.Zero;

                        BlockTableRecord drawingAreaRecord = acTrans.GetObject(acBlkTbl[DrawingArea.OLD_NAME], OpenMode.ForWrite) as BlockTableRecord;
                        foreach (ObjectId objId in modelRecord)
                        {
                            if (objId.ObjectClass == RXClass.GetClass(typeof(BlockReference)))
                            {
                                BlockReference block = acTrans.GetObject(objId, OpenMode.ForWrite) as BlockReference;
                                if (block != null && block.Name == DrawingArea.OLD_NAME)
                                {
                                    Point3d min = block.Bounds.Value.MinPoint;
                                    Point3d max = block.Bounds.Value.MaxPoint;
                                    dimension = new Size(max.X - min.X, max.Y - min.Y);
                                    insertionPoint = new Point3d(max.X, min.Y, 0);
                                    drawingArea.CalculateScale(out drawingArea.scale);
                                    if (!block.ExtensionDictionary.IsNull)
                                    {
                                        // If the block has data, get its scale
                                        DBDictionary dict = acTrans.GetObject(block.ExtensionDictionary, OpenMode.ForRead) as DBDictionary;
                                        Xrecord xRecScale = acTrans.GetObject(dict.GetAt(ATTRIBUTE_SCALE), OpenMode.ForRead) as Xrecord;
                                        TypedValue[] valuesScale = xRecScale.Data.AsArray();
                                        try
                                        {
                                            drawingArea.scale = double.Parse(valuesScale[0].Value.ToString());
                                        }
                                        catch (System.Exception)
                                        {
                                        }
                                    }
                                    block.Erase();
                                    break;
                                }
                            }
                        }

                        drawingArea.Format = PaperformatFactory.GetPaperformatTextfield(new Size(dimension.Width * drawingArea.Scale, dimension.Height * drawingArea.Scale), true);
                        drawingAreaRecord.Erase();
                        drawingArea.CreateBlock();

                        using (BlockReference newBlock = new BlockReference(Point3d.Origin, acTrans.GetObject(acBlkTbl[drawingArea.Name], OpenMode.ForWrite).ObjectId))
                        {
                            newBlock.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                            newBlock.LayerId = document.Database.LayerZero;
                            newBlock.Linetype = "Continuous";
                            newBlock.LineWeight = LineWeight.ByLineWeightDefault;
                            newBlock.Position = insertionPoint;

                            // Append the new block to the TableRecord and tell the transaction
                            modelRecord.AppendEntity(newBlock);
                            acTrans.AddNewlyCreatedDBObject(newBlock, true);

                            // Add an xRec with the scale
                            newBlock.CreateExtensionDictionary();
                            DBDictionary extensionDict = acTrans.GetObject(newBlock.ExtensionDictionary, OpenMode.ForWrite) as DBDictionary;
                            acTrans.AddNewlyCreatedDBObject(CreateScaleRecord(extensionDict, drawingArea.scale), true);
                            acTrans.AddNewlyCreatedDBObject(CreateTextfieldRecord(extensionDict, !drawingArea.format.OldTextfieldSize), true);

                            drawingArea.Resize(newBlock, dimension);
                        }
                    }
                    acTrans.Commit();
                }
            }
        }

        /// <summary>
        /// Calculates the scale based on annotation scale and unit and saves it to the static variable
        /// </summary>
        /// <returns>true, if successfully calculated scale</returns>
        private bool CalculateScale(out double scale)
        {
            bool found = false;

            using (document.LockDocument())
            {

                scale = document.Database.Cannoscale.PaperUnits / document.Database.Cannoscale.DrawingUnits;

                var enumer = document.Database.SummaryInfo.CustomProperties;
                do
                {
                    while (enumer.MoveNext())
                    {
                        if (enumer.Key.ToString() == "Zeichnungseinheit")
                        {
                            found = true;
                            // set the scale
                            scale *= int.Parse(enumer.Value.ToString());
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
            }

            return found;
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
        /// <param name="id">the id of the drawing area to be created</param>
        /// <returns>the created drawing area</returns>
        public DrawingArea Create(PaperformatTextfield format, int id)
        {
            DrawingArea drawingArea = new DrawingArea(this.document, id);
            // Calculate the scale
            double scale;
            if (!drawingArea.CalculateScale(out scale))
            {
                return null;
            }
            drawingArea.scale = scale;
            drawingArea.format = format;

            using (document.LockDocument())
            {
                // Start the Transaction
                using (Transaction acTrans = document.TransactionManager.StartTransaction())
                {
                    // Delete the current drawing frame
                    if (this.IsValid)
                    {
                        Delete();
                    }

                    // Get the needed drawing frame to the BlockTable
                    drawingArea.CreateBlock();

                    BlockTable acBlkTbl = acTrans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (BlockReference newBlock = new BlockReference(Point3d.Origin, acTrans.GetObject(acBlkTbl[drawingArea.Name], OpenMode.ForRead).ObjectId))
                    {
                        newBlock.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                        newBlock.LayerId = document.Database.LayerZero;
                        newBlock.Linetype = "Continuous";
                        newBlock.LineWeight = LineWeight.ByLineWeightDefault;

                        var jig = new DrawingAreaInsertionJig(newBlock, drawingArea);
                        PromptResult getPromptResult;
                        object oldCrossWidth = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CURSORSIZE");
                        Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", 100);
                        getPromptResult = document.Editor.Drag(jig);
                        Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", oldCrossWidth);
                        // Handle Cancel or Error
                        if (getPromptResult.Status == PromptStatus.Cancel || getPromptResult.Status == PromptStatus.Error)
                        {
                            acTrans.Abort();
                            return null;
                        }

                        // Append the new block to the TableRecord and tell the transaction
                        acBlkTblRec.AppendEntity(newBlock);
                        acTrans.AddNewlyCreatedDBObject(newBlock, true);

                        // Save ID and format + direction
                        drawingArea.drawingAreaId = newBlock.ObjectId;
                        drawingArea.isValid = true;

                        // Add an xRec with the scale
                        newBlock.CreateExtensionDictionary();
                        DBDictionary extensionDict = acTrans.GetObject(newBlock.ExtensionDictionary, OpenMode.ForWrite) as DBDictionary;
                        acTrans.AddNewlyCreatedDBObject(CreateScaleRecord(extensionDict, scale), true);
                        acTrans.AddNewlyCreatedDBObject(CreateTextfieldRecord(extensionDict, !format.OldTextfieldSize), true);
                    }

                    // Close transaction
                    acTrans.Commit();
                }
            }
            return drawingArea;
        }

        
        /// <summary>
        /// Defines a block for this drawing area accord to the specified parameters. Erases the old block definition if existing
        /// Indicies of the polyline are as follows:
        /// 5-----------4
        /// |           |
        /// |      2----3
        /// |      |
        /// 0------1
        /// </summary>
        private void CreateBlock()
        {
            // Start the Transaction
            using (Transaction acTrans = document.TransactionManager.StartTransaction())
            {
                // Get active BlockTable and BlockTableRecord
                BlockTable blockTable = acTrans.GetObject(document.Database.BlockTableId, OpenMode.ForWrite) as BlockTable;

                if (blockTable.Has(Name))
                {
                    acTrans.GetObject(blockTable[Name], OpenMode.ForWrite).Erase();
                    acTrans.TransactionManager.QueueForGraphicsFlush();
                }

                // Generate the new block
                using (BlockTableRecord newBlockDef = new BlockTableRecord())
                {
                    newBlockDef.Name = Name;

                    // Add the new block to the BlockTable
                    blockTable.Add(newBlockDef);
                    acTrans.AddNewlyCreatedDBObject(newBlockDef, true);

                    // Get some data: width and height
                    double width = format.ViewportSizeModel.Width;
                    double height = format.ViewportSizeModel.Height;

                    // Create a polyline as the drawing frame
                    using (Polyline poly = new Polyline())
                    {
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(-width / scale, 0), 0, 0, 0);
                        if (!format.FullTextfieldUsed)
                        {
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, 0), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, 0), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, 0), 0, 0, 0);
                        }
                        else
                        {
                            Size textfieldSize = Format.TextfieldSize;
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(-textfieldSize.Width / scale, 0), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(-textfieldSize.Width / scale, textfieldSize.Height / scale), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, textfieldSize.Height / scale), 0, 0, 0);
                        }
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, height / scale), 0, 0, 0);
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(-width / scale, height / scale), 0, 0, 0);
                        poly.Closed = true;
                        poly.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                        poly.Linetype = "Continuous";
                        poly.LineWeight = LineWeight.ByLineWeightDefault;
                        poly.LayerId = document.Database.LayerZero;

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
            // Start the Transaction
            using (Transaction acTrans = document.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTbl = acTrans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                if (drawingAreaId.IsValid && !drawingAreaId.IsErased)
                {
                    acTrans.GetObject(drawingAreaId, OpenMode.ForWrite).Erase();
                    acTrans.TransactionManager.QueueForGraphicsFlush();
                }

                if (acBlkTbl.Has(Name))
                {
                    acTrans.GetObject(acBlkTbl[Name], OpenMode.ForWrite).Erase();
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
        /// <returns>true if area was resized, false otherwise</returns>
        public bool Resize(bool oldTextfieldSize = false)
        {
            if (!IsValid)
            {
                return false;
            }
            
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // No copy done here
            PaperformatTextfield oldFormat = format;
            Format.OldTextfieldSize = oldTextfieldSize;

            // Start the Transaction
            using (Transaction acTrans = document.TransactionManager.StartTransaction())
            {
                // Get active BlockTable and BlockTableRecord
                BlockReference blockReference = acTrans.GetObject(drawingAreaId, OpenMode.ForRead) as BlockReference;
                
                DrawingAreaModificationJig jig = new DrawingAreaModificationJig(blockReference, this);
                PromptResult getPromptResult;

                // Set the phase counter of the jig and get it started
                getPromptResult = document.Editor.Drag(jig);
                // Handle Cancel or Error
                if (getPromptResult.Status == PromptStatus.Cancel || getPromptResult.Status == PromptStatus.Error)
                {
                    format = oldFormat;
                    acTrans.Abort();
                    return false;
                }

                // Close transaction
                acTrans.Commit();
            }
            return true;
        }

        private void Resize(BlockReference reference, Size size)
        {
            using (Transaction acTrans = document.Database.TransactionManager.StartTransaction())
            {
                Polyline poly = acTrans.GetObject(lineId, OpenMode.ForWrite) as Polyline;
                poly.SetPointAt(0, new Point2d(-size.Width, 0));
                poly.SetPointAt(4, new Point2d(0, size.Height));
                poly.SetPointAt(5, new Point2d(-size.Width, size.Height));
                if (!Format.FullTextfieldUsed)
                {
                    poly.SetPointAt(3, new Point2d(0, 0));
                    poly.SetPointAt(2, new Point2d(0, 0));
                    poly.SetPointAt(1, new Point2d(0, 0));
                }
                else
                {
                    poly.SetPointAt(3, new Point2d(0, Format.TextfieldSize.Height / Scale));
                    poly.SetPointAt(2, new Point2d(-Format.TextfieldSize.Width / Scale, Format.TextfieldSize.Height / Scale));
                    poly.SetPointAt(1, new Point2d(-Format.TextfieldSize.Width / Scale, 0));
                }
                BlockTable blockTable = acTrans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord drawingAreaRecord = acTrans.GetObject(blockTable[Name], OpenMode.ForRead) as BlockTableRecord;
                reference.UpgradeOpen();
                reference.BlockTableRecord = drawingAreaRecord.ObjectId;
                acTrans.TransactionManager.QueueForGraphicsFlush();
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
                PromptPointResult getPointResult = prompts.AcquirePoint(Environment.NewLine + LocalData.DrawingAreaInsertionText);
                Point3d oldPoint = insertionPoint;
                insertionPoint = getPointResult.Value;

                // If there is a format with a textfield, change the pick point to upper left of the textfield
                if (drawingArea.Format.FullTextfieldUsed)
                {
                    insertionPoint = insertionPoint.Add(new Vector3d(drawingArea.Format.TextfieldSize.Width / drawingArea.Scale, -drawingArea.Format.TextfieldSize.Height / drawingArea.Scale, 0));
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
                
                using (Transaction acTrans = drawingArea.Document.Database.TransactionManager.StartTransaction())
                {
                    Polyline poly = acTrans.GetObject(drawingArea.lineId, OpenMode.ForRead) as Polyline;
                    targetPoint = poly.GetPoint3dAt(5);
                }
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                if (prompts == null) return SamplerStatus.Cancel;

                PromptPointResult getPointResult = prompts.AcquirePoint(Environment.NewLine + LocalData.DrawingAreaModificationText);
                Point3d oldPoint = targetPoint;
                targetPoint = getPointResult.Value;
                
                // Return NoChange if difference is to low to avoid flimmering
                if (targetPoint.DistanceTo(oldPoint) < 0.001)
                {
                    return SamplerStatus.NoChange;
                }

                double width = insertionPoint.X - targetPoint.X;
                double height = targetPoint.Y - insertionPoint.Y;

                PaperformatTextfield format = drawingArea.Format.ChangeSize(new Size(width * drawingArea.Scale, height * drawingArea.Scale));
                SpecificFormat formatA3 = new SpecificFormat(Paperformat.A3);
                drawingArea.format = format;

                // Make a lot of decisions to show the right minimum size of the drawing frame
                /*if (format.Format != Paperformat.AMAX)
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
                }*/
                targetPoint = new Point3d(insertionPoint.X - format.ViewportSizeModel.Width / drawingArea.Scale, insertionPoint.Y + format.ViewportSizeModel.Height / drawingArea.Scale, 0);
                // Return that everything is fine
                return SamplerStatus.OK;
            }

            protected override bool Update()
            {
                drawingArea.Resize(reference, new Size(insertionPoint.X - targetPoint.X, targetPoint.Y - insertionPoint.Y));

                return true;
            }
        }

    }
}