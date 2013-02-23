namespace AutoCADTools.Tools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Annotations));
            this.TxtContent = new System.Windows.Forms.TextBox();
            this.CmbAnnotationCategories = new System.Windows.Forms.ComboBox();
            this.ListAnnotations = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.butClipboard = new System.Windows.Forms.Button();
            this.butClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtContent
            // 
            resources.ApplyResources(this.TxtContent, "TxtContent");
            this.TxtContent.BackColor = System.Drawing.Color.OldLace;
            this.TxtContent.Name = "TxtContent";
            this.TxtContent.ReadOnly = true;
            // 
            // CmbAnnotationCategories
            // 
            resources.ApplyResources(this.CmbAnnotationCategories, "CmbAnnotationCategories");
            this.CmbAnnotationCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAnnotationCategories.FormattingEnabled = true;
            this.CmbAnnotationCategories.Name = "CmbAnnotationCategories";
            this.CmbAnnotationCategories.SelectedIndexChanged += new System.EventHandler(this.CmbAnnotationCategories_SelectedIndexChanged);
            // 
            // ListAnnotations
            // 
            resources.ApplyResources(this.ListAnnotations, "ListAnnotations");
            this.ListAnnotations.HideSelection = false;
            this.ListAnnotations.MultiSelect = false;
            this.ListAnnotations.Name = "ListAnnotations";
            this.ListAnnotations.UseCompatibleStateImageBehavior = false;
            this.ListAnnotations.View = System.Windows.Forms.View.List;
            this.ListAnnotations.SelectedIndexChanged += new System.EventHandler(this.ListAnnotations_SelectedIndexChanged);
            this.ListAnnotations.DoubleClick += new System.EventHandler(this.butClipboard_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // butClipboard
            // 
            resources.ApplyResources(this.butClipboard, "butClipboard");
            this.butClipboard.Name = "butClipboard";
            this.butClipboard.UseVisualStyleBackColor = true;
            this.butClipboard.Click += new System.EventHandler(this.butClipboard_Click);
            // 
            // butClose
            // 
            resources.ApplyResources(this.butClose, "butClose");
            this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butClose.Name = "butClose";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // Annotations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butClose;
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butClipboard);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListAnnotations);
            this.Controls.Add(this.CmbAnnotationCategories);
            this.Controls.Add(this.TxtContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Annotations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtContent;
        private System.Windows.Forms.ComboBox CmbAnnotationCategories;
        private System.Windows.Forms.ListView ListAnnotations;
        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.Button butClipboard;
        private System.Windows.Forms.Label label1;
    }
}