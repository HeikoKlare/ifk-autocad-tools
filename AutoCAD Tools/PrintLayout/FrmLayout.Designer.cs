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
            this.cboPaperformat = new System.Windows.Forms.ComboBox();
            this.lblPaperformat = new System.Windows.Forms.Label();
            this.chkOptimizedPaperformats = new System.Windows.Forms.CheckBox();
            this.updDrawingUnit = new System.Windows.Forms.NumericUpDown();
            this.chkExactExtract = new System.Windows.Forms.CheckBox();
            this.cboScale = new System.Windows.Forms.ComboBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.butDefineExtract = new System.Windows.Forms.Button();
            this.txtLayoutName = new System.Windows.Forms.TextBox();
            this.butCreate = new System.Windows.Forms.Button();
            this.cboPrinter = new System.Windows.Forms.ComboBox();
            this.grpExtract = new System.Windows.Forms.GroupBox();
            this.optExtractManual = new System.Windows.Forms.RadioButton();
            this.optExtractDrawingArea = new System.Windows.Forms.RadioButton();
            this.lblCalculatedPapersizeCaption = new System.Windows.Forms.Label();
            this.lblCalculatedPapersize = new System.Windows.Forms.Label();
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
            this.grpExtract.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPaperformat
            // 
            grpPaperformat.Controls.Add(this.chkTextfield);
            grpPaperformat.Controls.Add(this.cboPaperformat);
            grpPaperformat.Controls.Add(this.lblPaperformat);
            grpPaperformat.Controls.Add(this.chkOptimizedPaperformats);
            resources.ApplyResources(grpPaperformat, "grpPaperformat");
            this.errorProvider.SetIconAlignment(grpPaperformat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpPaperformat.IconAlignment"))));
            grpPaperformat.Name = "grpPaperformat";
            grpPaperformat.TabStop = false;
            // 
            // chkTextfield
            // 
            resources.ApplyResources(this.chkTextfield, "chkTextfield");
            this.chkTextfield.Checked = true;
            this.chkTextfield.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider.SetIconAlignment(this.chkTextfield, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("chkTextfield.IconAlignment"))));
            this.chkTextfield.Name = "chkTextfield";
            this.chkTextfield.UseVisualStyleBackColor = true;
            // 
            // cboPaperformat
            // 
            this.cboPaperformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaperformat.DropDownWidth = 60;
            resources.ApplyResources(this.cboPaperformat, "cboPaperformat");
            this.cboPaperformat.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider.SetIconAlignment(this.cboPaperformat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboPaperformat.IconAlignment"))));
            this.cboPaperformat.Name = "cboPaperformat";
            this.cboPaperformat.SelectedIndexChanged += new System.EventHandler(this.CboPaperformat_SelectedIndexChanged);
            // 
            // lblPaperformat
            // 
            resources.ApplyResources(this.lblPaperformat, "lblPaperformat");
            this.errorProvider.SetIconAlignment(this.lblPaperformat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPaperformat.IconAlignment"))));
            this.lblPaperformat.Name = "lblPaperformat";
            // 
            // chkOptimizedPaperformats
            // 
            this.chkOptimizedPaperformats.AutoEllipsis = true;
            this.chkOptimizedPaperformats.Checked = true;
            this.chkOptimizedPaperformats.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.chkOptimizedPaperformats, "chkOptimizedPaperformats");
            this.errorProvider.SetIconAlignment(this.chkOptimizedPaperformats, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("chkOptimizedPaperformats.IconAlignment"))));
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
            this.errorProvider.SetIconAlignment(grpScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpScale.IconAlignment"))));
            grpScale.Name = "grpScale";
            grpScale.TabStop = false;
            // 
            // updDrawingUnit
            // 
            resources.ApplyResources(this.updDrawingUnit, "updDrawingUnit");
            this.errorProvider.SetIconAlignment(this.updDrawingUnit, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("updDrawingUnit.IconAlignment"))));
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
            this.updDrawingUnit.Validated += new System.EventHandler(this.ValidateInputs);
            // 
            // chkExactExtract
            // 
            resources.ApplyResources(this.chkExactExtract, "chkExactExtract");
            this.errorProvider.SetIconAlignment(this.chkExactExtract, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("chkExactExtract.IconAlignment"))));
            this.chkExactExtract.Name = "chkExactExtract";
            this.chkExactExtract.UseVisualStyleBackColor = true;
            this.chkExactExtract.CheckedChanged += new System.EventHandler(this.ValidateInputs);
            // 
            // lblDrawingUnitMm
            // 
            resources.ApplyResources(lblDrawingUnitMm, "lblDrawingUnitMm");
            this.errorProvider.SetIconAlignment(lblDrawingUnitMm, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDrawingUnitMm.IconAlignment"))));
            lblDrawingUnitMm.Name = "lblDrawingUnitMm";
            // 
            // lblDrawingUnit
            // 
            resources.ApplyResources(lblDrawingUnit, "lblDrawingUnit");
            this.errorProvider.SetIconAlignment(lblDrawingUnit, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDrawingUnit.IconAlignment"))));
            lblDrawingUnit.Name = "lblDrawingUnit";
            // 
            // cboScale
            // 
            resources.ApplyResources(this.cboScale, "cboScale");
            this.cboScale.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.cboScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboScale.IconAlignment"))));
            this.cboScale.Name = "cboScale";
            this.cboScale.Validated += new System.EventHandler(this.ValidateInputs);
            // 
            // lblScaleText
            // 
            resources.ApplyResources(lblScaleText, "lblScaleText");
            this.errorProvider.SetIconAlignment(lblScaleText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblScaleText.IconAlignment"))));
            lblScaleText.Name = "lblScaleText";
            // 
            // lblScale
            // 
            resources.ApplyResources(lblScale, "lblScale");
            this.errorProvider.SetIconAlignment(lblScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblScale.IconAlignment"))));
            lblScale.Name = "lblScale";
            // 
            // lblLayoutName
            // 
            resources.ApplyResources(lblLayoutName, "lblLayoutName");
            this.errorProvider.SetIconAlignment(lblLayoutName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLayoutName.IconAlignment"))));
            lblLayoutName.Name = "lblLayoutName";
            // 
            // lblPrinter
            // 
            resources.ApplyResources(lblPrinter, "lblPrinter");
            this.errorProvider.SetIconAlignment(lblPrinter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPrinter.IconAlignment"))));
            lblPrinter.Name = "lblPrinter";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // butDefineExtract
            // 
            resources.ApplyResources(this.butDefineExtract, "butDefineExtract");
            this.errorProvider.SetIconAlignment(this.butDefineExtract, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butDefineExtract.IconAlignment"))));
            this.butDefineExtract.Name = "butDefineExtract";
            this.butDefineExtract.UseVisualStyleBackColor = true;
            this.butDefineExtract.Click += new System.EventHandler(this.ButDefineExtract_Click);
            // 
            // txtLayoutName
            // 
            resources.ApplyResources(this.txtLayoutName, "txtLayoutName");
            this.errorProvider.SetIconAlignment(this.txtLayoutName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLayoutName.IconAlignment"))));
            this.txtLayoutName.Name = "txtLayoutName";
            // 
            // butCreate
            // 
            resources.ApplyResources(this.butCreate, "butCreate");
            this.errorProvider.SetIconAlignment(this.butCreate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butCreate.IconAlignment"))));
            this.butCreate.Name = "butCreate";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.ButCreate_Click);
            // 
            // cboPrinter
            // 
            this.cboPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboPrinter, "cboPrinter");
            this.cboPrinter.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.cboPrinter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboPrinter.IconAlignment"))));
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.SelectedIndexChanged += new System.EventHandler(this.CboPrinter_SelectedIndexChanged);
            // 
            // grpExtract
            // 
            this.grpExtract.Controls.Add(this.optExtractManual);
            this.grpExtract.Controls.Add(this.optExtractDrawingArea);
            this.grpExtract.Controls.Add(this.butDefineExtract);
            resources.ApplyResources(this.grpExtract, "grpExtract");
            this.grpExtract.Name = "grpExtract";
            this.grpExtract.TabStop = false;
            // 
            // optExtractManual
            // 
            resources.ApplyResources(this.optExtractManual, "optExtractManual");
            this.optExtractManual.Name = "optExtractManual";
            this.optExtractManual.TabStop = true;
            this.optExtractManual.UseVisualStyleBackColor = true;
            // 
            // optExtractDrawingArea
            // 
            resources.ApplyResources(this.optExtractDrawingArea, "optExtractDrawingArea");
            this.optExtractDrawingArea.Name = "optExtractDrawingArea";
            this.optExtractDrawingArea.TabStop = true;
            this.optExtractDrawingArea.UseVisualStyleBackColor = true;
            this.optExtractDrawingArea.CheckedChanged += new System.EventHandler(this.OptExtractDrawingArea_CheckedChanged);
            // 
            // lblCalculatedPapersizeCaption
            // 
            resources.ApplyResources(this.lblCalculatedPapersizeCaption, "lblCalculatedPapersizeCaption");
            this.lblCalculatedPapersizeCaption.Name = "lblCalculatedPapersizeCaption";
            // 
            // lblCalculatedPapersize
            // 
            resources.ApplyResources(this.lblCalculatedPapersize, "lblCalculatedPapersize");
            this.lblCalculatedPapersize.Name = "lblCalculatedPapersize";
            // 
            // FrmLayout
            // 
            this.AcceptButton = this.butCreate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCalculatedPapersize);
            this.Controls.Add(this.lblCalculatedPapersizeCaption);
            this.Controls.Add(this.grpExtract);
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
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmLayout_KeyPress);
            grpPaperformat.ResumeLayout(false);
            grpPaperformat.PerformLayout();
            grpScale.ResumeLayout(false);
            grpScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updDrawingUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpExtract.ResumeLayout(false);
            this.grpExtract.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox cboPaperformat;
        private System.Windows.Forms.Label lblPaperformat;
        private System.Windows.Forms.CheckBox chkExactExtract;
        private System.Windows.Forms.ComboBox cboScale;
        private System.Windows.Forms.TextBox txtLayoutName;
        private System.Windows.Forms.NumericUpDown updDrawingUnit;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.Button butDefineExtract;
        private System.Windows.Forms.CheckBox chkTextfield;
        private System.Windows.Forms.CheckBox chkOptimizedPaperformats;
        private System.Windows.Forms.ComboBox cboPrinter;
        private System.Windows.Forms.GroupBox grpExtract;
        private System.Windows.Forms.RadioButton optExtractManual;
        private System.Windows.Forms.RadioButton optExtractDrawingArea;
        private System.Windows.Forms.Label lblCalculatedPapersize;
        private System.Windows.Forms.Label lblCalculatedPapersizeCaption;
    }
}