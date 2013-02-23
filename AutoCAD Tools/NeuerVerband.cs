using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using acadDatabase = Autodesk.AutoCAD.DatabaseServices.Database;

namespace AutoCADTools
{
    /// <summary>
    /// This class provides windows and methods to create a new bracing.
    /// </summary>
    public partial class UFVerband : Form
    {
        private static Point3d basePoint, lengthPoint, widthPoint;
        private static bool traufeAtBase = true;
        private const int NODIRECTION = -1;
        private const int HORIZONTAL = 0;
        private const int VERTICAL = 1;
        private static int direction = NODIRECTION;
        private static double firstAbstandTB = 0.10;
        private static double traufAbstandTB = 0.10;
        private static int feldzahlTB = 0;
        private static String positionTB = "";
        private static bool endstabCB = true;
        private static double laengeTB = 0.0;
        private static double breiteTB = 0.0;


        /// <summary>
        /// Inititates a new windows to create a bracing.
        /// </summary>
        public UFVerband()
        {
            InitializeComponent();
            TBfirstabstand.Text = firstAbstandTB.ToString();
            TBtraufabstand.Text = traufAbstandTB.ToString();
            if (feldzahlTB != 0)
            {
                TBfeldzahl.Text = feldzahlTB.ToString();
            }
            TBposition.Text = positionTB;
            CBendstab.Checked = endstabCB;
            berechneMaße();
        }


        /// <summary>
        /// Hides the window and asks the user to input the extends for the bracing.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void Baendern_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Get the active document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            acadDatabase acCurDb = acDoc.Database;

            // Create a prompt for user input
            PromptPointOptions getPointOptions = new PromptPointOptions("");
            PromptPointResult getPointResult;

            // Make the three points to input
            Point3d startPoint = new Point3d(0, 0, 0);
            Point3d endPoint1 = new Point3d(0, 0, 0);
            Point3d endPoint2 = new Point3d(0, 0, 0);

            // Save snapMode and set new mode
            object oldSnapMode = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("OSMODE");
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("OSMODE", 4135);
            object oldAutoSnap = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("AUTOSNAP");

            // Get the startPoint
            getPointOptions.Message = "\nStartpunkt angeben (Traufe): ";
            getPointResult = acDoc.Editor.GetPoint(getPointOptions);
            switch (getPointResult.Status)
            {
                case PromptStatus.Cancel:
                    Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("OSMODE", oldSnapMode);
                    this.Show();
                    return;
                case PromptStatus.OK: 
                    startPoint = getPointResult.Value; 
                    break;
            }

            // Turn ortoMode on
            bool oldOrthoMode = acCurDb.Orthomode;
            acCurDb.Orthomode = true;
            
            // Get the first endPoint
            getPointOptions.Message = "\nErsten Endpunkt angeben: ";
            getPointOptions.BasePoint = startPoint;
            getPointOptions.UseBasePoint = true;
            getPointOptions.UseDashedLine = true;
            getPointResult = acDoc.Editor.GetPoint(getPointOptions);
            switch (getPointResult.Status)
            {
                case PromptStatus.Cancel:
                    Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("OSMODE", oldSnapMode);
                    acCurDb.Orthomode = oldOrthoMode;
                    this.Show();
                    return;
                case PromptStatus.OK: 
                    endPoint1 = getPointResult.Value; 
                    break;
            }

            // Get the second endPoint
            getPointOptions.Message = "\nZweiten Endpunkt angeben: ";
            getPointOptions.BasePoint = startPoint;
            getPointOptions.UseBasePoint = true;
            getPointOptions.UseDashedLine = true;
            getPointResult = acDoc.Editor.GetPoint(getPointOptions);
            
            // Reset orthoMode and snapMode
            acCurDb.Orthomode = oldOrthoMode;
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("OSMODE", oldSnapMode);
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("AUTOSNAP", oldAutoSnap);

            switch (getPointResult.Status)
            {
                case PromptStatus.Cancel:
                    this.Show();
                    return;
                case PromptStatus.OK: 
                    endPoint2 = getPointResult.Value; 
                    break;
            }
            
            // Calculate basePoint, lengthPoint, widthPoint and direction
            basePoint = new Point3d(Math.Min(endPoint1.X, endPoint2.X),
                Math.Min(endPoint1.Y, endPoint2.Y), 0);

            if (basePoint.Y == startPoint.Y)
            {
                traufeAtBase = true;
            }
            else
            {
                traufeAtBase = false;
            }

            if (Math.Abs(endPoint1.X - endPoint2.X) < Math.Abs(endPoint1.Y - endPoint2.Y))
            {
                direction = VERTICAL;
                lengthPoint = new Point3d(basePoint.X,
                    basePoint.Y + Math.Abs(endPoint1.Y - endPoint2.Y), 0);
                widthPoint = new Point3d(basePoint.X + Math.Abs(endPoint1.X - endPoint2.X),
                    basePoint.Y, 0);
            }
            else if (Math.Abs(endPoint1.X - endPoint2.X) > Math.Abs(endPoint1.Y - endPoint2.Y))
            {
                direction = HORIZONTAL;
                lengthPoint = new Point3d(basePoint.X + Math.Abs(endPoint1.X - endPoint2.X),
                    basePoint.Y, 0);
                widthPoint = new Point3d(basePoint.X,
                    basePoint.Y + Math.Abs(endPoint1.Y - endPoint2.Y), 0);
            }
            else
            {
                MessageBox.Show("Die Punkte liegen auf derselben Achse",
                    "Eingabefehler", MessageBoxButtons.OK);
                this.Show();
                return;
            }

            // Calculate length, width and so on
            berechneMaße();
            Baendern.Text = "Ändern";
            this.Show();

        }


        /// <summary>
        /// Calculates length and width of the bracing and sets the text of the textboxes
        /// for length, width and direction.
        /// </summary>
        private void berechneMaße()
        {
            if (direction == VERTICAL)
            {
                breiteTB = widthPoint.X - basePoint.X;
                TBbreite.Text = breiteTB.ToString();
                laengeTB = lengthPoint.Y - basePoint.Y - double.Parse(TBfirstabstand.Text)
                    - double.Parse(TBtraufabstand.Text);
                TBlaenge.Text = laengeTB.ToString();
                Lausrichtung.Text = "Ausrichtung: Vertikal";
            }
            else if (direction == HORIZONTAL)
            {
                breiteTB = widthPoint.Y - basePoint.Y;
                TBbreite.Text = breiteTB.ToString();
                laengeTB = lengthPoint.X - basePoint.X - double.Parse(TBfirstabstand.Text)
                    - double.Parse(TBtraufabstand.Text);
                TBlaenge.Text = laengeTB.ToString();
                Lausrichtung.Text = "Ausrichtung: Horizontal";
            }
        }


        /// <summary>
        /// Drawing the bracing based on the input data.
        /// If the number of field or the points are not valid an error is shown and the command is stopped.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void Bzeichnen_Click(object sender, EventArgs e)
        {
            // Get the active Document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            
            // Look for errors
            if (String.IsNullOrEmpty(TBfeldzahl.Text) || TBfeldzahl.Text == "0")
            {
                MessageBox.Show("Anzahl der Felder fehlerhaft", "Eingabefehler", MessageBoxButtons.OK);
                return;
            }

            if (direction == NODIRECTION)
            {
                MessageBox.Show("Abmessungspunkte wurden nicht definiert", "Eingabefehler", MessageBoxButtons.OK);
                return;
            }

            this.Hide();

            // Lock the document to draw
            using (DocumentLock acLock = acDoc.LockDocument())
            {
                // Start transaction
                using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
                {
                    // Open the Block table for read
                    BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId,
                               OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                  OpenMode.ForWrite) as BlockTableRecord;

                    // Calculate the start and end distances
                    double firstAbstand;
                    double traufAbstand;
                    if (traufeAtBase)
                    {
                        traufAbstand = traufAbstandTB;
                        firstAbstand = firstAbstandTB;
                    }
                    else
                    {
                        traufAbstand = firstAbstandTB;
                        firstAbstand = traufAbstandTB;
                    }
                    
                    // Calculate the points
                    double breite = breiteTB;
                    double laenge = laengeTB;
                    Point3d startPoint, endPoint;
                    Point3d widthStartPoint, widthEndPoint;
                    
                    if (direction == VERTICAL)
                    {
                        startPoint = new Point3d(basePoint.X, basePoint.Y + traufAbstand, 0);
                        endPoint = new Point3d(basePoint.X, lengthPoint.Y - firstAbstand, 0);
                        
                        widthStartPoint = new Point3d(startPoint.X + breite,
                            startPoint.Y, 0);
                        widthEndPoint = new Point3d(endPoint.X + breite,
                            endPoint.Y, 0);
                    }
                    else
                    {
                        startPoint = new Point3d(basePoint.X + traufAbstand, basePoint.Y, 0);
                        endPoint = new Point3d(lengthPoint.X - firstAbstand, basePoint.Y, 0);
                        
                        widthStartPoint = new Point3d(startPoint.X,
                            startPoint.Y + breite, 0);
                        widthEndPoint = new Point3d(endPoint.X,
                            endPoint.Y + breite, 0);
                    }

                    // Draw end verticals
                    if (CBendstab.Checked == true)
                    {
                        using (Line linie = new Line())
                        {
                            linie.SetDatabaseDefaults();
                            linie.StartPoint = startPoint;
                            linie.EndPoint = widthStartPoint;
                            acBlkTblRec.AppendEntity(linie);
                            acTrans.AddNewlyCreatedDBObject(linie, true);
                        }
                        using (Line linie = new Line())
                        {
                            linie.SetDatabaseDefaults();
                            linie.StartPoint = endPoint;
                            linie.EndPoint = widthEndPoint;
                            acBlkTblRec.AppendEntity(linie);
                            acTrans.AddNewlyCreatedDBObject(linie, true);
                        }
                    }

                    // Calculate fields and width of fields
                    double feldzahl = feldzahlTB;
                    double feldgr = laenge / feldzahl;

                    int counter = 0;
                    Point3d fsStartPoint = startPoint;
                    Point3d fsEndPoint;

                    // Create the fields
                    while (counter < feldzahl)
                    {
                        if (direction == VERTICAL)
                        {
                            fsEndPoint = new Point3d(fsStartPoint.X + breite, fsStartPoint.Y + feldgr, 0);
                        }
                        else
                        {
                            fsEndPoint = new Point3d(fsStartPoint.X + feldgr, fsStartPoint.Y + breite, 0);
                        }
                        using (Line linie = new Line())
                        {
                            linie.SetDatabaseDefaults();
                            linie.StartPoint = fsStartPoint;
                            linie.EndPoint = fsEndPoint;
                            acBlkTblRec.AppendEntity(linie);
                            acTrans.AddNewlyCreatedDBObject(linie, true);
                        }
                        fsStartPoint = fsEndPoint;
                        breite = -breite;
                        counter++;
                    }
                    breite = Math.Abs(breite);
                    
                    
                    // Create the position text and locate it correctly
                    if (!String.IsNullOrEmpty(TBposition.Text))
                    {
                        Point3d point;
                        if (direction == HORIZONTAL)
                        {
                            point = new Point3d(startPoint.X + laenge / 2, startPoint.Y + breite / 2, 0);
                        }
                        else
                        {
                            point = new Point3d(startPoint.X + breite / 2, startPoint.Y + laenge / 2, 0);
                        }

                        if (feldzahl % 2 != 0)
                        {
                            if (direction == HORIZONTAL)
                            {
                                point = new Point3d(point.X + feldgr / 2, point.Y, 0);

                            }
                            else
                            {
                                point = new Point3d(point.X, point.Y + feldgr / 2, 0);
                            }
                        }

                        if (feldzahl % 4 == 1 || feldzahl % 4 == 2)
                        {
                            if (direction == HORIZONTAL)
                            {
                                point = new Point3d(point.X, point.Y - breite / 8.0, 0);
                            }
                            else
                            {
                                point = new Point3d(point.X - breite / 8.0, point.Y, 0);
                            }
                        }
                        else
                        {
                            if (direction == HORIZONTAL)
                            {
                                point = new Point3d(point.X, point.Y + breite / 8.0, 0);
                            }
                            else
                            {
                                point = new Point3d(point.X + breite / 8.0, point.Y, 0);
                            }
                        }

                        using (MText text = new MText())
                        {
                            text.Annotative = AnnotativeStates.True;
                            text.Contents = TBposition.Text;
                            text.SetAttachmentMovingLocation(AttachmentPoint.MiddleCenter);
                            text.Location = point;
                            acBlkTblRec.AppendEntity(text);
                            acTrans.AddNewlyCreatedDBObject(text, true);


                            double radius = 0.0;
                            switch (TBposition.Text.Length)
                            {
                                case 1: radius = text.ActualHeight * 0.9; break;
                                case 2: radius = text.ActualHeight * 1.3; break;
                                case 3: radius = text.ActualHeight * 1.5; break;
                            }

                            using (Circle circle = new Circle(point, new Vector3d(0, 0, 1), radius))
                            {
                                circle.Linetype = "Continuous";
                                acBlkTblRec.AppendEntity(circle);
                                acTrans.AddNewlyCreatedDBObject(circle, true);
                            }
                        }
                    }

                    // Commit the changes
                    acTrans.Commit();
                }
            }
        }


        /// <summary>
        /// Ensures that the input in Traufabstand and Firstabstand are just numbers
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void TBtraufabstand_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberedInput(e, true);
        }


        /// <summary>
        /// A numbered input validation.
        /// If the input key is not a number or comma if allowed by parameter, the event is
        /// marked as handled.
        /// </summary>
        /// <param name="e">the event arguments</param>
        /// <param name="commaAllowed">tell if comma is allowed for input</param>
        private static void NumberedInput(KeyPressEventArgs e, bool commaAllowed)
        {
            if ((int)e.KeyChar == 8 || ((int)e.KeyChar >= 48 && (int)e.KeyChar < 58)
                || (commaAllowed && (int)e.KeyChar == 46))
            {
                return;
            }
            else
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Saves the input value of Traufabstand to a persistent variable.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void TBtraufabstand_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TBtraufabstand.Text))
            {
                traufAbstandTB = double.Parse(TBtraufabstand.Text);
                berechneMaße();
            }
        }


        /// <summary>
        /// Saves the input value of Firstabstand to a persistent variable.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void TBfirstabstand_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TBfirstabstand.Text))
            {
                firstAbstandTB = double.Parse(TBfirstabstand.Text);
                berechneMaße();
            }
        }


        /// <summary>
        /// Saves the number of fields to a persistent variable.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void TBfeldzahl_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(TBfeldzahl.Text)) feldzahlTB = int.Parse(TBfeldzahl.Text);
        }


        /// <summary>
        /// Saves the position textbox to a persistent variable.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void TBposition_TextChanged(object sender, EventArgs e)
        {
            positionTB = TBposition.Text;
        }


        /// <summary>
        /// Saves the checked state of the Enstäbe checkbox to a persistent variable.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void CBendstab_CheckedChanged(object sender, EventArgs e)
        {
            endstabCB = CBendstab.Checked;
        }


        /// <summary>
        /// Validates the input for the number of fields for numbered input wihout commas.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void TBfeldzahl_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberedInput(e, false);
        }


        /// <summary>
        /// Sets the Traufabstand to 0.10 if the textbox is left and empty.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void TBtraufabstand_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TBtraufabstand.Text)) TBtraufabstand.Text = "0.10";
        }


        /// <summary>
        /// Sets the Firstabstand to 0.10 if the textbox is left and empty.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void TBfirstabstand_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TBfirstabstand.Text)) TBfirstabstand.Text = "0.10";
        }


        /// <summary>
        /// Closes the dialog on pressing the escape key.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void UFVerband_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

    }
}