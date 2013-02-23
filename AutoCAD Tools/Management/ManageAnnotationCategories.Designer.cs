namespace AutoCADTools.Management
{
    partial class ManageAnnotationCategories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageAnnotationCategories));
            this.DgAnnotationCategories = new System.Windows.Forms.DataGridView();
            this.ButUpdate = new System.Windows.Forms.Button();
            this.ButDiscard = new System.Windows.Forms.Button();
            this.ButSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgAnnotationCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // DgAnnotationCategories
            // 
            resources.ApplyResources(this.DgAnnotationCategories, "DgAnnotationCategories");
            this.DgAnnotationCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgAnnotationCategories.Name = "DgAnnotationCategories";
            // 
            // ButUpdate
            // 
            resources.ApplyResources(this.ButUpdate, "ButUpdate");
            this.ButUpdate.Name = "ButUpdate";
            this.ButUpdate.UseVisualStyleBackColor = true;
            this.ButUpdate.Click += new System.EventHandler(this.ButUpdate_Click);
            // 
            // ButDiscard
            // 
            resources.ApplyResources(this.ButDiscard, "ButDiscard");
            this.ButDiscard.Name = "ButDiscard";
            this.ButDiscard.UseVisualStyleBackColor = true;
            this.ButDiscard.Click += new System.EventHandler(this.ButDiscard_Click);
            // 
            // ButSave
            // 
            resources.ApplyResources(this.ButSave, "ButSave");
            this.ButSave.Name = "ButSave";
            this.ButSave.UseVisualStyleBackColor = true;
            this.ButSave.Click += new System.EventHandler(this.ButSave_Click);
            // 
            // ManageAnnotationCategories
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
            this.Name = "ManageAnnotationCategories";
            ((System.ComponentModel.ISupportInitialize)(this.DgAnnotationCategories)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DgAnnotationCategories;
        private System.Windows.Forms.Button ButUpdate;
        private System.Windows.Forms.Button ButDiscard;
        private System.Windows.Forms.Button ButSave;
    }
}