namespace AutoCADTools.Tools
{
    partial class FrmDiagonalBracing
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2010")]
        private void InitializeComponent()
        {
            System.Windows.Forms.Label lblDescription;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDiagonalBracing));
            System.Windows.Forms.Label lblPositonnumber;
            System.Windows.Forms.Label lblCount;
            System.Windows.Forms.Label lblDistance;
            System.Windows.Forms.Label lblDistanceUnit;
            this.cboDescription = new System.Windows.Forms.ComboBox();
            this.butCreate = new System.Windows.Forms.Button();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.updCount = new System.Windows.Forms.NumericUpDown();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.optThirdsPoint = new System.Windows.Forms.RadioButton();
            this.optMiddlePoint = new System.Windows.Forms.RadioButton();
            this.optDirectInput = new System.Windows.Forms.RadioButton();
            this.grpInputMode = new System.Windows.Forms.GroupBox();
            this.grpDisplay = new System.Windows.Forms.GroupBox();
            this.optCurrentLayer = new System.Windows.Forms.RadioButton();
            this.optTopChordPlane = new System.Windows.Forms.RadioButton();
            lblDescription = new System.Windows.Forms.Label();
            lblPositonnumber = new System.Windows.Forms.Label();
            lblCount = new System.Windows.Forms.Label();
            lblDistance = new System.Windows.Forms.Label();
            lblDistanceUnit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.updCount)).BeginInit();
            this.grpInputMode.SuspendLayout();
            this.grpDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            resources.ApplyResources(lblDescription, "lblDescription");
            lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDescription.Name = "lblDescription";
            // 
            // lblPositonnumber
            // 
            resources.ApplyResources(lblPositonnumber, "lblPositonnumber");
            lblPositonnumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblPositonnumber.Name = "lblPositonnumber";
            // 
            // lblCount
            // 
            resources.ApplyResources(lblCount, "lblCount");
            lblCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblCount.Name = "lblCount";
            // 
            // lblDistance
            // 
            resources.ApplyResources(lblDistance, "lblDistance");
            lblDistance.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDistance.Name = "lblDistance";
            // 
            // lblDistanceUnit
            // 
            resources.ApplyResources(lblDistanceUnit, "lblDistanceUnit");
            lblDistanceUnit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDistanceUnit.Name = "lblDistanceUnit";
            // 
            // cboDescription
            // 
            resources.ApplyResources(this.cboDescription, "cboDescription");
            this.cboDescription.FormattingEnabled = true;
            this.cboDescription.Items.AddRange(new object[] {
            resources.GetString("cboDescription.Items"),
            resources.GetString("cboDescription.Items1"),
            resources.GetString("cboDescription.Items2"),
            resources.GetString("cboDescription.Items3")});
            this.cboDescription.Name = "cboDescription";
            // 
            // butCreate
            // 
            resources.ApplyResources(this.butCreate, "butCreate");
            this.butCreate.Name = "butCreate";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
            // 
            // txtPosition
            // 
            resources.ApplyResources(this.txtPosition, "txtPosition");
            this.txtPosition.Name = "txtPosition";
            // 
            // updCount
            // 
            resources.ApplyResources(this.updCount, "updCount");
            this.updCount.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.updCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updCount.Name = "updCount";
            this.updCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updCount.ValueChanged += new System.EventHandler(this.updCount_ValueChanged);
            // 
            // txtDistance
            // 
            resources.ApplyResources(this.txtDistance, "txtDistance");
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistance_KeyPress);
            // 
            // optThirdsPoint
            // 
            resources.ApplyResources(this.optThirdsPoint, "optThirdsPoint");
            this.optThirdsPoint.Checked = true;
            this.optThirdsPoint.Name = "optThirdsPoint";
            this.optThirdsPoint.TabStop = true;
            this.optThirdsPoint.UseVisualStyleBackColor = true;
            // 
            // optMiddlePoint
            // 
            resources.ApplyResources(this.optMiddlePoint, "optMiddlePoint");
            this.optMiddlePoint.Name = "optMiddlePoint";
            this.optMiddlePoint.UseVisualStyleBackColor = true;
            // 
            // optDirectInput
            // 
            resources.ApplyResources(this.optDirectInput, "optDirectInput");
            this.optDirectInput.Name = "optDirectInput";
            this.optDirectInput.UseVisualStyleBackColor = true;
            this.optDirectInput.CheckedChanged += new System.EventHandler(this.optDirectInput_CheckedChanged);
            // 
            // grpInputMode
            // 
            this.grpInputMode.Controls.Add(this.optThirdsPoint);
            this.grpInputMode.Controls.Add(this.optDirectInput);
            this.grpInputMode.Controls.Add(this.optMiddlePoint);
            this.grpInputMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpInputMode, "grpInputMode");
            this.grpInputMode.Name = "grpInputMode";
            this.grpInputMode.TabStop = false;
            // 
            // grpDisplay
            // 
            this.grpDisplay.Controls.Add(this.optCurrentLayer);
            this.grpDisplay.Controls.Add(this.optTopChordPlane);
            this.grpDisplay.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpDisplay, "grpDisplay");
            this.grpDisplay.Name = "grpDisplay";
            this.grpDisplay.TabStop = false;
            // 
            // optCurrentLayer
            // 
            resources.ApplyResources(this.optCurrentLayer, "optCurrentLayer");
            this.optCurrentLayer.Name = "optCurrentLayer";
            this.optCurrentLayer.UseVisualStyleBackColor = true;
            // 
            // optTopChordPlane
            // 
            resources.ApplyResources(this.optTopChordPlane, "optTopChordPlane");
            this.optTopChordPlane.Checked = true;
            this.optTopChordPlane.Name = "optTopChordPlane";
            this.optTopChordPlane.TabStop = true;
            this.optTopChordPlane.UseVisualStyleBackColor = true;
            // 
            // FrmDiagonalBracing
            // 
            this.AcceptButton = this.butCreate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.grpDisplay);
            this.Controls.Add(this.grpInputMode);
            this.Controls.Add(lblDistanceUnit);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(lblDistance);
            this.Controls.Add(this.updCount);
            this.Controls.Add(lblCount);
            this.Controls.Add(this.cboDescription);
            this.Controls.Add(this.butCreate);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(lblDescription);
            this.Controls.Add(lblPositonnumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDiagonalBracing";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FrmDiagonalBracing_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDiagonalBracing_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.updCount)).EndInit();
            this.grpInputMode.ResumeLayout(false);
            this.grpInputMode.PerformLayout();
            this.grpDisplay.ResumeLayout(false);
            this.grpDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDescription;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.NumericUpDown updCount;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.RadioButton optThirdsPoint;
        private System.Windows.Forms.RadioButton optMiddlePoint;
        private System.Windows.Forms.RadioButton optDirectInput;
        private System.Windows.Forms.GroupBox grpInputMode;
        private System.Windows.Forms.GroupBox grpDisplay;
        private System.Windows.Forms.RadioButton optCurrentLayer;
        private System.Windows.Forms.RadioButton optTopChordPlane;

    }
}