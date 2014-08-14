using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using AutoCADTools.Tools;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class provides methods to create drawing areas for different formats.
    /// There are also methods to draw a custom drawing area which take care of the
    /// minumum extends of possible drawing areas and there is a method for automatically
    /// creating a drawing frame.
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

        /// <summary>
        /// The constant for format A4
        /// </summary>
        public const int CA4 = 0;

        /// <summary>
        /// The constant for format A3
        /// </summary>
        public const int CA3 = 1;

        /// <summary>
        /// The constant for formats bigger than A3
        /// </summary>
        public const int CAX = 2;

        /// <summary>
        /// The constant for the maximum paper format
        /// </summary>
        public const int CAMAX = 3;

        /// <summary>
        /// The constant for horizontal paperformat
        /// </summary>
        public const int CHORIZONTAL = 4;

        /// <summary>
        /// The constant for vertical paperformat
        /// </summary>
        public const int CVERTICAL = 8;

        /// <summary>
        /// The constant for the width
        /// </summary>
        public const int CWIDTH = 0;

        /// <summary>
        /// The constant for the height
        /// </summary>
        public const int CHEIGHT = 1;
           
        /// <summary>
        /// An array for the paper formats: first dimension: format as CA4, CA3, CAX, CAMAX, sexond dimension: height and width as CWIDTH, CHEIGHT
        /// </summary>
        private static double[,] formats = new double[,] { { 171.37, 250.5 }, { 287.0, 395.0 }, { 287.0, 395.0 }, { 1189.0, 841.0 } };

        /// <summary>
        /// Size of the textfield
        /// </summary>
        private static readonly double[] textfieldSize = new double [] { 185.0, 57.5 };

        /// <summary>
        /// Size of the old textfield
        /// </summary>
        private static readonly double[] textfieldSizeOld = new double[] { 185.0, 77.0 };

        #endregion

        #region Attributes

        /// <summary>
        /// Indicator if the format is userdefined
        /// </summary>
        private bool userDefined;

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
        /// Specifies if the new text field size is used
        /// </summary>
        private bool newTextfieldUsed;

        /// <summary>
        /// Specifies if the new text field size is used
        /// </summary>
        public bool NewTextfieldUsed
        {
            get { return newTextfieldUsed; }
            set { newTextfieldUsed = value; }
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

        /// <summary>
        /// The saved format and direction of the current drawing frame; it is the sum of the format and direction constant
        /// </summary>
        private int drawingAreaFormatDir;

        /// <summary>
        /// Getter/Setter access to the saved format and direction of the current drawing frame; it is the sum of the format and direction constant
        /// </summary>
        public int DrawingAreaFormatDir
        {
            get { return drawingAreaFormatDir; }
            set { drawingAreaFormatDir = value; }
        }

        #endregion

        #region Singleton Specification

        private static DrawingArea instance;

        /// <summary>
        /// The singleton instance of the drawing area. If there is no drawing frame, null is returned!
        /// </summary>
        public static DrawingArea Instance
        {
            get {
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
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                    // Look if block does alread exist
                    if (acBlkTbl.Has(DrawingArea.NAME))
                    {
                        foreach (ObjectId objId in acBlkTblRec)
                        {
                            BlockReference block = acTrans.GetObject(objId, OpenMode.ForRead) as BlockReference;
                            try
                            {
                                if (block != null && block.Name == DrawingArea.NAME)
                                    drawingArea.drawingAreaId = objId;
                                found = true;

                                double scale;
                                // Get the scale
                                if (block.ExtensionDictionary.IsNull)
                                {
                                    // If block has no data, get the current scale
                                    if (!DrawingArea.CalculateScale(out scale)) return null;
                                    drawingArea.scale = scale;
                                    drawingArea.newTextfieldUsed = false;
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
                                        drawingArea.newTextfieldUsed = bool.Parse(valuesTextField[0].Value.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        if (!DrawingArea.CalculateScale(out scale)) return null;
                                        drawingArea.scale = scale;
                                        drawingArea.newTextfieldUsed = false;
                                    }
                                }

                                Double width = block.Bounds.Value.MaxPoint.X - block.Bounds.Value.MinPoint.X;
                                Double height = block.Bounds.Value.MaxPoint.Y - block.Bounds.Value.MinPoint.Y;


                                // Case A4 vertical
                                if (Math.Abs(width - formats[CA4, CWIDTH] / scale) < 0.01
                                    && Math.Abs(height - formats[CA4, CHEIGHT] / scale) < 0.01)
                                {
                                    drawingArea.drawingAreaFormatDir = DrawingArea.CA4 + DrawingArea.CVERTICAL;
                                }
                                // Case A4 horizontal
                                else if (Math.Abs(width - formats[CA4, CHEIGHT] / scale) < 0.01
                                    && Math.Abs(height - formats[CA4, CWIDTH] / scale) < 0.01)
                                {
                                    drawingArea.drawingAreaFormatDir = DrawingArea.CA4 + DrawingArea.CHORIZONTAL;
                                }
                                // Case A3
                                else if (Math.Abs(width - formats[CA3, CHEIGHT] / scale) < 0.01
                                    && Math.Abs(height - formats[CA3, CWIDTH] / scale) < 0.01)
                                {
                                    drawingArea.drawingAreaFormatDir = DrawingArea.CA3 + DrawingArea.CHORIZONTAL;
                                }
                                // Case bigger than A3
                                else
                                {
                                    drawingArea.drawingAreaFormatDir = DrawingArea.CAX + DrawingArea.CHORIZONTAL;
                                }
                            }
                            catch (Exception)
                            {
                                // Just catch an exception for all the other objects
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
        /// This methods tries to automatically draw a frame by analyzing the objects that are
        /// already in the current drawing and calculating their maximum extends.
        /// If there are no objects in the current drawing a message is given to the user.
        /// </summary>
        /*[System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2010")]
        public static void CreateAuto()
        {
            if (!DrawingArea.CalculateScale())
            {
                return;

            }
            
            // Get the document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            // Get the scale and set some variables
            double maxX = -1000000000.0, maxY = -1000000000.0, minX = 1000000000.0, minY = 1000000000.0;

            // Start a transaction
            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                // Get current BlockTable and the Record for the Model Space
                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                // Run through the entities and get maximum bounds
                foreach (ObjectId obj in acBlkTblRec)
                {
                    // Get each object as entity and look if it is a BlockReference and get the name
                    Entity ent = obj.GetObject(OpenMode.ForRead) as Entity;
                   
                    // Look if it is the drawing frame
                    BlockReference block = ent as BlockReference;
                    bool isDf = false;
                    try
                    {
                        if (block != null && block.Name == DrawingArea.NAME)
                        {
                            isDf = true;
                        }
                    }
                    catch (Exception)
                    {
                        // Just catch the exception of corrupt entites
                    }

                    // Test if is valid object and analyze the data
                    try
                    {
                        if (!ent.IsErased && !isDf)
                        {
                            Extents3d ext = ent.Bounds.Value;
                            Point3d p = ext.MinPoint;

                            for (int i = 0; i < 2; i++)
                            {
                                if (p.X < minX)
                                {
                                    minX = p.X;
                                }
                                else if (p.X > maxX)
                                {
                                    maxX = p.X;
                                }
                                if (p.Y < minY)
                                {
                                    minY = p.Y;
                                }
                                else if (p.Y > maxY)
                                {
                                    maxY = p.Y;
                                }
                                p = ext.MaxPoint;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Just catch the exception of corrupt entites
                    }
                }

                double minYtf = minY + 77.0 / scale;

                // Run through the entities again to find the lower right situation for A3(+) drawing frames
                foreach (ObjectId obj in acBlkTblRec)
                {
                    // Get each object as entity and look if it is a BlockReference and get the name
                    Entity ent = obj.GetObject(OpenMode.ForRead) as Entity;

                    bool isDf = false;
                    try
                    {
                        BlockReference block = ent as BlockReference;
                        if (block.Name == DrawingArea.NAME)
                        {
                            isDf = true;
                        }
                    }
                    catch
                    {
                        // Just catch the exception if entity is corrupt
                    }

                    // Test if is valid object and analyze the data
                    try
                    {
                        if (!ent.IsErased && !isDf)
                        {
                            Extents3d ext = ent.Bounds.Value;
                            double y = ext.MinPoint.Y;
                            double x = ext.MaxPoint.X;

                            if (x > maxX - 185.0 / scale && y < minYtf)
                            {
                                minYtf = y;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Just catch the exception if entity is corrupt
                    }
                }

                // Check if the bounds are valid and start drawing the drawing frame
                if (minX < maxX && minY < maxY)
                {
                    Double width = (maxX - minX) * scale;
                    Double height = (maxY - minY) * scale;
                    Point3d center = new Point3d(minX + (width / scale / 2), minY + (height / scale / 2), 0);
                    bool plotScale = false;
                    Double marginWidth = width + 100 / scale;
                    Double marginHeight = height + 100 / scale;
                    Double drawWidth = width;
                    Double drawHeight = height;
                    Double cutHeight = (maxY - minYtf) * scale;
                    Double cutMarginHeight = (maxY - minYtf) * scale + 0.01;

                    // Check for the optimal paperformat
                    if (marginWidth < formats[CA4, CWIDTH] && marginHeight < formats[CA4, CHEIGHT])
                    {
                        Draw(CA4, CVERTICAL);
                        drawWidth = formats[CA4, CWIDTH];
                        drawHeight = formats[CA4, CHEIGHT];
                        DrawingArea.drawingAreaFormatDir = CA4 + CVERTICAL;
                    }
                    else if (marginWidth < formats[CA4, CHEIGHT] && marginHeight < formats[CA4, CWIDTH])
                    {
                        Draw(CA4, CHORIZONTAL);
                        drawWidth = formats[CA4, CHEIGHT];
                        drawHeight = formats[CA4, CWIDTH];
                        DrawingArea.drawingAreaFormatDir = CA4 + CHORIZONTAL;
                    }
                    else if (marginWidth < formats[CA3, CHEIGHT] && cutMarginHeight + 77.0 < formats[CA3, CWIDTH])
                    {
                        Draw(CA3, CHORIZONTAL);
                        center = new Point3d(center.X, minYtf + (cutHeight / scale / 2) - 77.0 / scale / 2, 0);
                        drawWidth = formats[CA3, CHEIGHT];
                        drawHeight = formats[CA3, CWIDTH];
                        DrawingArea.drawingAreaFormatDir = CA3 + CHORIZONTAL;
                    }
                    else
                    {
                        height = cutHeight;
                        marginWidth += 100 / scale;
                        marginHeight = cutHeight + 200 / scale;
                        if (width < formats[CAX, CHEIGHT])
                        {
                            width = formats[CAX, CHEIGHT];
                            marginWidth = width;
                        }
                        if (height + 77.0 < formats[CAX, CWIDTH])
                        {
                            height = formats[CAX, CWIDTH] - 77.0;
                            marginHeight = height;
                        }
                        drawWidth = formats[CAX, CHEIGHT];
                        drawHeight = formats[CAX, CWIDTH] - (minYtf - minY) / scale * 2;
                        Draw(CA3, CHORIZONTAL);
                        center = new Point3d(center.X + (marginWidth - formats[CAX, CHEIGHT]) / scale / 2,
                            minYtf + (height / scale / 2) - (marginHeight - formats[CAX, CWIDTH]) / scale / 2 - 77.0 / scale, 0);
                        plotScale = true;
                        DrawingArea.drawingAreaFormatDir = CAX + CHORIZONTAL;
                    }

                    // Create a new reference of the block to add to model space and create the jig
                    using (BlockReference newBlock = new BlockReference(new Point3d(0, 0, 0),
                        acTrans.GetObject(acBlkTbl[DrawingArea.NAME], OpenMode.ForRead).ObjectId))
                    {

                        // Set the insert position for the block
                        newBlock.Position = new Point3d(center.X - drawWidth / scale / 2,
                            center.Y - drawHeight / scale / 2, 0);

                        // If it is a plot size frame adjust the size of the frame
                        if (plotScale)
                        {
                            Polyline poly = acTrans.GetObject(lineId, OpenMode.ForWrite) as Polyline;
                            poly.SetPointAt(0, new Point2d((formats[CA3, CHEIGHT] - marginWidth) / scale, 0));
                            poly.SetPointAt(4, new Point2d(formats[CA3, CHEIGHT] / scale, (marginHeight + 77.0) / scale));
                            poly.SetPointAt(5, new Point2d((formats[CA3, CHEIGHT] - marginWidth) / scale, (marginHeight + 77.0) / scale));
                        }

                        // Append entity to ModelSpace, tell the transaction and close everything
                        acBlkTblRec.AppendEntity(newBlock);
                        acTrans.AddNewlyCreatedDBObject(newBlock, true);
                        DrawingArea.drawingAreaId = newBlock.ObjectId;
                    }

                    // Commit the changes
                    acTrans.Commit();

                }
                else
                {
                    MessageBox.Show("Keine Objekte als Berechnungsgrundlage gefunden.");
                }
            }
        }*/


        /// <summary>
        /// This method creates a drawing frame of the given format and orientation, whereas the given format
        /// must be one of the constant CA4, CA3, CAX. To allow the user to select the insertion point or the
        /// extends in case of format bigger than A3, the helper class BlockJig is used.
        /// </summary>
        /// <param name="format">the format the new drawing frame shell be of</param>
        /// <param name="direction">the direction of the drawing frame that shell be created</param>
        /// <param name="oldTextFieldSize">specifies of the old (bigger) text field size shell be used</param>
        public static void Create(int format, int direction, bool oldTextFieldSize = false)
        {
            DrawingArea drawingArea = new DrawingArea();
            // Calculate the scale
            double scale;
            if (!DrawingArea.CalculateScale(out scale))
            {
                return;
            }
            drawingArea.scale = scale;

            // Get Document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            // Start the Transaction
            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {

                // Get active BlockTable and BlockTableRecord
                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                // Set flag if its userDefined input
                drawingArea.userDefined = (format == CAX);

                // Delete the current drawing frame

                if (Instance != null) Instance.Delete();
                
                // Get the needed drawing frame to the BlockTable
                drawingArea.Draw(format, direction, oldTextFieldSize);

                BlockReference newBlock = null;
                try
                {
                    // Create a new reference of the block to add to model space and create the jig
                    newBlock = new BlockReference(new Point3d(0, 0, 0),
                        acTrans.GetObject(acBlkTbl[DrawingArea.NAME], OpenMode.ForRead).ObjectId);
                    newBlock.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                    newBlock.LayerId = acDoc.Database.LayerZero;
                    newBlock.Linetype = "Continuous";
                    newBlock.LineWeight = LineWeight.ByLineWeightDefault;

                    BlockJig jig = new BlockJig(newBlock, drawingArea, oldTextFieldSize);
                    PromptResult getPromptResult;

                    // Save the old cross width
                    object oldCrossWidth = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CURSORSIZE");

                    // Run the jig to input the block
                    for (int i = 0; i < 2; i++)
                    {
                        // Rescale the cursor if custom input
                        if (i == 0 && drawingArea.userDefined)
                        {
                            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", 100);
                        }
                        else if (i == 1)
                        {
                            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", oldCrossWidth);
                        }


                        // Set the phase counter of the jig and get it started
                        jig.Counter = i;
                        getPromptResult = acDoc.Editor.Drag(jig);

                        // Handle Cancel or Error
                        if (getPromptResult.Status == PromptStatus.Cancel || getPromptResult.Status == PromptStatus.Error)
                        {
                            return;
                        }
                        // Stop after one run if no custom input
                        else if (!drawingArea.userDefined)
                        {
                            drawingArea.drawingAreaFormatDir = format + direction;
                            break;
                        }
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
                    acTrans.AddNewlyCreatedDBObject(CreateTextfieldRecord(extensionDict, !oldTextFieldSize), true);
                }
                finally
                {
                    newBlock.Dispose();
                }

                // Close transaction
                acTrans.Commit();
                instance = drawingArea;
            }

            // Reset variable
            drawingArea.userDefined = false;
        }


        /// <summary>
        /// Creates a new block definition using of given format and given orientation.
        /// There is no block reference added to modelspace, just the block "Zeichenbereich" is defined.
        /// </summary>
        /// <param name="format">the format the new drawing frame shell be of</param>
        /// <param name="direction">the direction the new drawing frame shell have</param>
        /// <param name="oldTextfieldSize">specifies if the old (bigger) text field size shell be used</param>
        private void Draw(int format, int direction, bool oldTextfieldSize)
        {
            // Get Document and Database
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            
            // Start the Transaction
            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {

                // Get active BlockTable and BlockTableRecord
                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                    OpenMode.ForWrite) as BlockTableRecord;

                // Generate the new block
                using (BlockTableRecord newBlockDef = new BlockTableRecord())
                {
                    newBlockDef.Name = DrawingArea.NAME;

                    // Add the new block to the BlockTable
                    acBlkTbl.Add(newBlockDef);
                    acTrans.AddNewlyCreatedDBObject(newBlockDef, true);

                    // Get some data: width and height
                    double width = formats[format, CWIDTH];
                    double height = formats[format, CHEIGHT];

                    // Swap height and width if drawing frame shell be horizontal
                    if (direction == CHORIZONTAL)
                    {
                        double temp = width;
                        width = height;
                        height = temp;
                    }

                    // Set the input origin lower right
                    newBlockDef.Origin = new Point3d(width / scale, 0, 0);
                    
                    // Create a polyline as the drawing frame
                    using (Polyline poly = new Polyline())
                    {
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, 0), 0, 0, 0);
                        if (format == CA4)
                        {
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(width / scale, 0), 0, 0, 0);
                        }
                        else
                        {
                            double[] tfSize = oldTextfieldSize ? textfieldSizeOld : textfieldSize;
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d((width - tfSize[CWIDTH]) / scale, 0), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d((width - tfSize[CWIDTH]) / scale, tfSize[CHEIGHT] / scale), 0, 0, 0);
                            poly.AddVertexAt(poly.NumberOfVertices, new Point2d(width / scale, tfSize[CHEIGHT] / scale), 0, 0, 0);
                        }
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(width / scale, height / scale), 0, 0, 0);
                        poly.AddVertexAt(poly.NumberOfVertices, new Point2d(0, height / scale), 0, 0, 0);
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

            // Get Document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            // Start the Transaction
            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                // Get active BlockTable and BlockTableRecord
                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                BlockReference oldBlock = acTrans.GetObject(drawingArea.drawingAreaId, OpenMode.ForRead) as BlockReference;
                Point3d oldPosition = oldBlock.Position;

                // Set flag if its userDefined input
                drawingArea.userDefined = true;
                
                // Delete the current drawing frame
                if (Instance != null) Instance.Delete();
                drawingArea.Draw(CAX, CHORIZONTAL, oldTextfieldSize);

                BlockReference newBlock = null;
                try
                {
                    // Create a new reference of the block to add to model space and create the jig
                    newBlock = new BlockReference(new Point3d(0, 0, 0),
                        acTrans.GetObject(acBlkTbl[DrawingArea.NAME], OpenMode.ForRead).ObjectId);
                    newBlock.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                    newBlock.LayerId = acDoc.Database.LayerZero;
                    newBlock.Linetype = "Continuous";
                    newBlock.LineWeight = LineWeight.ByLineWeightDefault;

                    BlockJig jig = new BlockJig(newBlock, drawingArea, oldTextfieldSize);
                    double[] tfSize = oldTextfieldSize ? textfieldSizeOld : textfieldSize;
                    jig.InsertionPoint = oldPosition.Subtract(new Vector3d(tfSize[CWIDTH] / drawingArea.Scale, -tfSize[CHEIGHT] / drawingArea.Scale, 0));
                    jig.ForceUpdate();
                    jig.Counter = 1;

                    PromptResult getPromptResult;

                    int oldFormatDir = drawingArea.drawingAreaFormatDir;
                    // Set the phase counter of the jig and get it started
                    getPromptResult = acDoc.Editor.Drag(jig);

                    // Handle Cancel or Error
                    if (getPromptResult.Status == PromptStatus.Cancel || getPromptResult.Status == PromptStatus.Error)
                    {
                        drawingArea.drawingAreaFormatDir = oldFormatDir;
                        return;
                    }

                    // Append the new block to the TableRecord and tell the transaction
                    acBlkTblRec.AppendEntity(newBlock);
                    acTrans.AddNewlyCreatedDBObject(newBlock, true);

                    // Save ID and format + direction
                    drawingArea.drawingAreaId = newBlock.ObjectId;

                    // Add an xRec with the scale
                    newBlock.CreateExtensionDictionary();
                    DBDictionary extensionDict = acTrans.GetObject(newBlock.ExtensionDictionary, OpenMode.ForWrite) as DBDictionary;
                    acTrans.AddNewlyCreatedDBObject(CreateScaleRecord(extensionDict, drawingArea.Scale), true);
                    acTrans.AddNewlyCreatedDBObject(CreateTextfieldRecord(extensionDict, !oldTextfieldSize), true);
                }
                finally
                {
                    newBlock.Dispose();
                }

                // Close transaction
                acTrans.Commit();
            }

            // Reset variable
            drawingArea.userDefined = false;

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

            public Point3d InsertionPoint
            {
                get { return insertionPoint; }
                set { insertionPoint = value; }
            }

            private Point3d targetPoint;
            private int counter;
            
            /// <summary>
            /// The counter of the current run of the jig
            /// </summary>
            public int Counter
            {
                set { counter = value; }
            }
            private double initialWidth = 0.0;
            private bool isA4 = false;
            private double[] tfSize;
            private DrawingArea drawingArea;

            /// <summary>
            /// Initiates a new BlockJig for the given BlockReference.
            /// </summary>
            /// <param name="br">the BlockReference to be positioned (and sized)</param>
            /// <param name="oldTextfieldSize">specifies if the old (bigger) text field size shell be used</param>
            public BlockJig(BlockReference br, DrawingArea drawingArea, bool oldTextfieldSize) : base(br)
            {
                tfSize = oldTextfieldSize ? textfieldSizeOld : textfieldSize;
                this.drawingArea = drawingArea;

                // Get the document
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                // Set the insertionPoint at a position
                insertionPoint = br.Position;

                // Get width, height and scale if custom input
                if (drawingArea.userDefined)
                {
                    using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
                    {
                        Polyline poly = acTrans.GetObject(drawingArea.lineId, OpenMode.ForRead) as Polyline;
                        initialWidth = poly.GetPoint2dAt(3).X - poly.GetPoint2dAt(0).X;
                    }
                }
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

                // Set the isA4 flag to default
                isA4 = false;

                // Switch the phase of input
                switch (counter)
                {
                    case 0:
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

                    case 1:
                        // Get the targetPoint
                        PromptPointResult getPointResult1 = prompts.AcquirePoint("\nZielpunkt angeben:  ");
                        Point3d oldPoint1 = targetPoint;
                        targetPoint = getPointResult1.Value;

                        if (drawingArea.userDefined)
                        {
                            targetPoint = new Point3d(targetPoint.X - tfSize[CWIDTH] / drawingArea.Scale, targetPoint.Y + tfSize[CHEIGHT] / drawingArea.Scale, 0);
                        }

                        // Return NoChange if difference is to low to avoid flimmering
                        if (targetPoint.DistanceTo(oldPoint1) < 0.001)
                        {
                            return SamplerStatus.NoChange;
                        }

                        // Make a lot of decisions to show the right minimum size of the drawing frame
                        if (insertionPoint.X - targetPoint.X < formats[CA4, CHEIGHT] / drawingArea.Scale
                            && targetPoint.Y - insertionPoint.Y < formats[CA4, CWIDTH] / drawingArea.Scale)
                        {
                            drawingArea.drawingAreaFormatDir = CA4 + CHORIZONTAL;
                            targetPoint = new Point3d(
                                insertionPoint.X - formats[CA4, CHEIGHT] / drawingArea.Scale,
                                insertionPoint.Y + formats[CA4, CWIDTH] / drawingArea.Scale, 0);
                            isA4 = true;
                        }
                        else if (insertionPoint.X - targetPoint.X < formats[CA4, CWIDTH] / drawingArea.Scale
                            && targetPoint.Y - insertionPoint.Y < formats[CA4, CHEIGHT] / drawingArea.Scale)
                        {
                            drawingArea.drawingAreaFormatDir = CA4 + CVERTICAL;
                            targetPoint = new Point3d(
                                insertionPoint.X - formats[CA4, CWIDTH] / drawingArea.Scale,
                                insertionPoint.Y + formats[CA4, CHEIGHT] / drawingArea.Scale, 0);
                            isA4 = true;
                        }
                        else if (insertionPoint.X - targetPoint.X < formats[CA3, CHEIGHT] / drawingArea.Scale
                            && targetPoint.Y - insertionPoint.Y < formats[CA3, CWIDTH] / drawingArea.Scale)
                        {
                            drawingArea.drawingAreaFormatDir = CA3 + CHORIZONTAL;
                            targetPoint = new Point3d(
                                insertionPoint.X - formats[CA3, CHEIGHT] / drawingArea.Scale,
                                insertionPoint.Y + formats[CA3, CWIDTH] / drawingArea.Scale, 0);
                        }
                        else if (targetPoint.Y - insertionPoint.Y < formats[CA3, CWIDTH] / drawingArea.Scale)
                        {
                            drawingArea.drawingAreaFormatDir = CAX + CHORIZONTAL;
                            targetPoint = new Point3d(targetPoint.X,
                                insertionPoint.Y + formats[CA3, CWIDTH] / drawingArea.Scale, 0);
                        }
                        else if (insertionPoint.X - targetPoint.X < formats[CA3, CHEIGHT] / drawingArea.Scale)
                        {
                            drawingArea.drawingAreaFormatDir = CAX + CHORIZONTAL;
                            targetPoint = new Point3d(insertionPoint.X - formats[CA3, CHEIGHT] / drawingArea.Scale,
                                targetPoint.Y, 0);
                        }

                        if (insertionPoint.X - targetPoint.X > formats[CAMAX, CWIDTH] / drawingArea.Scale)
                        {
                            targetPoint = new Point3d(insertionPoint.X - formats[CAMAX, CWIDTH] / drawingArea.Scale, targetPoint.Y, 0);
                        }
                        if (targetPoint.Y - insertionPoint.Y > formats[CAMAX, CHEIGHT] / drawingArea.Scale)
                        {
                            targetPoint = new Point3d(targetPoint.X, insertionPoint.Y + formats[CAMAX, CHEIGHT] / drawingArea.Scale, 0);
                        }
                        

                        // Return that everything is fine
                        return SamplerStatus.OK;
                }

                // Catch the case if there are more runs than "0" and "1" and return OK
                return SamplerStatus.OK;
            }


            public void ForceUpdate()
            {
                Update();
            }

            /// <summary>
            /// Updates this BlockJig.
            /// Changes the insertionPoint or the position of the polyline-vertices according to the
            /// current user input.
            /// </summary>
            /// <returns>true if everything is okay</returns>
            protected override bool Update()
            {
                // Get the document
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                // Switch the phase we are on
                switch (counter)
                {
                    case 0:
                        // Set the new cursor position as insertionPoint
                        if (!drawingArea.userDefined)
                        {
                            ((BlockReference)this.Entity).Position = insertionPoint;
                        }
                        else
                        {
                            ((BlockReference)this.Entity).Position = new Point3d(insertionPoint.X + tfSize[CWIDTH] / drawingArea.Scale,
                                insertionPoint.Y - tfSize[CHEIGHT] / drawingArea.Scale, 0);
                        }

                        break;

                    case 1:
                        // Update the block data depending on the current format (A4 or bigger) and set the new size
                        using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
                        {
                            Polyline poly = acTrans.GetObject(drawingArea.lineId, OpenMode.ForWrite) as Polyline;
                            poly.SetPointAt(0, new Point2d(targetPoint.X - insertionPoint.X + initialWidth, 0));
                            poly.SetPointAt(4, new Point2d(initialWidth, targetPoint.Y - insertionPoint.Y));
                            poly.SetPointAt(5, new Point2d(targetPoint.X - insertionPoint.X + initialWidth,
                                targetPoint.Y - insertionPoint.Y));
                            if (isA4)
                            {
                                poly.SetPointAt(2, new Point2d(initialWidth, 0));
                                poly.SetPointAt(1, new Point2d(initialWidth, 0));
                            }
                            else
                            {
                                poly.SetPointAt(2, new Point2d(initialWidth - tfSize[CWIDTH] / drawingArea.Scale, tfSize[CHEIGHT] / drawingArea.Scale));
                                poly.SetPointAt(1, new Point2d(initialWidth - tfSize[CWIDTH] / drawingArea.Scale, 0));
                            }
                            acTrans.Commit();
                        }
                        break;
                }

                // Return that everything is fine
                return true;
            }

        }
    }
}