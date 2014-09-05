namespace AutoCADTools.Tools
{
    partial class FrmDetails
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
            System.Windows.Forms.ColumnHeader Dummy;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetails));
            this.cboDetailCategories = new System.Windows.Forms.ComboBox();
            this.butOpen = new System.Windows.Forms.Button();
            this.lvwDetails = new System.Windows.Forms.ListView();
            this.picPng = new System.Windows.Forms.PictureBox();
            Dummy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.picPng)).BeginInit();
            this.SuspendLayout();
            // 
            // Dummy
            // 
            resources.ApplyResources(Dummy, "Dummy");
            // 
            // cboDetailCategories
            // 
            resources.ApplyResources(this.cboDetailCategories, "cboDetailCategories");
            this.cboDetailCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDetailCategories.FormattingEnabled = true;
            this.cboDetailCategories.Name = "cboDetailCategories";
            this.cboDetailCategories.SelectedIndexChanged += new System.EventHandler(this.cboAnnotationCategories_SelectedIndexChanged);
            // 
            // butOpen
            // 
            resources.ApplyResources(this.butOpen, "butOpen");
            this.butOpen.Name = "butOpen";
            this.butOpen.UseVisualStyleBackColor = true;
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // lvwDetails
            // 
            resources.ApplyResources(this.lvwDetails, "lvwDetails");
            this.lvwDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            Dummy});
            this.lvwDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwDetails.HideSelection = false;
            this.lvwDetails.MultiSelect = false;
            this.lvwDetails.Name = "lvwDetails";
            this.lvwDetails.UseCompatibleStateImageBehavior = false;
            this.lvwDetails.View = System.Windows.Forms.View.Details;
            this.lvwDetails.SelectedIndexChanged += new System.EventHandler(this.lvwDetails_SelectedIndexChanged);
            // 
            // picPng
            // 
            resources.ApplyResources(this.picPng, "picPng");
            this.picPng.BackColor = System.Drawing.Color.OldLace;
            this.picPng.Name = "picPng";
            this.picPng.TabStop = false;
            this.picPng.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPng_MouseClick);
            // 
            // FrmDetails
            // 
            this.AcceptButton = this.butOpen;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picPng);
            this.Controls.Add(this.lvwDetails);
            this.Controls.Add(this.cboDetailCategories);
            this.Controls.Add(this.butOpen);
            this.MinimizeBox = false;
            this.Name = "FrmDetails";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDetails_FormClosing);
            this.Load += new System.EventHandler(this.FrmDetails_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDetails_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picPng)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDetailCategories;
        private System.Windows.Forms.Button butOpen;
        private System.Windows.Forms.ListView lvwDetails;
        private System.Windows.Forms.PictureBox picPng;
    }
}