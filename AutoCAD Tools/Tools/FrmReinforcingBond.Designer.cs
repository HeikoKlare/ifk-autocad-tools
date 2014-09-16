namespace AutoCADTools.Tools
{
    partial class FrmReinforcingBond
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
            System.Windows.Forms.Label lblHalfFieldCount;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReinforcingBond));
            System.Windows.Forms.Label lblDistanceRidge;
            System.Windows.Forms.Label lblDistanceEave;
            System.Windows.Forms.Label lblPositionnumber;
            this.chkDrawVerticalMembers = new System.Windows.Forms.CheckBox();
            this.txtDistanceRidge = new System.Windows.Forms.TextBox();
            this.txtDistanceEave = new System.Windows.Forms.TextBox();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.butDraw = new System.Windows.Forms.Button();
            this.updHalfFields = new System.Windows.Forms.NumericUpDown();
            this.chkDrawChords = new System.Windows.Forms.CheckBox();
            this.optTopChord = new System.Windows.Forms.RadioButton();
            this.optBottomChord = new System.Windows.Forms.RadioButton();
            this.optVerticalChord = new System.Windows.Forms.RadioButton();
            this.optCurrentLayer = new System.Windows.Forms.RadioButton();
            lblHalfFieldCount = new System.Windows.Forms.Label();
            lblDistanceRidge = new System.Windows.Forms.Label();
            lblDistanceEave = new System.Windows.Forms.Label();
            lblPositionnumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.updHalfFields)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHalfFieldCount
            // 
            resources.ApplyResources(lblHalfFieldCount, "lblHalfFieldCount");
            lblHalfFieldCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblHalfFieldCount.Name = "lblHalfFieldCount";
            // 
            // lblDistanceRidge
            // 
            resources.ApplyResources(lblDistanceRidge, "lblDistanceRidge");
            lblDistanceRidge.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDistanceRidge.Name = "lblDistanceRidge";
            // 
            // lblDistanceEave
            // 
            resources.ApplyResources(lblDistanceEave, "lblDistanceEave");
            lblDistanceEave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDistanceEave.Name = "lblDistanceEave";
            // 
            // lblPositionnumber
            // 
            resources.ApplyResources(lblPositionnumber, "lblPositionnumber");
            lblPositionnumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblPositionnumber.Name = "lblPositionnumber";
            // 
            // chkDrawVerticalMembers
            // 
            resources.ApplyResources(this.chkDrawVerticalMembers, "chkDrawVerticalMembers");
            this.chkDrawVerticalMembers.Checked = true;
            this.chkDrawVerticalMembers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawVerticalMembers.Name = "chkDrawVerticalMembers";
            this.chkDrawVerticalMembers.UseVisualStyleBackColor = true;
            this.chkDrawVerticalMembers.CheckedChanged += new System.EventHandler(this.chkDrawVerticalMembers_CheckedChanged);
            // 
            // txtDistanceRidge
            // 
            resources.ApplyResources(this.txtDistanceRidge, "txtDistanceRidge");
            this.txtDistanceRidge.Name = "txtDistanceRidge";
            this.txtDistanceRidge.TextChanged += new System.EventHandler(this.txtDistance_TextChanged);
            this.txtDistanceRidge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDecimal_KeyPress);
            this.txtDistanceRidge.Leave += new System.EventHandler(this.txtDistanceRidge_Leave);
            // 
            // txtDistanceEave
            // 
            resources.ApplyResources(this.txtDistanceEave, "txtDistanceEave");
            this.txtDistanceEave.Name = "txtDistanceEave";
            this.txtDistanceEave.TextChanged += new System.EventHandler(this.txtDistance_TextChanged);
            this.txtDistanceEave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDecimal_KeyPress);
            this.txtDistanceEave.Leave += new System.EventHandler(this.txtDistanceEave_Leave);
            // 
            // txtPosition
            // 
            resources.ApplyResources(this.txtPosition, "txtPosition");
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.TextChanged += new System.EventHandler(this.txtPosition_TextChanged);
            // 
            // butDraw
            // 
            resources.ApplyResources(this.butDraw, "butDraw");
            this.butDraw.Name = "butDraw";
            this.butDraw.UseVisualStyleBackColor = true;
            this.butDraw.Click += new System.EventHandler(this.butDraw_Click);
            // 
            // updHalfFields
            // 
            resources.ApplyResources(this.updHalfFields, "updHalfFields");
            this.updHalfFields.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.updHalfFields.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updHalfFields.Name = "updHalfFields";
            this.updHalfFields.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.updHalfFields.ValueChanged += new System.EventHandler(this.updHalfFields_ValueChanged);
            // 
            // chkDrawChords
            // 
            resources.ApplyResources(this.chkDrawChords, "chkDrawChords");
            this.chkDrawChords.Name = "chkDrawChords";
            this.chkDrawChords.UseVisualStyleBackColor = true;
            this.chkDrawChords.CheckedChanged += new System.EventHandler(this.butDrawChords_CheckedChanged);
            // 
            // optTopChord
            // 
            resources.ApplyResources(this.optTopChord, "optTopChord");
            this.optTopChord.Name = "optTopChord";
            this.optTopChord.TabStop = true;
            this.optTopChord.UseVisualStyleBackColor = true;
            this.optTopChord.CheckedChanged += new System.EventHandler(this.optType_CheckedChanged);
            // 
            // optBottomChord
            // 
            resources.ApplyResources(this.optBottomChord, "optBottomChord");
            this.optBottomChord.Name = "optBottomChord";
            this.optBottomChord.TabStop = true;
            this.optBottomChord.UseVisualStyleBackColor = true;
            this.optBottomChord.CheckedChanged += new System.EventHandler(this.optType_CheckedChanged);
            // 
            // optVerticalChord
            // 
            resources.ApplyResources(this.optVerticalChord, "optVerticalChord");
            this.optVerticalChord.Name = "optVerticalChord";
            this.optVerticalChord.TabStop = true;
            this.optVerticalChord.UseVisualStyleBackColor = true;
            this.optVerticalChord.CheckedChanged += new System.EventHandler(this.optType_CheckedChanged);
            // 
            // optCurrentLayer
            // 
            resources.ApplyResources(this.optCurrentLayer, "optCurrentLayer");
            this.optCurrentLayer.Name = "optCurrentLayer";
            this.optCurrentLayer.TabStop = true;
            this.optCurrentLayer.UseVisualStyleBackColor = true;
            this.optCurrentLayer.CheckedChanged += new System.EventHandler(this.optType_CheckedChanged);
            // 
            // FrmReinforcingBond
            // 
            this.AcceptButton = this.butDraw;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optCurrentLayer);
            this.Controls.Add(this.optVerticalChord);
            this.Controls.Add(this.optBottomChord);
            this.Controls.Add(this.optTopChord);
            this.Controls.Add(this.chkDrawChords);
            this.Controls.Add(this.txtDistanceRidge);
            this.Controls.Add(this.updHalfFields);
            this.Controls.Add(this.txtDistanceEave);
            this.Controls.Add(lblHalfFieldCount);
            this.Controls.Add(lblDistanceRidge);
            this.Controls.Add(this.chkDrawVerticalMembers);
            this.Controls.Add(lblDistanceEave);
            this.Controls.Add(this.butDraw);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(lblPositionnumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReinforcingBond";
            this.Load += new System.EventHandler(this.FrmReinforcingBond_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmReinforcedBond_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.updHalfFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDrawVerticalMembers;
        private System.Windows.Forms.TextBox txtDistanceRidge;
        private System.Windows.Forms.TextBox txtDistanceEave;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Button butDraw;
        private System.Windows.Forms.NumericUpDown updHalfFields;
        private System.Windows.Forms.CheckBox chkDrawChords;
        private System.Windows.Forms.RadioButton optTopChord;
        private System.Windows.Forms.RadioButton optBottomChord;
        private System.Windows.Forms.RadioButton optVerticalChord;
        private System.Windows.Forms.RadioButton optCurrentLayer;
    }
}