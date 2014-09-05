namespace AutoCADTools.Tools
{
    partial class FrmDrawingSettings
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
            System.Windows.Forms.Label lblEmployer;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDrawingSettings));
            System.Windows.Forms.Label lblDescription;
            System.Windows.Forms.GroupBox grpProject;
            System.Windows.Forms.Label lblDescriptionShort;
            System.Windows.Forms.Label lblProjectNumber;
            System.Windows.Forms.Label lblProjectCopyFrom;
            System.Windows.Forms.Label lblObject;
            System.Windows.Forms.Label lblNumber;
            System.Windows.Forms.Label lblDrawingUnit;
            System.Windows.Forms.Label lblCreationDate;
            this.txtDescriptionShort = new System.Windows.Forms.TextBox();
            this.txtProjectnumber = new System.Windows.Forms.TextBox();
            this.butEditProjects = new System.Windows.Forms.Button();
            this.cboEmployers = new System.Windows.Forms.ComboBox();
            this.txtDescription4 = new System.Windows.Forms.TextBox();
            this.txtDescription3 = new System.Windows.Forms.TextBox();
            this.txtDescription2 = new System.Windows.Forms.TextBox();
            this.txtDescription1 = new System.Windows.Forms.TextBox();
            this.cboProjects = new System.Windows.Forms.ComboBox();
            this.txtSegment = new System.Windows.Forms.TextBox();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.butOK = new System.Windows.Forms.Button();
            this.optMeters = new System.Windows.Forms.RadioButton();
            this.optCentimeters = new System.Windows.Forms.RadioButton();
            this.optMillimeters = new System.Windows.Forms.RadioButton();
            this.butCancel = new System.Windows.Forms.Button();
            this.dtpCreationDate = new System.Windows.Forms.DateTimePicker();
            this.butDefaultValues = new System.Windows.Forms.Button();
            lblEmployer = new System.Windows.Forms.Label();
            lblDescription = new System.Windows.Forms.Label();
            grpProject = new System.Windows.Forms.GroupBox();
            lblDescriptionShort = new System.Windows.Forms.Label();
            lblProjectNumber = new System.Windows.Forms.Label();
            lblProjectCopyFrom = new System.Windows.Forms.Label();
            lblObject = new System.Windows.Forms.Label();
            lblNumber = new System.Windows.Forms.Label();
            lblDrawingUnit = new System.Windows.Forms.Label();
            lblCreationDate = new System.Windows.Forms.Label();
            grpProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEmployer
            // 
            resources.ApplyResources(lblEmployer, "lblEmployer");
            lblEmployer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblEmployer.Name = "lblEmployer";
            // 
            // lblDescription
            // 
            resources.ApplyResources(lblDescription, "lblDescription");
            lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDescription.Name = "lblDescription";
            // 
            // grpProject
            // 
            grpProject.Controls.Add(this.txtDescriptionShort);
            grpProject.Controls.Add(lblDescriptionShort);
            grpProject.Controls.Add(this.txtProjectnumber);
            grpProject.Controls.Add(this.butEditProjects);
            grpProject.Controls.Add(lblProjectNumber);
            grpProject.Controls.Add(this.cboEmployers);
            grpProject.Controls.Add(this.txtDescription4);
            grpProject.Controls.Add(this.txtDescription3);
            grpProject.Controls.Add(this.txtDescription2);
            grpProject.Controls.Add(this.txtDescription1);
            grpProject.Controls.Add(this.cboProjects);
            grpProject.Controls.Add(lblProjectCopyFrom);
            grpProject.Controls.Add(lblEmployer);
            grpProject.Controls.Add(lblDescription);
            resources.ApplyResources(grpProject, "grpProject");
            grpProject.Name = "grpProject";
            grpProject.TabStop = false;
            // 
            // txtDescriptionShort
            // 
            resources.ApplyResources(this.txtDescriptionShort, "txtDescriptionShort");
            this.txtDescriptionShort.Name = "txtDescriptionShort";
            // 
            // lblDescriptionShort
            // 
            resources.ApplyResources(lblDescriptionShort, "lblDescriptionShort");
            lblDescriptionShort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDescriptionShort.Name = "lblDescriptionShort";
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
            // lblProjectNumber
            // 
            resources.ApplyResources(lblProjectNumber, "lblProjectNumber");
            lblProjectNumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblProjectNumber.Name = "lblProjectNumber";
            // 
            // cboEmployers
            // 
            this.cboEmployers.FormattingEnabled = true;
            resources.ApplyResources(this.cboEmployers, "cboEmployers");
            this.cboEmployers.Name = "cboEmployers";
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
            // cboProjects
            // 
            this.cboProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboProjects, "cboProjects");
            this.cboProjects.FormattingEnabled = true;
            this.cboProjects.Name = "cboProjects";
            this.cboProjects.SelectedIndexChanged += new System.EventHandler(this.cboProjects_SelectedIndexChanged);
            // 
            // lblProjectCopyFrom
            // 
            resources.ApplyResources(lblProjectCopyFrom, "lblProjectCopyFrom");
            lblProjectCopyFrom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblProjectCopyFrom.Name = "lblProjectCopyFrom";
            // 
            // lblObject
            // 
            resources.ApplyResources(lblObject, "lblObject");
            lblObject.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblObject.Name = "lblObject";
            // 
            // lblNumber
            // 
            resources.ApplyResources(lblNumber, "lblNumber");
            lblNumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblNumber.Name = "lblNumber";
            // 
            // lblDrawingUnit
            // 
            resources.ApplyResources(lblDrawingUnit, "lblDrawingUnit");
            lblDrawingUnit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDrawingUnit.Name = "lblDrawingUnit";
            // 
            // lblCreationDate
            // 
            resources.ApplyResources(lblCreationDate, "lblCreationDate");
            lblCreationDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblCreationDate.Name = "lblCreationDate";
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
            // butOK
            // 
            this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.butOK, "butOK");
            this.butOK.Name = "butOK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // optMeters
            // 
            resources.ApplyResources(this.optMeters, "optMeters");
            this.optMeters.Checked = true;
            this.optMeters.Name = "optMeters";
            this.optMeters.TabStop = true;
            this.optMeters.UseVisualStyleBackColor = true;
            this.optMeters.Click += new System.EventHandler(this.Unit_Click);
            // 
            // optCentimeters
            // 
            resources.ApplyResources(this.optCentimeters, "optCentimeters");
            this.optCentimeters.Name = "optCentimeters";
            this.optCentimeters.UseVisualStyleBackColor = true;
            this.optCentimeters.Click += new System.EventHandler(this.Unit_Click);
            // 
            // optMillimeters
            // 
            resources.ApplyResources(this.optMillimeters, "optMillimeters");
            this.optMillimeters.Name = "optMillimeters";
            this.optMillimeters.UseVisualStyleBackColor = true;
            this.optMillimeters.Click += new System.EventHandler(this.Unit_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.butCancel, "butCancel");
            this.butCancel.Name = "butCancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // dtpCreationDate
            // 
            resources.ApplyResources(this.dtpCreationDate, "dtpCreationDate");
            this.dtpCreationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCreationDate.Name = "dtpCreationDate";
            this.dtpCreationDate.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            // 
            // butDefaultValues
            // 
            resources.ApplyResources(this.butDefaultValues, "butDefaultValues");
            this.butDefaultValues.Name = "butDefaultValues";
            this.butDefaultValues.UseVisualStyleBackColor = true;
            this.butDefaultValues.Click += new System.EventHandler(this.butStandard_Click);
            // 
            // FrmDrawingSettings
            // 
            this.AcceptButton = this.butOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.Controls.Add(this.butDefaultValues);
            this.Controls.Add(this.dtpCreationDate);
            this.Controls.Add(lblCreationDate);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.optMillimeters);
            this.Controls.Add(this.optCentimeters);
            this.Controls.Add(this.optMeters);
            this.Controls.Add(lblDrawingUnit);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.txtPage);
            this.Controls.Add(lblNumber);
            this.Controls.Add(this.txtSegment);
            this.Controls.Add(lblObject);
            this.Controls.Add(grpProject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDrawingSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DrawingSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmDrawingSettings_Load);
            grpProject.ResumeLayout(false);
            grpProject.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboProjects;
        private System.Windows.Forms.ComboBox cboEmployers;
        private System.Windows.Forms.TextBox txtDescription4;
        private System.Windows.Forms.TextBox txtDescription3;
        private System.Windows.Forms.TextBox txtDescription2;
        private System.Windows.Forms.TextBox txtDescription1;
        private System.Windows.Forms.TextBox txtProjectnumber;
        private System.Windows.Forms.TextBox txtSegment;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.TextBox txtDescriptionShort;
        private System.Windows.Forms.RadioButton optMeters;
        private System.Windows.Forms.RadioButton optCentimeters;
        private System.Windows.Forms.RadioButton optMillimeters;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butEditProjects;
        private System.Windows.Forms.DateTimePicker dtpCreationDate;
        private System.Windows.Forms.Button butDefaultValues;
    }
}