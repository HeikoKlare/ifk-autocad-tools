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
            resources.ApplyResources(grpPaperformat, "grpPaperformat");
            grpPaperformat.Controls.Add(this.chkTextfield);
            grpPaperformat.Controls.Add(this.cboPaperformat);
            grpPaperformat.Controls.Add(this.lblPaperformat);
            grpPaperformat.Controls.Add(this.chkOptimizedPaperformats);
            this.errorProvider.SetError(grpPaperformat, resources.GetString("grpPaperformat.Error"));
            this.errorProvider.SetIconAlignment(grpPaperformat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpPaperformat.IconAlignment"))));
            this.errorProvider.SetIconPadding(grpPaperformat, ((int)(resources.GetObject("grpPaperformat.IconPadding"))));
            grpPaperformat.Name = "grpPaperformat";
            grpPaperformat.TabStop = false;
            // 
            // chkTextfield
            // 
            resources.ApplyResources(this.chkTextfield, "chkTextfield");
            this.chkTextfield.Checked = true;
            this.chkTextfield.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider.SetError(this.chkTextfield, resources.GetString("chkTextfield.Error"));
            this.errorProvider.SetIconAlignment(this.chkTextfield, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("chkTextfield.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.chkTextfield, ((int)(resources.GetObject("chkTextfield.IconPadding"))));
            this.chkTextfield.Name = "chkTextfield";
            this.chkTextfield.UseVisualStyleBackColor = true;
            this.chkTextfield.CheckedChanged += new System.EventHandler(this.ChkTextfield_CheckedChanged);
            // 
            // cboPaperformat
            // 
            resources.ApplyResources(this.cboPaperformat, "cboPaperformat");
            this.cboPaperformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaperformat.DropDownWidth = 60;
            this.errorProvider.SetError(this.cboPaperformat, resources.GetString("cboPaperformat.Error"));
            this.cboPaperformat.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider.SetIconAlignment(this.cboPaperformat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboPaperformat.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.cboPaperformat, ((int)(resources.GetObject("cboPaperformat.IconPadding"))));
            this.cboPaperformat.Name = "cboPaperformat";
            this.cboPaperformat.SelectedIndexChanged += new System.EventHandler(this.CboPaperformat_SelectedIndexChanged);
            // 
            // lblPaperformat
            // 
            resources.ApplyResources(this.lblPaperformat, "lblPaperformat");
            this.errorProvider.SetError(this.lblPaperformat, resources.GetString("lblPaperformat.Error"));
            this.errorProvider.SetIconAlignment(this.lblPaperformat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPaperformat.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblPaperformat, ((int)(resources.GetObject("lblPaperformat.IconPadding"))));
            this.lblPaperformat.Name = "lblPaperformat";
            // 
            // chkOptimizedPaperformats
            // 
            resources.ApplyResources(this.chkOptimizedPaperformats, "chkOptimizedPaperformats");
            this.chkOptimizedPaperformats.AutoEllipsis = true;
            this.chkOptimizedPaperformats.Checked = true;
            this.chkOptimizedPaperformats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider.SetError(this.chkOptimizedPaperformats, resources.GetString("chkOptimizedPaperformats.Error"));
            this.errorProvider.SetIconAlignment(this.chkOptimizedPaperformats, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("chkOptimizedPaperformats.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.chkOptimizedPaperformats, ((int)(resources.GetObject("chkOptimizedPaperformats.IconPadding"))));
            this.chkOptimizedPaperformats.Name = "chkOptimizedPaperformats";
            this.chkOptimizedPaperformats.UseVisualStyleBackColor = true;
            this.chkOptimizedPaperformats.CheckedChanged += new System.EventHandler(this.ChkOptimizedPaperformats_CheckedChanged);
            // 
            // grpScale
            // 
            resources.ApplyResources(grpScale, "grpScale");
            grpScale.Controls.Add(this.updDrawingUnit);
            grpScale.Controls.Add(this.chkExactExtract);
            grpScale.Controls.Add(lblDrawingUnitMm);
            grpScale.Controls.Add(lblDrawingUnit);
            grpScale.Controls.Add(this.cboScale);
            grpScale.Controls.Add(lblScaleText);
            grpScale.Controls.Add(lblScale);
            this.errorProvider.SetError(grpScale, resources.GetString("grpScale.Error"));
            this.errorProvider.SetIconAlignment(grpScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpScale.IconAlignment"))));
            this.errorProvider.SetIconPadding(grpScale, ((int)(resources.GetObject("grpScale.IconPadding"))));
            grpScale.Name = "grpScale";
            grpScale.TabStop = false;
            // 
            // updDrawingUnit
            // 
            resources.ApplyResources(this.updDrawingUnit, "updDrawingUnit");
            this.errorProvider.SetError(this.updDrawingUnit, resources.GetString("updDrawingUnit.Error"));
            this.errorProvider.SetIconAlignment(this.updDrawingUnit, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("updDrawingUnit.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.updDrawingUnit, ((int)(resources.GetObject("updDrawingUnit.IconPadding"))));
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
            this.errorProvider.SetError(this.chkExactExtract, resources.GetString("chkExactExtract.Error"));
            this.errorProvider.SetIconAlignment(this.chkExactExtract, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("chkExactExtract.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.chkExactExtract, ((int)(resources.GetObject("chkExactExtract.IconPadding"))));
            this.chkExactExtract.Name = "chkExactExtract";
            this.chkExactExtract.UseVisualStyleBackColor = true;
            this.chkExactExtract.CheckedChanged += new System.EventHandler(this.ChkExactExtract_CheckedChanged);
            // 
            // lblDrawingUnitMm
            // 
            resources.ApplyResources(lblDrawingUnitMm, "lblDrawingUnitMm");
            this.errorProvider.SetError(lblDrawingUnitMm, resources.GetString("lblDrawingUnitMm.Error"));
            this.errorProvider.SetIconAlignment(lblDrawingUnitMm, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDrawingUnitMm.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDrawingUnitMm, ((int)(resources.GetObject("lblDrawingUnitMm.IconPadding"))));
            lblDrawingUnitMm.Name = "lblDrawingUnitMm";
            // 
            // lblDrawingUnit
            // 
            resources.ApplyResources(lblDrawingUnit, "lblDrawingUnit");
            this.errorProvider.SetError(lblDrawingUnit, resources.GetString("lblDrawingUnit.Error"));
            this.errorProvider.SetIconAlignment(lblDrawingUnit, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDrawingUnit.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDrawingUnit, ((int)(resources.GetObject("lblDrawingUnit.IconPadding"))));
            lblDrawingUnit.Name = "lblDrawingUnit";
            // 
            // cboScale
            // 
            resources.ApplyResources(this.cboScale, "cboScale");
            this.errorProvider.SetError(this.cboScale, resources.GetString("cboScale.Error"));
            this.cboScale.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.cboScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboScale.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.cboScale, ((int)(resources.GetObject("cboScale.IconPadding"))));
            this.cboScale.Name = "cboScale";
            this.cboScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboScale_KeyPress);
            this.cboScale.Validating += new System.ComponentModel.CancelEventHandler(this.CboScale_Validating);
            // 
            // lblScaleText
            // 
            resources.ApplyResources(lblScaleText, "lblScaleText");
            this.errorProvider.SetError(lblScaleText, resources.GetString("lblScaleText.Error"));
            this.errorProvider.SetIconAlignment(lblScaleText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblScaleText.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblScaleText, ((int)(resources.GetObject("lblScaleText.IconPadding"))));
            lblScaleText.Name = "lblScaleText";
            // 
            // lblScale
            // 
            resources.ApplyResources(lblScale, "lblScale");
            this.errorProvider.SetError(lblScale, resources.GetString("lblScale.Error"));
            this.errorProvider.SetIconAlignment(lblScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblScale.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblScale, ((int)(resources.GetObject("lblScale.IconPadding"))));
            lblScale.Name = "lblScale";
            // 
            // lblLayoutName
            // 
            resources.ApplyResources(lblLayoutName, "lblLayoutName");
            this.errorProvider.SetError(lblLayoutName, resources.GetString("lblLayoutName.Error"));
            this.errorProvider.SetIconAlignment(lblLayoutName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLayoutName.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblLayoutName, ((int)(resources.GetObject("lblLayoutName.IconPadding"))));
            lblLayoutName.Name = "lblLayoutName";
            // 
            // lblPrinter
            // 
            resources.ApplyResources(lblPrinter, "lblPrinter");
            this.errorProvider.SetError(lblPrinter, resources.GetString("lblPrinter.Error"));
            this.errorProvider.SetIconAlignment(lblPrinter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPrinter.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblPrinter, ((int)(resources.GetObject("lblPrinter.IconPadding"))));
            lblPrinter.Name = "lblPrinter";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // butDefineExtract
            // 
            resources.ApplyResources(this.butDefineExtract, "butDefineExtract");
            this.errorProvider.SetError(this.butDefineExtract, resources.GetString("butDefineExtract.Error"));
            this.errorProvider.SetIconAlignment(this.butDefineExtract, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butDefineExtract.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butDefineExtract, ((int)(resources.GetObject("butDefineExtract.IconPadding"))));
            this.butDefineExtract.Name = "butDefineExtract";
            this.butDefineExtract.UseVisualStyleBackColor = true;
            this.butDefineExtract.Click += new System.EventHandler(this.ButDefineExtract_Click);
            // 
            // txtLayoutName
            // 
            resources.ApplyResources(this.txtLayoutName, "txtLayoutName");
            this.errorProvider.SetError(this.txtLayoutName, resources.GetString("txtLayoutName.Error"));
            this.errorProvider.SetIconAlignment(this.txtLayoutName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLayoutName.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtLayoutName, ((int)(resources.GetObject("txtLayoutName.IconPadding"))));
            this.txtLayoutName.Name = "txtLayoutName";
            this.txtLayoutName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLayoutName_Validating);
            // 
            // butCreate
            // 
            resources.ApplyResources(this.butCreate, "butCreate");
            this.errorProvider.SetError(this.butCreate, resources.GetString("butCreate.Error"));
            this.errorProvider.SetIconAlignment(this.butCreate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butCreate.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butCreate, ((int)(resources.GetObject("butCreate.IconPadding"))));
            this.butCreate.Name = "butCreate";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.ButCreate_Click);
            // 
            // cboPrinter
            // 
            resources.ApplyResources(this.cboPrinter, "cboPrinter");
            this.cboPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider.SetError(this.cboPrinter, resources.GetString("cboPrinter.Error"));
            this.cboPrinter.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.cboPrinter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboPrinter.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.cboPrinter, ((int)(resources.GetObject("cboPrinter.IconPadding"))));
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.SelectedIndexChanged += new System.EventHandler(this.CboPrinter_SelectedIndexChanged);
            // 
            // grpExtract
            // 
            resources.ApplyResources(this.grpExtract, "grpExtract");
            this.grpExtract.Controls.Add(this.optExtractManual);
            this.grpExtract.Controls.Add(this.optExtractDrawingArea);
            this.grpExtract.Controls.Add(this.butDefineExtract);
            this.errorProvider.SetError(this.grpExtract, resources.GetString("grpExtract.Error"));
            this.errorProvider.SetIconAlignment(this.grpExtract, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpExtract.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.grpExtract, ((int)(resources.GetObject("grpExtract.IconPadding"))));
            this.grpExtract.Name = "grpExtract";
            this.grpExtract.TabStop = false;
            // 
            // optExtractManual
            // 
            resources.ApplyResources(this.optExtractManual, "optExtractManual");
            this.errorProvider.SetError(this.optExtractManual, resources.GetString("optExtractManual.Error"));
            this.errorProvider.SetIconAlignment(this.optExtractManual, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("optExtractManual.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.optExtractManual, ((int)(resources.GetObject("optExtractManual.IconPadding"))));
            this.optExtractManual.Name = "optExtractManual";
            this.optExtractManual.TabStop = true;
            this.optExtractManual.UseVisualStyleBackColor = true;
            // 
            // optExtractDrawingArea
            // 
            resources.ApplyResources(this.optExtractDrawingArea, "optExtractDrawingArea");
            this.errorProvider.SetError(this.optExtractDrawingArea, resources.GetString("optExtractDrawingArea.Error"));
            this.errorProvider.SetIconAlignment(this.optExtractDrawingArea, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("optExtractDrawingArea.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.optExtractDrawingArea, ((int)(resources.GetObject("optExtractDrawingArea.IconPadding"))));
            this.optExtractDrawingArea.Name = "optExtractDrawingArea";
            this.optExtractDrawingArea.TabStop = true;
            this.optExtractDrawingArea.UseVisualStyleBackColor = true;
            this.optExtractDrawingArea.CheckedChanged += new System.EventHandler(this.OptExtractDrawingArea_CheckedChanged);
            // 
            // lblCalculatedPapersizeCaption
            // 
            resources.ApplyResources(this.lblCalculatedPapersizeCaption, "lblCalculatedPapersizeCaption");
            this.errorProvider.SetError(this.lblCalculatedPapersizeCaption, resources.GetString("lblCalculatedPapersizeCaption.Error"));
            this.errorProvider.SetIconAlignment(this.lblCalculatedPapersizeCaption, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblCalculatedPapersizeCaption.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblCalculatedPapersizeCaption, ((int)(resources.GetObject("lblCalculatedPapersizeCaption.IconPadding"))));
            this.lblCalculatedPapersizeCaption.Name = "lblCalculatedPapersizeCaption";
            // 
            // lblCalculatedPapersize
            // 
            resources.ApplyResources(this.lblCalculatedPapersize, "lblCalculatedPapersize");
            this.errorProvider.SetError(this.lblCalculatedPapersize, resources.GetString("lblCalculatedPapersize.Error"));
            this.errorProvider.SetIconAlignment(this.lblCalculatedPapersize, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblCalculatedPapersize.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblCalculatedPapersize, ((int)(resources.GetObject("lblCalculatedPapersize.IconPadding"))));
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
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LayoutUI_KeyPress);
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