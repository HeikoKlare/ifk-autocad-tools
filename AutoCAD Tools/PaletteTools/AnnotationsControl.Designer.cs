namespace AutoCADTools.Tools
{
    partial class AnnotationsControl
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
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ListAnnotations = new System.Windows.Forms.ListView();
            this.CmbAnnotationCategories = new System.Windows.Forms.ComboBox();
            this.TxtContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(10, 334);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Current annotation:";
            // 
            // ListAnnotations
            // 
            this.ListAnnotations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListAnnotations.HideSelection = false;
            this.ListAnnotations.Location = new System.Drawing.Point(10, 41);
            this.ListAnnotations.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.ListAnnotations.MultiSelect = false;
            this.ListAnnotations.Name = "ListAnnotations";
            this.ListAnnotations.Size = new System.Drawing.Size(212, 275);
            this.ListAnnotations.TabIndex = 55;
            this.ListAnnotations.UseCompatibleStateImageBehavior = false;
            this.ListAnnotations.View = System.Windows.Forms.View.List;
            this.ListAnnotations.SelectedIndexChanged += new System.EventHandler(this.ListAnnotations_SelectedIndexChanged);
            this.ListAnnotations.DoubleClick += new System.EventHandler(this.butClipboard_Click);
            // 
            // CmbAnnotationCategories
            // 
            this.CmbAnnotationCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbAnnotationCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAnnotationCategories.FormattingEnabled = true;
            this.CmbAnnotationCategories.Location = new System.Drawing.Point(10, 10);
            this.CmbAnnotationCategories.Margin = new System.Windows.Forms.Padding(10);
            this.CmbAnnotationCategories.Name = "CmbAnnotationCategories";
            this.CmbAnnotationCategories.Size = new System.Drawing.Size(212, 21);
            this.CmbAnnotationCategories.TabIndex = 54;
            this.CmbAnnotationCategories.SelectedIndexChanged += new System.EventHandler(this.CmbAnnotationCategories_SelectedIndexChanged);
            // 
            // TxtContent
            // 
            this.TxtContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtContent.BackColor = System.Drawing.Color.OldLace;
            this.TxtContent.Location = new System.Drawing.Point(10, 357);
            this.TxtContent.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.TxtContent.MaxLength = 30000;
            this.TxtContent.Multiline = true;
            this.TxtContent.Name = "TxtContent";
            this.TxtContent.ReadOnly = true;
            this.TxtContent.Size = new System.Drawing.Size(207, 159);
            this.TxtContent.TabIndex = 56;
            // 
            // AnnotationsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListAnnotations);
            this.Controls.Add(this.CmbAnnotationCategories);
            this.Controls.Add(this.TxtContent);
            this.MinimumSize = new System.Drawing.Size(100, 0);
            this.Name = "AnnotationsControl";
            this.Size = new System.Drawing.Size(232, 526);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView ListAnnotations;
        private System.Windows.Forms.ComboBox CmbAnnotationCategories;
        private System.Windows.Forms.TextBox TxtContent;
    }
}
