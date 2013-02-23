namespace AutoCADTools.Management
{
    partial class Annotations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Annotations));
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtContent = new System.Windows.Forms.TextBox();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.CmbAnnotationCategories = new System.Windows.Forms.ComboBox();
            this.ButRemove = new System.Windows.Forms.Button();
            this.ButNew = new System.Windows.Forms.Button();
            this.ButModify = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ButEditCategories = new System.Windows.Forms.Button();
            this.ListAnnotations = new System.Windows.Forms.ListView();
            this.conAnnotations = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useForNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ButUseForNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.conAnnotations.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // TxtContent
            // 
            resources.ApplyResources(this.TxtContent, "TxtContent");
            this.TxtContent.Name = "TxtContent";
            this.TxtContent.TextChanged += new System.EventHandler(this.TxtNameContent_TextChanged);
            this.TxtContent.Validating += new System.ComponentModel.CancelEventHandler(this.TxtNameContent_Validating);
            // 
            // TxtName
            // 
            resources.ApplyResources(this.TxtName, "TxtName");
            this.TxtName.Name = "TxtName";
            this.TxtName.TextChanged += new System.EventHandler(this.TxtNameContent_TextChanged);
            this.TxtName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtNameContent_Validating);
            // 
            // CmbAnnotationCategories
            // 
            this.CmbAnnotationCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAnnotationCategories.FormattingEnabled = true;
            resources.ApplyResources(this.CmbAnnotationCategories, "CmbAnnotationCategories");
            this.CmbAnnotationCategories.Name = "CmbAnnotationCategories";
            this.CmbAnnotationCategories.SelectedIndexChanged += new System.EventHandler(this.CmbAnnotationCategories_SelectedIndexChanged);
            // 
            // ButRemove
            // 
            resources.ApplyResources(this.ButRemove, "ButRemove");
            this.ButRemove.Name = "ButRemove";
            this.ButRemove.UseVisualStyleBackColor = true;
            this.ButRemove.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // ButNew
            // 
            resources.ApplyResources(this.ButNew, "ButNew");
            this.ButNew.Name = "ButNew";
            this.ButNew.UseVisualStyleBackColor = true;
            this.ButNew.Click += new System.EventHandler(this.ButNew_Click);
            // 
            // ButModify
            // 
            resources.ApplyResources(this.ButModify, "ButModify");
            this.ButModify.Name = "ButModify";
            this.ButModify.UseVisualStyleBackColor = true;
            this.ButModify.Click += new System.EventHandler(this.ButModify_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ButEditCategories
            // 
            resources.ApplyResources(this.ButEditCategories, "ButEditCategories");
            this.ButEditCategories.Name = "ButEditCategories";
            this.ButEditCategories.UseVisualStyleBackColor = true;
            this.ButEditCategories.Click += new System.EventHandler(this.ButEditAnnotationCategories_Click);
            // 
            // ListAnnotations
            // 
            this.ListAnnotations.ContextMenuStrip = this.conAnnotations;
            this.ListAnnotations.HideSelection = false;
            resources.ApplyResources(this.ListAnnotations, "ListAnnotations");
            this.ListAnnotations.MultiSelect = false;
            this.ListAnnotations.Name = "ListAnnotations";
            this.ListAnnotations.UseCompatibleStateImageBehavior = false;
            this.ListAnnotations.View = System.Windows.Forms.View.List;
            this.ListAnnotations.SelectedIndexChanged += new System.EventHandler(this.ListAnnotations_SelectedIndexChanged);
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
            // ButUseForNew
            // 
            resources.ApplyResources(this.ButUseForNew, "ButUseForNew");
            this.ButUseForNew.Name = "ButUseForNew";
            this.ButUseForNew.UseVisualStyleBackColor = true;
            this.ButUseForNew.Click += new System.EventHandler(this.ButUseForNew_Click);
            // 
            // Annotations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButUseForNew);
            this.Controls.Add(this.ListAnnotations);
            this.Controls.Add(this.ButEditCategories);
            this.Controls.Add(this.ButRemove);
            this.Controls.Add(this.ButNew);
            this.Controls.Add(this.ButModify);
            this.Controls.Add(this.CmbAnnotationCategories);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TxtContent);
            this.Controls.Add(this.TxtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Annotations";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.conAnnotations.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtContent;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.ComboBox CmbAnnotationCategories;
        private System.Windows.Forms.Button ButRemove;
        private System.Windows.Forms.Button ButNew;
        private System.Windows.Forms.Button ButModify;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button ButEditCategories;
        private System.Windows.Forms.ListView ListAnnotations;
        private System.Windows.Forms.ContextMenuStrip conAnnotations;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useForNewToolStripMenuItem;
        private System.Windows.Forms.Button ButUseForNew;
    }
}