using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using acadDatabase = Autodesk.AutoCAD.DatabaseServices.Database;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// UFRispe is the class that handles the UserForm and the progress of drawing Rispen in a drawing.
    /// </summary>
    public partial class FrmDiagonalBracing : Form
    {
        #region Static Members

        private enum InputType { MiddlePoint, ThirdsPoint, DirectInput };

        private static String pos = "3";
        private static String descr = "RiBd 40/1.5";
        private static int panicleCount = 1;
        private static int panicleDistance = 10;
        private static InputType inputType = InputType.ThirdsPoint;
        private static string BlockName
        {
            get
            {
                return BLOCK_PREFIX + pos.Length.ToString() + Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CANNOSCALEVALUE").ToString()
                    + Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.Clayer + Properties.Settings.Default.DiagonalBracingDescriptionBlack;
            }
        }

        #endregion

        #region Constants

        private const string POSITION_TAG = "POSITION";
        private const string DESCRIPTION_TAG = "BESCHREIBUNG";
        private const string OBJECT_SNAP_MODE = "OSMODE";
        private const string BLOCK_PREFIX = "Panicle";

        #endregion

        #region Loading

        /// <summary>
        /// Panicle is the UserForm for creating a Panicle that lets the user input a position number and a description which is
        /// automatically added to the line of the Panicle.
        /// </summary>
        public FrmDiagonalBracing()
        {
            InitializeComponent();
        }

        private void FrmDiagonalBracing_Load(object sender, EventArgs e)
        {
            txtPosition.Text = pos;
            cboDescription.Text = descr;
            updCount.Value = panicleCount;
            txtDistance.Text = panicleDistance.ToString();
            optThirdsPoint.Checked = inputType == InputType.ThirdsPoint;
            optMiddlePoint.Checked = inputType == InputType.MiddlePoint;
            optDirectInput.Checked = inputType == InputType.DirectInput;
        }

        #endregion

        #region Panicle Creation

        /// <summary>
        /// Starts creating Panicles based on the made inputs.
        /// Asks the user to define start and endpoint for each Panicle.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void butCreate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPosition.Text) || String.IsNullOrEmpty(cboDescription.Text))
            {
                if (MessageBox.Show(LocalData.DiagonalBracingMissingDescriptionText, LocalData.DiagonalBracingMissingDescriptionTitle,
                    MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            this.Hide();

            // Save the value for next time
            pos = txtPosition.Text;
            descr = cboDescription.Text;
            panicleCount = (int)updCount.Value;
            panicleDistance = int.Parse(txtDistance.Text);
            inputType = optThirdsPoint.Checked ? InputType.ThirdsPoint : (optMiddlePoint.Checked ? InputType.MiddlePoint : InputType.DirectInput);

            // Get the current document and database
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            acadDatabase acCurDb = acDoc.Database;

            object oldSnapMode = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable(OBJECT_SNAP_MODE);
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable(OBJECT_SNAP_MODE, 4135);

            while (true)
            {
                // Start a transaction
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    // Open the Block table for read
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                        OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                        OpenMode.ForWrite) as BlockTableRecord;

                    // Look if block definition for current position text length exists
                    if (!acBlkTbl.Has(BlockName))
                    {
                        BlockTableRecord textBlockTable = new BlockTableRecord();
                        textBlockTable.Name = BlockName;
                        acBlkTbl.UpgradeOpen();
                        acBlkTbl.Add(textBlockTable);
                        acTrans.AddNewlyCreatedDBObject(textBlockTable, true);

                        // dummy text for dimensions
                        MText dummyText = new MText();
                        dummyText.Annotative = AnnotativeStates.True;
                        dummyText.Contents = pos;
                        acBlkTblRec.AppendEntity(dummyText);
                        acTrans.AddNewlyCreatedDBObject(dummyText, true);
                        
                        Double radius = 0.0;
                        Point3d location = new Point3d(dummyText.ActualHeight * pos.Length / 2, dummyText.ActualHeight * 0.8, 0);

                        // Place and add the position with circle if wanted
                        if (!String.IsNullOrEmpty(pos))
                        {
                            using (AttributeDefinition attrPos = new AttributeDefinition())
                            {
                                attrPos.Annotative = AnnotativeStates.True;
                                attrPos.Justify = AttachmentPoint.MiddleCenter;
                                attrPos.AlignmentPoint = location;
                                attrPos.Tag = POSITION_TAG;
                                attrPos.LockPositionInBlock = true;

                                textBlockTable.AppendEntity(attrPos);
                                acTrans.AddNewlyCreatedDBObject(attrPos, true);
                            }

                            // Add the circle
                            radius = Math.Max(dummyText.ActualHeight, dummyText.ActualWidth) * 0.5 + dummyText.ActualHeight * 0.45;

                            using (Circle circle = new Circle(location, new Vector3d(0, 0, 1), radius))
                            {
                                textBlockTable.AppendEntity(circle);
                                acTrans.AddNewlyCreatedDBObject(circle, true);
                            }

                            location += new Vector3d(radius * 1.3, 0, 0);
                        }

                        dummyText.Erase();
                        dummyText.Dispose();

                        // Place and add the description if wanted
                        AttributeDefinition attrDescription = new AttributeDefinition();
                        attrDescription.Annotative = AnnotativeStates.True;
                        attrDescription.Justify = AttachmentPoint.MiddleLeft;
                        attrDescription.AlignmentPoint = location;
                        attrDescription.Tag = DESCRIPTION_TAG;
                        if (Properties.Settings.Default.DiagonalBracingDescriptionBlack)
                            attrDescription.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                        attrDescription.LockPositionInBlock = true;
                        textBlockTable.AppendEntity(attrDescription);
                        acTrans.AddNewlyCreatedDBObject(attrDescription, true);
                        attrDescription.Dispose();
                    }

                    // Initiate jig with lines
                    PanicleLineJig jig = new PanicleLineJig(inputType == InputType.ThirdsPoint ? 1.0d / 3 : 0.5d, panicleDistance / 100d, inputType != InputType.DirectInput);

                    Line line = null;
                    for (int i = 0; i < panicleCount; i++)
                    {
                        Line currentLine = new Line();
                        currentLine.SetDatabaseDefaults();
                        acBlkTblRec.AppendEntity(currentLine);
                        acTrans.AddNewlyCreatedDBObject(currentLine, true);
                        jig.AddLine(currentLine);
                        if (i == 0) line = currentLine;
                    }

                    // Run jig
                    if (!jig.RunTillComplete(acDoc.Editor, acTrans))
                    {
                        acTrans.Commit();
                        break;
                    }

                    // Add reference to model space and align it
                    BlockTableRecord ms = (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    double angle = Math.Cos(line.Angle) < 0 ? line.Angle + Math.PI : line.Angle;
                    Point3d startPoint = Math.Cos(line.Angle) < 0 ? line.EndPoint : line.StartPoint;
                    Point3d endPoint = Math.Cos(line.Angle) < 0 ? line.StartPoint : line.EndPoint;
                    BlockReference textRef = new BlockReference(new Point3d(startPoint.X + Math.Cos(angle) * ((endPoint - startPoint).Length / 8.0),
                                        startPoint.Y + Math.Sin(angle) * ((endPoint - startPoint).Length / 8.0), 0), acBlkTbl[BlockName]);
                    textRef.Rotation = angle;
                    ms.AppendEntity(textRef);
                    acTrans.AddNewlyCreatedDBObject(textRef, true);

                    // Update attributes
                    foreach (ObjectId id in (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockName], OpenMode.ForRead))
                    {
                        using (AttributeDefinition attDef = id.GetObject(OpenMode.ForRead) as AttributeDefinition)
                        {
                            if ((attDef != null) && (!attDef.Constant))
                            {
                                //This is a non-constant AttributeDefinition
                                //Create a new AttributeReference
                                using (AttributeReference attRef = new AttributeReference())
                                {
                                    attRef.SetAttributeFromBlock(attDef, textRef.BlockTransform);
                                    if (attRef.Tag == POSITION_TAG)
                                    {
                                        attRef.TextString = pos;
                                    }
                                    else if (attRef.Tag == DESCRIPTION_TAG)
                                    {
                                        attRef.TextString = panicleCount > 1 ? panicleCount + " x " + cboDescription.Text :
                                            cboDescription.Text;
                                    }

                                    //Add the AttributeReference to the BlockReference
                                    textRef.AttributeCollection.AppendAttribute(attRef);
                                    acTrans.AddNewlyCreatedDBObject(attRef, true);
                                }
                            }
                        }
                    }

                    acTrans.Commit();
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable(OBJECT_SNAP_MODE, oldSnapMode);

        }

        #endregion

        #region Handler

        /// <summary>
        /// Handles the escape and enter keypress to exit the window or start drawing Rispen.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void FrmDiagonalBracing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                this.Close();
            }
        }


        /// <summary>
        /// Handles the key press in the distance box to allow only numbers
        /// </summary>
        /// <param name="sender">the sender of the key press</param>
        /// <param name="e">the event args</param>
        private void txtDistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Processes the count update. If there is only one panicle needed, disable the distance field.
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void updCount_ValueChanged(object sender, EventArgs e)
        {
            txtDistance.Enabled = updCount.Value > 1;
        }

        /// <summary>
        /// Processes that direct input selection changed. If direct input is selected, disable the selector for the number of panicles and reset the number to 1.
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void optDirectInput_CheckedChanged(object sender, EventArgs e)
        {
            if (optDirectInput.Checked)
            {
                updCount.Value = 1;
            }

            updCount.Enabled = !optDirectInput.Checked;
        }

        #endregion

        #region Jig

        /// <summary>
        /// This is a Jig helper class.
        /// This can be used to select the position or in case of format bigger than A3 the size of the
        /// drawing frame to create.
        /// </summary>
        class PanicleLineJig : DrawJig
        {
            // Set variables for points, the phase counter, width, height and scale of the block
            public const byte firstPointIndex = 0;
            public const byte secondPointIndex = 1;
            public const byte firstEndIndex = 0;
            public const byte secondEndIndex = 1;
            private Point3d[,] endPoints;
            private int currentPointIndex;
            private int currentEndIndex;
            private int numberOfPanicles;
            private double distance;
            private double pointFactor;
            private List<Line> lines;
            private bool interpolatedInput;

            /// <summary>
            /// Initiates a new PanicleLineJig for defining a panicle with a specified factor for the start-/endpoint
            /// as offset to the baseline and with a defined distance between the panicles.
            /// </summary>
            /// <param name="pointFactor">factor of base line length at which to start/end panicle</param>
            /// <param name="distance">the distance between two panicles if there is more than 1</param>
            /// <param name="interpolatedInput">specifies if there shell be used two points that are interpolated according to the pointfactor. If false, the pointFactor is not used.</param>
            public PanicleLineJig(double pointFactor, double distance = 0.0d, bool interpolatedInput = true)
            {
                this.endPoints = new Point3d[2, 2];
                this.currentPointIndex = 0;
                this.currentEndIndex = 0;
                this.distance = interpolatedInput ? distance : 1;
                this.lines = new List<Line>();
                this.pointFactor = pointFactor;
                this.interpolatedInput = interpolatedInput;
                if (interpolatedInput && distance < 0.00001) throw new ArgumentException("Distance is too small.");
            }

            /// <summary>
            /// Adds a line for a panicle to this jug
            /// </summary>
            /// <param name="line">the line to add</param>
            public void AddLine(Line line)
            {
                this.lines.Add(line);
                this.numberOfPanicles = lines.Count;
            }

            /// <summary>
            /// Draws the entities
            /// </summary>
            /// <param name="draw">the WorldDraw</param>
            /// <returns>true, if successfull</returns>
            protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
            {
                Update();

                WorldGeometry geo = draw.Geometry;
                if (geo != null)
                {
                    foreach (Line ent in lines)
                    {
                        geo.Draw(ent);
                    }
                }

                return true;
            }

            /// <summary>
            /// Samples the current jig status.
            /// Therefore the user is asked to input the currently needed point
            /// </summary>
            /// <param name="prompts">the JigPrompts to use</param>
            /// <returns>the current SamplerStatus, will be NoChange or OK</returns>
            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                if (prompts == null) return SamplerStatus.Cancel;

                // Combine the prompt message and options
                JigPromptPointOptions promptOptions = new JigPromptPointOptions();
                String queryString = Environment.NewLine;
                switch (currentPointIndex)
                {
                    case firstPointIndex:
                        queryString += LocalData.DiagonalBracingFirstPoint;
                        break;
                    case secondPointIndex:
                        queryString += LocalData.DiagonalBracingSecondPoint;
                        break;
                }
                queryString += LocalData.Whitespace;
                switch (currentEndIndex)
                {
                    case firstEndIndex:
                        queryString += LocalData.DiagonalBracingFirstAddition;
                        break;
                    case secondEndIndex:
                        queryString += LocalData.DiagonalBracingSecondsAddition;
                        promptOptions.UseBasePoint = true;
                        promptOptions.BasePoint = endPoints[currentPointIndex, firstPointIndex];
                        break;
                }

                promptOptions.Message = queryString;
                PromptPointResult getPointResult = prompts.AcquirePoint(promptOptions);
                Point3d oldPoint = endPoints[currentPointIndex, currentEndIndex];
                endPoints[currentPointIndex, currentEndIndex] = getPointResult.Value;

                // Return NoChange if difference is to low to avoid flimmering
                if (endPoints[currentPointIndex, currentEndIndex].DistanceTo(oldPoint) < 0.001)
                {
                    return SamplerStatus.NoChange;
                }

                // Otherwise return OK
                return SamplerStatus.OK;

            }

            public void ForceUpdate()
            {
                Update();
            }

            /// <summary>
            /// Updates this PanicleLineJig.
            /// Changes the draw panicle draft according to the already input points.
            /// </summary>
            /// <returns>true if everything is okay</returns>
            protected bool Update()
            {
                // Get the document
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                if (currentPointIndex == firstPointIndex) return true;

                switch (currentEndIndex)
                {
                    case firstEndIndex:
                        {
                            Vector3d differenceVector = endPoints[firstPointIndex, secondEndIndex].Subtract(endPoints[firstPointIndex, firstEndIndex].GetAsVector()).GetAsVector();
                            Vector3d vectorTwoPanicles = differenceVector / differenceVector.Length * distance;
                            Point3d startPoint = endPoints[secondPointIndex, firstEndIndex].Subtract(vectorTwoPanicles * (numberOfPanicles - 1) / 2);
                            uint index = 0;
                            foreach (Line line in lines)
                            {
                                line.EndPoint = startPoint + index++ * vectorTwoPanicles;
                            }
                            break;
                        }
                    case secondPointIndex:
                        {
                            Vector3d differenceVector = endPoints[secondPointIndex, secondEndIndex].Subtract(endPoints[secondPointIndex, firstEndIndex].GetAsVector()).GetAsVector();
                            Vector3d vectorTwoPanicles = differenceVector / differenceVector.Length * distance;
                            Point3d startPoint = endPoints[secondPointIndex, firstEndIndex].Add(differenceVector * (1 - pointFactor)).Subtract(vectorTwoPanicles * (numberOfPanicles - 1) / 2);
                            uint index = 0;
                            foreach (Line line in lines)
                            {
                                line.EndPoint = startPoint + index++ * vectorTwoPanicles;
                            }

                            break;
                        }
                }

                // Return that everything is fine
                return true;
            }

            /// <summary>
            /// A method for running the jig until all inputs are done.
            /// </summary>
            /// <param name="ed">the editor used</param>
            /// <param name="tr">the transaction to work in</param>
            /// <returns></returns>
            internal bool RunTillComplete(Editor ed, Transaction tr)
            {
                // Perform the jig operation in a loop
                while (true)
                {
                    var res = ed.Drag(this);

                    if (res.Status == PromptStatus.OK)
                    {
                        // If start and endpoint are equal, ask again
                        if (!(currentEndIndex == secondEndIndex && endPoints[currentPointIndex, firstEndIndex] == endPoints[currentPointIndex, secondEndIndex]))
                        {
                            // If we are not at the end, update state
                            if (!(currentPointIndex == secondPointIndex && currentEndIndex == secondEndIndex) && !(!interpolatedInput && currentPointIndex == secondPointIndex))
                            {
                                // Progress the phase
                                if (currentEndIndex == secondEndIndex)
                                {
                                    currentPointIndex++;
                                    currentEndIndex = 0;
                                    Vector3d differenceVector = endPoints[firstPointIndex, secondEndIndex].Subtract(endPoints[firstPointIndex, firstEndIndex].GetAsVector()).GetAsVector();
                                    Vector3d vectorTwoPanicles = differenceVector / differenceVector.Length * distance;
                                    Point3d startPoint = endPoints[firstPointIndex, firstEndIndex].Add(differenceVector * pointFactor).Subtract(vectorTwoPanicles * (numberOfPanicles - 1) / 2);
                                    uint index = 0;
                                    foreach (Line line in lines)
                                    {
                                        line.StartPoint = startPoint + index++ * vectorTwoPanicles;
                                    }
                                }
                                else if (!interpolatedInput)
                                {
                                    currentPointIndex++;
                                    foreach (Line line in lines)
                                    {
                                        line.StartPoint = endPoints[firstPointIndex, firstEndIndex];
                                    }
                                }
                                else
                                {
                                    currentEndIndex++;
                                }
                            }
                            else
                            {
                                // Everything is done
                                return true;
                            }
                        }
                    }
                    else
                    {
                        // The user has cancelled: returning aborts the transaction
                        foreach (Line l in lines)
                        {
                            l.Erase();
                        }
                        return false;
                    }
                }
            }
        }

        #endregion

    }
}
