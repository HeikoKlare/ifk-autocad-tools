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
            this.errorProvider.SetError(lblDescriptionShort, resources.GetString("lblDescriptionShort.Error"));
            this.errorProvider.SetIconAlignment(lblDescriptionShort, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDescriptionShort.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDescriptionShort, ((int)(resources.GetObject("lblDescriptionShort.IconPadding"))));
            lblDescriptionShort.Name = "lblDescriptionShort";
            // 
            // lblNumber
            // 
            resources.ApplyResources(lblNumber, "lblNumber");
            this.errorProvider.SetError(lblNumber, resources.GetString("lblNumber.Error"));
            this.errorProvider.SetIconAlignment(lblNumber, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblNumber.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblNumber, ((int)(resources.GetObject("lblNumber.IconPadding"))));
            lblNumber.Name = "lblNumber";
            // 
            // lblEmployer
            // 
            resources.ApplyResources(lblEmployer, "lblEmployer");
            this.errorProvider.SetError(lblEmployer, resources.GetString("lblEmployer.Error"));
            this.errorProvider.SetIconAlignment(lblEmployer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEmployer.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblEmployer, ((int)(resources.GetObject("lblEmployer.IconPadding"))));
            lblEmployer.Name = "lblEmployer";
            // 
            // lblDescription1
            // 
            resources.ApplyResources(lblDescription1, "lblDescription1");
            this.errorProvider.SetError(lblDescription1, resources.GetString("lblDescription1.Error"));
            this.errorProvider.SetIconAlignment(lblDescription1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDescription1.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDescription1, ((int)(resources.GetObject("lblDescription1.IconPadding"))));
            lblDescription1.Name = "lblDescription1";
            // 
            // grpProjects
            // 
            resources.ApplyResources(grpProjects, "grpProjects");
            grpProjects.Controls.Add(this.lvwProjects);
            this.errorProvider.SetError(grpProjects, resources.GetString("grpProjects.Error"));
            grpProjects.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(grpProjects, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpProjects.IconAlignment"))));
            this.errorProvider.SetIconPadding(grpProjects, ((int)(resources.GetObject("grpProjects.IconPadding"))));
            grpProjects.Name = "grpProjects";
            grpProjects.TabStop = false;
            // 
            // lvwProjects
            // 
            resources.ApplyResources(this.lvwProjects, "lvwProjects");
            this.lvwProjects.ContextMenuStrip = this.mnuProjects;
            this.errorProvider.SetError(this.lvwProjects, resources.GetString("lvwProjects.Error"));
            this.lvwProjects.HideSelection = false;
            this.errorProvider.SetIconAlignment(this.lvwProjects, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lvwProjects.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lvwProjects, ((int)(resources.GetObject("lvwProjects.IconPadding"))));
            this.lvwProjects.MultiSelect = false;
            this.lvwProjects.Name = "lvwProjects";
            this.lvwProjects.UseCompatibleStateImageBehavior = false;
            this.lvwProjects.View = System.Windows.Forms.View.Details;
            this.lvwProjects.SelectedIndexChanged += new System.EventHandler(this.ListProjects_SelectedIndexChanged);
            // 
            // mnuProjects
            // 
            resources.ApplyResources(this.mnuProjects, "mnuProjects");
            this.errorProvider.SetError(this.mnuProjects, resources.GetString("mnuProjects.Error"));
            this.errorProvider.SetIconAlignment(this.mnuProjects, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("mnuProjects.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.mnuProjects, ((int)(resources.GetObject("mnuProjects.IconPadding"))));
            this.mnuProjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProjectsRemove,
            this.mnuProjectsEdit,
            this.mnuProjectsUseForNew});
            this.mnuProjects.Name = "ConProjects";
            // 
            // mnuProjectsRemove
            // 
            resources.ApplyResources(this.mnuProjectsRemove, "mnuProjectsRemove");
            this.mnuProjectsRemove.Name = "mnuProjectsRemove";
            this.mnuProjectsRemove.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // mnuProjectsEdit
            // 
            resources.ApplyResources(this.mnuProjectsEdit, "mnuProjectsEdit");
            this.mnuProjectsEdit.Name = "mnuProjectsEdit";
            this.mnuProjectsEdit.Click += new System.EventHandler(this.ButModify_Click);
            // 
            // mnuProjectsUseForNew
            // 
            resources.ApplyResources(this.mnuProjectsUseForNew, "mnuProjectsUseForNew");
            this.mnuProjectsUseForNew.Name = "mnuProjectsUseForNew";
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
            this.errorProvider.SetError(grpProjectData, resources.GetString("grpProjectData.Error"));
            grpProjectData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(grpProjectData, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpProjectData.IconAlignment"))));
            this.errorProvider.SetIconPadding(grpProjectData, ((int)(resources.GetObject("grpProjectData.IconPadding"))));
            grpProjectData.Name = "grpProjectData";
            grpProjectData.TabStop = false;
            // 
            // butEditEmployers
            // 
            resources.ApplyResources(this.butEditEmployers, "butEditEmployers");
            this.errorProvider.SetError(this.butEditEmployers, resources.GetString("butEditEmployers.Error"));
            this.errorProvider.SetIconAlignment(this.butEditEmployers, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butEditEmployers.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butEditEmployers, ((int)(resources.GetObject("butEditEmployers.IconPadding"))));
            this.butEditEmployers.Name = "butEditEmployers";
            this.butEditEmployers.UseVisualStyleBackColor = true;
            this.butEditEmployers.Click += new System.EventHandler(this.ButEditEmployers_Click);
            // 
            // lblProjectCreatedAt
            // 
            resources.ApplyResources(this.lblProjectCreatedAt, "lblProjectCreatedAt");
            this.errorProvider.SetError(this.lblProjectCreatedAt, resources.GetString("lblProjectCreatedAt.Error"));
            this.errorProvider.SetIconAlignment(this.lblProjectCreatedAt, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjectCreatedAt.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblProjectCreatedAt, ((int)(resources.GetObject("lblProjectCreatedAt.IconPadding"))));
            this.lblProjectCreatedAt.Name = "lblProjectCreatedAt";
            // 
            // lblProjectCreatedAtPreamble
            // 
            resources.ApplyResources(lblProjectCreatedAtPreamble, "lblProjectCreatedAtPreamble");
            this.errorProvider.SetError(lblProjectCreatedAtPreamble, resources.GetString("lblProjectCreatedAtPreamble.Error"));
            this.errorProvider.SetIconAlignment(lblProjectCreatedAtPreamble, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjectCreatedAtPreamble.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblProjectCreatedAtPreamble, ((int)(resources.GetObject("lblProjectCreatedAtPreamble.IconPadding"))));
            lblProjectCreatedAtPreamble.Name = "lblProjectCreatedAtPreamble";
            // 
            // txtDescription4
            // 
            resources.ApplyResources(this.txtDescription4, "txtDescription4");
            this.errorProvider.SetError(this.txtDescription4, resources.GetString("txtDescription4.Error"));
            this.errorProvider.SetIconAlignment(this.txtDescription4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDescription4.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDescription4, ((int)(resources.GetObject("txtDescription4.IconPadding"))));
            this.txtDescription4.Name = "txtDescription4";
            // 
            // butRemove
            // 
            resources.ApplyResources(this.butRemove, "butRemove");
            this.errorProvider.SetError(this.butRemove, resources.GetString("butRemove.Error"));
            this.errorProvider.SetIconAlignment(this.butRemove, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butRemove.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butRemove, ((int)(resources.GetObject("butRemove.IconPadding"))));
            this.butRemove.Name = "butRemove";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // txtNumber
            // 
            resources.ApplyResources(this.txtNumber, "txtNumber");
            this.errorProvider.SetError(this.txtNumber, resources.GetString("txtNumber.Error"));
            this.errorProvider.SetIconAlignment(this.txtNumber, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtNumber.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtNumber, ((int)(resources.GetObject("txtNumber.IconPadding"))));
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.TextChanged += new System.EventHandler(this.TxtNumber_TextChanged);
            this.txtNumber.Validating += new System.ComponentModel.CancelEventHandler(this.TxtNumber_Validating);
            // 
            // butUseForNew
            // 
            resources.ApplyResources(this.butUseForNew, "butUseForNew");
            this.errorProvider.SetError(this.butUseForNew, resources.GetString("butUseForNew.Error"));
            this.errorProvider.SetIconAlignment(this.butUseForNew, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butUseForNew.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butUseForNew, ((int)(resources.GetObject("butUseForNew.IconPadding"))));
            this.butUseForNew.Name = "butUseForNew";
            this.butUseForNew.UseVisualStyleBackColor = true;
            this.butUseForNew.Click += new System.EventHandler(this.ButUseForNew_Click);
            // 
            // txtDescription1
            // 
            resources.ApplyResources(this.txtDescription1, "txtDescription1");
            this.errorProvider.SetError(this.txtDescription1, resources.GetString("txtDescription1.Error"));
            this.errorProvider.SetIconAlignment(this.txtDescription1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDescription1.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDescription1, ((int)(resources.GetObject("txtDescription1.IconPadding"))));
            this.txtDescription1.Name = "txtDescription1";
            // 
            // txtDescription3
            // 
            resources.ApplyResources(this.txtDescription3, "txtDescription3");
            this.errorProvider.SetError(this.txtDescription3, resources.GetString("txtDescription3.Error"));
            this.errorProvider.SetIconAlignment(this.txtDescription3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDescription3.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDescription3, ((int)(resources.GetObject("txtDescription3.IconPadding"))));
            this.txtDescription3.Name = "txtDescription3";
            // 
            // txtDescriptionShort
            // 
            resources.ApplyResources(this.txtDescriptionShort, "txtDescriptionShort");
            this.errorProvider.SetError(this.txtDescriptionShort, resources.GetString("txtDescriptionShort.Error"));
            this.errorProvider.SetIconAlignment(this.txtDescriptionShort, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDescriptionShort.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDescriptionShort, ((int)(resources.GetObject("txtDescriptionShort.IconPadding"))));
            this.txtDescriptionShort.Name = "txtDescriptionShort";
            // 
            // butNew
            // 
            resources.ApplyResources(this.butNew, "butNew");
            this.errorProvider.SetError(this.butNew, resources.GetString("butNew.Error"));
            this.errorProvider.SetIconAlignment(this.butNew, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butNew.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butNew, ((int)(resources.GetObject("butNew.IconPadding"))));
            this.butNew.Name = "butNew";
            this.butNew.UseVisualStyleBackColor = true;
            this.butNew.Click += new System.EventHandler(this.ButNew_Click);
            // 
            // cboEmployer
            // 
            resources.ApplyResources(this.cboEmployer, "cboEmployer");
            this.cboEmployer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider.SetError(this.cboEmployer, resources.GetString("cboEmployer.Error"));
            this.cboEmployer.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.cboEmployer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboEmployer.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.cboEmployer, ((int)(resources.GetObject("cboEmployer.IconPadding"))));
            this.cboEmployer.Name = "cboEmployer";
            // 
            // butModify
            // 
            resources.ApplyResources(this.butModify, "butModify");
            this.errorProvider.SetError(this.butModify, resources.GetString("butModify.Error"));
            this.errorProvider.SetIconAlignment(this.butModify, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butModify.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butModify, ((int)(resources.GetObject("butModify.IconPadding"))));
            this.butModify.Name = "butModify";
            this.butModify.UseVisualStyleBackColor = true;
            this.butModify.Click += new System.EventHandler(this.ButModify_Click);
            // 
            // txtDescription2
            // 
            resources.ApplyResources(this.txtDescription2, "txtDescription2");
            this.errorProvider.SetError(this.txtDescription2, resources.GetString("txtDescription2.Error"));
            this.errorProvider.SetIconAlignment(this.txtDescription2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDescription2.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDescription2, ((int)(resources.GetObject("txtDescription2.IconPadding"))));
            this.txtDescription2.Name = "txtDescription2";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
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