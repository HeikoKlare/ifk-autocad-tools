namespace AutoCADTools.Management
{
    partial class FrmSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lblDatabasePath;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label lblDatabaseDatabase;
            System.Windows.Forms.Label lblDatabaseTimeout;
            System.Windows.Forms.Label lblDatabasePassword;
            System.Windows.Forms.Label lblDatabaseLogin;
            System.Windows.Forms.Label lblDatabasePort;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label lblPrinterA3;
            System.Windows.Forms.Label lblPrinterA4;
            System.Windows.Forms.Label lblLayoutname;
            System.Windows.Forms.Label lblPrinterCustom;
            this.txtDatabaseDatabase = new System.Windows.Forms.TextBox();
            this.txtDatabaseTimeout = new System.Windows.Forms.TextBox();
            this.txtDatabasePassword = new System.Windows.Forms.TextBox();
            this.txtDatabaseLogin = new System.Windows.Forms.TextBox();
            this.txtDatabasePort = new System.Windows.Forms.TextBox();
            this.txtDatabasePath = new System.Windows.Forms.TextBox();
            this.butSave = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkDiagonalBracingDescriptionBlack = new System.Windows.Forms.CheckBox();
            this.txtLayoutname = new System.Windows.Forms.TextBox();
            this.txtPrinterA4 = new System.Windows.Forms.TextBox();
            this.txtPrinterA3 = new System.Windows.Forms.TextBox();
            this.txtPrinterCustom = new System.Windows.Forms.TextBox();
            lblDatabasePath = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lblDatabaseDatabase = new System.Windows.Forms.Label();
            lblDatabaseTimeout = new System.Windows.Forms.Label();
            lblDatabasePassword = new System.Windows.Forms.Label();
            lblDatabaseLogin = new System.Windows.Forms.Label();
            lblDatabasePort = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            lblPrinterA3 = new System.Windows.Forms.Label();
            lblPrinterA4 = new System.Windows.Forms.Label();
            lblLayoutname = new System.Windows.Forms.Label();
            lblPrinterCustom = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDatabasePath
            // 
            resources.ApplyResources(lblDatabasePath, "lblDatabasePath");
            lblDatabasePath.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDatabasePath.Name = "lblDatabasePath";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.txtDatabaseDatabase);
            groupBox1.Controls.Add(lblDatabaseDatabase);
            groupBox1.Controls.Add(this.txtDatabaseTimeout);
            groupBox1.Controls.Add(lblDatabaseTimeout);
            groupBox1.Controls.Add(this.txtDatabasePassword);
            groupBox1.Controls.Add(lblDatabasePassword);
            groupBox1.Controls.Add(this.txtDatabaseLogin);
            groupBox1.Controls.Add(lblDatabaseLogin);
            groupBox1.Controls.Add(this.txtDatabasePort);
            groupBox1.Controls.Add(lblDatabasePort);
            groupBox1.Controls.Add(this.txtDatabasePath);
            groupBox1.Controls.Add(lblDatabasePath);
            groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // txtDatabaseDatabase
            // 
            resources.ApplyResources(this.txtDatabaseDatabase, "txtDatabaseDatabase");
            this.txtDatabaseDatabase.Name = "txtDatabaseDatabase";
            // 
            // lblDatabaseDatabase
            // 
            resources.ApplyResources(lblDatabaseDatabase, "lblDatabaseDatabase");
            lblDatabaseDatabase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDatabaseDatabase.Name = "lblDatabaseDatabase";
            // 
            // txtDatabaseTimeout
            // 
            resources.ApplyResources(this.txtDatabaseTimeout, "txtDatabaseTimeout");
            this.txtDatabaseTimeout.Name = "txtDatabaseTimeout";
            this.txtDatabaseTimeout.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTimeoutValidate);
            // 
            // lblDatabaseTimeout
            // 
            resources.ApplyResources(lblDatabaseTimeout, "lblDatabaseTimeout");
            lblDatabaseTimeout.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDatabaseTimeout.Name = "lblDatabaseTimeout";
            // 
            // txtDatabasePassword
            // 
            resources.ApplyResources(this.txtDatabasePassword, "txtDatabasePassword");
            this.txtDatabasePassword.Name = "txtDatabasePassword";
            // 
            // lblDatabasePassword
            // 
            resources.ApplyResources(lblDatabasePassword, "lblDatabasePassword");
            lblDatabasePassword.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDatabasePassword.Name = "lblDatabasePassword";
            // 
            // txtDatabaseLogin
            // 
            resources.ApplyResources(this.txtDatabaseLogin, "txtDatabaseLogin");
            this.txtDatabaseLogin.Name = "txtDatabaseLogin";
            // 
            // lblDatabaseLogin
            // 
            resources.ApplyResources(lblDatabaseLogin, "lblDatabaseLogin");
            lblDatabaseLogin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDatabaseLogin.Name = "lblDatabaseLogin";
            // 
            // txtDatabasePort
            // 
            resources.ApplyResources(this.txtDatabasePort, "txtDatabasePort");
            this.txtDatabasePort.Name = "txtDatabasePort";
            this.txtDatabasePort.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPortValidate);
            // 
            // lblDatabasePort
            // 
            resources.ApplyResources(lblDatabasePort, "lblDatabasePort");
            lblDatabasePort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblDatabasePort.Name = "lblDatabasePort";
            // 
            // txtDatabasePath
            // 
            resources.ApplyResources(this.txtDatabasePath, "txtDatabasePath");
            this.txtDatabasePath.Name = "txtDatabasePath";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.txtPrinterCustom);
            groupBox2.Controls.Add(this.txtPrinterA3);
            groupBox2.Controls.Add(this.txtPrinterA4);
            groupBox2.Controls.Add(this.txtLayoutname);
            groupBox2.Controls.Add(lblPrinterCustom);
            groupBox2.Controls.Add(lblPrinterA3);
            groupBox2.Controls.Add(lblPrinterA4);
            groupBox2.Controls.Add(lblLayoutname);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // lblPrinterA3
            // 
            resources.ApplyResources(lblPrinterA3, "lblPrinterA3");
            lblPrinterA3.Name = "lblPrinterA3";
            // 
            // lblPrinterA4
            // 
            resources.ApplyResources(lblPrinterA4, "lblPrinterA4");
            lblPrinterA4.Name = "lblPrinterA4";
            // 
            // lblLayoutname
            // 
            resources.ApplyResources(lblLayoutname, "lblLayoutname");
            lblLayoutname.Name = "lblLayoutname";
            // 
            // butSave
            // 
            resources.ApplyResources(this.butSave, "butSave");
            this.butSave.Name = "butSave";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.ButSave_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.butCancel, "butCancel");
            this.butCancel.Name = "butCancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.ButCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // chkDiagonalBracingDescriptionBlack
            // 
            resources.ApplyResources(this.chkDiagonalBracingDescriptionBlack, "chkDiagonalBracingDescriptionBlack");
            this.chkDiagonalBracingDescriptionBlack.Name = "chkDiagonalBracingDescriptionBlack";
            this.chkDiagonalBracingDescriptionBlack.UseVisualStyleBackColor = true;
            // 
            // lblPrinterCustom
            // 
            resources.ApplyResources(lblPrinterCustom, "lblPrinterCustom");
            lblPrinterCustom.Name = "lblPrinterCustom";
            // 
            // txtLayoutname
            // 
            resources.ApplyResources(this.txtLayoutname, "txtLayoutname");
            this.txtLayoutname.Name = "txtLayoutname";
            // 
            // txtPrinterA4
            // 
            resources.ApplyResources(this.txtPrinterA4, "txtPrinterA4");
            this.txtPrinterA4.Name = "txtPrinterA4";
            // 
            // txtPrinterA3
            // 
            resources.ApplyResources(this.txtPrinterA3, "txtPrinterA3");
            this.txtPrinterA3.Name = "txtPrinterA3";
            // 
            // txtPrinterCustom
            // 
            resources.ApplyResources(this.txtPrinterCustom, "txtPrinterCustom");
            this.txtPrinterCustom.Name = "txtPrinterCustom";
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.butSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.Controls.Add(groupBox2);
            this.Controls.Add(this.chkDiagonalBracingDescriptionBlack);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butSave);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDatabasePath;
        private System.Windows.Forms.TextBox txtDatabasePassword;
        private System.Windows.Forms.TextBox txtDatabaseLogin;
        private System.Windows.Forms.TextBox txtDatabasePort;
        private System.Windows.Forms.TextBox txtDatabaseDatabase;
        private System.Windows.Forms.TextBox txtDatabaseTimeout;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox chkDiagonalBracingDescriptionBlack;
        private System.Windows.Forms.TextBox txtPrinterCustom;
        private System.Windows.Forms.TextBox txtPrinterA3;
        private System.Windows.Forms.TextBox txtPrinterA4;
        private System.Windows.Forms.TextBox txtLayoutname;
    }
}