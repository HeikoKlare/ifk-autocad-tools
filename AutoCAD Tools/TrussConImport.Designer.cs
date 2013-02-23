namespace AutoCADTools
{
    partial class UfTrussConImport
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
            this.CbHolz = new System.Windows.Forms.CheckBox();
            this.cbAussteifung = new System.Windows.Forms.CheckBox();
            this.cbAuflager = new System.Windows.Forms.CheckBox();
            this.cbPlatten = new System.Windows.Forms.CheckBox();
            this.cbMasse = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ofdQuelle = new System.Windows.Forms.OpenFileDialog();
            this.tbQuelle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSuchen = new System.Windows.Forms.Button();
            this.cbImportieren = new System.Windows.Forms.Button();
            this.rbRotateRight = new System.Windows.Forms.RadioButton();
            this.rbRotateLeft = new System.Windows.Forms.RadioButton();
            this.rbRotateNo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbLayer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CbHolz
            // 
            this.CbHolz.AutoSize = true;
            this.CbHolz.Checked = true;
            this.CbHolz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbHolz.Location = new System.Drawing.Point(23, 27);
            this.CbHolz.Name = "CbHolz";
            this.CbHolz.Size = new System.Drawing.Size(47, 17);
            this.CbHolz.TabIndex = 1;
            this.CbHolz.Text = "Holz";
            this.CbHolz.UseVisualStyleBackColor = true;
            // 
            // cbAussteifung
            // 
            this.cbAussteifung.AutoSize = true;
            this.cbAussteifung.Checked = true;
            this.cbAussteifung.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAussteifung.Location = new System.Drawing.Point(23, 50);
            this.cbAussteifung.Name = "cbAussteifung";
            this.cbAussteifung.Size = new System.Drawing.Size(109, 17);
            this.cbAussteifung.TabIndex = 2;
            this.cbAussteifung.Text = "Längsaussteifung";
            this.cbAussteifung.UseVisualStyleBackColor = true;
            // 
            // cbAuflager
            // 
            this.cbAuflager.AutoSize = true;
            this.cbAuflager.Checked = true;
            this.cbAuflager.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAuflager.Location = new System.Drawing.Point(23, 73);
            this.cbAuflager.Name = "cbAuflager";
            this.cbAuflager.Size = new System.Drawing.Size(65, 17);
            this.cbAuflager.TabIndex = 3;
            this.cbAuflager.Text = "Auflager";
            this.cbAuflager.UseVisualStyleBackColor = true;
            // 
            // cbPlatten
            // 
            this.cbPlatten.AutoSize = true;
            this.cbPlatten.Location = new System.Drawing.Point(23, 96);
            this.cbPlatten.Name = "cbPlatten";
            this.cbPlatten.Size = new System.Drawing.Size(86, 17);
            this.cbPlatten.TabIndex = 4;
            this.cbPlatten.Text = "Nagelplatten";
            this.cbPlatten.UseVisualStyleBackColor = true;
            // 
            // cbMasse
            // 
            this.cbMasse.AutoSize = true;
            this.cbMasse.Location = new System.Drawing.Point(23, 119);
            this.cbMasse.Name = "cbMasse";
            this.cbMasse.Size = new System.Drawing.Size(80, 17);
            this.cbMasse.TabIndex = 5;
            this.cbMasse.Text = "Vermaßung";
            this.cbMasse.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAussteifung);
            this.groupBox1.Controls.Add(this.cbMasse);
            this.groupBox1.Controls.Add(this.CbHolz);
            this.groupBox1.Controls.Add(this.cbPlatten);
            this.groupBox1.Controls.Add(this.cbAuflager);
            this.groupBox1.Location = new System.Drawing.Point(25, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 149);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layer kopieren";
            // 
            // ofdQuelle
            // 
            this.ofdQuelle.Filter = "Zeichnungsdateien (*.dxf, *.dwg)|*.dwg;*.dxf";
            // 
            // tbQuelle
            // 
            this.tbQuelle.Location = new System.Drawing.Point(91, 17);
            this.tbQuelle.Name = "tbQuelle";
            this.tbQuelle.ReadOnly = true;
            this.tbQuelle.Size = new System.Drawing.Size(341, 20);
            this.tbQuelle.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dateipfad:";
            // 
            // cbSuchen
            // 
            this.cbSuchen.Location = new System.Drawing.Point(349, 50);
            this.cbSuchen.Name = "cbSuchen";
            this.cbSuchen.Size = new System.Drawing.Size(83, 23);
            this.cbSuchen.TabIndex = 9;
            this.cbSuchen.Text = "Suchen";
            this.cbSuchen.UseVisualStyleBackColor = true;
            this.cbSuchen.Click += new System.EventHandler(this.CbSuchenClick);
            // 
            // cbImportieren
            // 
            this.cbImportieren.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbImportieren.Location = new System.Drawing.Point(212, 61);
            this.cbImportieren.Name = "cbImportieren";
            this.cbImportieren.Size = new System.Drawing.Size(101, 36);
            this.cbImportieren.TabIndex = 10;
            this.cbImportieren.Text = "Importieren";
            this.cbImportieren.UseVisualStyleBackColor = true;
            this.cbImportieren.Click += new System.EventHandler(this.CbImportierenClick);
            // 
            // rbRotateRight
            // 
            this.rbRotateRight.AutoSize = true;
            this.rbRotateRight.Image = global::AutoCADTools.Properties.Resources.Truss_right;
            this.rbRotateRight.Location = new System.Drawing.Point(166, 20);
            this.rbRotateRight.Name = "rbRotateRight";
            this.rbRotateRight.Size = new System.Drawing.Size(50, 60);
            this.rbRotateRight.TabIndex = 13;
            this.rbRotateRight.UseVisualStyleBackColor = true;
            // 
            // rbRotateLeft
            // 
            this.rbRotateLeft.AutoSize = true;
            this.rbRotateLeft.Checked = true;
            this.rbRotateLeft.Image = global::AutoCADTools.Properties.Resources.Truss_left;
            this.rbRotateLeft.Location = new System.Drawing.Point(20, 20);
            this.rbRotateLeft.Name = "rbRotateLeft";
            this.rbRotateLeft.Size = new System.Drawing.Size(50, 60);
            this.rbRotateLeft.TabIndex = 12;
            this.rbRotateLeft.TabStop = true;
            this.rbRotateLeft.UseVisualStyleBackColor = true;
            // 
            // rbRotateNo
            // 
            this.rbRotateNo.AutoSize = true;
            this.rbRotateNo.Image = global::AutoCADTools.Properties.Resources.Truss;
            this.rbRotateNo.Location = new System.Drawing.Point(86, 32);
            this.rbRotateNo.Name = "rbRotateNo";
            this.rbRotateNo.Size = new System.Drawing.Size(74, 36);
            this.rbRotateNo.TabIndex = 11;
            this.rbRotateNo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbRotateLeft);
            this.groupBox2.Controls.Add(this.rbRotateRight);
            this.groupBox2.Controls.Add(this.rbRotateNo);
            this.groupBox2.Location = new System.Drawing.Point(192, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 90);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ausrichtung";
            // 
            // tbLayer
            // 
            this.tbLayer.Location = new System.Drawing.Point(91, 221);
            this.tbLayer.Name = "tbLayer";
            this.tbLayer.Size = new System.Drawing.Size(341, 20);
            this.tbLayer.TabIndex = 15;
            this.tbLayer.Text = "Schnitt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Ziel-Layer:";
            // 
            // UfTrussConImport
            // 
            this.AcceptButton = this.cbImportieren;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 253);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbLayer);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbImportieren);
            this.Controls.Add(this.cbSuchen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbQuelle);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UfTrussConImport";
            this.Text = "TrussCon Import";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UFTrussConImport_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CbHolz;
        private System.Windows.Forms.CheckBox cbAussteifung;
        private System.Windows.Forms.CheckBox cbAuflager;
        private System.Windows.Forms.CheckBox cbPlatten;
        private System.Windows.Forms.CheckBox cbMasse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog ofdQuelle;
        private System.Windows.Forms.TextBox tbQuelle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cbSuchen;
        private System.Windows.Forms.Button cbImportieren;
        private System.Windows.Forms.RadioButton rbRotateNo;
        private System.Windows.Forms.RadioButton rbRotateLeft;
        private System.Windows.Forms.RadioButton rbRotateRight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbLayer;
        private System.Windows.Forms.Label label2;

    }
}