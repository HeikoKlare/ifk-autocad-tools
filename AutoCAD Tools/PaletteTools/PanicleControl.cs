using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using acadDatabase = Autodesk.AutoCAD.DatabaseServices.Database;

namespace AutoCADTools.Tools
{
    public partial class PanicleControl : UserControl
    {
        private static String pos = "3";
        private static String descr = "RiBd 60/2,0";

        public Palettes ParentPalette
        {
            get;
            set;
        }

        /// <summary>
        /// UFRispe is the UserForm for creating a Rispe that lets the user input a position number and a description which is
        /// automatically added to the line of the Rispe.
        /// </summary>
        public PanicleControl()
        {
            InitializeComponent();
            txtPosition.Text = pos;
            cmbDescription.Text = descr;
        }


        /// <summary>
        /// Starts creating Rispen based on the made inputs.
        /// Asks the user to define start and endpoint for each Rispe.
        /// After drawing one, pressing enter lets it start at the last endpoint.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void butCreate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPosition.Text) || String.IsNullOrEmpty(cmbDescription.Text))
            {
                if (MessageBox.Show("Position oder Beschreibung fehlt.\n Fortfahren?", "Fortfahren?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if (ParentPalette != null)
            {
                //ParentPalette.minimizePaniclePalette();
            }

            // Save the value for next time
            pos = txtPosition.Text;
            descr = cmbDescription.Text;

            // Get the current document and database
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            acadDatabase acCurDb = acDoc.Database;

            Point3d p1, p2;
            Point3d saved = new Point3d();
            PromptPointResult getPointResult;
            PromptPointOptions getPointOptions = new PromptPointOptions("");
            Boolean draw;

            getPointOptions.AllowArbitraryInput = true;
            getPointOptions.UseDashedLine = true;

            object oldSnapMode = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("OSMODE");
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("OSMODE", 4135);

            while (true)
            {
                draw = true;
                // Prompt for the start point
                getPointOptions.UseBasePoint = false;
                getPointOptions.Message = "\nErsten Punkt für Rispe angeben: ";
                getPointResult = acDoc.Editor.GetPoint(getPointOptions);
                // Handle the input
                switch (getPointResult.Status)
                {
                    case PromptStatus.Cancel:
                        Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("OSMODE", oldSnapMode);
                        return;
                    case PromptStatus.OK:
                        p1 = getPointResult.Value; 
                        break;
                    default: 
                        p1 = saved; 
                        break;
                }
               
                // Prompt for the end point
                getPointOptions.Message = "\nZweiten Punkt für Rispe angeben: ";
                getPointOptions.BasePoint = p1;
                getPointOptions.UseBasePoint = true;
                getPointResult = acDoc.Editor.GetPoint(getPointOptions);
                // Handle the input
                p2 = new Point3d();
                switch (getPointResult.Status)
                {
                    case PromptStatus.Cancel:
                        Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("OSMODE", oldSnapMode);
                        return;
                    case PromptStatus.OK: 
                        p2 = getPointResult.Value; 
                        break;
                    default: 
                        if (getPointResult.StringResult.ToLower() == "z")
                        {
                            draw = false;
                        } 
                        else 
                        {
                            return;
                        }
                        break;
                }

                // Save the last point to use as beginning for next Rispe
                saved = p2;

                // Swap Points so direction is from bottom left to top right
                if (p1.X > p2.X || (p1.X == p2.X && p1.Y > p2.Y))
                {
                    Point3d temp = p1;
                    p1 = p2;
                    p2 = temp;
                }

                if (draw)
                {
                    using (DocumentLock acLckDoc = acDoc.LockDocument())
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


                            // Define the new line
                            Line line = null;

                            if (chkDouble.Checked)
                            {
                                if (chkVertical.Checked)
                                {
                                    p1 = p1.Subtract(new Vector3d(0, -0.05f, 0));
                                    p2 = p2.Subtract(new Vector3d(0, -0.05f, 0));
                                }
                                else
                                {
                                    p1 = p1.Subtract(new Vector3d(0.05f, 0, 0));
                                    p2 = p2.Subtract(new Vector3d(0.05f, 0, 0));
                                }

                                line = new Line(p1, p2);
                                line.SetDatabaseDefaults();
                                acBlkTblRec.AppendEntity(line);
                                acTrans.AddNewlyCreatedDBObject(line, true);

                                if (chkVertical.Checked)
                                {
                                    p1 = p1.Subtract(new Vector3d(0, 0.1f, 0));
                                    p2 = p2.Subtract(new Vector3d(0, 0.1f, 0));
                                }
                                else
                                {
                                    p1 = p1.Subtract(new Vector3d(-0.1f, 0, 0));
                                    p2 = p2.Subtract(new Vector3d(-0.1f, 0, 0));
                                }
                            }
                            
                            line = new Line(p1, p2);
                            line.SetDatabaseDefaults();
                            acBlkTblRec.AppendEntity(line);
                            acTrans.AddNewlyCreatedDBObject(line, true);

                            // AttachmentPoint for the description and radius for position
                            MText dummyText = new MText();
                            dummyText.Annotative = AnnotativeStates.True;
                            dummyText.Contents = "1,0";
                            acBlkTblRec.AppendEntity(dummyText);
                            dummyText.Erase();
                            p1 = new Point3d(p1.X + Math.Cos(line.Angle) * (line.Length / 8.0) - Math.Sin(line.Angle) * (dummyText.ActualHeight * 0.8),
                                                p1.Y + Math.Sin(line.Angle) * (line.Length / 8.0) + Math.Cos(line.Angle) * (dummyText.ActualHeight * 0.8), 0);
                            

                            Double radius = 0.0;

                            // Place and add the position with circle if wanted
                            if (!String.IsNullOrEmpty(txtPosition.Text))
                            {
                                MText position = new MText();
                                position.Annotative = AnnotativeStates.True;
                                position.SetAttachmentMovingLocation(AttachmentPoint.MiddleCenter);
                                position.Location = p1;
                                position.Width = 1;
                                position.Contents = txtPosition.Text;
                                position.Rotation = line.Angle;
                                
                                acBlkTblRec.AppendEntity(position);
                                acTrans.AddNewlyCreatedDBObject(position, true);

                                // Add the circle
                                switch (txtPosition.Text.Length)
                                {
                                    case 1: radius = position.ActualHeight * 0.9; break;
                                    case 2: radius = position.ActualHeight * 1.3; break;
                                    case 3: radius = position.ActualHeight * 1.5; break;
                                }
                                Circle circle = new Circle(p1, new Vector3d(0, 0, 1), radius);
                                acBlkTblRec.AppendEntity(circle);
                                acTrans.AddNewlyCreatedDBObject(circle, true);

                                position.Dispose();
                                circle.Dispose();
                            }

                            // Place and add the description if wanted
                            if (!String.IsNullOrEmpty(cmbDescription.Text))
                            {
                                MText description = new MText();
                                description.Annotative = AnnotativeStates.True;
                                description.SetAttachmentMovingLocation(AttachmentPoint.MiddleLeft);
                                description.Location = new Point3d(p1.X + Math.Cos(line.Angle) * (radius + 0.4 * dummyText.ActualHeight), 
                                    p1.Y + Math.Sin(line.Angle) * (radius + 0.4 * dummyText.ActualHeight), 0);
                                description.Width = 10;
                                description.Contents = chkDouble.Enabled && chkDouble.Checked ? "2 x " + cmbDescription.Text :
                                    cmbDescription.Text;
                                description.Rotation = line.Angle;
                                description.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7);
                                acBlkTblRec.AppendEntity(description);
                                acTrans.AddNewlyCreatedDBObject(description, true);
                                description.Dispose();
                            }

                            line.Dispose();
                            dummyText.Dispose();
                            acTrans.Commit();
                        }
                    }
                    Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("OSMODE", oldSnapMode);
                }
            }
        }


        /// <summary>
        /// Handles the escape and enter keypress to exit the window or start drawing Rispen.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void Panicle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Hide();
            }
        }

        private void chkDouble_CheckedChanged(object sender, EventArgs e)
        {
            chkVertical.Enabled = chkDouble.Checked; 
        }
    }
}
