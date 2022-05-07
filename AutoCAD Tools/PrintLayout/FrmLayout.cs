using AutoCADTools.Tools;
using AutoCADTools.Utils;
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
    /// <summary>
    /// A UI for creating a layout with different attributes.
    /// </summary>
    public partial class FrmLayout : Form
    {
        #region Fields
        private const string AutoCadAnnotationScalesDatabaseEntryName = "ACDB_ANNOTATIONSCALES";
        private const string AutoCadCursorsizeSystemVariableName = "CURSORSIZE";

        // Instance fields
        private readonly bool oldTextfieldUsed;
        private Document document;

        // State fields
        private readonly LayoutCreationSpecification layoutCreationSpecification = new LayoutCreationSpecification();
        private Printer selectedPrinter;
        private IReadOnlyList<PrinterPaperformat> selectablePaperformats = new List<PrinterPaperformat>();
        private Point extractLowerRightPoint;
        private Size extractSize;
        private Paperformat currentPaperformat;

        #endregion

        #region Loading

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmLayout"/> class.
        /// </summary>
        /// <param name="oldTextfieldUsed">if set to <c>true</c> the old textfield is used.</param>
        public FrmLayout(bool oldTextfieldUsed)
        {
            InitializeComponent();
            this.oldTextfieldUsed = oldTextfieldUsed;
        }

        private void FrmLayout_Load(object sender, EventArgs e)
        {
            document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            txtLayoutName.DataBindings.Add(nameof(txtLayoutName.Text), layoutCreationSpecification, nameof(LayoutCreationSpecification.LayoutName), false, DataSourceUpdateMode.OnPropertyChanged);
            updDrawingUnit.DataBindings.Add(nameof(updDrawingUnit.Value), layoutCreationSpecification, nameof(LayoutCreationSpecification.DrawingUnit), false, DataSourceUpdateMode.OnPropertyChanged);
            ActivateDrawingAreaSelectionIfAvailable();
            LoadTextfield();
            LoadPrinters();
            LoadAnnotationScales();
            CalculateCurrentPaperformat();
            SelectDefaultPrinter();
            ValidateCreationAvailable();
        }

        private void LoadTextfield()
        {
            using (var transaction = document.Database.TransactionManager.StartOpenCloseTransaction())
            {
                var blockTable = transaction.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                if (!blockTable.Has(PaperformatTextfieldA4.TEXTFIELD_BLOCK_NAME) || !blockTable.Has(PaperformatTextfieldFullTextfield.TEXTFIELD_BLOCK_NAME))
                {
                    chkTextfield.Enabled = false;
                    chkTextfield.Checked = false;
                }
            }
        }

        private void LoadPrinters()
        {
            cboPrinter.SelectedIndexChanged -= CboPrinter_SelectedIndexChanged;
            cboPrinter.DataSource = PrinterRepository.Instance.PrinterNames;
            cboPrinter.SelectedIndex = -1;
            cboPrinter.SelectedIndexChanged += CboPrinter_SelectedIndexChanged;
        }

        private void LoadAnnotationScales()
        {
            ObjectContextCollection annotationScalesContextCollection = document.Database.ObjectContextManager.GetContextCollection(AutoCadAnnotationScalesDatabaseEntryName);
            IList<double> annotationScales = annotationScalesContextCollection.Cast<AnnotationScale>().Select(scale => scale.DrawingUnits).ToList();
            cboScale.DataSource = annotationScales;
            cboScale.SelectedItem = document.Database.Cannoscale.DrawingUnits;
        }

        private void ActivateDrawingAreaSelectionIfAvailable()
        {
            DrawingAreaDocumentWrapper drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            chkUseDrawingArea.Enabled = drawingAreaWrapper.DrawingArea.IsValid;
            if (chkUseDrawingArea.Enabled)
            {
                chkUseDrawingArea.Checked = true;
            }
        }

        private bool LoadDrawingArea()
        {
            DrawingAreaDocumentWrapper drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            if (drawingAreaWrapper.DrawingArea.IsValid)
            {
                var drawingData = document.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
                layoutCreationSpecification.DrawingUnit = drawingData.DrawingUnit;
                extractSize = 1 / drawingAreaWrapper.DrawingArea.Scale * drawingAreaWrapper.DrawingArea.Format.ViewportSizeModel;
                using (Transaction trans = document.TransactionManager.StartTransaction())
                {
                    var point = (drawingAreaWrapper.DrawingArea.DrawingAreaId.GetObject(OpenMode.ForRead) as BlockReference).Position;
                    extractLowerRightPoint = new Point(point.X, point.Y);
                }
                cboScale.Text = Math.Round(drawingData.DrawingUnit / drawingAreaWrapper.DrawingArea.Scale).ToString();
                return true;
            }
            return false;
        }

        #endregion

        #region Actions

        private void ButDefineExtract_Click(object sender, EventArgs e)
        {
            var interact = document.Editor.StartUserInteraction(this.Handle);

            object oldCrossWidth = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable(AutoCadCursorsizeSystemVariableName);
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable(AutoCadCursorsizeSystemVariableName, 100);

            // Define to points to get from user
            Point3d p1;
            Point3d p2;

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
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable(AutoCadCursorsizeSystemVariableName, oldCrossWidth);
                interact.End();
                return;
            }

            // Get the last point of the layout extends and show line from first point
            var endpointOpts = new PromptCornerOptions(LocalData.ExtractEndPointText, p1)
            {
                UseDashedLine = true
            };
            getPointResult = document.Editor.GetCorner(endpointOpts);
            // Set cursorsize to normal
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable(AutoCadCursorsizeSystemVariableName, oldCrossWidth);
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

            chkUseDrawingArea.Checked = false;
            CalculatePaperformatAndValidateSelectedPrinterPaperformat();
            SelectDefaultPrinter();
        }

        private void ButCreate_Click(object sender, EventArgs e)
        {
            ValidateCreationAvailable();
            if (!butCreate.Enabled) return;

            if (!LayoutManager.Current.GetLayoutId(layoutCreationSpecification.LayoutName).IsNull)
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

            layoutCreationSpecification.Orientation = optLandscape.Checked ? LayoutCreationSpecification.PaperOrientation.Landscape : LayoutCreationSpecification.PaperOrientation.Portrait;
            layoutCreationSpecification.Scale = 1.0 / int.Parse(cboScale.Text);

            if (chkExactExtract.Checked)
            {
                layoutCreationSpecification.Scale = SetPaperformatForExactExtract();
            }
            else if (!chkTextfield.Checked)
            {
                Size difference = (1 / layoutCreationSpecification.Scale / layoutCreationSpecification.DrawingUnit) * currentPaperformat.ViewportSizeLayout - extractSize;
                extractLowerRightPoint += 0.5 * new Size(difference.Width, -difference.Height);
            }

            layoutCreationSpecification.ExtractLowerRightPoint = extractLowerRightPoint;
            layoutCreationSpecification.Paperformat = currentPaperformat;
            layoutCreationSpecification.Printerformat = printerformat;
            new LayoutCreator(layoutCreationSpecification).CreateLayout();
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

        private void PrinterChanged()
        {
            var oldFormat = cboPaperformat.Text;
            this.selectedPrinter = PrinterRepository.Instance[cboPrinter.Text];
            if (selectedPrinter != null)
            {
                using (var progressDialog = new ProgressDialog())
                {
                    this.selectablePaperformats = selectedPrinter.InitializeAndGetPaperformats(chkOptimizedPaperformats.Checked, progressDialog);
                }
            }
            else
            {
                this.selectablePaperformats = new List<PrinterPaperformat>().ToArray();
            }
            cboPaperformat.Items.Clear();
            cboPaperformat.Items.AddRange(selectablePaperformats.Select(format => format.Name).ToArray());
            int index = cboPaperformat.FindStringExact(oldFormat);
            cboPaperformat.SelectedIndex = index != -1 || cboPaperformat.Items.Count == 0 ? index : 0;
            ValidatePaperformats();
            ValidateSelectedPrinterPaperformat();
        }

        private void UpdatePaperOrientations()
        {
            optLandscape.Visible = (chkExactExtract.Checked || !chkTextfield.Checked) && cboPaperformat.Text == "A4";
            optPortrait.Visible = (chkExactExtract.Checked || !chkTextfield.Checked) && cboPaperformat.Text == "A4";
        }

        private void CalculateCurrentPaperformat()
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
        }

        private void CalculatePaperformatAndValidateSelectedPrinterPaperformat()
        {
            CalculateCurrentPaperformat();
            ValidateSelectedPrinterPaperformat();
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
            else if (selectedPrinter == null)
            {
                cboPrinter.SelectedIndex = 0;
            }
        }

        private void SelectOptimalPaperformat()
        {
            if (!chkExactExtract.Checked && currentPaperformat != null && selectedPrinter != null && selectedPrinter.Initialized)
            {
                using (var progressDialog = new ProgressDialog())
                {
                    var printerPaperformat = currentPaperformat.GetFittingPaperformat(selectedPrinter, chkOptimizedPaperformats.Checked, progressDialog);
                    int formatIndex = printerPaperformat != null ? cboPaperformat.FindStringExact(printerPaperformat.Name) : -1;
                    if (formatIndex != -1)
                    {
                        cboPaperformat.SelectedIndex = formatIndex;
                    }
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

        private void ValidatePrinterPaperformatFitting()
        {
            var matchingPaperformats = selectablePaperformats.Where(format => format.Name == cboPaperformat.Text);
            PrinterPaperformat printerformat = matchingPaperformats.Any() ? matchingPaperformats.First() : null;
            using (var progressDialog = new ProgressDialog())
            {
                if (!chkExactExtract.Checked && !PaperformatPrinterMapping.IsFormatFitting(printerformat, currentPaperformat, progressDialog))
                {
                    errorProvider.SetError(cboPaperformat, LocalData.PaperformatNotFitting);
                }
                else
                {
                    errorProvider.SetError(cboPaperformat, String.Empty);
                }
            }
            ValidateCreationAvailable();
        }

        private void ValidateSelectedPrinterPaperformat()
        {
            ValidatePrinterPaperformatFitting();
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

        private void TxtLayoutName_Validating(object sender, CancelEventArgs e)
        {
            ValidateLayoutName();
        }

        private void CboScale_Validating(object sender, CancelEventArgs e)
        {
            ValidateScale();
            CalculatePaperformatAndValidateSelectedPrinterPaperformat();
        }

        private void UpdDrawingUnit_ValueChanged(object sender, EventArgs e)
        {
            CalculatePaperformatAndValidateSelectedPrinterPaperformat();
        }

        private void CboPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrinterChanged();
        }

        private void CboPaperformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculatePaperformatAndValidateSelectedPrinterPaperformat();
            UpdatePaperOrientations();
            ValidateSelectedPrinterPaperformat();
            bool exactExtractPossible = cboPaperformat.Text == "A4" || cboPaperformat.Text == "A3";
            chkExactExtract.Enabled = exactExtractPossible;
            if (!exactExtractPossible)
            {
                chkExactExtract.Checked = false;
            }
        }

        private void ChkOptimizedPaperformats_CheckedChanged(object sender, EventArgs e)
        {
            PrinterChanged();
            if (chkExactExtract.Checked && currentPaperformat != null)
            {
                PrinterPaperformat printerformat = null;
                foreach (var format in selectablePaperformats)
                {
                    if (format.Name == cboPaperformat.Text) printerformat = format;
                }
                using (var progressDialog = new ProgressDialog())
                {
                    if (!PaperformatPrinterMapping.IsFormatFitting(printerformat, currentPaperformat, progressDialog))
                    {
                        SelectOptimalPaperformat();
                    }
                }
            }
        }

        private void ChkExactExtract_CheckedChanged(object sender, EventArgs e)
        {
            cboScale.Enabled = !chkExactExtract.Checked;
            updDrawingUnit.Enabled = !chkExactExtract.Checked;
            ValidateScale();
            ValidatePrinterPaperformatFitting();
            UpdatePaperOrientations();
        }

        private void ChkTextfield_CheckedChanged(object sender, EventArgs e)
        {
            CalculatePaperformatAndValidateSelectedPrinterPaperformat();
            UpdatePaperOrientations();
            SelectOptimalPaperformat();
        }

        private void ChkUseDrawingArea_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDrawingArea.Checked)
            {
                LoadDrawingArea();
            }
        }

        private void CboScale_KeyPress(object sender, KeyPressEventArgs e)
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
