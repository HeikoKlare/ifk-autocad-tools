namespace AutoCADTools.Tools
{
    partial class Details
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Details));
            this.CmbDetailCategories = new System.Windows.Forms.ComboBox();
            this.ButOpen = new System.Windows.Forms.Button();
            this.ListDetails = new System.Windows.Forms.ListView();
            this.PicPng = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicPng)).BeginInit();
            this.SuspendLayout();
            // 
            // CmbDetailCategories
            // 
            resources.ApplyResources(this.CmbDetailCategories, "CmbDetailCategories");
            this.CmbDetailCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDetailCategories.FormattingEnabled = true;
            this.CmbDetailCategories.Name = "CmbDetailCategories";
            this.CmbDetailCategories.SelectedIndexChanged += new System.EventHandler(this.CmbAnnotationCategories_SelectedIndexChanged);
            // 
            // ButOpen
            // 
            resources.ApplyResources(this.ButOpen, "ButOpen");
            this.ButOpen.Name = "ButOpen";
            this.ButOpen.UseVisualStyleBackColor = true;
            this.ButOpen.Click += new System.EventHandler(this.ButOpen_Click);
            // 
            // ListDetails
            // 
            resources.ApplyResources(this.ListDetails, "ListDetails");
            this.ListDetails.HideSelection = false;
            this.ListDetails.MultiSelect = false;
            this.ListDetails.Name = "ListDetails";
            this.ListDetails.UseCompatibleStateImageBehavior = false;
            this.ListDetails.View = System.Windows.Forms.View.List;
            this.ListDetails.SelectedIndexChanged += new System.EventHandler(this.ListDetails_SelectedIndexChanged);
            // 
            // PicPng
            // 
            resources.ApplyResources(this.PicPng, "PicPng");
            this.PicPng.BackColor = System.Drawing.Color.OldLace;
            this.PicPng.Name = "PicPng";
            this.PicPng.TabStop = false;
            this.PicPng.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicPng_MouseClick);
            // 
            // Details
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PicPng);
            this.Controls.Add(this.ListDetails);
            this.Controls.Add(this.CmbDetailCategories);
            this.Controls.Add(this.ButOpen);
            this.MinimizeBox = false;
            this.Name = "Details";
            ((System.ComponentModel.ISupportInitialize)(this.PicPng)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbDetailCategories;
        private System.Windows.Forms.Button ButOpen;
        private System.Windows.Forms.ListView ListDetails;
        private System.Windows.Forms.PictureBox PicPng;
    }
}