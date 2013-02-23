namespace AutoCADTools.Management
{
    partial class ManageDetailCategories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDetailCategories));
            this.ButUpdate = new System.Windows.Forms.Button();
            this.ButDiscard = new System.Windows.Forms.Button();
            this.ButSave = new System.Windows.Forms.Button();
            this.DgAnnotationCategories = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DgAnnotationCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // ButUpdate
            // 
            resources.ApplyResources(this.ButUpdate, "ButUpdate");
            this.ButUpdate.Name = "ButUpdate";
            this.ButUpdate.UseVisualStyleBackColor = true;
            // 
            // ButDiscard
            // 
            resources.ApplyResources(this.ButDiscard, "ButDiscard");
            this.ButDiscard.Name = "ButDiscard";
            this.ButDiscard.UseVisualStyleBackColor = true;
            // 
            // ButSave
            // 
            resources.ApplyResources(this.ButSave, "ButSave");
            this.ButSave.Name = "ButSave";
            this.ButSave.UseVisualStyleBackColor = true;
            this.ButSave.Click += new System.EventHandler(this.ButSave_Click);
            // 
            // DgAnnotationCategories
            // 
            resources.ApplyResources(this.DgAnnotationCategories, "DgAnnotationCategories");
            this.DgAnnotationCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgAnnotationCategories.Name = "DgAnnotationCategories";
            // 
            // ManageDetailCategories
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButUpdate);
            this.Controls.Add(this.ButDiscard);
            this.Controls.Add(this.ButSave);
            this.Controls.Add(this.DgAnnotationCategories);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageDetailCategories";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageDetailCategories_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.DgAnnotationCategories)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButUpdate;
        private System.Windows.Forms.Button ButDiscard;
        private System.Windows.Forms.Button ButSave;
        private System.Windows.Forms.DataGridView DgAnnotationCategories;
    }
}