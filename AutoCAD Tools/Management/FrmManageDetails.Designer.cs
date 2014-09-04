namespace AutoCADTools.Management
{
    partial class FrmManageDetails
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
            System.Windows.Forms.Label lblName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageDetails));
            this.txtName = new System.Windows.Forms.TextBox();
            this.cboDetailCategories = new System.Windows.Forms.ComboBox();
            this.butRemove = new System.Windows.Forms.Button();
            this.butAdd = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.butEditCategories = new System.Windows.Forms.Button();
            this.lvwDetails = new System.Windows.Forms.ListView();
            this.picPng = new System.Windows.Forms.PictureBox();
            this.butEdit = new System.Windows.Forms.Button();
            lblName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPng)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            resources.ApplyResources(lblName, "lblName");
            lblName.Name = "lblName";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // cboDetailCategories
            // 
            this.cboDetailCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboDetailCategories, "cboDetailCategories");
            this.cboDetailCategories.FormattingEnabled = true;
            this.cboDetailCategories.Name = "cboDetailCategories";
            this.cboDetailCategories.SelectedIndexChanged += new System.EventHandler(this.cboAnnotationCategories_SelectedIndexChanged);
            // 
            // butRemove
            // 
            resources.ApplyResources(this.butRemove, "butRemove");
            this.butRemove.Name = "butRemove";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
            // 
            // butAdd
            // 
            resources.ApplyResources(this.butAdd, "butAdd");
            this.butAdd.Name = "butAdd";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
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
            this.butEditCategories.Click += new System.EventHandler(this.butEditCategories_Click);
            // 
            // lvwDetails
            // 
            resources.ApplyResources(this.lvwDetails, "lvwDetails");
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
            // butEdit
            // 
            resources.ApplyResources(this.butEdit, "butEdit");
            this.butEdit.Name = "butEdit";
            this.butEdit.UseVisualStyleBackColor = true;
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // FrmManageDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.butEdit);
            this.Controls.Add(this.picPng);
            this.Controls.Add(this.lvwDetails);
            this.Controls.Add(this.cboDetailCategories);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.butEditCategories);
            this.Controls.Add(this.butAdd);
            this.Controls.Add(lblName);
            this.Controls.Add(this.butRemove);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FrmManageDetails";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManageDetails_FormClosing);
            this.Load += new System.EventHandler(this.FrmManageDetails_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmanageDetails_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPng)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cboDetailCategories;
        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button butEditCategories;
        private System.Windows.Forms.ListView lvwDetails;
        private System.Windows.Forms.PictureBox picPng;
        private System.Windows.Forms.Button butEdit;
    }
}