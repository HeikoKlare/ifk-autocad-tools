namespace AutoCADTools.Tools
{
    partial class ReinforcingBondUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReinforcingBondUI));
            this.butDrawVerticalMembers = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDistanceRidge = new System.Windows.Forms.TextBox();
            this.txtDistanceEave = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.butDraw = new System.Windows.Forms.Button();
            this.numHalfFields = new System.Windows.Forms.NumericUpDown();
            this.butDrawChords = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numHalfFields)).BeginInit();
            this.SuspendLayout();
            // 
            // butDrawVerticalMembers
            // 
            resources.ApplyResources(this.butDrawVerticalMembers, "butDrawVerticalMembers");
            this.butDrawVerticalMembers.Checked = true;
            this.butDrawVerticalMembers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.butDrawVerticalMembers.Name = "butDrawVerticalMembers";
            this.butDrawVerticalMembers.UseVisualStyleBackColor = true;
            this.butDrawVerticalMembers.CheckedChanged += new System.EventHandler(this.butDrawVerticalMembers_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // numHalfFields
            // 
            resources.ApplyResources(this.numHalfFields, "numHalfFields");
            this.numHalfFields.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numHalfFields.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numHalfFields.Name = "numHalfFields";
            this.numHalfFields.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numHalfFields.ValueChanged += new System.EventHandler(this.numHalfFields_ValueChanged);
            // 
            // butDrawChords
            // 
            resources.ApplyResources(this.butDrawChords, "butDrawChords");
            this.butDrawChords.Name = "butDrawChords";
            this.butDrawChords.UseVisualStyleBackColor = true;
            this.butDrawChords.CheckedChanged += new System.EventHandler(this.butDrawChords_CheckedChanged);
            // 
            // ReinforcingBondUI
            // 
            this.AcceptButton = this.butDraw;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.butDrawChords);
            this.Controls.Add(this.txtDistanceRidge);
            this.Controls.Add(this.numHalfFields);
            this.Controls.Add(this.txtDistanceEave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.butDrawVerticalMembers);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.butDraw);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReinforcingBondUI";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReinforcedBondUi_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.numHalfFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox butDrawVerticalMembers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDistanceRidge;
        private System.Windows.Forms.TextBox txtDistanceEave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Button butDraw;
        private System.Windows.Forms.NumericUpDown numHalfFields;
        private System.Windows.Forms.CheckBox butDrawChords;
    }
}