namespace AutoCADTools.Tools
{
    partial class PanicleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbDescription = new System.Windows.Forms.ComboBox();
            this.butcreate = new System.Windows.Forms.Button();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDouble = new System.Windows.Forms.CheckBox();
            this.chkVertical = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmbDescription
            // 
            this.cmbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDescription.FormattingEnabled = true;
            this.cmbDescription.Items.AddRange(new object[] {
            "RiBd 60/2,0",
            "RiBd 60/1,5",
            "RiBd 40/2,0",
            "RiBd 40/1,5"});
            this.cmbDescription.Location = new System.Drawing.Point(95, 42);
            this.cmbDescription.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.cmbDescription.Name = "cmbDescription";
            this.cmbDescription.Size = new System.Drawing.Size(96, 21);
            this.cmbDescription.TabIndex = 10;
            this.cmbDescription.Text = "RiBd 60/2,0";
            // 
            // butcreate
            // 
            this.butcreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.butcreate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butcreate.Location = new System.Drawing.Point(10, 127);
            this.butcreate.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.butcreate.Name = "butcreate";
            this.butcreate.Size = new System.Drawing.Size(181, 35);
            this.butcreate.TabIndex = 9;
            this.butcreate.Text = "Rispen erstellen";
            this.butcreate.UseVisualStyleBackColor = true;
            this.butcreate.Click += new System.EventHandler(this.butCreate_Click);
            // 
            // txtPosition
            // 
            this.txtPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosition.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosition.Location = new System.Drawing.Point(109, 10);
            this.txtPosition.Margin = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.txtPosition.MaxLength = 3;
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(82, 22);
            this.txtPosition.TabIndex = 8;
            this.txtPosition.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Beschreibung:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Positionsnummer:";
            // 
            // chkDouble
            // 
            this.chkDouble.AutoSize = true;
            this.chkDouble.Location = new System.Drawing.Point(10, 73);
            this.chkDouble.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.chkDouble.Name = "chkDouble";
            this.chkDouble.Size = new System.Drawing.Size(82, 17);
            this.chkDouble.TabIndex = 11;
            this.chkDouble.Text = "Doppelrispe";
            this.chkDouble.UseVisualStyleBackColor = true;
            this.chkDouble.CheckedChanged += new System.EventHandler(this.chkDouble_CheckedChanged);
            // 
            // chkVertical
            // 
            this.chkVertical.AutoSize = true;
            this.chkVertical.Enabled = false;
            this.chkVertical.Location = new System.Drawing.Point(10, 100);
            this.chkVertical.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.chkVertical.Name = "chkVertical";
            this.chkVertical.Size = new System.Drawing.Size(61, 17);
            this.chkVertical.TabIndex = 12;
            this.chkVertical.Text = "Vertikal";
            this.chkVertical.UseVisualStyleBackColor = true;
            // 
            // PanicleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkVertical);
            this.Controls.Add(this.chkDouble);
            this.Controls.Add(this.cmbDescription);
            this.Controls.Add(this.butcreate);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(200, 0);
            this.Name = "PanicleControl";
            this.Size = new System.Drawing.Size(201, 171);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDescription;
        private System.Windows.Forms.Button butcreate;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDouble;
        private System.Windows.Forms.CheckBox chkVertical;
    }
}
