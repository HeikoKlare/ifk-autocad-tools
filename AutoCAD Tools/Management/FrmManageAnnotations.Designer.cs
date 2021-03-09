namespace AutoCADTools.Management
{
    partial class FrmManageAnnotations
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageAnnotations));
            this.lblAnnotationText = new System.Windows.Forms.Label();
            this.lblAnnotationName = new System.Windows.Forms.Label();
            this.txtAnnotationName = new System.Windows.Forms.TextBox();
            this.cboAnnotationCategories = new System.Windows.Forms.ComboBox();
            this.butRemove = new System.Windows.Forms.Button();
            this.butNew = new System.Windows.Forms.Button();
            this.butModify = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.butEditCategories = new System.Windows.Forms.Button();
            this.lvwAnnotations = new System.Windows.Forms.ListView();
            this.conAnnotations = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useForNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.butUseForNew = new System.Windows.Forms.Button();
            this.rtfAnnotationContent = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.conAnnotations.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAnnotationText
            // 
            resources.ApplyResources(this.lblAnnotationText, "lblAnnotationText");
            this.lblAnnotationText.Name = "lblAnnotationText";
            // 
            // lblAnnotationName
            // 
            resources.ApplyResources(this.lblAnnotationName, "lblAnnotationName");
            this.lblAnnotationName.Name = "lblAnnotationName";
            // 
            // txtAnnotationName
            // 
            resources.ApplyResources(this.txtAnnotationName, "txtAnnotationName");
            this.txtAnnotationName.Name = "txtAnnotationName";
            this.txtAnnotationName.TextChanged += new System.EventHandler(this.TxtName_TextChanged);
            this.txtAnnotationName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtNameContent_Validating);
            // 
            // cboAnnotationCategories
            // 
            this.cboAnnotationCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboAnnotationCategories, "cboAnnotationCategories");
            this.cboAnnotationCategories.FormattingEnabled = true;
            this.cboAnnotationCategories.Name = "cboAnnotationCategories";
            this.cboAnnotationCategories.SelectedIndexChanged += new System.EventHandler(this.CboAnnotationCategories_SelectedIndexChanged);
            // 
            // butRemove
            // 
            resources.ApplyResources(this.butRemove, "butRemove");
            this.butRemove.Name = "butRemove";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // butNew
            // 
            resources.ApplyResources(this.butNew, "butNew");
            this.butNew.Name = "butNew";
            this.butNew.UseVisualStyleBackColor = true;
            this.butNew.Click += new System.EventHandler(this.ButNew_Click);
            // 
            // butModify
            // 
            resources.ApplyResources(this.butModify, "butModify");
            this.butModify.Name = "butModify";
            this.butModify.UseVisualStyleBackColor = true;
            this.butModify.Click += new System.EventHandler(this.ButModify_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // butEditCategories
            // 
            resources.ApplyResources(this.butEditCategories, "butEditCategories");
            this.butEditCategories.Name = "butEditCategories";
            this.butEditCategories.UseVisualStyleBackColor = true;
            this.butEditCategories.Click += new System.EventHandler(this.ButEditAnnotationCategories_Click);
            // 
            // lvwAnnotations
            // 
            resources.ApplyResources(this.lvwAnnotations, "lvwAnnotations");
            this.lvwAnnotations.ContextMenuStrip = this.conAnnotations;
            this.lvwAnnotations.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwAnnotations.HideSelection = false;
            this.lvwAnnotations.MultiSelect = false;
            this.lvwAnnotations.Name = "lvwAnnotations";
            this.lvwAnnotations.UseCompatibleStateImageBehavior = false;
            this.lvwAnnotations.View = System.Windows.Forms.View.Details;
            this.lvwAnnotations.SelectedIndexChanged += new System.EventHandler(this.LvwAnnotations_SelectedIndexChanged);
            // 
            // conAnnotations
            // 
            this.conAnnotations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.editToolStripMenuItem,
            this.useForNewToolStripMenuItem});
            this.conAnnotations.Name = "contextMenuStrip1";
            resources.ApplyResources(this.conAnnotations, "conAnnotations");
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
            // butUseForNew
            // 
            resources.ApplyResources(this.butUseForNew, "butUseForNew");
            this.butUseForNew.Name = "butUseForNew";
            this.butUseForNew.UseVisualStyleBackColor = true;
            this.butUseForNew.Click += new System.EventHandler(this.ButUseForNew_Click);
            // 
            // rtfAnnotationContent
            // 
            resources.ApplyResources(this.rtfAnnotationContent, "rtfAnnotationContent");
            this.rtfAnnotationContent.BackColor = System.Drawing.Color.OldLace;
            this.rtfAnnotationContent.ForeColor = System.Drawing.Color.Black;
            this.rtfAnnotationContent.Name = "rtfAnnotationContent";
            this.rtfAnnotationContent.TextChanged += new System.EventHandler(this.RtfContent_TextChanged);
            this.rtfAnnotationContent.Validating += new System.ComponentModel.CancelEventHandler(this.TxtNameContent_Validating);
            // 
            // FrmManageAnnotations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtfAnnotationContent);
            this.Controls.Add(this.butUseForNew);
            this.Controls.Add(this.lvwAnnotations);
            this.Controls.Add(this.butEditCategories);
            this.Controls.Add(this.butRemove);
            this.Controls.Add(this.butNew);
            this.Controls.Add(this.butModify);
            this.Controls.Add(this.cboAnnotationCategories);
            this.Controls.Add(this.lblAnnotationText);
            this.Controls.Add(this.lblAnnotationName);
            this.Controls.Add(this.txtAnnotationName);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManageAnnotations";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManageAnnotations_FormClosing);
            this.Load += new System.EventHandler(this.FrmManageAnnotations_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmManageAnnotations_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.conAnnotations.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAnnotationText;
        private System.Windows.Forms.Label lblAnnotationName;
        private System.Windows.Forms.TextBox txtAnnotationName;
        private System.Windows.Forms.ComboBox cboAnnotationCategories;
        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.Button butNew;
        private System.Windows.Forms.Button butModify;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button butEditCategories;
        private System.Windows.Forms.ListView lvwAnnotations;
        private System.Windows.Forms.ContextMenuStrip conAnnotations;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useForNewToolStripMenuItem;
        private System.Windows.Forms.Button butUseForNew;
        private System.Windows.Forms.RichTextBox rtfAnnotationContent;
    }
}