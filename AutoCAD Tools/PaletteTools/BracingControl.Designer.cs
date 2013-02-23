namespace AutoCADTools.Tools
{
    partial class BracingControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Lausrichtung = new System.Windows.Forms.Label();
            this.CBendstab = new System.Windows.Forms.CheckBox();
            this.TBfeldzahl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butChangeGeometry = new System.Windows.Forms.Button();
            this.TBlaenge = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TBbreite = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRidgeDistance = new System.Windows.Forms.TextBox();
            this.txtEaveDistance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TBposition = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.butDraw = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Lausrichtung);
            this.groupBox1.Controls.Add(this.CBendstab);
            this.groupBox1.Controls.Add(this.TBfeldzahl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.butChangeGeometry);
            this.groupBox1.Controls.Add(this.TBlaenge);
            this.groupBox1.Controls.Add(this.TBbreite);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 196);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geometrie";
            // 
            // Lausrichtung
            // 
            this.Lausrichtung.AutoSize = true;
            this.Lausrichtung.Location = new System.Drawing.Point(22, 82);
            this.Lausrichtung.Name = "Lausrichtung";
            this.Lausrichtung.Size = new System.Drawing.Size(95, 13);
            this.Lausrichtung.TabIndex = 10;
            this.Lausrichtung.Text = "Ausrichtung: keine";
            // 
            // CBendstab
            // 
            this.CBendstab.AutoSize = true;
            this.CBendstab.Checked = true;
            this.CBendstab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBendstab.Location = new System.Drawing.Point(25, 168);
            this.CBendstab.Name = "CBendstab";
            this.CBendstab.Size = new System.Drawing.Size(117, 17);
            this.CBendstab.TabIndex = 3;
            this.CBendstab.Text = "Endstäbe zeichnen";
            this.CBendstab.UseVisualStyleBackColor = true;
            this.CBendstab.CheckedChanged += new System.EventHandler(this.chkEndMember_CheckedChanged);
            // 
            // TBfeldzahl
            // 
            this.TBfeldzahl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBfeldzahl.Location = new System.Drawing.Point(134, 137);
            this.TBfeldzahl.MaxLength = 2;
            this.TBfeldzahl.Name = "TBfeldzahl";
            this.TBfeldzahl.Size = new System.Drawing.Size(70, 20);
            this.TBfeldzahl.TabIndex = 2;
            this.TBfeldzahl.TextChanged += new System.EventHandler(this.txtFieldCount_TextChanged);
            this.TBfeldzahl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFieldCount_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Anzahl halber Felder:";
            // 
            // butChangeGeometry
            // 
            this.butChangeGeometry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.butChangeGeometry.Location = new System.Drawing.Point(25, 104);
            this.butChangeGeometry.Name = "butChangeGeometry";
            this.butChangeGeometry.Size = new System.Drawing.Size(179, 23);
            this.butChangeGeometry.TabIndex = 1;
            this.butChangeGeometry.Text = "Erstellen";
            this.butChangeGeometry.UseVisualStyleBackColor = true;
            this.butChangeGeometry.Click += new System.EventHandler(this.butChangeGeometry_Click);
            // 
            // TBlaenge
            // 
            this.TBlaenge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBlaenge.Enabled = false;
            this.TBlaenge.Location = new System.Drawing.Point(65, 54);
            this.TBlaenge.Name = "TBlaenge";
            this.TBlaenge.Size = new System.Drawing.Size(139, 20);
            this.TBlaenge.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Länge:";
            // 
            // TBbreite
            // 
            this.TBbreite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBbreite.Enabled = false;
            this.TBbreite.Location = new System.Drawing.Point(65, 27);
            this.TBbreite.Name = "TBbreite";
            this.TBbreite.Size = new System.Drawing.Size(139, 20);
            this.TBbreite.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Breite:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRidgeDistance);
            this.groupBox2.Controls.Add(this.txtEaveDistance);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(10, 216);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 91);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Positionierung Enstäbe";
            // 
            // txtRidgeDistance
            // 
            this.txtRidgeDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRidgeDistance.Location = new System.Drawing.Point(108, 54);
            this.txtRidgeDistance.Name = "txtRidgeDistance";
            this.txtRidgeDistance.Size = new System.Drawing.Size(98, 20);
            this.txtRidgeDistance.TabIndex = 5;
            this.txtRidgeDistance.Text = "0.10";
            this.txtRidgeDistance.TextChanged += new System.EventHandler(this.txtRidgeDistance_TextChanged);
            this.txtRidgeDistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEaveDistance_KeyPress);
            this.txtRidgeDistance.Leave += new System.EventHandler(this.txtRidgeDistance_Leave);
            // 
            // txtEaveDistance
            // 
            this.txtEaveDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEaveDistance.Location = new System.Drawing.Point(108, 27);
            this.txtEaveDistance.Name = "txtEaveDistance";
            this.txtEaveDistance.Size = new System.Drawing.Size(98, 20);
            this.txtEaveDistance.TabIndex = 4;
            this.txtEaveDistance.Text = "0.10";
            this.txtEaveDistance.TextChanged += new System.EventHandler(this.txtEaveDistance_TextChanged);
            this.txtEaveDistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEaveDistance_KeyPress);
            this.txtEaveDistance.Leave += new System.EventHandler(this.txtEaveDistance_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Abstand First:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Abstand Traufe: ";
            // 
            // TBposition
            // 
            this.TBposition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBposition.Location = new System.Drawing.Point(118, 314);
            this.TBposition.MaxLength = 3;
            this.TBposition.Name = "TBposition";
            this.TBposition.Size = new System.Drawing.Size(98, 20);
            this.TBposition.TabIndex = 10;
            this.TBposition.TextChanged += new System.EventHandler(this.txtPosition_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Positionsnummer:";
            // 
            // butDraw
            // 
            this.butDraw.Location = new System.Drawing.Point(10, 347);
            this.butDraw.Margin = new System.Windows.Forms.Padding(10);
            this.butDraw.Name = "butDraw";
            this.butDraw.Size = new System.Drawing.Size(221, 36);
            this.butDraw.TabIndex = 12;
            this.butDraw.Text = "Verband zeichnen";
            this.butDraw.UseVisualStyleBackColor = true;
            this.butDraw.Click += new System.EventHandler(this.butDraw_Click);
            // 
            // BracingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.butDraw);
            this.Controls.Add(this.TBposition);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BracingControl";
            this.Size = new System.Drawing.Size(241, 395);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Lausrichtung;
        private System.Windows.Forms.CheckBox CBendstab;
        private System.Windows.Forms.TextBox TBfeldzahl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butChangeGeometry;
        private System.Windows.Forms.TextBox TBlaenge;
        private System.Windows.Forms.TextBox TBbreite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRidgeDistance;
        private System.Windows.Forms.TextBox txtEaveDistance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TBposition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button butDraw;

    }
}
