using System;
using System.Drawing;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using AutoCADTools.Tools;
using AutoCADTools.PrintLayout;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace AutoCADTools
{
    /// <summary>
    /// This class provides the window and methods to create a new layout.
    /// There is the possibility to create simple layout or layouts with textfields basing on
    /// the database and using existing drawing frames for the layout extends.
    /// </summary>
    public partial class UFLayoutErstellen : Form
    {
        private bool silent;
        private Point3d startpoint;
        private Point3d endpoint;
        private const int CA4 = 0;
        private const int CA3 = 1;
        private const int CAX = 2;
        private const int CA4O = 3;
        private const int CA3O = 4;
        private const int CAXO = 5;
        private const int CWIDTH = 0;
        private const int CHEIGHT = 1;
        private double scale = 0.0;
        private double[,] viewports = new double[,] { { 171.028, 250.0}, { 395.0, 287.0 }, { 395.0, 287.0 },
                                                     { 297.0, 210.0 }, { 420.0, 297.0 }, { 420.0, 297.0} };
        private double[,] borders = new double[,] { { 175.0, 281.0 }, { 420.0, 297.0 }, { 420.0, 297.0 },
                                                    { 297.0, 210.0}, { 420.0, 297.0 }, { 420.0, 297.0 }};
        private double[,] insertionView = new double[,] { { 26.986, 11.0 }, { 20.0, 5.0 }, { 40.0, 25.0},
                                                        { 0.0, 0.0 }, { 0.0, 0.0 }, { 20.0, 20.0 } };
        private double[,] insertionBorder = new double[,] { { 25.0, 9.0 }, { 0.0, 0.0 }, { 20.0, 20.0},
                                                        { 0.0, 0.0 }, { 0.0, 0.0 }, { 20.0, 20.0 } };
        private String dfFormat;
        
        
        private static List<string> printerNames;
        private static Printer printer;
        private bool standardTemplate = true;

        /// <summary>
        /// Initiates a new instance of the create layout class. If silent is checked, the command will be executed without showing a dialog.
        /// </summary>
        /// <param name="silent">if true, default parameters are used and no dialog is shown</param>
        public UFLayoutErstellen(bool silent = false)
        {
            Initialize();
            this.silent = silent;
        }

        /// <summary>
        /// Initiates a new instance of the create layout class. It looks if the user is in modelspace and will
        /// otherwise give a message and stop the command.
        /// Initiates the components and loads the available printers.
        /// Creating this form should just be enabled in ModelSpace, otherwise the results are probably nor as wanted.
        /// </summary>
        public void Initialize()
        {
            InitializeComponent();
            
            // Add the configurations
            CBconfig.Items.Add("IFK");
            CBconfig.Items.Add("Hellmuth");
            CBconfig.Text = "IFK";

            // Get the current document
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                                    
            // Set the coordinate system to WCS
            if (!(document.Editor.CurrentUserCoordinateSystem.Equals(Matrix3d.Identity)))
            {
                document.Editor.CurrentUserCoordinateSystem = Matrix3d.Identity;
                MessageBox.Show("Koordinatensystem auf WCS zurückgesetzt");
            }
            
            // Get the annotation scales an add them to the scale combobox
            ObjectContextCollection occ = document.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            foreach (AnnotationScale annoScale in occ)
            {
                CBmassstab.Items.Add(annoScale.DrawingUnits.ToString());
            }
            
            // Reset drawing frame format and try to find it
            this.dfFormat = "";
            
            var drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            DrawingArea drawingArea = drawingAreaWrapper.DrawingArea;

            // Load the printers if not done this session and add them
            loadPrinters();
            for (int i = 0; i < printerNames.Count; i++)
            {
                CBdrucker.Items.Add(printerNames[i]);
            }
            if (CBdrucker.Items.Count > 0) CBdrucker.SelectedIndex = 0;

            // Look for drawing frame and textfields to enable the right options
            using (Transaction acTrans = document.Database.TransactionManager.StartTransaction())
            {
                // Get BlockTable and the BlockTableRecord of the active layout
                BlockTable acBlkTbl = acTrans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                // If there is no drawing frame or the textfields are not defined, disable the option
                if (!AusschnittErkennen() || !acBlkTbl.Has("Textfeld A4") || !(acBlkTbl.Has("Textfeld A3+") || acBlkTbl.Has("Textfeld A3+ klein")))
                {
                    if (!acBlkTbl.Has("Textfeld A4") || !(acBlkTbl.Has("Textfeld A3+") || acBlkTbl.Has("Textfeld A3+ klein")))
                    {
                        standardTemplate = false;
                        CBkopf.Visible = false;
                        CBkopf.Checked = false;
                    }
                    RBmanuell.Checked = true;
                    RBzeichenbereich.Enabled = false;
                }
                else
                {
                    RBzeichenbereich_CheckedChanged(null, null);
                }
            }
            
            // Get the current scale
            scale = drawingArea != null ? drawingArea.Scale : 0.0d;
            if (scale == 0.0)
            {
                scale = double.Parse(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable(
                    "CANNOSCALEVALUE").ToString());
            }
            else
            {
                scale /= int.Parse(TBeinheit.Text);
            }

            // Set the predefined scale to the calculated one
            CBmassstab.Text = Math.Round(1.0 / scale).ToString();
        }


        /// <summary>
        /// Loads all available printers and their paperformats to use them for the dialog
        /// </summary>
        private static void loadPrinters()
        {
            // Get the devices
            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            System.Collections.Specialized.StringCollection devlist = psv.GetPlotDeviceList();
            printerNames = new List<string>();
            // Initialize a printer array and start counting and adding the printers
            foreach (var device in devlist)
            {
                // Get the devices and look for those ending with "pc3" and add them to the array
                if (!device.Contains("Default")
                    && device.Length > 4 && device.Substring(device.Length - 4, 4) == ".pc3")
                {
                    //MessageBox.Show("Printer found: " + device);
                    try
                    {
                        printerNames.Add(device.Substring(0, device.Length - 4));
                        //printer.Add(ÜP(device.Substring(0, device.Length - 4)));
                    }
                    catch (System.ArgumentException)
                    {
                        // Just catch the exception when adding a null- or empty printer
                    }
                }
            }

        }


        /// <summary>
        /// Command to manually create the extends of the wanted layout.
        /// The user is asked to choose lower left extend and upper right extend.
        /// </summary>
        /// <param name="sender">the object sending the event to start the command</param>
        /// <param name="e">the event arguments</param>
        private void BausschnittErstellen_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Get the document and save and set cursorsize very big
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            object oldCrossWidth = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CURSORSIZE");
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", 100);

            // Define to points to get from user
            Point3d p1 = new Point3d(0, 0, 0);
            Point3d p2 = new Point3d(0, 0, 0);

            // Create prompt
            PromptPointOptions getPointOptions = new PromptPointOptions("");
            PromptPointResult getPointResult;

            // Get the first point of the layout extends
            getPointOptions.Message = "Anfangspunkt angeben: ";
            getPointResult = acDoc.Editor.GetPoint(getPointOptions);
            if (getPointResult.Status == PromptStatus.OK)
            {
                p1 = getPointResult.Value;
            }
            else
            {
                // On error stop
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", oldCrossWidth);
                this.Show();
                return;
            }

            // Get the last point of the layout extends and show line from first point
            getPointOptions.Message = "Endpunkt angeben: ";
            getPointOptions.BasePoint = p1;
            getPointOptions.UseBasePoint = true;
            getPointOptions.UseDashedLine = true;
            getPointResult = acDoc.Editor.GetPoint(getPointOptions);
            // Set cursorsize to normal
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", oldCrossWidth);
            if (getPointResult.Status == PromptStatus.OK)
            {
                p2 = getPointResult.Value;
            }
            else
            {
                // On error stop
                this.Show();
                return;
            }

            // Set startpoint to minimum extends and endpoint to maximum extends
            startpoint = new Point3d(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), 0);
            endpoint = new Point3d(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y), 0);
            Lausschnitt.Text = "Ausschnitt festgelegt";
            Lausschnitt.ForeColor = Color.Green;

            this.Show();
        }


        /// <summary>
        /// Tries to automatically find the wanted extract for the layout.
        /// Therefore the drawing area is used. If there is no drawing area created yet it is
        /// created using the "ZeichenbereichAuto"-command.
        /// </summary>
        /// <returns>true if a drawing frame exists, false if not</returns>
        private bool AusschnittErkennen()
        {
            // A bool to indicate wheather a drawing frame already exists or not
            bool dfExists = false;

            var document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            DrawingArea drawingArea = drawingAreaWrapper.DrawingArea;

            if (drawingArea != null && !drawingArea.DrawingAreaId.IsErased)
            {
                // Found so set return value true
                dfExists = true;

                PaperformatTextfield format = drawingArea.Format;
                if (format is PaperformatTextfieldA4Vertical)
                {
                    CBdrucker.SelectedIndex = CBdrucker.FindStringExact("Konica");
                    RBhochformat.Checked = true;
                    this.dfFormat = "A4";
                    CBdrehen.Checked = false;
                }
                else if (format is PaperformatTextfieldA4Horizontal)
                {
                    CBdrucker.SelectedIndex = CBdrucker.FindStringExact("Konica");
                    RBhochformat.Checked = true;
                    this.dfFormat = "A4";
                    CBdrehen.Checked = true;
                }
                else if (format is PaperformatTextfieldA3)
                {
                    CBdrucker.SelectedIndex = CBdrucker.FindStringExact("Konica");
                    RBquerformat.Checked = true;
                    this.dfFormat = "A3";
                    CBdrehen.Checked = false;
                }
                else
                {
                    CBdrucker.SelectedIndex = CBdrucker.FindStringExact("Plotter");
                    RBquerformat.Checked = true;
                    this.dfFormat = "A0";
                    CBdrehen.Checked = false; 
                }
                CBdrucker_SelectedIndexChanged(null, null);
                                
                using (Transaction acTrans = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
                {
                    // Get the block an the bounds
                    BlockReference block = acTrans.GetObject(drawingArea.DrawingAreaId, OpenMode.ForRead) as BlockReference;
                    startpoint = block.Bounds.Value.MinPoint;
                    endpoint = block.Bounds.Value.MaxPoint;
                }
            }
            return dfExists;
        }


        /// <summary>
        /// Creates a new layout when clicking the "create" button.
        /// This function just calls the "CreateLayout" method.
        /// </summary>
        /// <param name="sender">the object sending the event to start the command</param>
        /// <param name="e">the event arguments</param>
        private void Berstellen_Click(object sender, EventArgs e)
        {
            var document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            DrawingArea drawingArea = drawingAreaWrapper.DrawingArea;

            // An indicator wheter an error occured
            bool error = false;
            try
            {
                // Look if there is some error with the printer or the paperformat
                if (CBoptimiertePapierformate.Checked 
                    && printer.GetPaperformats(true)[CBpapierformat.SelectedIndex].Name != CBpapierformat.Text)
                {
                    MessageBox.Show("Das ausgewählte Papierformat kann nicht gefunden werden.");
                    error = true;
                }
                else if (CBoptimiertePapierformate.Checked
                    && printer.GetPaperformats(true)[CBpapierformat.SelectedIndex].Name != CBpapierformat.Text)
                {
                    MessageBox.Show("Das ausgewählte Papierformat kann nicht gefunden werden.");
                    error = true;
                }
                // Look if the given data are correct
                else if (String.IsNullOrEmpty(TBeinheit.Text))
                {
                    MessageBox.Show("Angabe der Zeichnungseinheit fehlerhaft", "Eingabefehler");
                    error = true;
                }
                else if (startpoint.X == endpoint.X && startpoint.Y == endpoint.Y)
                {
                    MessageBox.Show("Eckpunkte nicht definiert", "Eingabefehler");
                    error = true;
                }
                else if (String.IsNullOrEmpty(TBlayout.Text))
                {
                    MessageBox.Show("Layoutname fehlt", "Eingabefehler");
                    error = true;
                }
                else if (String.IsNullOrEmpty(CBpapierformat.Text))
                {
                    MessageBox.Show("Papierformat nicht angegeben", "Eingabefehler");
                    error = true;
                }
                // Look if paperformat fits drawing frame
                else if (RBzeichenbereich.Checked)
                {
                    PaperformatTextfield format = drawingArea.Format;
                    if (format is PaperformatTextfieldA4 && CBpapierformat.Text != "A4")
                    {
                        MessageBox.Show("Papierformat entspricht nicht dem Zeichenbereich. Muss \"A4\" sein");
                        error = true;
                    }
                    else if (format is PaperformatTextfieldA3 && CBpapierformat.Text != "A3")
                    {
                        MessageBox.Show("Papierformat entspricht nicht dem Zeichenbereich. Muss \"A3\" sein");
                        error = true;
                    }
                    else if (format is PaperformatTextfieldCustom && (CBpapierformat.Text == "A4" || CBpapierformat.Text == "A3"))
                    {
                        MessageBox.Show("Papierformat entspricht nicht dem Zeichenbereich. Muss größer als \"A3\" sein");
                        error = true;
                    }
                }
                // Ask to overwrite layout if already existing and delete the old one
                if (!LayoutManager.Current.GetLayoutId(TBlayout.Text).IsNull && !error)
                {
                    if (MessageBox.Show("Layoutname existiert bereits. Layout überschreiben?", "Überschreiben?",
                        MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        error = true;
                    }
                    else
                    {
                        LayoutManager.Current.DeleteLayout(TBlayout.Text);
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Ein unbekannter Fehler ist aufgetreten. Bitte den Softwareentwickler kontaktieren");
                error = true;
            }

            if (!error)
            {
                this.Hide();
                try
                {
                    CreateLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ein außergewöhnlicher Fehler ist aufgetreten. Versuchen Sie es erneut "
                        + "oder starten Sie gegebenenfalls das Programm neu. Sollte der Fehler weiterhin auftreten "
                        + "kontaktieren Sie den Entwickler mit folgenden Angaben: \n"
                        + ex.StackTrace);
                }
            }
        }


        /// <summary>
        /// Creates the defined layout.
        /// Given error messages if there are mistakes in the drawing unit, the scale, if no corners are defined,
        /// if there is no layoutname or no paperformat and then will stop the command.
        /// If the layout already exists a warning is given and asked to overwrite it.
        /// </summary>
        /// <returns>true if successfully created layout</returns>
        private bool CreateLayout()
        {
            // Get the current document
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                                    
            // Create the new layout and activate it
            LayoutManager.Current.CreateLayout(TBlayout.Text);
            LayoutManager.Current.CurrentLayout = TBlayout.Text;
            
            // Start the transaction
            using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
            {
                // Get BlockTable and the BlockTableRecord of the active layout
                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord layout = acTrans.GetObject(acDoc.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                // Get the current layout and create new plot settings copied from current layouts one
                Layout acLayout = acTrans.GetObject(LayoutManager.Current.GetLayoutId(LayoutManager.Current.CurrentLayout),
                    OpenMode.ForWrite) as Layout;
                
                    // Get the paperformat
                    String paperformat;
                    paperformat = printer.GetPaperformats(CBoptimiertePapierformate.Checked)[CBpapierformat.SelectedIndex].FormatName;

                    // Get current PlotSettingsValidator and set printer and format
                    PlotSettingsValidator psv = PlotSettingsValidator.Current;
                    psv.SetPlotConfigurationName(acLayout, CBdrucker.Text + ".pc3", paperformat);

                    // Erase the existing object in the new layout (especially the automatically created viewport)
                    foreach (ObjectId id in layout)
                    {
                        id.GetObject(OpenMode.ForWrite).Erase();
                    }

                    // Get the current drawing units and scale
                    double unit = double.Parse(TBeinheit.Text);
                    scale = 1.0 / double.Parse(CBmassstab.Text);

                    // Set the plot settings and validate them
                    psv.RefreshLists(acLayout);
                    if (CBdrucker.Text == "PNG")
                    {
                        psv.SetPlotPaperUnits(acLayout, PlotPaperUnit.Pixels);
                        psv.SetCustomPrintScale(acLayout, new CustomScale(unit * 10, 1));
                    }
                    else
                    {
                        psv.SetPlotPaperUnits(acLayout, PlotPaperUnit.Millimeters);
                        psv.SetCustomPrintScale(acLayout, new CustomScale(unit, 1));
                    }
                    psv.SetPlotType(acLayout, Autodesk.AutoCAD.DatabaseServices.PlotType.Layout);
                    

                    // Regenerate the layout after setting plot settings
                    //acDoc.Editor.Regen();

                    // Get size and margins of the layout
                    Double width = acLayout.PlotPaperSize.X;
                    Double height = acLayout.PlotPaperSize.Y;
                    Point2d m1 = acLayout.PlotPaperMargins.MinPoint;
                    //Point2d m2 = acLayout.PlotPaperMargins.MaxPoint;

                    // Divide the size and margins by unit or set to 0 if no margins wanted
                    width /= unit;
                    height /= unit;
                    if (CBpapierformat.Text == "A4" || CBpapierformat.Text == "A3")
                    {
                        m1 = new Point2d(m1.X / unit, m1.Y / unit);
                        //m2 = new Point2d(m2.X / unit, m2.Y / unit);
                    }
                    else
                    {
                        m1 = new Point2d();
                        //m2 = new Point2d();
                    }

                    // Get the used format and set extends if A3+ format
                    int format = 0;
                    if (CBpapierformat.Text == "A4")
                    {
                        if (CBkopf.Checked)
                        {
                            format = CA4;
                        }
                        else
                        {
                            format = CA4O;
                        }
                    }
                    else if (CBpapierformat.Text == "A3")
                    {
                        if (CBkopf.Checked)
                        {
                            format = CA3;
                        }
                        else
                        {
                            format = CA3O;
                        }
                    }
                    else
                    {
                        if (CBkopf.Checked)
                        {
                            format = CAX;
                            viewports[format, CWIDTH] = (endpoint.X - startpoint.X) * scale * unit;
                            viewports[format, CHEIGHT] = (endpoint.Y - startpoint.Y) * scale * unit;
                            borders[format, CWIDTH] = viewports[format, CWIDTH] + 25.0;
                            borders[format, CHEIGHT] = viewports[format, CHEIGHT] + 10.0;
                        }
                        else
                        {
                            format = CAXO;
                            viewports[format, CWIDTH] = (endpoint.X - startpoint.X) * scale * unit;
                            viewports[format, CHEIGHT] = (endpoint.Y - startpoint.Y) * scale * unit;
                            borders[format, CWIDTH] = viewports[format, CWIDTH];
                            borders[format, CHEIGHT] = viewports[format, CHEIGHT];
                        }
                    }

                    // Get the right rotation and switch width and height if needed
                    if ((width < height && RBhochformat.Checked) || (height < width && RBquerformat.Checked))
                    {
                        psv.SetPlotRotation(acLayout, PlotRotation.Degrees000);
                    }
                    else
                    {
                        psv.SetPlotRotation(acLayout, PlotRotation.Degrees090);
                        Double temp = width;
                        width = height;
                        height = temp;
                        //Point2d tempP = m1;
                        //m1 = m2;
                        //m2 = tempP;
                    }

                    // Turn the predefined borders if it is a layout without textfield but verticl
                    if ((format == CA4O || format == CA3O) && RBhochformat.Checked)
                    {
                        Double temp = borders[format, CWIDTH];
                        borders[format, CWIDTH] = borders[format, CHEIGHT];
                        borders[format, CHEIGHT] = temp;
                        temp = viewports[format, CWIDTH];
                        viewports[format, CWIDTH] = viewports[format, CHEIGHT];
                        viewports[format, CHEIGHT] = temp;
                    }

                    // Set plot settings again after changing rotation
                    acLayout.CopyFrom(acLayout);

                    // Create new viewport and a polyline to use as viewport
                    Viewport PVport = new Viewport();
                    Polyline pViewport = new Polyline();
                    pViewport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                    PVport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);

                    // Set the vertices of the viewport poly
                    pViewport.AddVertexAt(pViewport.NumberOfVertices,
                        new Point2d(insertionView[format, CWIDTH] / unit - m1.X,
                            insertionView[format, CHEIGHT] / unit - m1.Y), 0, 0, 0);
                    if (format == CA3 || format == CAX)
                    {
                        pViewport.AddVertexAt(pViewport.NumberOfVertices,
                            new Point2d((insertionView[format, CWIDTH] + viewports[format, CWIDTH] - 185.0) / unit - m1.X,
                             insertionView[format, CHEIGHT] / unit - m1.Y), 0, 0, 0);
                        pViewport.AddVertexAt(pViewport.NumberOfVertices,
                            new Point2d((insertionView[format, CWIDTH] + viewports[format, CWIDTH] - 185.0) / unit - m1.X,
                                (insertionView[format, CHEIGHT] + 77.0) / unit - m1.Y), 0, 0, 0);
                        pViewport.AddVertexAt(pViewport.NumberOfVertices,
                            new Point2d((insertionView[format, CWIDTH] + viewports[format, CWIDTH]) / unit - m1.X,
                                (insertionView[format, CHEIGHT] + 77.0) / unit - m1.Y), 0, 0, 0);
                    }
                    else
                    {
                        pViewport.AddVertexAt(pViewport.NumberOfVertices,
                           new Point2d((insertionView[format, CWIDTH] + viewports[format, CWIDTH]) / unit - m1.X,
                               insertionView[format, CHEIGHT] / unit - m1.Y), 0, 0, 0);
                    }
                    pViewport.AddVertexAt(pViewport.NumberOfVertices,
                        new Point2d((insertionView[format, CWIDTH] + viewports[format, CWIDTH]) / unit - m1.X,
                            (insertionView[format, CHEIGHT] + viewports[format, CHEIGHT]) / unit - m1.Y), 0, 0, 0);
                    pViewport.AddVertexAt(pViewport.NumberOfVertices,
                        new Point2d(insertionView[format, CWIDTH] / unit - m1.X,
                            (insertionView[format, CHEIGHT] + viewports[format, CHEIGHT]) / unit - m1.Y), 0, 0, 0);
                    pViewport.Closed = true;

                    // Append polyline and viewport and tell transaction about it
                    layout.AppendEntity(pViewport);
                    acTrans.AddNewlyCreatedDBObject(pViewport, true);
                    layout.AppendEntity(PVport);
                    acTrans.AddNewlyCreatedDBObject(PVport, true);

                    // Make viewport non rectangular and set lineweight
                    PVport.NonRectClipEntityId = pViewport.ObjectId;
                    PVport.NonRectClipOn = true;
                    pViewport.LineWeight = LineWeight.LineWeight050;
                    PVport.LineWeight = LineWeight.LineWeight050;

                    // If A4 format set viewport borders white
                    if (format == CA4)
                    {
                        pViewport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.White);
                        PVport.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.White);
                    }

                    // If its a layout with borders and textfield create the border polyline
                    if (CBkopf.Checked)
                    {
                        // Create polyline and add the vertices
                        Polyline pBorder = new Polyline();
                        pBorder.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);

                        pBorder.AddVertexAt(pBorder.NumberOfVertices,
                            new Point2d(insertionBorder[format, CWIDTH] / unit - m1.X,
                                insertionBorder[format, CHEIGHT] / unit - m1.Y), 0, 0, 0);
                        pBorder.AddVertexAt(pBorder.NumberOfVertices,
                            new Point2d((insertionBorder[format, CWIDTH] + borders[format, CWIDTH]) / unit - m1.X,
                                insertionBorder[format, CHEIGHT] / unit - m1.Y), 0, 0, 0);
                        pBorder.AddVertexAt(pBorder.NumberOfVertices,
                            new Point2d((insertionBorder[format, CWIDTH] + borders[format, CWIDTH]) / unit - m1.X,
                                (insertionBorder[format, CHEIGHT] + borders[format, CHEIGHT]) / unit - m1.Y), 0, 0, 0);
                        pBorder.AddVertexAt(pBorder.NumberOfVertices,
                            new Point2d(insertionBorder[format, CWIDTH] / unit - m1.X,
                                (insertionBorder[format, CHEIGHT] + borders[format, CHEIGHT]) / unit - m1.Y), 0, 0, 0);
                        pBorder.Closed = true;

                        // Append polyline and tell transaction about it
                        layout.AppendEntity(pBorder);
                        acTrans.AddNewlyCreatedDBObject(pBorder, true);
                        pBorder.Dispose();

                        // Create a line for the nippel and set start- and endpoint depending on format
                        Line lNippel = new Line();
                        lNippel.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);

                        if (format == CA4)
                        {
                            lNippel.StartPoint = new Point3d((insertionBorder[format, CWIDTH] - 10.0) / unit - m1.X,
                                (insertionBorder[format, CHEIGHT] + borders[format, CHEIGHT] / 2) / unit - m1.Y, 0);
                            lNippel.EndPoint = new Point3d(insertionBorder[format, CWIDTH] / unit - m1.X,
                                (insertionBorder[format, CHEIGHT] + borders[format, CHEIGHT] / 2) / unit - m1.Y, 0);
                        }
                        else
                        {
                            if (borders[format, CHEIGHT] > 297.0)
                            {
                                Line lNippelTrennung1 = new Line();
                                lNippelTrennung1.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                                lNippelTrennung1.StartPoint = new Point3d((insertionView[format, CWIDTH] - 20.0) / unit - m1.X,
                                    (insertionView[format, CHEIGHT] + 292.0) / unit - m1.Y, 0);
                                lNippelTrennung1.EndPoint = new Point3d(insertionView[format, CWIDTH] / unit - m1.X,
                                (insertionView[format, CHEIGHT] + 292.0) / unit - m1.Y, 0);
                                layout.AppendEntity(lNippelTrennung1);
                                acTrans.AddNewlyCreatedDBObject(lNippelTrennung1, true);
                                lNippelTrennung1.Dispose();
                                if (borders[format, CHEIGHT] > 594.0)
                                {
                                    Line lNippelTrennung2 = new Line();
                                    lNippelTrennung2.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                                    lNippelTrennung2.StartPoint = new Point3d((insertionView[format, CWIDTH] - 20.0) / unit - m1.X,
                                        (insertionView[format, CHEIGHT] + 589.0) / unit - m1.Y, 0);
                                    lNippelTrennung2.EndPoint = new Point3d(insertionView[format, CWIDTH] / unit - m1.X,
                                        (insertionView[format, CHEIGHT] + 589.0) / unit - m1.Y, 0);
                                    layout.AppendEntity(lNippelTrennung2);
                                    acTrans.AddNewlyCreatedDBObject(lNippelTrennung2, true);
                                    lNippelTrennung2.Dispose();
                                }
                            }
                            
                            
                            lNippel.StartPoint = new Point3d((insertionView[format, CWIDTH] - 10.0) / unit - m1.X,
                                (insertionView[format, CHEIGHT] + 143.5) / unit - m1.Y, 0);
                            lNippel.EndPoint = new Point3d(insertionView[format, CWIDTH] / unit - m1.X,
                                (insertionView[format, CHEIGHT] + 143.5) / unit - m1.Y, 0);
                        }

                        // Append nippel polyline and tell transaction about it
                        layout.AppendEntity(lNippel);
                        acTrans.AddNewlyCreatedDBObject(lNippel, true);
                        lNippel.Dispose();

                        // If 90° rotation for A4 format is turned on, turn the view and swap width and height
                        if (CBdrehen.Checked)
                        {
                            PVport.TwistAngle = Math.PI / 2;
                            if (format == CA4)
                            {
                                double temp = viewports[format, CWIDTH];
                                viewports[format, CWIDTH] = viewports[format, CHEIGHT];
                                viewports[format, CHEIGHT] = temp;
                            }
                        }

                        // Create a new BlockReference of the right textfield and get the record of the block
                        BlockTableRecord tfBlock;
                        BlockReference text;
                        if (format == CA4)
                        {
                            tfBlock = acTrans.GetObject(acBlkTbl["Textfeld A4"], OpenMode.ForRead) as BlockTableRecord;
                            text = new BlockReference(new Point3d(25 / unit - m1.X, 290 / unit - m1.Y, 0), acBlkTbl["Textfeld A4"]);
                        }
                        else
                        {
                            text = new BlockReference(new Point3d((insertionView[format, CWIDTH] + viewports[format, CWIDTH])
                                / unit - m1.X, insertionView[format, CHEIGHT] / unit - m1.Y, 0), acBlkTbl["Textfeld A3+"]);
                            tfBlock = acTrans.GetObject(acBlkTbl["Textfeld A3+"], OpenMode.ForRead) as BlockTableRecord;
                        }

                        text.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);

                        // Set right scalefactor (should be 0.01)
                        text.ScaleFactors = new Scale3d(10 / unit);

                        // Append the textfield and tell transaction about it
                        layout.AppendEntity(text);
                        acTrans.AddNewlyCreatedDBObject(text, true);

                        // Go through object in record and add a reference of the attributes to the BlockReference
                        foreach (ObjectId id in tfBlock)
                        {
                            AttributeDefinition attDef = acTrans.GetObject(id, OpenMode.ForRead) as AttributeDefinition;
                            if (attDef != null)
                            {
                                using (AttributeReference attRef = new AttributeReference())
                                {
                                    // Position the attribute reference in the block reference
                                    attRef.SetAttributeFromBlock(attDef, text.BlockTransform);
                                    attRef.Position = attDef.Position.TransformBy(text.BlockTransform);

                                    // Set scale or date in the textfield
                                    if (attRef.Tag == "MAßSTÄBE")
                                    {
                                        attRef.TextString = "1:" + (1.0 / scale).ToString();
                                    }
                                    
                                    // Append the attribute and tell transaction about it
                                    text.AttributeCollection.AppendAttribute(attRef);
                                    acTrans.AddNewlyCreatedDBObject(attRef, true);
                                }

                                attDef.Dispose();
                            }
                        }

                        text.Dispose();
                    }
                                        
                    // Turn the viewport on
                    PVport.On = true;
                    
                    


                    // Swap end if turned VP
                    if (CBdrehen.Checked)
                    {
                        // Take care of the turned viewport, x-axis is the original y and the y-axis is the inverted original x
                        PVport.ViewCenter = new Point2d(-(endpoint.Y + startpoint.Y) / 2, (startpoint.X + endpoint.X) / 2);
                        endpoint = new Point3d(startpoint.X + endpoint.Y - startpoint.Y, startpoint.Y + endpoint.X - startpoint.X, 0);
                    }
                    else
                    {
                        // Set the view of the viewport
                        PVport.ViewCenter = new Point2d((endpoint.X + startpoint.X) / 2, (endpoint.Y + startpoint.Y) / 2);
                    }

                    PVport.ViewHeight = endpoint.Y - startpoint.Y;
                    if (!CBexakterAusschnitt.Checked)
                    {
                        PVport.CustomScale = scale;
                    }

                    // Auto-Zoom in paperspace
                    if (!(CBdrucker.Text == "PNG"))
                    {
                        acDoc.SendStringToExecute("_.ZOOM _E ", true, false, true);
                        acDoc.SendStringToExecute("_.ZOOM .8x ", true, false, true);
                    }

                    // Switch to modelspace and back to prevent unwanted situations
                    LayoutManager.Current.CurrentLayout = "Model";
                    LayoutManager.Current.CurrentLayout = TBlayout.Text;

                    // Set the annotation scale for the viewport
                    ObjectContextCollection occ = acDoc.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
                    AnnotationScale annoScale = occ.GetContext("1:" + (1.0 / scale).ToString()) as AnnotationScale;
                    if (annoScale != null)
                    {
                        PVport.AnnotationScale = annoScale;
                    }
                    else
                    {
                        MessageBox.Show("Keine passende Skalierung gefunden!");
                    }

                    // Dispose the used objects
                    PVport.Dispose();
                    pViewport.Dispose();

                // using acPlSet

                // Close everything and commit changes
                acTrans.Commit();
            }

            return true;
        }

                
        /// <summary>
        /// Handles the change of the predefined config.
        /// Sets data to the predefined settings.
        /// </summary>
        /// <param name="sender">the object sending the event to start this command</param>
        /// <param name="e">the event arguments</param>
        private void CBconfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBconfig.Text == "IFK")
            {
                // Set standards for IFK
                TBeinheit.Text = "1000";
                if (standardTemplate)
                {
                    CBkopf.Checked = true;
                }
            }
            else if (CBconfig.Text == "Hellmuth")
            {
                // Set standard for Hellmuth
                RBmanuell.Checked = true;
                TBeinheit.Text = "100000";
                CBkopf.Checked = false;
                String temp = CBpapierformat.Text;
                for (int i = 0; i < CBpapierformat.Items.Count && temp != "A4"; i++)
                {
                    CBpapierformat.SelectedIndex = i;
                    if (CBpapierformat.Text == "A4" || CBpapierformat.Text == "(A4)")
                    {
                        temp = CBpapierformat.Text;
                        RBhochformat.Checked = false;
                    }
                }
                CBpapierformat.ValueMember = temp;
            }
        }


        /// <summary>
        /// Sets the defaults for the active config if its not custom.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        /// <see cref=" CBconfig_SelectedIndexChanged(object, EventArgs)"/>
        private void Breset_Click(object sender, EventArgs e)
        {
            if (CBconfig.Text != "Benutzerdefiniert")
            {
                CBconfig_SelectedIndexChanged(sender, e);
            }
        }


        /// <summary>
        /// Disables the radio buttons for paper orientation of the textfield-button is checked.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void CBkopf_CheckedChanged(object sender, EventArgs e)
        {
            RBquerformat.Enabled = !CBkopf.Checked;
            RBhochformat.Enabled = !CBkopf.Checked;
        }


        /// <summary>
        /// Disables the scale and drawing unit boxes if the exact extract button is checked.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void CBexakterAusschnitt_CheckedChanged(object sender, EventArgs e)
        {
            CBmassstab.Enabled = !CBexakterAusschnitt.Checked;
            TBeinheit.Enabled = !CBexakterAusschnitt.Checked;
        }


        /// <summary>
        /// Updates the available paperformats if the selected printer is changed.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void CBdrucker_SelectedIndexChanged(object sender, EventArgs e)
        {
            String savePaperformat = CBpapierformat.Text;
            bool saveVertical = RBhochformat.Checked;

            
            // Load the paperformats for the selected printer
            //foreach (Printer pr in printer)
            //{
            //MessageBox.Show("Creating printer");
                printer = PrinterCache.Instance[CBdrucker.SelectedItem.ToString()];
                //if (pr.Name == CBdrucker.SelectedItem.ToString())
                //{
                    CBpapierformat.Items.Clear();
                    var formats = printer.GetPaperformats(CBoptimiertePapierformate.Checked);
                    foreach (PrinterPaperformat paper in formats)
                    {
                        CBpapierformat.Items.Add(paper.Name);
                    }
                //}
            //}
            
            // Look for adequate paperformat for current drawing frame
            if (RBzeichenbereich.Enabled)
            {
                if ((CBpapierformat.SelectedIndex = CBpapierformat.Items.IndexOf(this.dfFormat)) == -1)
                {
                    LFehlerFormat.Text = "Kein passendes Format bei diesem Drucker.";
                    LFehlerFormat.ForeColor = Color.Red;
                }
                else
                {
                    LFehlerFormat.Text = "Format passt zum Zeichenbereich.";
                    LFehlerFormat.ForeColor = Color.Green;
                }
            }
            else
            {
                CBpapierformat.SelectedIndex = CBpapierformat.Items.IndexOf(savePaperformat);
                RBhochformat.Checked = saveVertical;
                RBquerformat.Checked = !saveVertical;
                if (CBpapierformat.SelectedIndex == -1 && CBpapierformat.Items.Count > 0)
                {
                    CBpapierformat.SelectedIndex = 0;
                    LFehlerFormat.Text = "Format in Ordnung.";
                    LFehlerFormat.ForeColor = Color.Green;
                }
                else
                {
                    LFehlerFormat.Text = "Format nicht in Ordnung.";
                    LFehlerFormat.ForeColor = Color.Red;
                }
            }

            if (CBpapierformat.SelectedIndex == -1)
            {
                try
                {
                    CBpapierformat.SelectedIndex = 0;
                }
                catch (Exception) { };
            }
        }


        /// <summary>
        /// Updates the enabled boxes if the chosen paperformat is changed.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void CBpapierformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBexakterAusschnitt.Enabled = (CBpapierformat.Text == "A4" || CBpapierformat.Text == "A3");
            
            if (CBpapierformat.Text == "A4" && CBkopf.Checked)
            {
                RBhochformat.Checked = true;
            }
            else if (CBpapierformat.Text != "A4")
            {
                RBquerformat.Checked = true;
            }
            
            CBdrehen.Enabled = (CBpapierformat.Text == "A4" && !RBzeichenbereich.Checked);

            Lpapierformat.ForeColor = DefaultForeColor;
            
            // Look if paperformat fits drawing frame, otherwise turn text red
            if (RBzeichenbereich.Checked && LFehlerFormat.Text != "Kein passendes Format bei diesem Drucker")
            {
                if (this.dfFormat != CBpapierformat.Text || (this.dfFormat == "A0" && (CBpapierformat.Text == "A4" || CBpapierformat.Text == "A3")))
                {
                    LFehlerFormat.Text = "Format passt nicht zum Zeichenbereich";
                    LFehlerFormat.ForeColor = Color.Red;
                }
                else
                {
                    LFehlerFormat.Text = "Format passt zum Zeichenbereich.";
                    LFehlerFormat.ForeColor = Color.Green;
                }
            }
        }

        
        /// <summary>
        /// Method validating the input for textboxes for numbered input.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void NumberedTexBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Handles the selection of drawing with manuel bounds or drawing area. Disables or enables several controls
        /// and gets the drawing area if selected
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void RBzeichenbereich_CheckedChanged(object sender, EventArgs e)
        {
            if (RBzeichenbereich.Checked)
            {
                AusschnittErkennen();
                BausschnittErstellen.Enabled = false;

                Lausschnitt.Text = "Ausschnitt festgelegt, (" + dfFormat + ")";
                Lausschnitt.ForeColor = Color.Green;

                CBkopf.Checked = true;
                CBkopf.Enabled = false;
                CBkopf_CheckedChanged(null, null);
                
                TBeinheit.Text = "1000";
                System.Collections.IDictionaryEnumerator enumer = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.
                    Database.SummaryInfo.CustomProperties;
                while (enumer.MoveNext())
                {
                    if (enumer.Key.ToString() == "Zeichnungseinheit")
                    {
                        // set the scale
                        TBeinheit.Text = enumer.Value.ToString();
                    }
                }
                
                TBeinheit.Enabled = false;
                CBmassstab.Text = ((int)(1.0 / this.scale)).ToString();
                CBmassstab.Enabled = false;
                CBdrehen.Enabled = false;
            }
            else
            {
                BausschnittErstellen.Enabled = true;
                Lausschnitt.Text = "Ausschnitt nicht festgelegt";
                Lausschnitt.ForeColor = Color.Red;
                startpoint = new Point3d();
                endpoint = new Point3d();

                CBkopf.Enabled = true;
                TBeinheit.Enabled = true;
                CBmassstab.Enabled = true;
                CBdrehen.Enabled = false;
            }

            CBpapierformat_SelectedIndexChanged(null, null);
        }


        /// <summary>
        /// Actualizes the paperformats depending on optimized allowed or not
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void CBoptimiertePapierformate_CheckedChanged(object sender, EventArgs e)
        {
            CBdrucker_SelectedIndexChanged(null, null);
        }


        /// <summary>
        /// Closes the dialog on pressing the escape key.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void UFLayoutErstellen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }


        /// <summary>
        /// Creates a layout to use for PNG.
        /// </summary>
        /// <returns>true if successfull</returns>
        public bool CreatePngLayout()
        {
            var document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            DrawingArea drawingArea = drawingAreaWrapper.DrawingArea;
            if (!RBzeichenbereich.Enabled)
            {
                MessageBox.Show("Diese Zeichnung enthält keinen gültigen Zeichenbereich");
                return false;
            }

            if (!CBdrucker.Items.Contains("PNG"))
            {
                MessageBox.Show("Es ist kein Plotter mit Namen \"PNG\" definiert");
                return false;
            }

            CBdrucker.SelectedIndex = CBdrucker.Items.IndexOf("PNG");

            PaperformatTextfield format = drawingArea.Format;
            if (format is PaperformatTextfieldA4 && !CBpapierformat.Items.Contains("A4"))
            {
                MessageBox.Show("Für den PLotter ist kein A4-Papierformat definiert");
                return false;
            }
            else if (format is PaperformatTextfieldA3 && !CBpapierformat.Items.Contains("A3"))
            {
                MessageBox.Show("Für den Plotter ist kein A3-Papierformat definiert");
                return false;
            }
            else if (Lpapierformat.ForeColor.Equals(Color.Red))
            {
                MessageBox.Show("Ein Detail muss A3 oder A4 Format haben");
                return false;
            }

            TBlayout.Text = "PNG";

            if (!this.CreateLayout())
            {
                return false;
            }

            return true;
        }

    }
}
