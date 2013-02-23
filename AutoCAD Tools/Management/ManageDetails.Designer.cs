namespace AutoCADTools.Management
{
    partial class ManageDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDetails));
            this.label8 = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.CmbDetailCategories = new System.Windows.Forms.ComboBox();
            this.ButRemove = new System.Windows.Forms.Button();
            this.ButAdd = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ButEditCategories = new System.Windows.Forms.Button();
            this.ListDetails = new System.Windows.Forms.ListView();
            this.conDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TsiEditCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.PicPng = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.conDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicPng)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // TxtName
            // 
            resources.ApplyResources(this.TxtName, "TxtName");
            this.TxtName.Name = "TxtName";
            // 
            // CmbDetailCategories
            // 
            this.CmbDetailCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDetailCategories.FormattingEnabled = true;
            resources.ApplyResources(this.CmbDetailCategories, "CmbDetailCategories");
            this.CmbDetailCategories.Name = "CmbDetailCategories";
            this.CmbDetailCategories.SelectedIndexChanged += new System.EventHandler(this.CmbAnnotationCategories_SelectedIndexChanged);
            // 
            // ButRemove
            // 
            resources.ApplyResources(this.ButRemove, "ButRemove");
            this.ButRemove.Name = "ButRemove";
            this.ButRemove.UseVisualStyleBackColor = true;
            this.ButRemove.Click += new System.EventHandler(this.ButRemove_Click);
            // 
            // ButAdd
            // 
            resources.ApplyResources(this.ButAdd, "ButAdd");
            this.ButAdd.Name = "ButAdd";
            this.ButAdd.UseVisualStyleBackColor = true;
            this.ButAdd.Click += new System.EventHandler(this.ButAdd_Click);
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
            this.ButEditCategories.Click += new System.EventHandler(this.ButEditCategories_Click);
            // 
            // ListDetails
            // 
            resources.ApplyResources(this.ListDetails, "ListDetails");
            this.ListDetails.ContextMenuStrip = this.conDetails;
            this.ListDetails.HideSelection = false;
            this.ListDetails.MultiSelect = false;
            this.ListDetails.Name = "ListDetails";
            this.ListDetails.UseCompatibleStateImageBehavior = false;
            this.ListDetails.View = System.Windows.Forms.View.List;
            this.ListDetails.SelectedIndexChanged += new System.EventHandler(this.ListDetails_SelectedIndexChanged);
            // 
            // conDetails
            // 
            this.conDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsiEditCategory});
            this.conDetails.Name = "conDetails";
            resources.ApplyResources(this.conDetails, "conDetails");
            // 
            // TsiEditCategory
            // 
            this.TsiEditCategory.Name = "TsiEditCategory";
            resources.ApplyResources(this.TsiEditCategory, "TsiEditCategory");
            this.TsiEditCategory.Click += new System.EventHandler(this.TsiEditCategory_Click);
            // 
            // PicPng
            // 
            resources.ApplyResources(this.PicPng, "PicPng");
            this.PicPng.BackColor = System.Drawing.Color.OldLace;
            this.PicPng.Name = "PicPng";
            this.PicPng.TabStop = false;
            this.PicPng.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicPng_MouseClick);
            // 
            // ManageDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PicPng);
            this.Controls.Add(this.ListDetails);
            this.Controls.Add(this.CmbDetailCategories);
            this.Controls.Add(this.TxtName);
            this.Controls.Add(this.ButEditCategories);
            this.Controls.Add(this.ButAdd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ButRemove);
            this.Name = "ManageDetails";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.conDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicPng)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.ComboBox CmbDetailCategories;
        private System.Windows.Forms.Button ButRemove;
        private System.Windows.Forms.Button ButAdd;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button ButEditCategories;
        private System.Windows.Forms.ListView ListDetails;
        private System.Windows.Forms.PictureBox PicPng;
        private System.Windows.Forms.ContextMenuStrip conDetails;
        private System.Windows.Forms.ToolStripMenuItem TsiEditCategory;
    }
}