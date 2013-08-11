namespace AutoCADTools.Tools
{
    partial class TrussImportUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrussImportUI));
            this.chkMember = new System.Windows.Forms.CheckBox();
            this.chkBracings = new System.Windows.Forms.CheckBox();
            this.chkBearings = new System.Windows.Forms.CheckBox();
            this.chkPlates = new System.Windows.Forms.CheckBox();
            this.chkDimensions = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ofdSource = new System.Windows.Forms.OpenFileDialog();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butSearchSource = new System.Windows.Forms.Button();
            this.butImport = new System.Windows.Forms.Button();
            this.butRotateRight = new System.Windows.Forms.RadioButton();
            this.butRotateLeft = new System.Windows.Forms.RadioButton();
            this.butRotateNo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLayerPrefix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMember
            // 
            resources.ApplyResources(this.chkMember, "chkMember");
            this.chkMember.Checked = true;
            this.chkMember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMember.Name = "chkMember";
            this.chkMember.UseVisualStyleBackColor = true;
            this.chkMember.CheckedChanged += new System.EventHandler(this.layerChecked_CheckedChanged);
            // 
            // chkBracings
            // 
            resources.ApplyResources(this.chkBracings, "chkBracings");
            this.chkBracings.Checked = true;
            this.chkBracings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBracings.Name = "chkBracings";
            this.chkBracings.UseVisualStyleBackColor = true;
            this.chkBracings.CheckedChanged += new System.EventHandler(this.layerChecked_CheckedChanged);
            // 
            // chkBearings
            // 
            resources.ApplyResources(this.chkBearings, "chkBearings");
            this.chkBearings.Checked = true;
            this.chkBearings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBearings.Name = "chkBearings";
            this.chkBearings.UseVisualStyleBackColor = true;
            this.chkBearings.CheckedChanged += new System.EventHandler(this.layerChecked_CheckedChanged);
            // 
            // chkPlates
            // 
            resources.ApplyResources(this.chkPlates, "chkPlates");
            this.chkPlates.Name = "chkPlates";
            this.chkPlates.UseVisualStyleBackColor = true;
            this.chkPlates.CheckedChanged += new System.EventHandler(this.layerChecked_CheckedChanged);
            // 
            // chkDimensions
            // 
            resources.ApplyResources(this.chkDimensions, "chkDimensions");
            this.chkDimensions.Name = "chkDimensions";
            this.chkDimensions.UseVisualStyleBackColor = true;
            this.chkDimensions.CheckedChanged += new System.EventHandler(this.layerChecked_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBracings);
            this.groupBox1.Controls.Add(this.chkDimensions);
            this.groupBox1.Controls.Add(this.chkMember);
            this.groupBox1.Controls.Add(this.chkPlates);
            this.groupBox1.Controls.Add(this.chkBearings);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // butSearchSource
            // 
            resources.ApplyResources(this.butSearchSource, "butSearchSource");
            this.butSearchSource.Name = "butSearchSource";
            this.butSearchSource.UseVisualStyleBackColor = true;
            this.butSearchSource.Click += new System.EventHandler(this.butSearchSource_Click);
            // 
            // butImport
            // 
            resources.ApplyResources(this.butImport, "butImport");
            this.butImport.Name = "butImport";
            this.butImport.UseVisualStyleBackColor = true;
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
            // 
            // butRotateRight
            // 
            resources.ApplyResources(this.butRotateRight, "butRotateRight");
            this.butRotateRight.Image = global::AutoCADTools.Properties.Resources.Truss_right;
            this.butRotateRight.Name = "butRotateRight";
            this.butRotateRight.UseVisualStyleBackColor = true;
            this.butRotateRight.CheckedChanged += new System.EventHandler(this.butRotate_CheckedChanged);
            // 
            // butRotateLeft
            // 
            resources.ApplyResources(this.butRotateLeft, "butRotateLeft");
            this.butRotateLeft.Checked = true;
            this.butRotateLeft.Image = global::AutoCADTools.Properties.Resources.Truss_left;
            this.butRotateLeft.Name = "butRotateLeft";
            this.butRotateLeft.TabStop = true;
            this.butRotateLeft.UseVisualStyleBackColor = true;
            this.butRotateLeft.CheckedChanged += new System.EventHandler(this.butRotate_CheckedChanged);
            // 
            // butRotateNo
            // 
            resources.ApplyResources(this.butRotateNo, "butRotateNo");
            this.butRotateNo.Image = global::AutoCADTools.Properties.Resources.Truss;
            this.butRotateNo.Name = "butRotateNo";
            this.butRotateNo.UseVisualStyleBackColor = true;
            this.butRotateNo.CheckedChanged += new System.EventHandler(this.butRotate_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.butRotateLeft);
            this.groupBox2.Controls.Add(this.butRotateRight);
            this.groupBox2.Controls.Add(this.butRotateNo);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtLayerPrefix
            // 
            resources.ApplyResources(this.txtLayerPrefix, "txtLayerPrefix");
            this.txtLayerPrefix.Name = "txtLayerPrefix";
            this.txtLayerPrefix.TextChanged += new System.EventHandler(this.txtLayerPrefix_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // TrussImportUI
            // 
            this.AcceptButton = this.butImport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLayerPrefix);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.butImport);
            this.Controls.Add(this.butSearchSource);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "TrussImportUI";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TrussImportUi_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMember;
        private System.Windows.Forms.CheckBox chkBracings;
        private System.Windows.Forms.CheckBox chkBearings;
        private System.Windows.Forms.CheckBox chkPlates;
        private System.Windows.Forms.CheckBox chkDimensions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog ofdSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butSearchSource;
        private System.Windows.Forms.Button butImport;
        private System.Windows.Forms.RadioButton butRotateNo;
        private System.Windows.Forms.RadioButton butRotateLeft;
        private System.Windows.Forms.RadioButton butRotateRight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLayerPrefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

    }
}