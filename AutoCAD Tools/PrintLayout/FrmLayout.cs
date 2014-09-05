using AutoCADTools.Tools;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace AutoCADTools.PrintLayout
{
    public partial class FrmLayout : Form
    {
        #region Fields

        // Instance fields
        private List<string> printerNames;
        private bool oldTextfieldUsed;
        private Document document;

        // State fields
        private Printer selectedPrinter;
        private IReadOnlyList<PrinterPaperformat> selectablePaperformats;
        private Point extractLowerRightPoint;
        private Size extractSize;
        private Paperformat currentPaperformat;

        #endregion

        #region Loading

        public FrmLayout(bool oldTextfieldUsed)
        {
            InitializeComponent();
        }
        
        private void FrmLayout_Load(object sender, EventArgs e)
        {
            this.document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            this.oldTextfieldUsed = oldTextfieldUsed;

            printerNames = Printer.PrinterNamesList;
            cboPrinter.DataSource = printerNames;

            txtLayoutName.Text = Properties.Settings.Default.DefaultLayoutName;

            currentPaperformat = new PaperformatTextfieldA4Vertical(oldTextfieldUsed);
            SelectDefaultPrinter();

            // Get the annotation scales an add them to the scale combobox
            ObjectContextCollection occ = document.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            cboScale.DataSource = occ.Cast<AnnotationScale>().ToList<AnnotationScale>();
            cboScale.DisplayMember = "DrawingUnits";
            cboScale.SelectedIndex = cboScale.FindStringExact(document.Database.Cannoscale.DrawingUnits.ToString());

            DrawingAreaDocumentWrapper drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            chkUseDrawingArea.Enabled = drawingAreaWrapper.DrawingArea.IsValid;
            LoadDrawingArea();

            using (var trans = document.Database.TransactionManager.StartOpenCloseTransaction())
            {
                var blockTable = trans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                if (!blockTable.Has(PaperformatTextfieldA4.TEXTFIELD_BLOCK_NAME) || !blockTable.Has(PaperformatTextfieldFullTextfield.TEXTFIELD_BLOCK_NAME))
                {
                    chkTextfield.Enabled = false;
                    chkTextfield.Checked = false;
                }
            }
            ValidateCreationAvailable();
        }

        #endregion

        #region Actions

        private void butDefineExtract_Click(object sender, EventArgs e)
        {
            var interact = document.Editor.StartUserInteraction(this.Handle);

            object oldCrossWidth = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CURSORSIZE");
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", 100);

            // Define to points to get from user
            Point3d p1 = Point3d.Origin;
            Point3d p2 = Point3d.Origin;

            PromptPointResult getPointResult;

            // Get the first point of the layout extends
            getPointResult = document.Editor.GetPoint(Environment.NewLine + LocalData.ExtractStartPointText);
            if (getPointResult.Status == PromptStatus.OK)
            {
                p1 = getPointResult.Value;
            }
            else
            {
                // On error stop
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", oldCrossWidth);
                interact.End();
                return;
            }

            // Get the last point of the layout extends and show line from first point
            var endpointOpts = new PromptCornerOptions(LocalData.ExtractEndPointText, p1);
            endpointOpts.UseDashedLine = true;
            getPointResult = document.Editor.GetCorner(endpointOpts);
            // Set cursorsize to normal
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CURSORSIZE", oldCrossWidth);
            if (getPointResult.Status == PromptStatus.OK)
            {
                p2 = getPointResult.Value;
            }
            else
            {
                // On error stop
                interact.End();
                return;
            }

            // Set startpoint to minimum extends and endpoint to maximum extends
            extractLowerRightPoint = new Point(Math.Max(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            extractSize = new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));

            interact.End();

            UpdateSelectedPaperformat();
            SelectDefaultPrinter();
        }

        private void butCreate_Click(object sender, EventArgs e)
        {
            ValidateCreationAvailable();
            if (!butCreate.Enabled) return;
            
            if (!LayoutManager.Current.GetLayoutId(txtLayoutName.Text).IsNull)
            {
                if (MessageBox.Show(LocalData.DuplicateLayoutNameText, LocalData.DuplicateLayoutNameTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            this.Close();

            PrinterPaperformat printerformat = null;
            foreach (var format in selectablePaperformats)
            {
                if (format.Name == cboPaperformat.Text) printerformat = format;
            }

            LayoutCreation.PaperOrientation orientation = optLandscape.Checked ? LayoutCreation.PaperOrientation.Landscape : LayoutCreation.PaperOrientation.Portrait;
            double drawingUnit = (double)updDrawingUnit.Value;
            double scale = 1.0 / int.Parse(cboScale.Text);

            if (chkExactExtract.Checked)
            {
                scale = SetPaperformatForExactExtract();
            }
            else if (!chkTextfield.Checked)
            {
                Size difference = (1 / scale / drawingUnit) * currentPaperformat.ViewportSizeLayout - extractSize;
                extractLowerRightPoint += 0.5 * new Size(difference.Width, -difference.Height);
            }

            LayoutCreation creation = null;
            if (chkTextfield.Checked)
            {
                creation = new LayoutTextfield((PaperformatTextfield)currentPaperformat);
            }
            else
            {
                creation = new LayoutPlain(currentPaperformat);
            }
            creation.DrawingUnit = drawingUnit;
            creation.ExtractLowerRightPoint = extractLowerRightPoint;
            creation.LayoutName = txtLayoutName.Text;
            creation.Orientation = orientation;
            creation.Printerformat = printerformat;
            creation.Scale = scale;

            creation.CreateLayout();
        }

        private double SetPaperformatForExactExtract()
        {
            if (cboPaperformat.Text == "A4")
            {
                if (chkTextfield.Checked)
                {
                    if (optLandscape.Checked)
                    {
                        currentPaperformat = new PaperformatTextfieldA4Horizontal(oldTextfieldUsed);
                    }
                    else
                    {
                        currentPaperformat = new PaperformatTextfieldA4Vertical(oldTextfieldUsed);
                    }
                }
                else
                {
                    if (optLandscape.Checked)
                    {
                        currentPaperformat = new PaperformatA4Horizontal();
                    }
                    else
                    {
                        currentPaperformat = new PaperformatA4Vertical();
                    }
                }
            }
            else if (cboPaperformat.Text == "A3")
            {
                if (chkTextfield.Checked)
                {
                    currentPaperformat = new PaperformatTextfieldA3(oldTextfieldUsed);
                }
                else
                {
                    currentPaperformat = new PaperformatA3();
                }
            }

            double drawingUnit = (double)updDrawingUnit.Value;
            double scaleWidth = currentPaperformat.ViewportSizeModel.Width / (extractSize.Width * drawingUnit);
            double scaleHeight = currentPaperformat.ViewportSizeModel.Height / (extractSize.Height * drawingUnit);
            Size addition = scaleHeight < scaleWidth ? new Size(extractSize.Width * (scaleWidth / scaleHeight - 1), 0) : new Size(0, extractSize.Height * (scaleHeight / scaleWidth - 1));
            extractLowerRightPoint += 0.5 * new Size(addition.Width, -addition.Height);
            extractSize += addition;
            var viewportSize = Math.Min(scaleHeight, scaleWidth) * drawingUnit * extractSize;
            if (chkTextfield.Checked)
            {
                currentPaperformat = PaperformatFactory.GetPaperformatTextfield(viewportSize, oldTextfieldUsed);
            } 
            else
            {
                currentPaperformat = PaperformatFactory.GetPlainPaperformat(viewportSize);
            }
            return Math.Min(scaleHeight, scaleWidth);
        }

        #endregion
        
        #region Methods

        private void LoadDrawingArea()
        {
            DrawingAreaDocumentWrapper drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            if (drawingAreaWrapper.DrawingArea.IsValid)
            {
                var drawingData = document.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
                updDrawingUnit.Value = drawingData.DrawingUnit;
                extractSize = 1 / drawingAreaWrapper.DrawingArea.Scale * drawingAreaWrapper.DrawingArea.Format.ViewportSizeModel;
                using (Transaction trans = document.TransactionManager.StartTransaction())
                {
                    var point = (drawingAreaWrapper.DrawingArea.DrawingAreaId.GetObject(OpenMode.ForRead) as BlockReference).Position;
                    extractLowerRightPoint = new Point(point.X, point.Y);
                }
                cboScale.Text = Math.Round(drawingData.DrawingUnit / drawingAreaWrapper.DrawingArea.Scale).ToString();
                UpdateSelectedPaperformat();
                SelectDefaultPrinter();
                chkUseDrawingArea.Checked = true;
            }
            ValidateExtract();
        }

        private void PrinterChanged()
        {
            var oldFormat = cboPaperformat.Text;
            this.selectedPrinter = PrinterCache.Instance[cboPrinter.Text];
            this.selectablePaperformats = selectedPrinter.GetPaperformats(chkOptimizedPaperformats.Checked);
            /*cboPaperformat.BeginUpdate();
            cboPaperformat.DataSource = null;
            cboPaperformat.DataSource = paperformats;
            cboPaperformat.DisplayMember = "Name";
            cboPaperformat.EndUpdate();*/
            cboPaperformat.Items.Clear();
            foreach (var format in selectablePaperformats)
            {
                cboPaperformat.Items.Add(format.Name);
            }
            int index = cboPaperformat.FindStringExact(oldFormat);
            if (index != -1)
            {
                cboPaperformat.SelectedIndex = index;
            }
            else if (cboPaperformat.Items.Count > 0)
            {
                cboPaperformat.SelectedIndex = 0;
            }
            ValidatePaperformats();
        }

        private void UpdatePaperOrientations()
        {
            optLandscape.Visible = (chkExactExtract.Checked || !chkTextfield.Checked) && cboPaperformat.Text == "A4";
            optPortrait.Visible = (chkExactExtract.Checked || !chkTextfield.Checked) && cboPaperformat.Text == "A4";
        }

        private void UpdateSelectedPaperformat()
        {
            if (!chkExactExtract.Checked && String.IsNullOrEmpty(errorProvider.GetError(cboScale)) && String.IsNullOrEmpty(errorProvider.GetError(updDrawingUnit)) && extractSize != null)
            {
                Size viewportSize = 1.0 / int.Parse(cboScale.Text) * (double)updDrawingUnit.Value * extractSize;
                if (chkTextfield.Checked)
                {
                    this.currentPaperformat = PaperformatFactory.GetPaperformatTextfield(viewportSize, oldTextfieldUsed);
                }
                else
                {
                    this.currentPaperformat = PaperformatFactory.GetPlainPaperformat(viewportSize);
                }
            }
            ValidateExtract();
            ValidateSelectedPaperformat();
        }

        private void SelectDefaultPrinter()
        {
            if (!chkExactExtract.Checked && currentPaperformat != null)
            {
                var defaultPrinter = currentPaperformat.GetDefaultPrinter();
                int printerIndex = defaultPrinter == null ? -1 : cboPrinter.FindStringExact(defaultPrinter.Name);
                
                if (printerIndex != -1)
                {
                    cboPrinter.SelectedIndex = printerIndex;
                }
                SelectOptimalPaperformat();
            }
        }

        private void SelectOptimalPaperformat()
        {
            if (!chkExactExtract.Checked && currentPaperformat != null)
            {
                var paperformat = currentPaperformat.GetFittingPaperformat(selectedPrinter, chkOptimizedPaperformats.Checked);
                int formatIndex = cboPaperformat.FindStringExact(paperformat.Name);
                if (formatIndex != -1)
                {
                    cboPaperformat.SelectedIndex = formatIndex;
                }
                if (currentPaperformat is PaperformatA4Vertical || currentPaperformat is PaperformatTextfieldA4)
                {
                    optPortrait.Checked = true;
                }
                else
                {
                    optLandscape.Checked = true;
                }
            }
        }

        #endregion

        #region Validation

        private void ValidateExtract()
        {
            if (extractLowerRightPoint == null)
            {
                errorProvider.SetError(butDefineExtract, LocalData.NoExtractDefinedError);
            }
            else
            {
                errorProvider.SetError(butDefineExtract, String.Empty);
            }
        }

        private void ValidateLayoutName()
        {
            if (string.IsNullOrWhiteSpace(txtLayoutName.Text))
            {
                errorProvider.SetError(txtLayoutName, Environment.NewLine + LocalData.LayoutNameEmptyError);
            }
            else
            {
                errorProvider.SetError(txtLayoutName, String.Empty);
            }
            ValidateCreationAvailable();
        }

        private void ValidatePaperformats()
        {
            if (this.selectablePaperformats.Count == 0)
            {
                errorProvider.SetError(cboPaperformat, LocalData.PaperformatListEmptyError);
            }
            else
            {
                errorProvider.SetError(cboPaperformat, String.Empty);
            }
            ValidateCreationAvailable();
        }

        private void ValidateScale()
        {
            if (!chkExactExtract.Checked)
            {
                if (String.IsNullOrWhiteSpace(cboScale.Text))
                {
                    errorProvider.SetError(cboScale, LocalData.ScaleEmptyError);
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(cboScale.Text, "[^0-9]") || int.Parse(cboScale.Text) == 0)
                {
                    errorProvider.SetError(cboScale, LocalData.ScaleNoNumberError);
                }
                else
                {
                    errorProvider.SetError(cboScale, String.Empty);
                }
            }
            else
            {
                errorProvider.SetError(cboScale, String.Empty);
            }
            ValidateCreationAvailable();
        }

        private void ValidatePaperformatFitting()
        {
            PrinterPaperformat printerformat = null;
            foreach (var format in selectablePaperformats)
            {
                if (format.Name == cboPaperformat.Text) printerformat = format;
            }
            if (!chkExactExtract.Checked && currentPaperformat != null && !PaperformatPrinterMapping.IsFormatFitting(printerformat, currentPaperformat))
            {
                errorProvider.SetError(cboPaperformat, LocalData.PaperformatNotFitting);
            }
            else
            {
                errorProvider.SetError(cboPaperformat, String.Empty);
            }
            ValidateCreationAvailable();
        }

        private void ValidateSelectedPaperformat()
        {
            ValidatePaperformatFitting();
            ValidateCreationAvailable();
        }

        private void ValidateCreationAvailable()
        {
            bool invalid = false;
            invalid |= !String.IsNullOrEmpty(errorProvider.GetError(cboScale));
            invalid |= !String.IsNullOrEmpty(errorProvider.GetError(cboPaperformat));
            invalid |= !String.IsNullOrEmpty(errorProvider.GetError(txtLayoutName));
            invalid |= extractLowerRightPoint == null;

            butCreate.Enabled = !invalid;
        }

        #endregion
        
        #region Handler

        private void txtLayoutName_Validating(object sender, CancelEventArgs e)
        {
            ValidateLayoutName();
        }

        private void cboScale_Validating(object sender, CancelEventArgs e)
        {
            ValidateScale();
            UpdateSelectedPaperformat();
        }

        private void updDrawingUnit_ValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedPaperformat();
        }

        private void cboPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrinterChanged();
        }
        
        private void cboPaperformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedPaperformat();
            UpdatePaperOrientations();
            ValidateSelectedPaperformat();
            bool exactExtractPossible = cboPaperformat.Text == "A4" || cboPaperformat.Text == "A3";
            chkExactExtract.Enabled = exactExtractPossible;
            if (!exactExtractPossible)
            {
                chkExactExtract.Checked = false;
            }
        }

        private void chkOptimizedPaperformats_CheckedChanged(object sender, EventArgs e)
        {
            PrinterChanged();
            if (chkExactExtract.Checked && currentPaperformat != null)
            {
                PrinterPaperformat printerformat = null;
                foreach (var format in selectablePaperformats)
                {
                    if (format.Name == cboPaperformat.Text) printerformat = format;
                }
                if (!PaperformatPrinterMapping.IsFormatFitting(printerformat, currentPaperformat))
                {
                    SelectOptimalPaperformat();
                }
            }
        }

        private void chkExactExtract_CheckedChanged(object sender, EventArgs e)
        {
            cboScale.Enabled = !chkExactExtract.Checked;
            updDrawingUnit.Enabled = !chkExactExtract.Checked;
            ValidateScale();
            ValidatePaperformatFitting();
            UpdatePaperOrientations();
        }

        private void chkTextfield_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSelectedPaperformat();
            UpdatePaperOrientations();
            SelectOptimalPaperformat();
        }

        private void chkUseDrawingArea_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUseDrawingArea.Checked) chkUseDrawingArea.Checked = true;
            if (chkUseDrawingArea.Checked) SelectDefaultPrinter();
        }

        private void cboScale_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void LayoutUI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                this.Close();
            }
        }

        #endregion
          
    }
}
