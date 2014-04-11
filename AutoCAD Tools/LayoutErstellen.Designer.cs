namespace AutoCADTools
{
    partial class UFLayoutErstellen
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
            this.CBconfig = new System.Windows.Forms.ComboBox();
            this.Lconfig = new System.Windows.Forms.Label();
            this.Breset = new System.Windows.Forms.Button();
            this.GBausschnitt = new System.Windows.Forms.GroupBox();
            this.Lausschnitt = new System.Windows.Forms.Label();
            this.RBmanuell = new System.Windows.Forms.RadioButton();
            this.RBzeichenbereich = new System.Windows.Forms.RadioButton();
            this.BausschnittErstellen = new System.Windows.Forms.Button();
            this.GBmassstab = new System.Windows.Forms.GroupBox();
            this.CBexakterAusschnitt = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.LunitText = new System.Windows.Forms.Label();
            this.TBeinheit = new System.Windows.Forms.TextBox();
            this.CBmassstab = new System.Windows.Forms.ComboBox();
            this.LscaleText = new System.Windows.Forms.Label();
            this.Lunit = new System.Windows.Forms.Label();
            this.Lscale = new System.Windows.Forms.Label();
            this.GBblatt = new System.Windows.Forms.GroupBox();
            this.LFehlerFormat = new System.Windows.Forms.Label();
            this.CBdrehen = new System.Windows.Forms.CheckBox();
            this.CBkopf = new System.Windows.Forms.CheckBox();
            this.RBhochformat = new System.Windows.Forms.RadioButton();
            this.RBquerformat = new System.Windows.Forms.RadioButton();
            this.CBpapierformat = new System.Windows.Forms.ComboBox();
            this.Lpapierformat = new System.Windows.Forms.Label();
            this.CBoptimiertePapierformate = new System.Windows.Forms.CheckBox();
            this.Lprinter = new System.Windows.Forms.Label();
            this.Llayoutname = new System.Windows.Forms.Label();
            this.CBdrucker = new System.Windows.Forms.ComboBox();
            this.TBlayout = new System.Windows.Forms.TextBox();
            this.Berstellen = new System.Windows.Forms.Button();
            this.GBausschnitt.SuspendLayout();
            this.GBmassstab.SuspendLayout();
            this.GBblatt.SuspendLayout();
            this.SuspendLayout();
            // 
            // CBconfig
            // 
            this.CBconfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBconfig.FormattingEnabled = true;
            this.CBconfig.Location = new System.Drawing.Point(218, 32);
            this.CBconfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBconfig.Name = "CBconfig";
            this.CBconfig.Size = new System.Drawing.Size(304, 28);
            this.CBconfig.TabIndex = 0;
            this.CBconfig.SelectedIndexChanged += new System.EventHandler(this.CBconfig_SelectedIndexChanged);
            // 
            // Lconfig
            // 
            this.Lconfig.AutoSize = true;
            this.Lconfig.Location = new System.Drawing.Point(32, 37);
            this.Lconfig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lconfig.Name = "Lconfig";
            this.Lconfig.Size = new System.Drawing.Size(178, 20);
            this.Lconfig.TabIndex = 1;
            this.Lconfig.Text = "Standard-Konfiguration:";
            // 
            // Breset
            // 
            this.Breset.Location = new System.Drawing.Point(543, 32);
            this.Breset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Breset.Name = "Breset";
            this.Breset.Size = new System.Drawing.Size(112, 35);
            this.Breset.TabIndex = 2;
            this.Breset.Text = "Reset";
            this.Breset.UseVisualStyleBackColor = true;
            this.Breset.Click += new System.EventHandler(this.Breset_Click);
            // 
            // GBausschnitt
            // 
            this.GBausschnitt.Controls.Add(this.Lausschnitt);
            this.GBausschnitt.Controls.Add(this.RBmanuell);
            this.GBausschnitt.Controls.Add(this.RBzeichenbereich);
            this.GBausschnitt.Controls.Add(this.BausschnittErstellen);
            this.GBausschnitt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBausschnitt.Location = new System.Drawing.Point(36, 88);
            this.GBausschnitt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GBausschnitt.Name = "GBausschnitt";
            this.GBausschnitt.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GBausschnitt.Size = new System.Drawing.Size(620, 131);
            this.GBausschnitt.TabIndex = 3;
            this.GBausschnitt.TabStop = false;
            this.GBausschnitt.Text = "Ausschnitt";
            // 
            // Lausschnitt
            // 
            this.Lausschnitt.AutoSize = true;
            this.Lausschnitt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lausschnitt.ForeColor = System.Drawing.Color.Red;
            this.Lausschnitt.Location = new System.Drawing.Point(332, 85);
            this.Lausschnitt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lausschnitt.Name = "Lausschnitt";
            this.Lausschnitt.Size = new System.Drawing.Size(207, 20);
            this.Lausschnitt.TabIndex = 9;
            this.Lausschnitt.Text = "Ausschnitt nicht festgelegt";
            // 
            // RBmanuell
            // 
            this.RBmanuell.AutoSize = true;
            this.RBmanuell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBmanuell.Location = new System.Drawing.Point(44, 78);
            this.RBmanuell.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RBmanuell.Name = "RBmanuell";
            this.RBmanuell.Size = new System.Drawing.Size(191, 24);
            this.RBmanuell.TabIndex = 8;
            this.RBmanuell.Text = "Manueller Ausschnitt";
            this.RBmanuell.UseVisualStyleBackColor = true;
            // 
            // RBzeichenbereich
            // 
            this.RBzeichenbereich.AutoSize = true;
            this.RBzeichenbereich.Checked = true;
            this.RBzeichenbereich.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBzeichenbereich.Location = new System.Drawing.Point(44, 43);
            this.RBzeichenbereich.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RBzeichenbereich.Name = "RBzeichenbereich";
            this.RBzeichenbereich.Size = new System.Drawing.Size(223, 24);
            this.RBzeichenbereich.TabIndex = 7;
            this.RBzeichenbereich.TabStop = true;
            this.RBzeichenbereich.Text = "Zeichnbereich verwenden";
            this.RBzeichenbereich.UseVisualStyleBackColor = true;
            this.RBzeichenbereich.CheckedChanged += new System.EventHandler(this.RBzeichenbereich_CheckedChanged);
            // 
            // BausschnittErstellen
            // 
            this.BausschnittErstellen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BausschnittErstellen.Location = new System.Drawing.Point(318, 29);
            this.BausschnittErstellen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BausschnittErstellen.Name = "BausschnittErstellen";
            this.BausschnittErstellen.Size = new System.Drawing.Size(244, 40);
            this.BausschnittErstellen.TabIndex = 4;
            this.BausschnittErstellen.Text = "Neu festlegen";
            this.BausschnittErstellen.UseVisualStyleBackColor = true;
            this.BausschnittErstellen.Click += new System.EventHandler(this.BausschnittErstellen_Click);
            // 
            // GBmassstab
            // 
            this.GBmassstab.Controls.Add(this.CBexakterAusschnitt);
            this.GBmassstab.Controls.Add(this.label8);
            this.GBmassstab.Controls.Add(this.LunitText);
            this.GBmassstab.Controls.Add(this.TBeinheit);
            this.GBmassstab.Controls.Add(this.CBmassstab);
            this.GBmassstab.Controls.Add(this.LscaleText);
            this.GBmassstab.Controls.Add(this.Lunit);
            this.GBmassstab.Controls.Add(this.Lscale);
            this.GBmassstab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBmassstab.Location = new System.Drawing.Point(36, 228);
            this.GBmassstab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GBmassstab.Name = "GBmassstab";
            this.GBmassstab.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GBmassstab.Size = new System.Drawing.Size(363, 197);
            this.GBmassstab.TabIndex = 4;
            this.GBmassstab.TabStop = false;
            this.GBmassstab.Text = "Maßstab";
            // 
            // CBexakterAusschnitt
            // 
            this.CBexakterAusschnitt.AutoSize = true;
            this.CBexakterAusschnitt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBexakterAusschnitt.Location = new System.Drawing.Point(38, 157);
            this.CBexakterAusschnitt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBexakterAusschnitt.Name = "CBexakterAusschnitt";
            this.CBexakterAusschnitt.Size = new System.Drawing.Size(286, 24);
            this.CBexakterAusschnitt.TabIndex = 7;
            this.CBexakterAusschnitt.Text = "Exakter Ausschnitt ohne Maßstab";
            this.CBexakterAusschnitt.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CBexakterAusschnitt.UseVisualStyleBackColor = true;
            this.CBexakterAusschnitt.CheckedChanged += new System.EventHandler(this.CBexakterAusschnitt_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(310, 118);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 20);
            this.label8.TabIndex = 6;
            this.label8.Text = "mm";
            // 
            // LunitText
            // 
            this.LunitText.AutoSize = true;
            this.LunitText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LunitText.Location = new System.Drawing.Point(48, 112);
            this.LunitText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LunitText.Name = "LunitText";
            this.LunitText.Size = new System.Drawing.Size(153, 20);
            this.LunitText.TabIndex = 5;
            this.LunitText.Text = "1 Einheit entspricht";
            // 
            // TBeinheit
            // 
            this.TBeinheit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBeinheit.Location = new System.Drawing.Point(202, 108);
            this.TBeinheit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TBeinheit.MaxLength = 6;
            this.TBeinheit.Name = "TBeinheit";
            this.TBeinheit.Size = new System.Drawing.Size(97, 26);
            this.TBeinheit.TabIndex = 4;
            this.TBeinheit.Text = "1000";
            this.TBeinheit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberedTexBox_KeyPress);
            // 
            // CBmassstab
            // 
            this.CBmassstab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBmassstab.FormattingEnabled = true;
            this.CBmassstab.Location = new System.Drawing.Point(202, 37);
            this.CBmassstab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBmassstab.MaxLength = 3;
            this.CBmassstab.Name = "CBmassstab";
            this.CBmassstab.Size = new System.Drawing.Size(97, 28);
            this.CBmassstab.TabIndex = 3;
            this.CBmassstab.Text = "100";
            this.CBmassstab.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberedTexBox_KeyPress);
            // 
            // LscaleText
            // 
            this.LscaleText.AutoSize = true;
            this.LscaleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LscaleText.Location = new System.Drawing.Point(170, 42);
            this.LscaleText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LscaleText.Name = "LscaleText";
            this.LscaleText.Size = new System.Drawing.Size(23, 20);
            this.LscaleText.TabIndex = 2;
            this.LscaleText.Text = "1:";
            // 
            // Lunit
            // 
            this.Lunit.AutoSize = true;
            this.Lunit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lunit.Location = new System.Drawing.Point(30, 85);
            this.Lunit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lunit.Name = "Lunit";
            this.Lunit.Size = new System.Drawing.Size(148, 20);
            this.Lunit.TabIndex = 1;
            this.Lunit.Text = "Zeichnungseinheit:";
            // 
            // Lscale
            // 
            this.Lscale.AutoSize = true;
            this.Lscale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lscale.Location = new System.Drawing.Point(30, 42);
            this.Lscale.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lscale.Name = "Lscale";
            this.Lscale.Size = new System.Drawing.Size(124, 20);
            this.Lscale.TabIndex = 0;
            this.Lscale.Text = "Druckmaßstab:";
            // 
            // GBblatt
            // 
            this.GBblatt.Controls.Add(this.LFehlerFormat);
            this.GBblatt.Controls.Add(this.CBdrehen);
            this.GBblatt.Controls.Add(this.CBkopf);
            this.GBblatt.Controls.Add(this.RBhochformat);
            this.GBblatt.Controls.Add(this.RBquerformat);
            this.GBblatt.Controls.Add(this.CBpapierformat);
            this.GBblatt.Controls.Add(this.Lpapierformat);
            this.GBblatt.Controls.Add(this.CBoptimiertePapierformate);
            this.GBblatt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBblatt.Location = new System.Drawing.Point(422, 228);
            this.GBblatt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GBblatt.Name = "GBblatt";
            this.GBblatt.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GBblatt.Size = new System.Drawing.Size(234, 380);
            this.GBblatt.TabIndex = 5;
            this.GBblatt.TabStop = false;
            this.GBblatt.Text = "Blatt";
            // 
            // LFehlerFormat
            // 
            this.LFehlerFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LFehlerFormat.ForeColor = System.Drawing.Color.Red;
            this.LFehlerFormat.Location = new System.Drawing.Point(22, 152);
            this.LFehlerFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LFehlerFormat.Name = "LFehlerFormat";
            this.LFehlerFormat.Size = new System.Drawing.Size(189, 54);
            this.LFehlerFormat.TabIndex = 14;
            this.LFehlerFormat.Text = "Kein passendes Format bei diesem Drucker.";
            // 
            // CBdrehen
            // 
            this.CBdrehen.AutoSize = true;
            this.CBdrehen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBdrehen.Location = new System.Drawing.Point(27, 289);
            this.CBdrehen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBdrehen.Name = "CBdrehen";
            this.CBdrehen.Size = new System.Drawing.Size(147, 24);
            this.CBdrehen.TabIndex = 13;
            this.CBdrehen.Text = "Um 90° drehen";
            this.CBdrehen.UseVisualStyleBackColor = true;
            // 
            // CBkopf
            // 
            this.CBkopf.AutoSize = true;
            this.CBkopf.Checked = true;
            this.CBkopf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBkopf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBkopf.Location = new System.Drawing.Point(27, 331);
            this.CBkopf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBkopf.Name = "CBkopf";
            this.CBkopf.Size = new System.Drawing.Size(151, 24);
            this.CBkopf.TabIndex = 12;
            this.CBkopf.Text = "Kopf + Rahmen";
            this.CBkopf.UseVisualStyleBackColor = true;
            this.CBkopf.CheckedChanged += new System.EventHandler(this.CBkopf_CheckedChanged);
            // 
            // RBhochformat
            // 
            this.RBhochformat.AutoSize = true;
            this.RBhochformat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBhochformat.Location = new System.Drawing.Point(62, 245);
            this.RBhochformat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RBhochformat.Name = "RBhochformat";
            this.RBhochformat.Size = new System.Drawing.Size(122, 24);
            this.RBhochformat.TabIndex = 11;
            this.RBhochformat.Text = "Hochformat";
            this.RBhochformat.UseVisualStyleBackColor = true;
            // 
            // RBquerformat
            // 
            this.RBquerformat.AutoSize = true;
            this.RBquerformat.Checked = true;
            this.RBquerformat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBquerformat.Location = new System.Drawing.Point(62, 211);
            this.RBquerformat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RBquerformat.Name = "RBquerformat";
            this.RBquerformat.Size = new System.Drawing.Size(119, 24);
            this.RBquerformat.TabIndex = 10;
            this.RBquerformat.TabStop = true;
            this.RBquerformat.Text = "Querformat";
            this.RBquerformat.UseVisualStyleBackColor = true;
            // 
            // CBpapierformat
            // 
            this.CBpapierformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBpapierformat.DropDownWidth = 60;
            this.CBpapierformat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBpapierformat.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CBpapierformat.Location = new System.Drawing.Point(135, 100);
            this.CBpapierformat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBpapierformat.Name = "CBpapierformat";
            this.CBpapierformat.Size = new System.Drawing.Size(74, 28);
            this.CBpapierformat.TabIndex = 9;
            this.CBpapierformat.SelectedIndexChanged += new System.EventHandler(this.CBpapierformat_SelectedIndexChanged);
            // 
            // Lpapierformat
            // 
            this.Lpapierformat.AutoSize = true;
            this.Lpapierformat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lpapierformat.Location = new System.Drawing.Point(22, 105);
            this.Lpapierformat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lpapierformat.Name = "Lpapierformat";
            this.Lpapierformat.Size = new System.Drawing.Size(110, 20);
            this.Lpapierformat.TabIndex = 8;
            this.Lpapierformat.Text = "Papierformat:";
            // 
            // CBoptimiertePapierformate
            // 
            this.CBoptimiertePapierformate.AutoEllipsis = true;
            this.CBoptimiertePapierformate.Checked = true;
            this.CBoptimiertePapierformate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBoptimiertePapierformate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBoptimiertePapierformate.Location = new System.Drawing.Point(27, 29);
            this.CBoptimiertePapierformate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBoptimiertePapierformate.Name = "CBoptimiertePapierformate";
            this.CBoptimiertePapierformate.Size = new System.Drawing.Size(198, 62);
            this.CBoptimiertePapierformate.TabIndex = 0;
            this.CBoptimiertePapierformate.Text = "Nur optimierte Papier formate nutzen";
            this.CBoptimiertePapierformate.UseVisualStyleBackColor = true;
            this.CBoptimiertePapierformate.CheckedChanged += new System.EventHandler(this.CBoptimiertePapierformate_CheckedChanged);
            // 
            // Lprinter
            // 
            this.Lprinter.AutoSize = true;
            this.Lprinter.Location = new System.Drawing.Point(51, 451);
            this.Lprinter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lprinter.Name = "Lprinter";
            this.Lprinter.Size = new System.Drawing.Size(69, 20);
            this.Lprinter.TabIndex = 6;
            this.Lprinter.Text = "Drucker:";
            // 
            // Llayoutname
            // 
            this.Llayoutname.AutoSize = true;
            this.Llayoutname.Location = new System.Drawing.Point(51, 503);
            this.Llayoutname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Llayoutname.Name = "Llayoutname";
            this.Llayoutname.Size = new System.Drawing.Size(101, 20);
            this.Llayoutname.TabIndex = 7;
            this.Llayoutname.Text = "Layoutname:";
            // 
            // CBdrucker
            // 
            this.CBdrucker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBdrucker.FormattingEnabled = true;
            this.CBdrucker.Location = new System.Drawing.Point(132, 446);
            this.CBdrucker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBdrucker.Name = "CBdrucker";
            this.CBdrucker.Size = new System.Drawing.Size(265, 28);
            this.CBdrucker.TabIndex = 8;
            this.CBdrucker.SelectedIndexChanged += new System.EventHandler(this.CBdrucker_SelectedIndexChanged);
            // 
            // TBlayout
            // 
            this.TBlayout.Location = new System.Drawing.Point(162, 498);
            this.TBlayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TBlayout.Name = "TBlayout";
            this.TBlayout.Size = new System.Drawing.Size(235, 26);
            this.TBlayout.TabIndex = 9;
            this.TBlayout.Text = "Plot";
            // 
            // Berstellen
            // 
            this.Berstellen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Berstellen.Location = new System.Drawing.Point(36, 554);
            this.Berstellen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Berstellen.Name = "Berstellen";
            this.Berstellen.Size = new System.Drawing.Size(363, 54);
            this.Berstellen.TabIndex = 10;
            this.Berstellen.Text = "Layout erstellen";
            this.Berstellen.UseVisualStyleBackColor = true;
            this.Berstellen.Click += new System.EventHandler(this.Berstellen_Click);
            // 
            // UFLayoutErstellen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 637);
            this.Controls.Add(this.Berstellen);
            this.Controls.Add(this.TBlayout);
            this.Controls.Add(this.CBdrucker);
            this.Controls.Add(this.Llayoutname);
            this.Controls.Add(this.Lprinter);
            this.Controls.Add(this.GBblatt);
            this.Controls.Add(this.GBmassstab);
            this.Controls.Add(this.GBausschnitt);
            this.Controls.Add(this.Breset);
            this.Controls.Add(this.Lconfig);
            this.Controls.Add(this.CBconfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UFLayoutErstellen";
            this.Text = "LayoutErstellen";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UFLayoutErstellen_KeyPress);
            this.GBausschnitt.ResumeLayout(false);
            this.GBausschnitt.PerformLayout();
            this.GBmassstab.ResumeLayout(false);
            this.GBmassstab.PerformLayout();
            this.GBblatt.ResumeLayout(false);
            this.GBblatt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CBconfig;
        private System.Windows.Forms.Label Lconfig;
        private System.Windows.Forms.Button Breset;
        private System.Windows.Forms.GroupBox GBausschnitt;
        private System.Windows.Forms.Button BausschnittErstellen;
        private System.Windows.Forms.GroupBox GBmassstab;
        private System.Windows.Forms.CheckBox CBexakterAusschnitt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label LunitText;
        private System.Windows.Forms.TextBox TBeinheit;
        private System.Windows.Forms.ComboBox CBmassstab;
        private System.Windows.Forms.Label LscaleText;
        private System.Windows.Forms.Label Lunit;
        private System.Windows.Forms.Label Lscale;
        private System.Windows.Forms.GroupBox GBblatt;
        private System.Windows.Forms.RadioButton RBhochformat;
        private System.Windows.Forms.RadioButton RBquerformat;
        private System.Windows.Forms.ComboBox CBpapierformat;
        private System.Windows.Forms.Label Lpapierformat;
        private System.Windows.Forms.CheckBox CBoptimiertePapierformate;
        private System.Windows.Forms.Label Lprinter;
        private System.Windows.Forms.Label Llayoutname;
        private System.Windows.Forms.ComboBox CBdrucker;
        private System.Windows.Forms.TextBox TBlayout;
        private System.Windows.Forms.Button Berstellen;
        private System.Windows.Forms.CheckBox CBkopf;
        private System.Windows.Forms.CheckBox CBdrehen;
        private System.Windows.Forms.RadioButton RBmanuell;
        private System.Windows.Forms.RadioButton RBzeichenbereich;
        private System.Windows.Forms.Label Lausschnitt;
        private System.Windows.Forms.Label LFehlerFormat;
    }
}