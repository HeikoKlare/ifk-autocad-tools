namespace AutoCADTools.Management
{
    partial class ManageProjects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageProjects));
            this.TxtDescriptionShort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CmbEmployer = new System.Windows.Forms.ComboBox();
            this.TxtDescription4 = new System.Windows.Forms.TextBox();
            this.TxtDescription3 = new System.Windows.Forms.TextBox();
            this.TxtDescription2 = new System.Windows.Forms.TextBox();
            this.TxtDescription1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ConProjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useForNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ButNew = new System.Windows.Forms.Button();
            this.ListProjects = new System.Windows.Forms.ListView();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ButModify = new System.Windows.Forms.Button();
            this.ButUseForNew = new System.Windows.Forms.Button();
            this.ButRemove = new System.Windows.Forms.Button();
            this.GrpProjects = new System.Windows.Forms.GroupBox();
            this.GrpProjectData = new System.Windows.Forms.GroupBox();
            this.ButEditEmployers = new System.Windows.Forms.Button();
            this.LblProjectCreatedAt = new System.Windows.Forms.Label();
            this.LblProjectCreatedAtPreamble = new System.Windows.Forms.Label();
            this.ConProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GrpProjects.SuspendLayout();
            this.GrpProjectData.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtDescriptionShort
            // 
            resources.ApplyResources(this.TxtDescriptionShort, "TxtDescriptionShort");
            this.TxtDescriptionShort.Name = "TxtDescriptionShort";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // TxtNumber
            // 
            resources.ApplyResources(this.TxtNumber, "TxtNumber");
            this.TxtNumber.Name = "TxtNumber";
            this.TxtNumber.TextChanged += new System.EventHandler(this.TxtNumber_TextChanged);
            this.TxtNumber.Validating += new System.ComponentModel.CancelEventHandler(this.TxtNumber_Validating);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // CmbEmployer
            // 
            this.CmbEmployer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbEmployer.FormattingEnabled = true;
            resources.ApplyResources(this.CmbEmployer, "CmbEmployer");
            this.CmbEmployer.Name = "CmbEmployer";
            // 
            // TxtDescription4
            // 
            resources.ApplyResources(this.TxtDescription4, "TxtDescription4");
            this.TxtDescription4.Name = "TxtDescription4";
            // 
            // TxtDescription3
            // 
            resources.ApplyResources(this.TxtDescription3, "TxtDescription3");
            this.TxtDescription3.Name = "TxtDescription3";
            // 
            // TxtDescription2
            // 
            resources.ApplyResources(this.TxtDescription2, "TxtDescription2");
            this.TxtDescription2.Name = "TxtDescription2";
            // 
            // TxtDescription1
            // 
            resources.ApplyResources(this.TxtDescription1, "TxtDescription1");
            this.TxtDescription1.Name = "TxtDescription1";
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
            // ConProjects
            // 
            this.ConProjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.editToolStripMenuItem,
            this.useForNewToolStripMenuItem});
            this.ConProjects.Name = "ConProjects";
            resources.ApplyResources(this.ConProjects, "ConProjects");
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(this.removeToolStripMenuItem, "removeToolStripMenuItem");
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            this.editToolStripMenuItem.Click += new System.EventHandler(this.ButModify_Click);
            // 
            // useForNewToolStripMenuItem
            // 
            this.useForNewToolStripMenuItem.Name = "useForNewToolStripMenuItem";
            resources.ApplyResources(this.useForNewToolStripMenuItem, "useForNewToolStripMenuItem");
            this.useForNewToolStripMenuItem.Click += new System.EventHandler(this.ButUseForNew_Click);
            // 
            // ButNew
            // 
            resources.ApplyResources(this.ButNew, "ButNew");
            this.ButNew.Name = "ButNew";
            this.ButNew.UseVisualStyleBackColor = true;
            this.ButNew.Click += new System.EventHandler(this.ButNew_Click);
            // 
            // ListProjects
            // 
            this.ListProjects.ContextMenuStrip = this.ConProjects;
            this.ListProjects.HideSelection = false;
            resources.ApplyResources(this.ListProjects, "ListProjects");
            this.ListProjects.MultiSelect = false;
            this.ListProjects.Name = "ListProjects";
            this.ListProjects.UseCompatibleStateImageBehavior = false;
            this.ListProjects.View = System.Windows.Forms.View.Details;
            this.ListProjects.SelectedIndexChanged += new System.EventHandler(this.ListProjects_SelectedIndexChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ButModify
            // 
            resources.ApplyResources(this.ButModify, "ButModify");
            this.ButModify.Name = "ButModify";
            this.ButModify.UseVisualStyleBackColor = true;
            this.ButModify.Click += new System.EventHandler(this.ButModify_Click);
            // 
            // ButUseForNew
            // 
            resources.ApplyResources(this.ButUseForNew, "ButUseForNew");
            this.ButUseForNew.Name = "ButUseForNew";
            this.ButUseForNew.UseVisualStyleBackColor = true;
            this.ButUseForNew.Click += new System.EventHandler(this.ButUseForNew_Click);
            // 
            // ButRemove
            // 
            resources.ApplyResources(this.ButRemove, "ButRemove");
            this.ButRemove.Name = "ButRemove";
            this.ButRemove.UseVisualStyleBackColor = true;
            this.ButRemove.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // GrpProjects
            // 
            this.GrpProjects.Controls.Add(this.ListProjects);
            resources.ApplyResources(this.GrpProjects, "GrpProjects");
            this.GrpProjects.Name = "GrpProjects";
            this.GrpProjects.TabStop = false;
            // 
            // GrpProjectData
            // 
            this.GrpProjectData.Controls.Add(this.ButEditEmployers);
            this.GrpProjectData.Controls.Add(this.LblProjectCreatedAt);
            this.GrpProjectData.Controls.Add(this.LblProjectCreatedAtPreamble);
            this.GrpProjectData.Controls.Add(this.label7);
            this.GrpProjectData.Controls.Add(this.TxtDescription4);
            this.GrpProjectData.Controls.Add(this.ButRemove);
            this.GrpProjectData.Controls.Add(this.TxtNumber);
            this.GrpProjectData.Controls.Add(this.ButUseForNew);
            this.GrpProjectData.Controls.Add(this.TxtDescription1);
            this.GrpProjectData.Controls.Add(this.label5);
            this.GrpProjectData.Controls.Add(this.label4);
            this.GrpProjectData.Controls.Add(this.TxtDescription3);
            this.GrpProjectData.Controls.Add(this.label3);
            this.GrpProjectData.Controls.Add(this.TxtDescriptionShort);
            this.GrpProjectData.Controls.Add(this.label1);
            this.GrpProjectData.Controls.Add(this.ButNew);
            this.GrpProjectData.Controls.Add(this.label2);
            this.GrpProjectData.Controls.Add(this.label6);
            this.GrpProjectData.Controls.Add(this.CmbEmployer);
            this.GrpProjectData.Controls.Add(this.ButModify);
            this.GrpProjectData.Controls.Add(this.TxtDescription2);
            resources.ApplyResources(this.GrpProjectData, "GrpProjectData");
            this.GrpProjectData.Name = "GrpProjectData";
            this.GrpProjectData.TabStop = false;
            // 
            // ButEditEmployers
            // 
            resources.ApplyResources(this.ButEditEmployers, "ButEditEmployers");
            this.ButEditEmployers.Name = "ButEditEmployers";
            this.ButEditEmployers.UseVisualStyleBackColor = true;
            this.ButEditEmployers.Click += new System.EventHandler(this.ButEditEmployers_Click);
            // 
            // LblProjectCreatedAt
            // 
            resources.ApplyResources(this.LblProjectCreatedAt, "LblProjectCreatedAt");
            this.LblProjectCreatedAt.Name = "LblProjectCreatedAt";
            // 
            // LblProjectCreatedAtPreamble
            // 
            resources.ApplyResources(this.LblProjectCreatedAtPreamble, "LblProjectCreatedAtPreamble");
            this.LblProjectCreatedAtPreamble.Name = "LblProjectCreatedAtPreamble";
            // 
            // ManageProjects
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GrpProjectData);
            this.Controls.Add(this.GrpProjects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageProjects";
            this.ConProjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.GrpProjects.ResumeLayout(false);
            this.GrpProjectData.ResumeLayout(false);
            this.GrpProjectData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtDescriptionShort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CmbEmployer;
        private System.Windows.Forms.TextBox TxtDescription4;
        private System.Windows.Forms.TextBox TxtDescription3;
        private System.Windows.Forms.TextBox TxtDescription2;
        private System.Windows.Forms.TextBox TxtDescription1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip ConProjects;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Button ButNew;
        private System.Windows.Forms.ListView ListProjects;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button ButRemove;
        private System.Windows.Forms.Button ButUseForNew;
        private System.Windows.Forms.Button ButModify;
        private System.Windows.Forms.GroupBox GrpProjectData;
        private System.Windows.Forms.GroupBox GrpProjects;
        private System.Windows.Forms.Label LblProjectCreatedAtPreamble;
        private System.Windows.Forms.Label LblProjectCreatedAt;
        private System.Windows.Forms.Button ButEditEmployers;
        private System.Windows.Forms.ToolStripMenuItem useForNewToolStripMenuItem;
    }
}