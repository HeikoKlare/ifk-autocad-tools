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
using static AutoCADTools.PrintLayout.LayoutCreationSpecification;

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
        private const string PaperformatDescriptionA4Horizontal = "A4 horizontal";
        private const string PaperformatDescriptionA4Vertical = "A4 vertical";
        private const string PaperformatDescriptionA3 = "A3";
        private const string PaperformatDescriptionA0 = "A0";

        private readonly LayoutCreationSpecification layoutCreationSpecification = new LayoutCreationSpecification();
        private Printer selectedPrinter;
        private readonly List<PrinterPaperformat> selectablePaperformats = new List<PrinterPaperformat>();
        private IReadOnlyList<PrinterPaperformat> SelectablePaperformats
        {
            get => selectablePaperformats;
            set
            {
                selectablePaperformats.Clear();
                selectablePaperformats.AddRange(value);
            }
        }
        public bool UseExactExtract { get; set; }

        #endregion

        #region Loading

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmLayout"/> class.
        /// </summary>
        public FrmLayout()
        {
            InitializeComponent();
        }

        private void FrmLayout_Load(object sender, EventArgs e)
        {
            InitializeAndBindScaleElements();
            InitializeAndBindExtractElements();
            InitializeAndBindPaperformatElements();
            InitializeAndBindTopLevelElements();
            SelectDefaultPrinterAndOptimalFormat();
            InputChanged();
        }

        private void InitializeAndBindTopLevelElements()
        {
            txtLayoutName.DataBindings.Add(nameof(txtLayoutName.Text), layoutCreationSpecification, nameof(LayoutCreationSpecification.LayoutName), false, DataSourceUpdateMode.OnValidation);
            var calculatedPapersizeBinding = new Binding(nameof(lblCalculatedPapersize.Text), layoutCreationSpecification, nameof(LayoutCreationSpecification.Paperformat), false, DataSourceUpdateMode.OnPropertyChanged);
            calculatedPapersizeBinding.Format += (sender, eventArgs) =>
            {
                eventArgs.Value = GenerateTextForPaperformat(eventArgs.Value as Paperformat);
            };
            lblCalculatedPapersize.DataBindings.Add(calculatedPapersizeBinding);
        }
        private static string GenerateTextForPaperformat(Paperformat paperformat)
        {
            if (paperformat == null)
            {
                return "-";
            }
            switch (paperformat.GetType().Name)
            {
                case nameof(PaperformatA4Horizontal):
                case nameof(PaperformatTextfieldA4Horizontal): return PaperformatDescriptionA4Horizontal;
                case nameof(PaperformatA4Vertical):
                case nameof(PaperformatTextfieldA4Vertical): return PaperformatDescriptionA4Vertical;
                case nameof(PaperformatA3):
                case nameof(PaperformatTextfieldA3): return PaperformatDescriptionA3;
                default: return PaperformatDescriptionA0;
            }
        }

        private void InitializeAndBindScaleElements()
        {
            chkExactExtract.DataBindings.Add(nameof(chkExactExtract.Checked), this, nameof(FrmLayout.UseExactExtract), false, DataSourceUpdateMode.OnPropertyChanged);
            ObjectContextCollection annotationScalesContextCollection = layoutCreationSpecification.Document.Database.ObjectContextManager.GetContextCollection(AutoCadAnnotationScalesDatabaseEntryName);
            IList<double> annotationScales = annotationScalesContextCollection.Cast<AnnotationScale>().Select(scale => scale.DrawingUnits).ToList();
            cboScale.DataSource = annotationScales;
            Binding scaleBinding = new Binding(nameof(cboScale.Text), layoutCreationSpecification, nameof(LayoutCreationSpecification.Scale), false, DataSourceUpdateMode.OnValidation);
            scaleBinding.Format += (sender, eventArgs) =>
            {
                eventArgs.Value = (int)(1 / (double)eventArgs.Value);
            };
            scaleBinding.Parse += (sender, eventArgs) =>
            {
                var scaleString = (string)eventArgs.Value;
                if (scaleString.Length == 0 || scaleString == "0")
                {
                    eventArgs.Value = layoutCreationSpecification.Scale;
                }
                else
                {
                    eventArgs.Value = 1.0 / int.Parse((string)eventArgs.Value);
                }
            };
            cboScale.DataBindings.Add(scaleBinding);
        }

        private void InitializeAndBindExtractElements()
        {
            optExtractDrawingArea.DataBindings.Add(nameof(optExtractDrawingArea.Enabled), layoutCreationSpecification, nameof(LayoutCreationSpecification.HasPredefinedDrawingArea), false);
            Binding cboScaleEnabledBinding = new Binding(nameof(cboScale.Enabled), chkExactExtract, nameof(CheckBox.Checked), false, DataSourceUpdateMode.OnPropertyChanged);
            cboScaleEnabledBinding.Format += (sender, eventArgs) => { eventArgs.Value = !(bool)eventArgs.Value; };
            cboScale.DataBindings.Add(cboScaleEnabledBinding);
            updDrawingUnit.DataBindings.Add(nameof(updDrawingUnit.Value), layoutCreationSpecification, nameof(LayoutCreationSpecification.DrawingUnit), false, DataSourceUpdateMode.OnPropertyChanged);
            optExtractDrawingArea.Checked = optExtractDrawingArea.Enabled;
            optExtractManual.Checked = !optExtractDrawingArea.Enabled;
        }

        private void InitializeAndBindPaperformatElements()
        {
            chkTextfield.DataBindings.Add(nameof(chkTextfield.Enabled), layoutCreationSpecification, nameof(LayoutCreationSpecification.CanUseTextfield), false);
            chkTextfield.DataBindings.Add(nameof(chkTextfield.Checked), layoutCreationSpecification, nameof(LayoutCreationSpecification.UseTextfield), false, DataSourceUpdateMode.OnPropertyChanged);
            cboPrinter.SelectedIndexChanged -= CboPrinter_SelectedIndexChanged;
            cboPrinter.DataSource = PrinterRepository.Instance.Printers;
            cboPrinter.DisplayMember = nameof(Printer.Name);
            cboPrinter.SelectedIndex = -1;
            cboPrinter.SelectedIndexChanged += CboPrinter_SelectedIndexChanged;
            cboPrinterPaperformat.DisplayMember = nameof(PrinterPaperformat.Name);
            cboPrinterPaperformat.ValueMember = nameof(PrinterPaperformat.Name);
        }

        #endregion

        #region Actions

        private void ButDefineExtract_Click(object sender, EventArgs e)
        {
            layoutCreationSpecification.DrawingArea = PromptExtractExtends();
            optExtractManual.Checked = true;
            SelectOptimalPrinterPaperformat(false);
            InputChanged();
        }

        private Frame PromptExtractExtends()
        {
            var editor = layoutCreationSpecification.Document.Editor;
            var interact = editor.StartUserInteraction(this.Handle);

            object oldCrossWidth = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable(AutoCadCursorsizeSystemVariableName);
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable(AutoCadCursorsizeSystemVariableName, 100);

            // Define to points to get from user
            Point3d firstPoint;
            Point3d secondPoint;

            PromptPointResult getPointResult;

            // Get the first point of the layout extends
            getPointResult = editor.GetPoint(Environment.NewLine + LocalData.ExtractStartPointText);
            if (getPointResult.Status == PromptStatus.OK)
            {
                firstPoint = getPointResult.Value;
            }
            else
            {
                // On error stop
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable(AutoCadCursorsizeSystemVariableName, oldCrossWidth);
                interact.End();
                return null;
            }

            // Get the last point of the layout extends and show line from first point
            var endpointOpts = new PromptCornerOptions(LocalData.ExtractEndPointText, firstPoint)
            {
                UseDashedLine = true
            };
            getPointResult = editor.GetCorner(endpointOpts);
            // Set cursorsize to normal
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable(AutoCadCursorsizeSystemVariableName, oldCrossWidth);
            if (getPointResult.Status == PromptStatus.OK)
            {
                secondPoint = getPointResult.Value;
            }
            else
            {
                // On error stop
                interact.End();
                return null;
            }

            interact.End();

            // Set startpoint to minimum extends and endpoint to maximum extends
            var lowerRightPoint = new Point(Math.Max(firstPoint.X, secondPoint.X), Math.Min(firstPoint.Y, secondPoint.Y));
            var size = new Size(Math.Abs(firstPoint.X - secondPoint.X), Math.Abs(firstPoint.Y - secondPoint.Y));
            return new Frame(lowerRightPoint, size);
        }

        private void ButCreate_Click(object sender, EventArgs e)
        {
            InputChanged();
            if (!butCreate.Enabled) return;

            if (!LayoutManager.Current.GetLayoutId(layoutCreationSpecification.LayoutName).IsNull)
            {
                if (MessageBox.Show(LocalData.DuplicateLayoutNameText, LocalData.DuplicateLayoutNameTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            this.Close();

            if (!layoutCreationSpecification.UseTextfield)
            {
                Size difference = 1 / layoutCreationSpecification.Scale / (layoutCreationSpecification.DrawingUnit) * layoutCreationSpecification.Paperformat.ViewportSizeLayout - layoutCreationSpecification.DrawingArea.Size;
                layoutCreationSpecification.DrawingArea = new Frame(layoutCreationSpecification.DrawingArea.LowerRightPoint + 0.5 * new Size(difference.Width, -difference.Height), layoutCreationSpecification.DrawingArea.Size);
            }

            new LayoutCreator(layoutCreationSpecification).CreateLayout();
        }

        #endregion

        #region Methods

        private void ReloadPrinterPaperformats()
        {
            string oldFormatName = (string)cboPrinterPaperformat.SelectedValue;
            using (var progressDialog = new ProgressDialog())
            {
                SelectablePaperformats = selectedPrinter != null ? selectedPrinter.InitializeAndGetPaperformats(chkOptimizedPaperformats.Checked, progressDialog).ToList() : new List<PrinterPaperformat>();
            }
            // Rebind printer paperformats
            cboPrinterPaperformat.DataSource = null;
            cboPrinterPaperformat.DataSource = SelectablePaperformats;
            cboPrinterPaperformat.DisplayMember = nameof(PrinterPaperformat.Name);
            if (!string.IsNullOrEmpty(oldFormatName))
            {
                cboPrinterPaperformat.SelectedValue = oldFormatName;
            }
            if (cboPrinterPaperformat.SelectedIndex == 0 && cboPrinterPaperformat.Items.Count > 0)
            {
                cboPrinterPaperformat.SelectedIndex = 0;
            }
            InputChanged();
        }

        private void SelectDefaultPrinterAndOptimalFormat()
        {
            if (!UseExactExtract && layoutCreationSpecification.Paperformat != null)
            {
                var defaultPrinter = layoutCreationSpecification.Paperformat.GetDefaultPrinter();
                if (defaultPrinter != null)
                {
                    cboPrinter.SelectedItem = defaultPrinter;
                }
                SelectOptimalPrinterPaperformat(true);
            }
            else if (selectedPrinter == null)
            {
                cboPrinter.SelectedIndex = 0;
            }
        }

        private void SelectOptimalPrinterPaperformat(bool optimizeIfFitting)
        {
            using (var progressDialog = new ProgressDialog())
            {
                if (!UseExactExtract && layoutCreationSpecification.Paperformat != null && selectedPrinter != null && selectedPrinter.Initialized &&
                    PaperformatPrinterMapping.IsFormatFitting(layoutCreationSpecification.Printerformat, layoutCreationSpecification.Paperformat, progressDialog))
                {
                    var printerPaperformat = layoutCreationSpecification.Paperformat.GetFittingPaperformat(selectedPrinter, chkOptimizedPaperformats.Checked, progressDialog);
                    if (printerPaperformat != null)
                    {
                        cboPrinterPaperformat.SelectedItem = printerPaperformat;
                    }
                    InputChanged();
                }
            }
        }

        private void InputChanged()
        {
            if (UseExactExtract)
            {
                CalculateAndSetAreaAndScaleForExactExtract();
            }
            ValidateExtract();
            ValidatePrinterPaperformats();
            ValidateCreationAvailable();
        }

        private void CalculateAndSetAreaAndScaleForExactExtract()
        {
            if (layoutCreationSpecification.DrawingArea == null || string.IsNullOrEmpty(cboPrinterPaperformat.Text))
            {
                throw new InvalidOperationException("cannot calculate scale for extract when drawing area or paperformat is undefined");
            }

            Size viewportSize = null;
            if (cboPrinterPaperformat.Text == "A4")
            {
                viewportSize = layoutCreationSpecification.UseTextfield ? PaperformatTextfieldA4Horizontal.MaximumViewportSize : PaperformatA4Horizontal.MaximumViewportSize;
                if (layoutCreationSpecification.DrawingArea.Size.Width < layoutCreationSpecification.DrawingArea.Size.Height)
                {
                    viewportSize = viewportSize.Rotate();
                }
            }
            else if (cboPrinterPaperformat.Text == "A3")
            {
                viewportSize = layoutCreationSpecification.UseTextfield ? PaperformatTextfieldA3.MaximumViewportSize : PaperformatA3.MaximumViewportSize;
            }
            if (viewportSize == null)
            {
                throw new InvalidOperationException("cannot calculate scale for other formats than A4 and A3");
            }

            Size drawingAreaSize = layoutCreationSpecification.DrawingArea.Size;
            Point drawingAreaCenter = layoutCreationSpecification.DrawingArea.LowerRightPoint + 0.5 * new Size(-drawingAreaSize.Width, drawingAreaSize.Height);
            double scaleWidth = viewportSize.Width / drawingAreaSize.Width;
            double scaleHeight = viewportSize.Height / drawingAreaSize.Height;
            double minimumScaleFactor = Math.Min(scaleHeight, scaleWidth);
            var size = 1 / minimumScaleFactor * viewportSize;
            var lowerRightPoint = drawingAreaCenter + 0.5 * new Size(size.Width, -size.Height);
            layoutCreationSpecification.DrawingArea = new Frame(lowerRightPoint, size);
            layoutCreationSpecification.Scale = minimumScaleFactor / layoutCreationSpecification.DrawingUnit;
        }

        #endregion

        #region Validation

        private void ValidateExtract()
        {
            if (layoutCreationSpecification.DrawingArea == null)
            {
                errorProvider.SetError(butDefineExtract, LocalData.NoExtractDefinedError);
            }
            else
            {
                errorProvider.SetError(butDefineExtract, String.Empty);
            }
        }

        private void ValidatePrinterPaperformats()
        {
            if (this.SelectablePaperformats.Count == 0)
            {
                errorProvider.SetError(cboPrinterPaperformat, LocalData.PaperformatListEmptyError);
            }
            else
            {
                ValidatePrinterPaperformatFitting();
            }
        }

        private void ValidatePrinterPaperformatFitting()
        {
            using (var progressDialog = new ProgressDialog())
            {
                if (!UseExactExtract && !PaperformatPrinterMapping.IsFormatFitting(layoutCreationSpecification.Printerformat, layoutCreationSpecification.Paperformat, progressDialog))
                {
                    errorProvider.SetError(cboPrinterPaperformat, LocalData.PaperformatNotFitting);
                }
                else
                {
                    errorProvider.SetError(cboPrinterPaperformat, String.Empty);
                }
            }
        }

        private void ValidateCreationAvailable()
        {
            butCreate.Enabled = layoutCreationSpecification.IsValid;
        }

        #endregion

        #region Handler

        private void CboPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPrinter = cboPrinter.SelectedItem as Printer;
            ReloadPrinterPaperformats();
        }

        private void CboPrinterPaperformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkExactExtract.Enabled = cboPrinterPaperformat.Text == "A4" || cboPrinterPaperformat.Text == "A3";
            if (!chkExactExtract.Enabled)
            {
                chkExactExtract.Checked = false;
            }
            layoutCreationSpecification.Printerformat = cboPrinterPaperformat.SelectedItem as PrinterPaperformat;
            InputChanged();
        }

        private void ChkOptimizedPaperformats_CheckedChanged(object sender, EventArgs e)
        {
            ReloadPrinterPaperformats();
            SelectOptimalPrinterPaperformat(false);
            InputChanged();
        }

        private void OptExtractDrawingArea_CheckedChanged(object sender, EventArgs e)
        {
            if (optExtractDrawingArea.Checked)
            {
                layoutCreationSpecification.LoadDataForPredefinedDrawingArea();
                SelectDefaultPrinterAndOptimalFormat();
                InputChanged();
            }
        }

        private void OptExtractManual_CheckedChanged(object sender, EventArgs e)
        {
            if (optExtractManual.Checked)
            {
                SelectOptimalPrinterPaperformat(false);
                InputChanged();
            }
        }

        private void ValidateInputs(object sender, EventArgs e)
        {
            InputChanged();
        }

        private void CboScale_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void FrmLayout_KeyPress(object sender, KeyPressEventArgs e)
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
