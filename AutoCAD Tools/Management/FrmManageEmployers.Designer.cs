namespace AutoCADTools.Management
{
    partial class FrmManageEmployers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageEmployers));
            this.dgdEmployers = new System.Windows.Forms.DataGridView();
            this.butSave = new System.Windows.Forms.Button();
            this.butDiscard = new System.Windows.Forms.Button();
            this.butUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgdEmployers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgdEmployers
            // 
            resources.ApplyResources(this.dgdEmployers, "dgdEmployers");
            this.dgdEmployers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdEmployers.Name = "dgdEmployers";
            this.dgdEmployers.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgdEmployers_DataError);
            // 
            // butSave
            // 
            resources.ApplyResources(this.butSave, "butSave");
            this.butSave.Name = "butSave";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.ButSave_Click);
            // 
            // butDiscard
            // 
            resources.ApplyResources(this.butDiscard, "butDiscard");
            this.butDiscard.Name = "butDiscard";
            this.butDiscard.UseVisualStyleBackColor = true;
            this.butDiscard.Click += new System.EventHandler(this.ButDiscard_Click);
            // 
            // butUpdate
            // 
            resources.ApplyResources(this.butUpdate, "butUpdate");
            this.butUpdate.Name = "butUpdate";
            this.butUpdate.UseVisualStyleBackColor = true;
            this.butUpdate.Click += new System.EventHandler(this.ButUpdate_Click);
            // 
            // FrmManageEmployers
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.butUpdate);
            this.Controls.Add(this.butDiscard);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.dgdEmployers);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManageEmployers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageEmployers_FormClosing);
            this.Load += new System.EventHandler(this.FrmManageEmployers_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmManageEmployers_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dgdEmployers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgdEmployers;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Button butDiscard;
        private System.Windows.Forms.Button butUpdate;

    }
}