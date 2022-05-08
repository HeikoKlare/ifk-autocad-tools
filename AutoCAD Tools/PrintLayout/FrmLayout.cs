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

        // Instance fields
        private readonly bool oldTextfieldUsed;
        
        // State fields
        private readonly LayoutCreationSpecification layoutCreationSpecification = new LayoutCreationSpecification();
        private Printer selectedPrinter;
        private IReadOnlyList<PrinterPaperformat> selectablePaperformats = new List<PrinterPaperformat>();

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
            txtLayoutName.DataBindings.Add(nameof(txtLayoutName.Text), layoutCreationSpecification, nameof(LayoutCreationSpecification.LayoutName), false, DataSourceUpdateMode.OnValidation);
            chkTextfield.DataBindings.Add(nameof(chkTextfield.Enabled), layoutCreationSpecification, nameof(LayoutCreationSpecification.CanUseTextfield), false);
            chkTextfield.DataBindings.Add(nameof(chkTextfield.Checked), layoutCreationSpecification, nameof(LayoutCreationSpecification.UseTextfield), false, DataSourceUpdateMode.OnPropertyChanged);
            BindExtractElements();
            BindPapersizeLabel();
            SetInitialValues();
            LoadPrinters();
            SelectDefaultPrinterAndOptimalFormat();
            ValidateInputs();
        }

        private void BindExtractElements()
        {
            optExtractDrawingArea.DataBindings.Add(nameof(optExtractDrawingArea.Enabled), layoutCreationSpecification, nameof(LayoutCreationSpecification.HasPredefinedDrawingArea), false);
            Binding cboScaleEnabledBinding = new Binding(nameof(cboScale.Enabled), chkExactExtract, nameof(CheckBox.Checked), false, DataSourceUpdateMode.OnPropertyChanged);
            cboScaleEnabledBinding.Format += (sender, eventArgs) => { eventArgs.Value = !(bool)eventArgs.Value; };
            cboScale.DataBindings.Add(cboScaleEnabledBinding);
            Binding updDrawingUnitEnabledBinding = new Binding(nameof(updDrawingUnit.Enabled), chkExactExtract, nameof(CheckBox.Checked), false, DataSourceUpdateMode.OnPropertyChanged);
            updDrawingUnitEnabledBinding.Format += (sender, eventArgs) => { eventArgs.Value = !(bool)eventArgs.Value; };
            updDrawingUnit.DataBindings.Add(updDrawingUnitEnabledBinding);
            updDrawingUnit.DataBindings.Add(nameof(updDrawingUnit.Value), layoutCreationSpecification, nameof(LayoutCreationSpecification.DrawingUnit), false, DataSourceUpdateMode.OnPropertyChanged);
            LoadAndBindAnnotationScales();
        }

        private void BindPapersizeLabel()
        {
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

        private void LoadPrinters()
        {
            cboPrinter.SelectedIndexChanged -= CboPrinter_SelectedIndexChanged;
            cboPrinter.DataSource = PrinterRepository.Instance.Printers;
            cboPrinter.DisplayMember = nameof(Printer.Name);
            cboPrinter.SelectedIndex = -1;
            cboPrinter.SelectedIndexChanged += CboPrinter_SelectedIndexChanged;
        }

        private void LoadAndBindAnnotationScales()
        {
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

        private void SetInitialValues()
        {
            optExtractDrawingArea.Checked = optExtractDrawingArea.Enabled;
            optExtractManual.Checked = !optExtractManual.Enabled;
            cboScale.SelectedItem = layoutCreationSpecification.Document.Database.Cannoscale.DrawingUnits;
        }

        #endregion

        #region Actions

        private void ButDefineExtract_Click(object sender, EventArgs e)
        {
            var promptedFrame = PromptExtractExtends();
            layoutCreationSpecification.DrawingArea.LowerRightPoint = promptedFrame.LowerRightPoint;
            layoutCreationSpecification.DrawingArea.Size = promptedFrame.Size;
            optExtractManual.Checked = true;
            SelectDefaultPrinterAndOptimalFormat();
            ValidateInputs();
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
            var result = new Frame(null)
            {
                LowerRightPoint = new Point(Math.Max(firstPoint.X, secondPoint.X), Math.Min(firstPoint.Y, secondPoint.Y)),
                Size = new Size(Math.Abs(firstPoint.X - secondPoint.X), Math.Abs(firstPoint.Y - secondPoint.Y))
            };
            return result;
        }

        private void ButCreate_Click(object sender, EventArgs e)
        {
            ValidateInputs();
            if (!butCreate.Enabled) return;

            if (!LayoutManager.Current.GetLayoutId(layoutCreationSpecification.LayoutName).IsNull)
            {
                if (MessageBox.Show(LocalData.DuplicateLayoutNameText, LocalData.DuplicateLayoutNameTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            this.Close();

            if (chkExactExtract.Checked)
            {
                layoutCreationSpecification.Scale = SetPaperformatForExactExtract();
            }
            else if (!layoutCreationSpecification.UseTextfield)
            {
                Size difference = 1 / layoutCreationSpecification.Scale / ((double)layoutCreationSpecification.DrawingUnit) * layoutCreationSpecification.Paperformat.ViewportSizeLayout - layoutCreationSpecification.DrawingArea.Size;
                layoutCreationSpecification.DrawingArea.LowerRightPoint += 0.5 * new Size(difference.Width, -difference.Height);
            }

            new LayoutCreator(layoutCreationSpecification).CreateLayout();
        }

        private double SetPaperformatForExactExtract()
        {
            Paperformat temporaryPaperformat = null;
            if (cboPaperformat.Text == "A4")
            {
                if (chkTextfield.Checked)
                {
                    if (layoutCreationSpecification.DrawingArea.Size.Width > layoutCreationSpecification.DrawingArea.Size.Height)
                    {
                        temporaryPaperformat = new PaperformatTextfieldA4Horizontal(oldTextfieldUsed);
                    }
                    else
                    {
                        temporaryPaperformat = new PaperformatTextfieldA4Vertical(oldTextfieldUsed);
                    }
                }
                else
                {
                    if (layoutCreationSpecification.DrawingArea.Size.Width > layoutCreationSpecification.DrawingArea.Size.Height)
                    {
                        temporaryPaperformat = new PaperformatA4Horizontal();
                    }
                    else
                    {
                        temporaryPaperformat = new PaperformatA4Vertical();
                    }
                }
            }
            else if (cboPaperformat.Text == "A3")
            {
                if (chkTextfield.Checked)
                {
                    temporaryPaperformat = new PaperformatTextfieldA3(oldTextfieldUsed);
                }
                else
                {
                    temporaryPaperformat = new PaperformatA3();
                }
            }

            double drawingUnit = (double)updDrawingUnit.Value;
            double scaleWidth = temporaryPaperformat.ViewportSizeModel.Width / (layoutCreationSpecification.DrawingArea.Size.Width * drawingUnit);
            double scaleHeight = temporaryPaperformat.ViewportSizeModel.Height / (layoutCreationSpecification.DrawingArea.Size.Height * drawingUnit);
            Size addition = scaleHeight < scaleWidth ? new Size(layoutCreationSpecification.DrawingArea.Size.Width * (scaleWidth / scaleHeight - 1), 0) : new Size(0, layoutCreationSpecification.DrawingArea.Size.Height * (scaleHeight / scaleWidth - 1));
            layoutCreationSpecification.DrawingArea.LowerRightPoint += 0.5 * new Size(addition.Width, -addition.Height);
            layoutCreationSpecification.DrawingArea.Size += addition;
            return Math.Min(scaleHeight, scaleWidth);
        }

        #endregion

        #region Methods

        private void ReloadPrinterPaperformats()
        {
            var oldFormatName = cboPaperformat.Text;
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
            cboPaperformat.DisplayMember = nameof(PrinterPaperformat.Name);
            cboPaperformat.Items.AddRange(selectablePaperformats.ToArray());
            int index = cboPaperformat.FindStringExact(oldFormatName);
            cboPaperformat.SelectedIndex = index != -1 || cboPaperformat.Items.Count == 0 ? index : 0;
            ValidateInputs();
        }

        private void SelectDefaultPrinterAndOptimalFormat()
        {
            if (!chkExactExtract.Checked && layoutCreationSpecification.Paperformat != null)
            {
                var defaultPrinter = layoutCreationSpecification.Paperformat.GetDefaultPrinter();
                int printerIndex = defaultPrinter == null ? -1 : cboPrinter.FindStringExact(defaultPrinter.Name);

                if (printerIndex != -1)
                {
                    cboPrinter.SelectedIndex = printerIndex;
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
            if (!chkExactExtract.Checked && layoutCreationSpecification.Paperformat != null && selectedPrinter != null && selectedPrinter.Initialized)
            {
                using (var progressDialog = new ProgressDialog())
                {
                    if (!optimizeIfFitting && PaperformatPrinterMapping.IsFormatFitting(layoutCreationSpecification.Printerformat, layoutCreationSpecification.Paperformat, progressDialog))
                    {
                        return;
                    }
                    var printerPaperformat = layoutCreationSpecification.Paperformat.GetFittingPaperformat(selectedPrinter, chkOptimizedPaperformats.Checked, progressDialog);
                    int formatIndex = printerPaperformat != null ? cboPaperformat.FindStringExact(printerPaperformat.Name) : -1;
                    if (formatIndex != -1)
                    {
                        cboPaperformat.SelectedIndex = formatIndex;
                    }
                }
            }
            ValidateInputs();
        }

        #endregion

        #region Validation

        private void ValidateInputs()
        {
            ValidateExtract();
            ValidatePrinterPaperformats();
            ValidateCreationAvailable();
        }

        private void ValidateExtract()
        {
            if (layoutCreationSpecification.DrawingArea.LowerRightPoint == null)
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
            if (this.selectablePaperformats.Count == 0)
            {
                errorProvider.SetError(cboPaperformat, LocalData.PaperformatListEmptyError);
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
                if (!chkExactExtract.Checked && !PaperformatPrinterMapping.IsFormatFitting(layoutCreationSpecification.Printerformat, layoutCreationSpecification.Paperformat, progressDialog))
                {
                    errorProvider.SetError(cboPaperformat, LocalData.PaperformatNotFitting);
                }
                else
                {
                    errorProvider.SetError(cboPaperformat, String.Empty);
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

        private void CboPaperformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkExactExtract.Enabled = cboPaperformat.Text == "A4" || cboPaperformat.Text == "A3";
            if (!chkExactExtract.Enabled)
            {
                chkExactExtract.Checked = false;
            }
            layoutCreationSpecification.Printerformat = cboPaperformat.SelectedItem as PrinterPaperformat;
            ValidateInputs();
        }

        private void ChkOptimizedPaperformats_CheckedChanged(object sender, EventArgs e)
        {
            ReloadPrinterPaperformats();
            SelectOptimalPrinterPaperformat(false);
            ValidateInputs();
        }

        private void OptExtractDrawingArea_CheckedChanged(object sender, EventArgs e)
        {
            if (optExtractDrawingArea.Checked)
            {
                layoutCreationSpecification.LoadDataForPredefinedDrawingArea();
            }
            SelectDefaultPrinterAndOptimalFormat();
            ValidateInputs();
        }

        private void ValidateInputs(object sender, EventArgs e)
        {
            ValidateInputs();
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
