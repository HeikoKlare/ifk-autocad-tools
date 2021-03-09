namespace AutoCADTools.PrintLayout
{
    partial class FrmLayout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox grpPaperformat;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLayout));
            System.Windows.Forms.GroupBox grpScale;
            System.Windows.Forms.Label lblDrawingUnitMm;
            System.Windows.Forms.Label lblDrawingUnit;
            System.Windows.Forms.Label lblScaleText;
            System.Windows.Forms.Label lblScale;
            System.Windows.Forms.Label lblLayoutName;
            System.Windows.Forms.Label lblPrinter;
            this.chkTextfield = new System.Windows.Forms.CheckBox();
            this.optPortrait = new System.Windows.Forms.RadioButton();
            this.optLandscape = new System.Windows.Forms.RadioButton();
            this.cboPaperformat = new System.Windows.Forms.ComboBox();
            this.lblPaperformat = new System.Windows.Forms.Label();
            this.chkOptimizedPaperformats = new System.Windows.Forms.CheckBox();
            this.updDrawingUnit = new System.Windows.Forms.NumericUpDown();
            this.chkExactExtract = new System.Windows.Forms.CheckBox();
            this.cboScale = new System.Windows.Forms.ComboBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.butDefineExtract = new System.Windows.Forms.Button();
            this.txtLayoutName = new System.Windows.Forms.TextBox();
            this.cboPrinter = new System.Windows.Forms.ComboBox();
            this.butCreate = new System.Windows.Forms.Button();
            this.chkUseDrawingArea = new System.Windows.Forms.CheckBox();
            grpPaperformat = new System.Windows.Forms.GroupBox();
            grpScale = new System.Windows.Forms.GroupBox();
            lblDrawingUnitMm = new System.Windows.Forms.Label();
            lblDrawingUnit = new System.Windows.Forms.Label();
            lblScaleText = new System.Windows.Forms.Label();
            lblScale = new System.Windows.Forms.Label();
            lblLayoutName = new System.Windows.Forms.Label();
            lblPrinter = new System.Windows.Forms.Label();
            grpPaperformat.SuspendLayout();
            grpScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updDrawingUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPaperformat
            // 
            grpPaperformat.Controls.Add(this.chkTextfield);
            grpPaperformat.Controls.Add(this.optPortrait);
            grpPaperformat.Controls.Add(this.optLandscape);
            grpPaperformat.Controls.Add(this.cboPaperformat);
            grpPaperformat.Controls.Add(this.lblPaperformat);
            grpPaperformat.Controls.Add(this.chkOptimizedPaperformats);
            resources.ApplyResources(grpPaperformat, "grpPaperformat");
            grpPaperformat.Name = "grpPaperformat";
            grpPaperformat.TabStop = false;
            // 
            // chkTextfield
            // 
            resources.ApplyResources(this.chkTextfield, "chkTextfield");
            this.chkTextfield.Checked = true;
            this.chkTextfield.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTextfield.Name = "chkTextfield";
            this.chkTextfield.UseVisualStyleBackColor = true;
            this.chkTextfield.CheckedChanged += new System.EventHandler(this.ChkTextfield_CheckedChanged);
            // 
            // optPortrait
            // 
            resources.ApplyResources(this.optPortrait, "optPortrait");
            this.optPortrait.Name = "optPortrait";
            this.optPortrait.UseVisualStyleBackColor = true;
            // 
            // optLandscape
            // 
            resources.ApplyResources(this.optLandscape, "optLandscape");
            this.optLandscape.Checked = true;
            this.optLandscape.Name = "optLandscape";
            this.optLandscape.TabStop = true;
            this.optLandscape.UseVisualStyleBackColor = true;
            // 
            // cboPaperformat
            // 
            this.cboPaperformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaperformat.DropDownWidth = 60;
            resources.ApplyResources(this.cboPaperformat, "cboPaperformat");
            this.cboPaperformat.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPaperformat.Name = "cboPaperformat";
            this.cboPaperformat.SelectedIndexChanged += new System.EventHandler(this.CboPaperformat_SelectedIndexChanged);
            // 
            // lblPaperformat
            // 
            resources.ApplyResources(this.lblPaperformat, "lblPaperformat");
            this.lblPaperformat.Name = "lblPaperformat";
            // 
            // chkOptimizedPaperformats
            // 
            this.chkOptimizedPaperformats.AutoEllipsis = true;
            this.chkOptimizedPaperformats.Checked = true;
            this.chkOptimizedPaperformats.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.chkOptimizedPaperformats, "chkOptimizedPaperformats");
            this.chkOptimizedPaperformats.Name = "chkOptimizedPaperformats";
            this.chkOptimizedPaperformats.UseVisualStyleBackColor = true;
            this.chkOptimizedPaperformats.CheckedChanged += new System.EventHandler(this.ChkOptimizedPaperformats_CheckedChanged);
            // 
            // grpScale
            // 
            grpScale.Controls.Add(this.updDrawingUnit);
            grpScale.Controls.Add(this.chkExactExtract);
            grpScale.Controls.Add(lblDrawingUnitMm);
            grpScale.Controls.Add(lblDrawingUnit);
            grpScale.Controls.Add(this.cboScale);
            grpScale.Controls.Add(lblScaleText);
            grpScale.Controls.Add(lblScale);
            resources.ApplyResources(grpScale, "grpScale");
            grpScale.Name = "grpScale";
            grpScale.TabStop = false;
            // 
            // updDrawingUnit
            // 
            resources.ApplyResources(this.updDrawingUnit, "updDrawingUnit");
            this.updDrawingUnit.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.updDrawingUnit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updDrawingUnit.Name = "updDrawingUnit";
            this.updDrawingUnit.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.updDrawingUnit.ValueChanged += new System.EventHandler(this.UpdDrawingUnit_ValueChanged);
            // 
            // chkExactExtract
            // 
            resources.ApplyResources(this.chkExactExtract, "chkExactExtract");
            this.chkExactExtract.Name = "chkExactExtract";
            this.chkExactExtract.UseVisualStyleBackColor = true;
            this.chkExactExtract.CheckedChanged += new System.EventHandler(this.ChkExactExtract_CheckedChanged);
            // 
            // lblDrawingUnitMm
            // 
            resources.ApplyResources(lblDrawingUnitMm, "lblDrawingUnitMm");
            lblDrawingUnitMm.Name = "lblDrawingUnitMm";
            // 
            // lblDrawingUnit
            // 
            resources.ApplyResources(lblDrawingUnit, "lblDrawingUnit");
            lblDrawingUnit.Name = "lblDrawingUnit";
            // 
            // cboScale
            // 
            resources.ApplyResources(this.cboScale, "cboScale");
            this.cboScale.FormattingEnabled = true;
            this.cboScale.Name = "cboScale";
            this.cboScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboScale_KeyPress);
            this.cboScale.Validating += new System.ComponentModel.CancelEventHandler(this.CboScale_Validating);
            // 
            // lblScaleText
            // 
            resources.ApplyResources(lblScaleText, "lblScaleText");
            lblScaleText.Name = "lblScaleText";
            // 
            // lblScale
            // 
            resources.ApplyResources(lblScale, "lblScale");
            lblScale.Name = "lblScale";
            // 
            // lblLayoutName
            // 
            resources.ApplyResources(lblLayoutName, "lblLayoutName");
            lblLayoutName.Name = "lblLayoutName";
            // 
            // lblPrinter
            // 
            resources.ApplyResources(lblPrinter, "lblPrinter");
            lblPrinter.Name = "lblPrinter";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // butDefineExtract
            // 
            resources.ApplyResources(this.butDefineExtract, "butDefineExtract");
            this.butDefineExtract.Name = "butDefineExtract";
            this.butDefineExtract.UseVisualStyleBackColor = true;
            this.butDefineExtract.Click += new System.EventHandler(this.ButDefineExtract_Click);
            // 
            // txtLayoutName
            // 
            resources.ApplyResources(this.txtLayoutName, "txtLayoutName");
            this.txtLayoutName.Name = "txtLayoutName";
            this.txtLayoutName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLayoutName_Validating);
            // 
            // cboPrinter
            // 
            this.cboPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboPrinter, "cboPrinter");
            this.cboPrinter.FormattingEnabled = true;
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.SelectedIndexChanged += new System.EventHandler(this.CboPrinter_SelectedIndexChanged);
            // 
            // butCreate
            // 
            resources.ApplyResources(this.butCreate, "butCreate");
            this.butCreate.Name = "butCreate";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.ButCreate_Click);
            // 
            // chkUseDrawingArea
            // 
            resources.ApplyResources(this.chkUseDrawingArea, "chkUseDrawingArea");
            this.chkUseDrawingArea.Name = "chkUseDrawingArea";
            this.chkUseDrawingArea.UseVisualStyleBackColor = true;
            this.chkUseDrawingArea.CheckedChanged += new System.EventHandler(this.ChkUseDrawingArea_CheckedChanged);
            // 
            // FrmLayout
            // 
            this.AcceptButton = this.butCreate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkUseDrawingArea);
            this.Controls.Add(this.butDefineExtract);
            this.Controls.Add(this.butCreate);
            this.Controls.Add(this.txtLayoutName);
            this.Controls.Add(this.cboPrinter);
            this.Controls.Add(lblLayoutName);
            this.Controls.Add(lblPrinter);
            this.Controls.Add(grpScale);
            this.Controls.Add(grpPaperformat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLayout";
            this.Load += new System.EventHandler(this.FrmLayout_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LayoutUI_KeyPress);
            grpPaperformat.ResumeLayout(false);
            grpPaperformat.PerformLayout();
            grpScale.ResumeLayout(false);
            grpScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updDrawingUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.RadioButton optPortrait;
        private System.Windows.Forms.RadioButton optLandscape;
        private System.Windows.Forms.ComboBox cboPaperformat;
        private System.Windows.Forms.Label lblPaperformat;
        private System.Windows.Forms.CheckBox chkExactExtract;
        private System.Windows.Forms.ComboBox cboScale;
        private System.Windows.Forms.TextBox txtLayoutName;
        private System.Windows.Forms.ComboBox cboPrinter;
        private System.Windows.Forms.NumericUpDown updDrawingUnit;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.Button butDefineExtract;
        private System.Windows.Forms.CheckBox chkTextfield;
        private System.Windows.Forms.CheckBox chkOptimizedPaperformats;
        private System.Windows.Forms.CheckBox chkUseDrawingArea;
    }
}