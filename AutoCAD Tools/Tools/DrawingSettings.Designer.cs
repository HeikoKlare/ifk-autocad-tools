namespace AutoCADTools.Tools
{
    partial class DrawingSettings
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
                if (connection != null) connection.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescriptionShort = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtProjectnumber = new System.Windows.Forms.TextBox();
            this.butEditProjects = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEmployers = new System.Windows.Forms.ComboBox();
            this.txtDescription4 = new System.Windows.Forms.TextBox();
            this.txtDescription3 = new System.Windows.Forms.TextBox();
            this.txtDescription2 = new System.Windows.Forms.TextBox();
            this.txtDescription1 = new System.Windows.Forms.TextBox();
            this.cmbProjects = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblObject = new System.Windows.Forms.Label();
            this.txtSegment = new System.Windows.Forms.TextBox();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabChanged1 = new System.Windows.Forms.TabPage();
            this.chkChanged1Active = new System.Windows.Forms.CheckBox();
            this.dtChanged1Date = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.txtChanged1Note = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbChanged1Name = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabChanged2 = new System.Windows.Forms.TabPage();
            this.chkChanged2Active = new System.Windows.Forms.CheckBox();
            this.dtChanged2Date = new System.Windows.Forms.DateTimePicker();
            this.txtChanged2Note = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtChanged2Name = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.rbMeters = new System.Windows.Forms.RadioButton();
            this.rbCentimeters = new System.Windows.Forms.RadioButton();
            this.rbMillimeters = new System.Windows.Forms.RadioButton();
            this.butCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dtCreation = new System.Windows.Forms.DateTimePicker();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.butStandard = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabChanged1.SuspendLayout();
            this.tabChanged2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescriptionShort);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtProjectnumber);
            this.groupBox1.Controls.Add(this.butEditProjects);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbEmployers);
            this.groupBox1.Controls.Add(this.txtDescription4);
            this.groupBox1.Controls.Add(this.txtDescription3);
            this.groupBox1.Controls.Add(this.txtDescription2);
            this.groupBox1.Controls.Add(this.txtDescription1);
            this.groupBox1.Controls.Add(this.cmbProjects);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtDescriptionShort
            // 
            resources.ApplyResources(this.txtDescriptionShort, "txtDescriptionShort");
            this.txtDescriptionShort.Name = "txtDescriptionShort";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtProjectnumber
            // 
            resources.ApplyResources(this.txtProjectnumber, "txtProjectnumber");
            this.txtProjectnumber.Name = "txtProjectnumber";
            // 
            // butEditProjects
            // 
            this.butEditProjects.AutoEllipsis = true;
            this.butEditProjects.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.butEditProjects, "butEditProjects");
            this.butEditProjects.Name = "butEditProjects";
            this.butEditProjects.UseVisualStyleBackColor = true;
            this.butEditProjects.Click += new System.EventHandler(this.butEditProjects_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbEmployers
            // 
            this.cmbEmployers.FormattingEnabled = true;
            resources.ApplyResources(this.cmbEmployers, "cmbEmployers");
            this.cmbEmployers.Name = "cmbEmployers";
            // 
            // txtDescription4
            // 
            resources.ApplyResources(this.txtDescription4, "txtDescription4");
            this.txtDescription4.Name = "txtDescription4";
            // 
            // txtDescription3
            // 
            resources.ApplyResources(this.txtDescription3, "txtDescription3");
            this.txtDescription3.Name = "txtDescription3";
            // 
            // txtDescription2
            // 
            resources.ApplyResources(this.txtDescription2, "txtDescription2");
            this.txtDescription2.Name = "txtDescription2";
            // 
            // txtDescription1
            // 
            resources.ApplyResources(this.txtDescription1, "txtDescription1");
            this.txtDescription1.Name = "txtDescription1";
            // 
            // cmbProjects
            // 
            this.cmbProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjects.FormattingEnabled = true;
            resources.ApplyResources(this.cmbProjects, "cmbProjects");
            this.cmbProjects.Name = "cmbProjects";
            this.cmbProjects.SelectedIndexChanged += new System.EventHandler(this.cmbProjects_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblObject
            // 
            resources.ApplyResources(this.lblObject, "lblObject");
            this.lblObject.Name = "lblObject";
            // 
            // txtSegment
            // 
            resources.ApplyResources(this.txtSegment, "txtSegment");
            this.txtSegment.Name = "txtSegment";
            // 
            // txtPage
            // 
            resources.ApplyResources(this.txtPage, "txtPage");
            this.txtPage.Name = "txtPage";
            // 
            // lblNumber
            // 
            resources.ApplyResources(this.lblNumber, "lblNumber");
            this.lblNumber.Name = "lblNumber";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabChanged1);
            this.tabControl1.Controls.Add(this.tabChanged2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabChanged1
            // 
            this.tabChanged1.Controls.Add(this.chkChanged1Active);
            this.tabChanged1.Controls.Add(this.dtChanged1Date);
            this.tabChanged1.Controls.Add(this.label16);
            this.tabChanged1.Controls.Add(this.txtChanged1Note);
            this.tabChanged1.Controls.Add(this.label12);
            this.tabChanged1.Controls.Add(this.label11);
            this.tabChanged1.Controls.Add(this.cmbChanged1Name);
            this.tabChanged1.Controls.Add(this.label10);
            resources.ApplyResources(this.tabChanged1, "tabChanged1");
            this.tabChanged1.Name = "tabChanged1";
            this.tabChanged1.UseVisualStyleBackColor = true;
            // 
            // chkChanged1Active
            // 
            resources.ApplyResources(this.chkChanged1Active, "chkChanged1Active");
            this.chkChanged1Active.Name = "chkChanged1Active";
            this.chkChanged1Active.UseVisualStyleBackColor = true;
            this.chkChanged1Active.CheckedChanged += new System.EventHandler(this.chkChanged1Active_CheckedChanged);
            // 
            // dtChanged1Date
            // 
            resources.ApplyResources(this.dtChanged1Date, "dtChanged1Date");
            this.dtChanged1Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtChanged1Date.Name = "dtChanged1Date";
            this.dtChanged1Date.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // txtChanged1Note
            // 
            resources.ApplyResources(this.txtChanged1Note, "txtChanged1Note");
            this.txtChanged1Note.Name = "txtChanged1Note";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // cmbChanged1Name
            // 
            this.cmbChanged1Name.FormattingEnabled = true;
            this.cmbChanged1Name.Items.AddRange(new object[] {
            resources.GetString("cmbChanged1Name.Items")});
            resources.ApplyResources(this.cmbChanged1Name, "cmbChanged1Name");
            this.cmbChanged1Name.Name = "cmbChanged1Name";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // tabChanged2
            // 
            this.tabChanged2.Controls.Add(this.chkChanged2Active);
            this.tabChanged2.Controls.Add(this.dtChanged2Date);
            this.tabChanged2.Controls.Add(this.txtChanged2Note);
            this.tabChanged2.Controls.Add(this.label13);
            this.tabChanged2.Controls.Add(this.label14);
            this.tabChanged2.Controls.Add(this.txtChanged2Name);
            this.tabChanged2.Controls.Add(this.label15);
            resources.ApplyResources(this.tabChanged2, "tabChanged2");
            this.tabChanged2.Name = "tabChanged2";
            this.tabChanged2.UseVisualStyleBackColor = true;
            // 
            // chkChanged2Active
            // 
            resources.ApplyResources(this.chkChanged2Active, "chkChanged2Active");
            this.chkChanged2Active.Name = "chkChanged2Active";
            this.chkChanged2Active.UseVisualStyleBackColor = true;
            this.chkChanged2Active.CheckedChanged += new System.EventHandler(this.chkChanged2Active_CheckedChanged);
            // 
            // dtChanged2Date
            // 
            resources.ApplyResources(this.dtChanged2Date, "dtChanged2Date");
            this.dtChanged2Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtChanged2Date.Name = "dtChanged2Date";
            this.dtChanged2Date.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            // 
            // txtChanged2Note
            // 
            resources.ApplyResources(this.txtChanged2Note, "txtChanged2Note");
            this.txtChanged2Note.Name = "txtChanged2Note";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtChanged2Name
            // 
            this.txtChanged2Name.FormattingEnabled = true;
            this.txtChanged2Name.Items.AddRange(new object[] {
            resources.GetString("txtChanged2Name.Items")});
            resources.ApplyResources(this.txtChanged2Name, "txtChanged2Name");
            this.txtChanged2Name.Name = "txtChanged2Name";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // butOK
            // 
            this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.butOK, "butOK");
            this.butOK.Name = "butOK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // rbMeters
            // 
            resources.ApplyResources(this.rbMeters, "rbMeters");
            this.rbMeters.Name = "rbMeters";
            this.rbMeters.UseVisualStyleBackColor = true;
            this.rbMeters.Click += new System.EventHandler(this.Unit_Click);
            // 
            // rbCentimeters
            // 
            resources.ApplyResources(this.rbCentimeters, "rbCentimeters");
            this.rbCentimeters.Name = "rbCentimeters";
            this.rbCentimeters.UseVisualStyleBackColor = true;
            this.rbCentimeters.Click += new System.EventHandler(this.Unit_Click);
            // 
            // rbMillimeters
            // 
            resources.ApplyResources(this.rbMillimeters, "rbMillimeters");
            this.rbMillimeters.Name = "rbMillimeters";
            this.rbMillimeters.UseVisualStyleBackColor = true;
            this.rbMillimeters.Click += new System.EventHandler(this.Unit_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.butCancel, "butCancel");
            this.butCancel.Name = "butCancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // dtCreation
            // 
            resources.ApplyResources(this.dtCreation, "dtCreation");
            this.dtCreation.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCreation.Name = "dtCreation";
            this.dtCreation.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            // 
            // chkDate
            // 
            resources.ApplyResources(this.chkDate, "chkDate");
            this.chkDate.Checked = true;
            this.chkDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDate.Name = "chkDate";
            this.chkDate.UseVisualStyleBackColor = true;
            this.chkDate.CheckedChanged += new System.EventHandler(this.chkDate_CheckedChanged);
            // 
            // butStandard
            // 
            resources.ApplyResources(this.butStandard, "butStandard");
            this.butStandard.Name = "butStandard";
            this.butStandard.UseVisualStyleBackColor = true;
            this.butStandard.Click += new System.EventHandler(this.butStandard_Click);
            // 
            // DrawingSettings
            // 
            this.AcceptButton = this.butOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.Controls.Add(this.butStandard);
            this.Controls.Add(this.chkDate);
            this.Controls.Add(this.dtCreation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.rbMillimeters);
            this.Controls.Add(this.rbCentimeters);
            this.Controls.Add(this.rbMeters);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtPage);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.txtSegment);
            this.Controls.Add(this.lblObject);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DrawingSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DrawingSettings_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabChanged1.ResumeLayout(false);
            this.tabChanged1.PerformLayout();
            this.tabChanged2.ResumeLayout(false);
            this.tabChanged2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbProjects;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbEmployers;
        private System.Windows.Forms.TextBox txtDescription4;
        private System.Windows.Forms.TextBox txtDescription3;
        private System.Windows.Forms.TextBox txtDescription2;
        private System.Windows.Forms.TextBox txtDescription1;
        private System.Windows.Forms.TextBox txtProjectnumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.TextBox txtSegment;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabChanged1;
        private System.Windows.Forms.TextBox txtChanged1Note;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbChanged1Name;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabChanged2;
        private System.Windows.Forms.TextBox txtChanged2Note;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox txtChanged2Name;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDescriptionShort;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RadioButton rbMeters;
        private System.Windows.Forms.RadioButton rbCentimeters;
        private System.Windows.Forms.RadioButton rbMillimeters;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butEditProjects;
        private System.Windows.Forms.DateTimePicker dtChanged1Date;
        private System.Windows.Forms.DateTimePicker dtChanged2Date;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtCreation;
        private System.Windows.Forms.CheckBox chkChanged1Active;
        private System.Windows.Forms.CheckBox chkChanged2Active;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.Button butStandard;
    }
}