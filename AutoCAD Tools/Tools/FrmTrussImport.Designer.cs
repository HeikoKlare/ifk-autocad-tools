namespace AutoCADTools.Tools
{
    partial class FrmTrussImport
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
        [System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2010")]
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox grpRotation;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTrussImport));
            this.optRotateLeft = new System.Windows.Forms.RadioButton();
            this.optRotateRight = new System.Windows.Forms.RadioButton();
            this.optRotateNo = new System.Windows.Forms.RadioButton();
            this.chkMember = new System.Windows.Forms.CheckBox();
            this.chkBracings = new System.Windows.Forms.CheckBox();
            this.chkBearings = new System.Windows.Forms.CheckBox();
            this.chkPlates = new System.Windows.Forms.CheckBox();
            this.chkDimensions = new System.Windows.Forms.CheckBox();
            this.grpCopyLayer = new System.Windows.Forms.GroupBox();
            this.ofdSource = new System.Windows.Forms.OpenFileDialog();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.butSearchSource = new System.Windows.Forms.Button();
            this.butImport = new System.Windows.Forms.Button();
            this.txtLayerPrefix = new System.Windows.Forms.TextBox();
            this.lblTargetLayer = new System.Windows.Forms.Label();
            this.lblLayerSuffix = new System.Windows.Forms.Label();
            grpRotation = new System.Windows.Forms.GroupBox();
            grpRotation.SuspendLayout();
            this.grpCopyLayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRotation
            // 
            grpRotation.Controls.Add(this.optRotateLeft);
            grpRotation.Controls.Add(this.optRotateRight);
            grpRotation.Controls.Add(this.optRotateNo);
            grpRotation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(grpRotation, "grpRotation");
            grpRotation.Name = "grpRotation";
            grpRotation.TabStop = false;
            // 
            // optRotateLeft
            // 
            resources.ApplyResources(this.optRotateLeft, "optRotateLeft");
            this.optRotateLeft.Checked = true;
            this.optRotateLeft.Image = global::AutoCADTools.Properties.Resources.Truss_left;
            this.optRotateLeft.Name = "optRotateLeft";
            this.optRotateLeft.TabStop = true;
            this.optRotateLeft.UseVisualStyleBackColor = true;
            this.optRotateLeft.CheckedChanged += new System.EventHandler(this.ButRotate_CheckedChanged);
            // 
            // optRotateRight
            // 
            resources.ApplyResources(this.optRotateRight, "optRotateRight");
            this.optRotateRight.Image = global::AutoCADTools.Properties.Resources.Truss_right;
            this.optRotateRight.Name = "optRotateRight";
            this.optRotateRight.UseVisualStyleBackColor = true;
            this.optRotateRight.CheckedChanged += new System.EventHandler(this.ButRotate_CheckedChanged);
            // 
            // optRotateNo
            // 
            resources.ApplyResources(this.optRotateNo, "optRotateNo");
            this.optRotateNo.Image = global::AutoCADTools.Properties.Resources.Truss;
            this.optRotateNo.Name = "optRotateNo";
            this.optRotateNo.UseVisualStyleBackColor = true;
            this.optRotateNo.CheckedChanged += new System.EventHandler(this.ButRotate_CheckedChanged);
            // 
            // chkMember
            // 
            resources.ApplyResources(this.chkMember, "chkMember");
            this.chkMember.Checked = true;
            this.chkMember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMember.Name = "chkMember";
            this.chkMember.UseVisualStyleBackColor = true;
            this.chkMember.CheckedChanged += new System.EventHandler(this.LayerChecked_CheckedChanged);
            // 
            // chkBracings
            // 
            resources.ApplyResources(this.chkBracings, "chkBracings");
            this.chkBracings.Checked = true;
            this.chkBracings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBracings.Name = "chkBracings";
            this.chkBracings.UseVisualStyleBackColor = true;
            this.chkBracings.CheckedChanged += new System.EventHandler(this.LayerChecked_CheckedChanged);
            // 
            // chkBearings
            // 
            resources.ApplyResources(this.chkBearings, "chkBearings");
            this.chkBearings.Checked = true;
            this.chkBearings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBearings.Name = "chkBearings";
            this.chkBearings.UseVisualStyleBackColor = true;
            this.chkBearings.CheckedChanged += new System.EventHandler(this.LayerChecked_CheckedChanged);
            // 
            // chkPlates
            // 
            resources.ApplyResources(this.chkPlates, "chkPlates");
            this.chkPlates.Name = "chkPlates";
            this.chkPlates.UseVisualStyleBackColor = true;
            this.chkPlates.CheckedChanged += new System.EventHandler(this.LayerChecked_CheckedChanged);
            // 
            // chkDimensions
            // 
            resources.ApplyResources(this.chkDimensions, "chkDimensions");
            this.chkDimensions.Name = "chkDimensions";
            this.chkDimensions.UseVisualStyleBackColor = true;
            this.chkDimensions.CheckedChanged += new System.EventHandler(this.LayerChecked_CheckedChanged);
            // 
            // grpCopyLayer
            // 
            this.grpCopyLayer.Controls.Add(this.chkBracings);
            this.grpCopyLayer.Controls.Add(this.chkDimensions);
            this.grpCopyLayer.Controls.Add(this.chkMember);
            this.grpCopyLayer.Controls.Add(this.chkPlates);
            this.grpCopyLayer.Controls.Add(this.chkBearings);
            this.grpCopyLayer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpCopyLayer, "grpCopyLayer");
            this.grpCopyLayer.Name = "grpCopyLayer";
            this.grpCopyLayer.TabStop = false;
            // 
            // ofdSource
            // 
            resources.ApplyResources(this.ofdSource, "ofdSource");
            // 
            // txtSource
            // 
            resources.ApplyResources(this.txtSource, "txtSource");
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.TextChanged += new System.EventHandler(this.TxtSource_TextChanged);
            // 
            // lblFilePath
            // 
            resources.ApplyResources(this.lblFilePath, "lblFilePath");
            this.lblFilePath.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFilePath.Name = "lblFilePath";
            // 
            // butSearchSource
            // 
            resources.ApplyResources(this.butSearchSource, "butSearchSource");
            this.butSearchSource.Name = "butSearchSource";
            this.butSearchSource.UseVisualStyleBackColor = true;
            this.butSearchSource.Click += new System.EventHandler(this.ButSearchSource_Click);
            // 
            // butImport
            // 
            resources.ApplyResources(this.butImport, "butImport");
            this.butImport.Name = "butImport";
            this.butImport.UseVisualStyleBackColor = true;
            this.butImport.Click += new System.EventHandler(this.ButImport_Click);
            // 
            // txtLayerPrefix
            // 
            resources.ApplyResources(this.txtLayerPrefix, "txtLayerPrefix");
            this.txtLayerPrefix.Name = "txtLayerPrefix";
            this.txtLayerPrefix.TextChanged += new System.EventHandler(this.TxtLayerPrefix_TextChanged);
            // 
            // lblTargetLayer
            // 
            resources.ApplyResources(this.lblTargetLayer, "lblTargetLayer");
            this.lblTargetLayer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTargetLayer.Name = "lblTargetLayer";
            // 
            // lblLayerSuffix
            // 
            resources.ApplyResources(this.lblLayerSuffix, "lblLayerSuffix");
            this.lblLayerSuffix.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblLayerSuffix.Name = "lblLayerSuffix";
            // 
            // FrmTrussImport
            // 
            this.AcceptButton = this.butImport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLayerSuffix);
            this.Controls.Add(this.lblTargetLayer);
            this.Controls.Add(this.txtLayerPrefix);
            this.Controls.Add(grpRotation);
            this.Controls.Add(this.butImport);
            this.Controls.Add(this.butSearchSource);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.grpCopyLayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTrussImport";
            this.Load += new System.EventHandler(this.FrmTrussImport_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmTrussImport_KeyPress);
            grpRotation.ResumeLayout(false);
            grpRotation.PerformLayout();
            this.grpCopyLayer.ResumeLayout(false);
            this.grpCopyLayer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMember;
        private System.Windows.Forms.CheckBox chkBracings;
        private System.Windows.Forms.CheckBox chkBearings;
        private System.Windows.Forms.CheckBox chkPlates;
        private System.Windows.Forms.CheckBox chkDimensions;
        private System.Windows.Forms.GroupBox grpCopyLayer;
        private System.Windows.Forms.OpenFileDialog ofdSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button butSearchSource;
        private System.Windows.Forms.Button butImport;
        private System.Windows.Forms.RadioButton optRotateNo;
        private System.Windows.Forms.RadioButton optRotateLeft;
        private System.Windows.Forms.RadioButton optRotateRight;
        private System.Windows.Forms.TextBox txtLayerPrefix;
        private System.Windows.Forms.Label lblTargetLayer;
        private System.Windows.Forms.Label lblLayerSuffix;

    }
}