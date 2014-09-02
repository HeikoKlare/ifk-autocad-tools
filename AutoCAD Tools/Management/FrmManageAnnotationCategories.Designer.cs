namespace AutoCADTools.Management
{
    partial class FrmManageAnnotationCategories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageAnnotationCategories));
            this.dgdAnnotationCategories = new System.Windows.Forms.DataGridView();
            this.butUpdate = new System.Windows.Forms.Button();
            this.butDiscard = new System.Windows.Forms.Button();
            this.butSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgdAnnotationCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // dgdAnnotationCategories
            // 
            resources.ApplyResources(this.dgdAnnotationCategories, "dgdAnnotationCategories");
            this.dgdAnnotationCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdAnnotationCategories.Name = "dgdAnnotationCategories";
            // 
            // butUpdate
            // 
            resources.ApplyResources(this.butUpdate, "butUpdate");
            this.butUpdate.Name = "butUpdate";
            this.butUpdate.UseVisualStyleBackColor = true;
            this.butUpdate.Click += new System.EventHandler(this.ButUpdate_Click);
            // 
            // butDiscard
            // 
            resources.ApplyResources(this.butDiscard, "butDiscard");
            this.butDiscard.Name = "butDiscard";
            this.butDiscard.UseVisualStyleBackColor = true;
            this.butDiscard.Click += new System.EventHandler(this.ButDiscard_Click);
            // 
            // butSave
            // 
            resources.ApplyResources(this.butSave, "butSave");
            this.butSave.Name = "butSave";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.ButSave_Click);
            // 
            // FrmManageAnnotationCategories
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.butUpdate);
            this.Controls.Add(this.butDiscard);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.dgdAnnotationCategories);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManageAnnotationCategories";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmManageAnnotationCategories_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dgdAnnotationCategories)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgdAnnotationCategories;
        private System.Windows.Forms.Button butUpdate;
        private System.Windows.Forms.Button butDiscard;
        private System.Windows.Forms.Button butSave;
    }
}