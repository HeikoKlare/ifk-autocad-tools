namespace AutoCADTools
{
    partial class UFVerband
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UFVerband));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Lausrichtung = new System.Windows.Forms.Label();
            this.CBendstab = new System.Windows.Forms.CheckBox();
            this.TBfeldzahl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Baendern = new System.Windows.Forms.Button();
            this.TBlaenge = new System.Windows.Forms.TextBox();
            this.TBbreite = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TBfirstabstand = new System.Windows.Forms.TextBox();
            this.TBtraufabstand = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TBposition = new System.Windows.Forms.TextBox();
            this.Bzeichnen = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lausrichtung);
            this.groupBox1.Controls.Add(this.CBendstab);
            this.groupBox1.Controls.Add(this.TBfeldzahl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Baendern);
            this.groupBox1.Controls.Add(this.TBlaenge);
            this.groupBox1.Controls.Add(this.TBbreite);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 196);
            this.groupBox1.TabIndex = 0;
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
            this.CBendstab.CheckedChanged += new System.EventHandler(this.CBendstab_CheckedChanged);
            // 
            // TBfeldzahl
            // 
            this.TBfeldzahl.Location = new System.Drawing.Point(134, 137);
            this.TBfeldzahl.MaxLength = 2;
            this.TBfeldzahl.Name = "TBfeldzahl";
            this.TBfeldzahl.Size = new System.Drawing.Size(83, 20);
            this.TBfeldzahl.TabIndex = 2;
            this.TBfeldzahl.TextChanged += new System.EventHandler(this.TBfeldzahl_TextChanged);
            this.TBfeldzahl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBfeldzahl_KeyPress);
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
            // Baendern
            // 
            this.Baendern.Location = new System.Drawing.Point(25, 104);
            this.Baendern.Name = "Baendern";
            this.Baendern.Size = new System.Drawing.Size(192, 23);
            this.Baendern.TabIndex = 1;
            this.Baendern.Text = "Erstellen";
            this.Baendern.UseVisualStyleBackColor = true;
            this.Baendern.Click += new System.EventHandler(this.Baendern_Click);
            // 
            // TBlaenge
            // 
            this.TBlaenge.Enabled = false;
            this.TBlaenge.Location = new System.Drawing.Point(65, 54);
            this.TBlaenge.Name = "TBlaenge";
            this.TBlaenge.Size = new System.Drawing.Size(152, 20);
            this.TBlaenge.TabIndex = 3;
            // 
            // TBbreite
            // 
            this.TBbreite.Enabled = false;
            this.TBbreite.Location = new System.Drawing.Point(65, 27);
            this.TBbreite.Name = "TBbreite";
            this.TBbreite.Size = new System.Drawing.Size(152, 20);
            this.TBbreite.TabIndex = 2;
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
            this.groupBox2.Controls.Add(this.TBfirstabstand);
            this.groupBox2.Controls.Add(this.TBtraufabstand);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(261, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 91);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Positionierung Enstäbe";
            // 
            // TBfirstabstand
            // 
            this.TBfirstabstand.Location = new System.Drawing.Point(108, 54);
            this.TBfirstabstand.Name = "TBfirstabstand";
            this.TBfirstabstand.Size = new System.Drawing.Size(54, 20);
            this.TBfirstabstand.TabIndex = 5;
            this.TBfirstabstand.Text = "0.10";
            this.toolTip.SetToolTip(this.TBfirstabstand, "Der Abstand des Enstabes am Firstende gegenüber dem gewählten Punkt. Als First wi" +
        "rd der als zweites gewählte Punkt interpretiert.");
            this.TBfirstabstand.TextChanged += new System.EventHandler(this.TBfirstabstand_TextChanged);
            this.TBfirstabstand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBtraufabstand_KeyPress);
            this.TBfirstabstand.Leave += new System.EventHandler(this.TBfirstabstand_Leave);
            // 
            // TBtraufabstand
            // 
            this.TBtraufabstand.Location = new System.Drawing.Point(108, 27);
            this.TBtraufabstand.Name = "TBtraufabstand";
            this.TBtraufabstand.Size = new System.Drawing.Size(54, 20);
            this.TBtraufabstand.TabIndex = 4;
            this.TBtraufabstand.Text = "0.10";
            this.toolTip.SetToolTip(this.TBtraufabstand, "Der Abstand des Enstabes am Traufende gegenüber dem gewählten Punkt. Als Traufe w" +
        "ird der zuerst aufgewählte Punkt interpretiert.");
            this.TBtraufabstand.TextChanged += new System.EventHandler(this.TBtraufabstand_TextChanged);
            this.TBtraufabstand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBtraufabstand_KeyPress);
            this.TBtraufabstand.Leave += new System.EventHandler(this.TBtraufabstand_Leave);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Positionsnummer:";
            // 
            // TBposition
            // 
            this.TBposition.Location = new System.Drawing.Point(372, 119);
            this.TBposition.MaxLength = 3;
            this.TBposition.Name = "TBposition";
            this.TBposition.Size = new System.Drawing.Size(52, 20);
            this.TBposition.TabIndex = 6;
            this.TBposition.TextChanged += new System.EventHandler(this.TBposition_TextChanged);
            // 
            // Bzeichnen
            // 
            this.Bzeichnen.Location = new System.Drawing.Point(280, 150);
            this.Bzeichnen.Name = "Bzeichnen";
            this.Bzeichnen.Size = new System.Drawing.Size(144, 48);
            this.Bzeichnen.TabIndex = 7;
            this.Bzeichnen.Text = "Verband zeichnen";
            this.Bzeichnen.UseVisualStyleBackColor = true;
            this.Bzeichnen.Click += new System.EventHandler(this.Bzeichnen_Click);
            // 
            // UFVerband
            // 
            this.AcceptButton = this.Bzeichnen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 221);
            this.Controls.Add(this.Bzeichnen);
            this.Controls.Add(this.TBposition);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UFVerband";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verband erstellen";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UFVerband_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CBendstab;
        private System.Windows.Forms.TextBox TBfeldzahl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Baendern;
        private System.Windows.Forms.TextBox TBlaenge;
        private System.Windows.Forms.TextBox TBbreite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TBfirstabstand;
        private System.Windows.Forms.TextBox TBtraufabstand;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TBposition;
        private System.Windows.Forms.Button Bzeichnen;
        private System.Windows.Forms.Label Lausrichtung;
        private System.Windows.Forms.ToolTip toolTip;
    }
}