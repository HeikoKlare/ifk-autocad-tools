namespace AutoCADTools.Management
{
    partial class FrmManageProjects
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
                connection.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lblDescriptionShort;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageProjects));
            System.Windows.Forms.Label lblNumber;
            System.Windows.Forms.Label lblEmployer;
            System.Windows.Forms.Label lblDescription1;
            System.Windows.Forms.GroupBox grpProjects;
            System.Windows.Forms.GroupBox grpProjectData;
            System.Windows.Forms.Label lblProjectCreatedAtPreamble;
            this.lvwProjects = new System.Windows.Forms.ListView();
            this.mnuProjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuProjectsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProjectsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProjectsUseForNew = new System.Windows.Forms.ToolStripMenuItem();
            this.butEditEmployers = new System.Windows.Forms.Button();
            this.lblProjectCreatedAt = new System.Windows.Forms.Label();
            this.txtDescription4 = new System.Windows.Forms.TextBox();
            this.butRemove = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.butUseForNew = new System.Windows.Forms.Button();
            this.txtDescription1 = new System.Windows.Forms.TextBox();
            this.txtDescription3 = new System.Windows.Forms.TextBox();
            this.txtDescriptionShort = new System.Windows.Forms.TextBox();
            this.butNew = new System.Windows.Forms.Button();
            this.cboEmployer = new System.Windows.Forms.ComboBox();
            this.butModify = new System.Windows.Forms.Button();
            this.txtDescription2 = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            lblDescriptionShort = new System.Windows.Forms.Label();
            lblNumber = new System.Windows.Forms.Label();
            lblEmployer = new System.Windows.Forms.Label();
            lblDescription1 = new System.Windows.Forms.Label();
            grpProjects = new System.Windows.Forms.GroupBox();
            grpProjectData = new System.Windows.Forms.GroupBox();
            lblProjectCreatedAtPreamble = new System.Windows.Forms.Label();
            grpProjects.SuspendLayout();
            this.mnuProjects.SuspendLayout();
            grpProjectData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDescriptionShort
            // 
            resources.ApplyResources(lblDescriptionShort, "lblDescriptionShort");
            lblDescriptionShort.Name = "lblDescriptionShort";
            // 
            // lblNumber
            // 
            resources.ApplyResources(lblNumber, "lblNumber");
            lblNumber.Name = "lblNumber";
            // 
            // lblEmployer
            // 
            resources.ApplyResources(lblEmployer, "lblEmployer");
            lblEmployer.Name = "lblEmployer";
            // 
            // lblDescription1
            // 
            resources.ApplyResources(lblDescription1, "lblDescription1");
            lblDescription1.Name = "lblDescription1";
            // 
            // grpProjects
            // 
            resources.ApplyResources(grpProjects, "grpProjects");
            grpProjects.Controls.Add(this.lvwProjects);
            grpProjects.FlatStyle = System.Windows.Forms.FlatStyle.System;
            grpProjects.Name = "grpProjects";
            grpProjects.TabStop = false;
            // 
            // lvwProjects
            // 
            resources.ApplyResources(this.lvwProjects, "lvwProjects");
            this.lvwProjects.ContextMenuStrip = this.mnuProjects;
            this.lvwProjects.HideSelection = false;
            this.lvwProjects.MultiSelect = false;
            this.lvwProjects.Name = "lvwProjects";
            this.lvwProjects.UseCompatibleStateImageBehavior = false;
            this.lvwProjects.View = System.Windows.Forms.View.Details;
            this.lvwProjects.SelectedIndexChanged += new System.EventHandler(this.ListProjects_SelectedIndexChanged);
            // 
            // mnuProjects
            // 
            this.mnuProjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProjectsRemove,
            this.mnuProjectsEdit,
            this.mnuProjectsUseForNew});
            this.mnuProjects.Name = "ConProjects";
            resources.ApplyResources(this.mnuProjects, "mnuProjects");
            // 
            // mnuProjectsRemove
            // 
            this.mnuProjectsRemove.Name = "mnuProjectsRemove";
            resources.ApplyResources(this.mnuProjectsRemove, "mnuProjectsRemove");
            this.mnuProjectsRemove.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // mnuProjectsEdit
            // 
            this.mnuProjectsEdit.Name = "mnuProjectsEdit";
            resources.ApplyResources(this.mnuProjectsEdit, "mnuProjectsEdit");
            this.mnuProjectsEdit.Click += new System.EventHandler(this.ButModify_Click);
            // 
            // mnuProjectsUseForNew
            // 
            this.mnuProjectsUseForNew.Name = "mnuProjectsUseForNew";
            resources.ApplyResources(this.mnuProjectsUseForNew, "mnuProjectsUseForNew");
            this.mnuProjectsUseForNew.Click += new System.EventHandler(this.ButUseForNew_Click);
            // 
            // grpProjectData
            // 
            resources.ApplyResources(grpProjectData, "grpProjectData");
            grpProjectData.Controls.Add(this.butEditEmployers);
            grpProjectData.Controls.Add(this.lblProjectCreatedAt);
            grpProjectData.Controls.Add(lblProjectCreatedAtPreamble);
            grpProjectData.Controls.Add(lblNumber);
            grpProjectData.Controls.Add(this.txtDescription4);
            grpProjectData.Controls.Add(this.butRemove);
            grpProjectData.Controls.Add(this.txtNumber);
            grpProjectData.Controls.Add(this.butUseForNew);
            grpProjectData.Controls.Add(this.txtDescription1);
            grpProjectData.Controls.Add(this.txtDescription3);
            grpProjectData.Controls.Add(this.txtDescriptionShort);
            grpProjectData.Controls.Add(lblEmployer);
            grpProjectData.Controls.Add(this.butNew);
            grpProjectData.Controls.Add(lblDescription1);
            grpProjectData.Controls.Add(lblDescriptionShort);
            grpProjectData.Controls.Add(this.cboEmployer);
            grpProjectData.Controls.Add(this.butModify);
            grpProjectData.Controls.Add(this.txtDescription2);
            grpProjectData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            grpProjectData.Name = "grpProjectData";
            grpProjectData.TabStop = false;
            // 
            // butEditEmployers
            // 
            resources.ApplyResources(this.butEditEmployers, "butEditEmployers");
            this.butEditEmployers.Name = "butEditEmployers";
            this.butEditEmployers.UseVisualStyleBackColor = true;
            this.butEditEmployers.Click += new System.EventHandler(this.ButEditEmployers_Click);
            // 
            // lblProjectCreatedAt
            // 
            resources.ApplyResources(this.lblProjectCreatedAt, "lblProjectCreatedAt");
            this.lblProjectCreatedAt.Name = "lblProjectCreatedAt";
            // 
            // lblProjectCreatedAtPreamble
            // 
            resources.ApplyResources(lblProjectCreatedAtPreamble, "lblProjectCreatedAtPreamble");
            lblProjectCreatedAtPreamble.Name = "lblProjectCreatedAtPreamble";
            // 
            // txtDescription4
            // 
            resources.ApplyResources(this.txtDescription4, "txtDescription4");
            this.txtDescription4.Name = "txtDescription4";
            // 
            // butRemove
            // 
            resources.ApplyResources(this.butRemove, "butRemove");
            this.butRemove.Name = "butRemove";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // txtNumber
            // 
            resources.ApplyResources(this.txtNumber, "txtNumber");
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.TextChanged += new System.EventHandler(this.TxtNumber_TextChanged);
            this.txtNumber.Validating += new System.ComponentModel.CancelEventHandler(this.TxtNumber_Validating);
            // 
            // butUseForNew
            // 
            resources.ApplyResources(this.butUseForNew, "butUseForNew");
            this.butUseForNew.Name = "butUseForNew";
            this.butUseForNew.UseVisualStyleBackColor = true;
            this.butUseForNew.Click += new System.EventHandler(this.ButUseForNew_Click);
            // 
            // txtDescription1
            // 
            resources.ApplyResources(this.txtDescription1, "txtDescription1");
            this.txtDescription1.Name = "txtDescription1";
            // 
            // txtDescription3
            // 
            resources.ApplyResources(this.txtDescription3, "txtDescription3");
            this.txtDescription3.Name = "txtDescription3";
            // 
            // txtDescriptionShort
            // 
            resources.ApplyResources(this.txtDescriptionShort, "txtDescriptionShort");
            this.txtDescriptionShort.Name = "txtDescriptionShort";
            // 
            // butNew
            // 
            resources.ApplyResources(this.butNew, "butNew");
            this.butNew.Name = "butNew";
            this.butNew.UseVisualStyleBackColor = true;
            this.butNew.Click += new System.EventHandler(this.ButNew_Click);
            // 
            // cboEmployer
            // 
            this.cboEmployer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboEmployer, "cboEmployer");
            this.cboEmployer.FormattingEnabled = true;
            this.cboEmployer.Name = "cboEmployer";
            // 
            // butModify
            // 
            resources.ApplyResources(this.butModify, "butModify");
            this.butModify.Name = "butModify";
            this.butModify.UseVisualStyleBackColor = true;
            this.butModify.Click += new System.EventHandler(this.ButModify_Click);
            // 
            // txtDescription2
            // 
            resources.ApplyResources(this.txtDescription2, "txtDescription2");
            this.txtDescription2.Name = "txtDescription2";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // FrmManageProjects
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(grpProjectData);
            this.Controls.Add(grpProjects);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FrmManageProjects";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManageProjects_FormClosing);
            this.Load += new System.EventHandler(this.FrmManageProjects_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmManageProjects_KeyPress);
            grpProjects.ResumeLayout(false);
            this.mnuProjects.ResumeLayout(false);
            grpProjectData.ResumeLayout(false);
            grpProjectData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescriptionShort;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.ComboBox cboEmployer;
        private System.Windows.Forms.TextBox txtDescription4;
        private System.Windows.Forms.TextBox txtDescription3;
        private System.Windows.Forms.TextBox txtDescription2;
        private System.Windows.Forms.TextBox txtDescription1;
        private System.Windows.Forms.ContextMenuStrip mnuProjects;
        private System.Windows.Forms.ToolStripMenuItem mnuProjectsRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuProjectsEdit;
        private System.Windows.Forms.Button butNew;
        private System.Windows.Forms.ListView lvwProjects;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.Button butUseForNew;
        private System.Windows.Forms.Button butModify;
        private System.Windows.Forms.Label lblProjectCreatedAt;
        private System.Windows.Forms.Button butEditEmployers;
        private System.Windows.Forms.ToolStripMenuItem mnuProjectsUseForNew;
    }
}