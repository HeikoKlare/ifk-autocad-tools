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
            System.Windows.Forms.Label lblPrinterCustom;
            System.Windows.Forms.Label lblPrinterA3;
            System.Windows.Forms.Label lblPrinterA4;
            System.Windows.Forms.Label lblLayoutname;
            this.txtDatabaseDatabase = new System.Windows.Forms.TextBox();
            this.txtDatabaseTimeout = new System.Windows.Forms.TextBox();
            this.txtDatabasePassword = new System.Windows.Forms.TextBox();
            this.txtDatabaseLogin = new System.Windows.Forms.TextBox();
            this.txtDatabasePort = new System.Windows.Forms.TextBox();
            this.txtDatabasePath = new System.Windows.Forms.TextBox();
            this.txtPrinterCustom = new System.Windows.Forms.TextBox();
            this.txtPrinterA3 = new System.Windows.Forms.TextBox();
            this.txtPrinterA4 = new System.Windows.Forms.TextBox();
            this.txtLayoutname = new System.Windows.Forms.TextBox();
            this.butSave = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkDiagonalBracingDescriptionBlack = new System.Windows.Forms.CheckBox();
            lblDatabasePath = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lblDatabaseDatabase = new System.Windows.Forms.Label();
            lblDatabaseTimeout = new System.Windows.Forms.Label();
            lblDatabasePassword = new System.Windows.Forms.Label();
            lblDatabaseLogin = new System.Windows.Forms.Label();
            lblDatabasePort = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            lblPrinterCustom = new System.Windows.Forms.Label();
            lblPrinterA3 = new System.Windows.Forms.Label();
            lblPrinterA4 = new System.Windows.Forms.Label();
            lblLayoutname = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDatabasePath
            // 
            resources.ApplyResources(lblDatabasePath, "lblDatabasePath");
            this.errorProvider.SetError(lblDatabasePath, resources.GetString("lblDatabasePath.Error"));
            lblDatabasePath.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(lblDatabasePath, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDatabasePath.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDatabasePath, ((int)(resources.GetObject("lblDatabasePath.IconPadding"))));
            lblDatabasePath.Name = "lblDatabasePath";
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
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
            this.errorProvider.SetError(groupBox1, resources.GetString("groupBox1.Error"));
            groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider.SetIconPadding(groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // txtDatabaseDatabase
            // 
            resources.ApplyResources(this.txtDatabaseDatabase, "txtDatabaseDatabase");
            this.errorProvider.SetError(this.txtDatabaseDatabase, resources.GetString("txtDatabaseDatabase.Error"));
            this.errorProvider.SetIconAlignment(this.txtDatabaseDatabase, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDatabaseDatabase.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDatabaseDatabase, ((int)(resources.GetObject("txtDatabaseDatabase.IconPadding"))));
            this.txtDatabaseDatabase.Name = "txtDatabaseDatabase";
            // 
            // lblDatabaseDatabase
            // 
            resources.ApplyResources(lblDatabaseDatabase, "lblDatabaseDatabase");
            this.errorProvider.SetError(lblDatabaseDatabase, resources.GetString("lblDatabaseDatabase.Error"));
            lblDatabaseDatabase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(lblDatabaseDatabase, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDatabaseDatabase.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDatabaseDatabase, ((int)(resources.GetObject("lblDatabaseDatabase.IconPadding"))));
            lblDatabaseDatabase.Name = "lblDatabaseDatabase";
            // 
            // txtDatabaseTimeout
            // 
            resources.ApplyResources(this.txtDatabaseTimeout, "txtDatabaseTimeout");
            this.errorProvider.SetError(this.txtDatabaseTimeout, resources.GetString("txtDatabaseTimeout.Error"));
            this.errorProvider.SetIconAlignment(this.txtDatabaseTimeout, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDatabaseTimeout.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDatabaseTimeout, ((int)(resources.GetObject("txtDatabaseTimeout.IconPadding"))));
            this.txtDatabaseTimeout.Name = "txtDatabaseTimeout";
            this.txtDatabaseTimeout.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTimeoutValidate);
            // 
            // lblDatabaseTimeout
            // 
            resources.ApplyResources(lblDatabaseTimeout, "lblDatabaseTimeout");
            this.errorProvider.SetError(lblDatabaseTimeout, resources.GetString("lblDatabaseTimeout.Error"));
            lblDatabaseTimeout.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(lblDatabaseTimeout, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDatabaseTimeout.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDatabaseTimeout, ((int)(resources.GetObject("lblDatabaseTimeout.IconPadding"))));
            lblDatabaseTimeout.Name = "lblDatabaseTimeout";
            // 
            // txtDatabasePassword
            // 
            resources.ApplyResources(this.txtDatabasePassword, "txtDatabasePassword");
            this.errorProvider.SetError(this.txtDatabasePassword, resources.GetString("txtDatabasePassword.Error"));
            this.errorProvider.SetIconAlignment(this.txtDatabasePassword, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDatabasePassword.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDatabasePassword, ((int)(resources.GetObject("txtDatabasePassword.IconPadding"))));
            this.txtDatabasePassword.Name = "txtDatabasePassword";
            // 
            // lblDatabasePassword
            // 
            resources.ApplyResources(lblDatabasePassword, "lblDatabasePassword");
            this.errorProvider.SetError(lblDatabasePassword, resources.GetString("lblDatabasePassword.Error"));
            lblDatabasePassword.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(lblDatabasePassword, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDatabasePassword.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDatabasePassword, ((int)(resources.GetObject("lblDatabasePassword.IconPadding"))));
            lblDatabasePassword.Name = "lblDatabasePassword";
            // 
            // txtDatabaseLogin
            // 
            resources.ApplyResources(this.txtDatabaseLogin, "txtDatabaseLogin");
            this.errorProvider.SetError(this.txtDatabaseLogin, resources.GetString("txtDatabaseLogin.Error"));
            this.errorProvider.SetIconAlignment(this.txtDatabaseLogin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDatabaseLogin.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDatabaseLogin, ((int)(resources.GetObject("txtDatabaseLogin.IconPadding"))));
            this.txtDatabaseLogin.Name = "txtDatabaseLogin";
            // 
            // lblDatabaseLogin
            // 
            resources.ApplyResources(lblDatabaseLogin, "lblDatabaseLogin");
            this.errorProvider.SetError(lblDatabaseLogin, resources.GetString("lblDatabaseLogin.Error"));
            lblDatabaseLogin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(lblDatabaseLogin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDatabaseLogin.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDatabaseLogin, ((int)(resources.GetObject("lblDatabaseLogin.IconPadding"))));
            lblDatabaseLogin.Name = "lblDatabaseLogin";
            // 
            // txtDatabasePort
            // 
            resources.ApplyResources(this.txtDatabasePort, "txtDatabasePort");
            this.errorProvider.SetError(this.txtDatabasePort, resources.GetString("txtDatabasePort.Error"));
            this.errorProvider.SetIconAlignment(this.txtDatabasePort, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDatabasePort.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDatabasePort, ((int)(resources.GetObject("txtDatabasePort.IconPadding"))));
            this.txtDatabasePort.Name = "txtDatabasePort";
            this.txtDatabasePort.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPortValidate);
            // 
            // lblDatabasePort
            // 
            resources.ApplyResources(lblDatabasePort, "lblDatabasePort");
            this.errorProvider.SetError(lblDatabasePort, resources.GetString("lblDatabasePort.Error"));
            lblDatabasePort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(lblDatabasePort, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDatabasePort.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblDatabasePort, ((int)(resources.GetObject("lblDatabasePort.IconPadding"))));
            lblDatabasePort.Name = "lblDatabasePort";
            // 
            // txtDatabasePath
            // 
            resources.ApplyResources(this.txtDatabasePath, "txtDatabasePath");
            this.errorProvider.SetError(this.txtDatabasePath, resources.GetString("txtDatabasePath.Error"));
            this.errorProvider.SetIconAlignment(this.txtDatabasePath, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDatabasePath.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDatabasePath, ((int)(resources.GetObject("txtDatabasePath.IconPadding"))));
            this.txtDatabasePath.Name = "txtDatabasePath";
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(this.txtPrinterCustom);
            groupBox2.Controls.Add(this.txtPrinterA3);
            groupBox2.Controls.Add(this.txtPrinterA4);
            groupBox2.Controls.Add(this.txtLayoutname);
            groupBox2.Controls.Add(lblPrinterCustom);
            groupBox2.Controls.Add(lblPrinterA3);
            groupBox2.Controls.Add(lblPrinterA4);
            groupBox2.Controls.Add(lblLayoutname);
            this.errorProvider.SetError(groupBox2, resources.GetString("groupBox2.Error"));
            this.errorProvider.SetIconAlignment(groupBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox2.IconAlignment"))));
            this.errorProvider.SetIconPadding(groupBox2, ((int)(resources.GetObject("groupBox2.IconPadding"))));
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // txtPrinterCustom
            // 
            resources.ApplyResources(this.txtPrinterCustom, "txtPrinterCustom");
            this.errorProvider.SetError(this.txtPrinterCustom, resources.GetString("txtPrinterCustom.Error"));
            this.errorProvider.SetIconAlignment(this.txtPrinterCustom, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtPrinterCustom.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtPrinterCustom, ((int)(resources.GetObject("txtPrinterCustom.IconPadding"))));
            this.txtPrinterCustom.Name = "txtPrinterCustom";
            // 
            // txtPrinterA3
            // 
            resources.ApplyResources(this.txtPrinterA3, "txtPrinterA3");
            this.errorProvider.SetError(this.txtPrinterA3, resources.GetString("txtPrinterA3.Error"));
            this.errorProvider.SetIconAlignment(this.txtPrinterA3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtPrinterA3.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtPrinterA3, ((int)(resources.GetObject("txtPrinterA3.IconPadding"))));
            this.txtPrinterA3.Name = "txtPrinterA3";
            // 
            // txtPrinterA4
            // 
            resources.ApplyResources(this.txtPrinterA4, "txtPrinterA4");
            this.errorProvider.SetError(this.txtPrinterA4, resources.GetString("txtPrinterA4.Error"));
            this.errorProvider.SetIconAlignment(this.txtPrinterA4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtPrinterA4.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtPrinterA4, ((int)(resources.GetObject("txtPrinterA4.IconPadding"))));
            this.txtPrinterA4.Name = "txtPrinterA4";
            // 
            // txtLayoutname
            // 
            resources.ApplyResources(this.txtLayoutname, "txtLayoutname");
            this.errorProvider.SetError(this.txtLayoutname, resources.GetString("txtLayoutname.Error"));
            this.errorProvider.SetIconAlignment(this.txtLayoutname, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLayoutname.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtLayoutname, ((int)(resources.GetObject("txtLayoutname.IconPadding"))));
            this.txtLayoutname.Name = "txtLayoutname";
            // 
            // lblPrinterCustom
            // 
            resources.ApplyResources(lblPrinterCustom, "lblPrinterCustom");
            this.errorProvider.SetError(lblPrinterCustom, resources.GetString("lblPrinterCustom.Error"));
            this.errorProvider.SetIconAlignment(lblPrinterCustom, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPrinterCustom.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblPrinterCustom, ((int)(resources.GetObject("lblPrinterCustom.IconPadding"))));
            lblPrinterCustom.Name = "lblPrinterCustom";
            // 
            // lblPrinterA3
            // 
            resources.ApplyResources(lblPrinterA3, "lblPrinterA3");
            this.errorProvider.SetError(lblPrinterA3, resources.GetString("lblPrinterA3.Error"));
            this.errorProvider.SetIconAlignment(lblPrinterA3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPrinterA3.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblPrinterA3, ((int)(resources.GetObject("lblPrinterA3.IconPadding"))));
            lblPrinterA3.Name = "lblPrinterA3";
            // 
            // lblPrinterA4
            // 
            resources.ApplyResources(lblPrinterA4, "lblPrinterA4");
            this.errorProvider.SetError(lblPrinterA4, resources.GetString("lblPrinterA4.Error"));
            this.errorProvider.SetIconAlignment(lblPrinterA4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPrinterA4.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblPrinterA4, ((int)(resources.GetObject("lblPrinterA4.IconPadding"))));
            lblPrinterA4.Name = "lblPrinterA4";
            // 
            // lblLayoutname
            // 
            resources.ApplyResources(lblLayoutname, "lblLayoutname");
            this.errorProvider.SetError(lblLayoutname, resources.GetString("lblLayoutname.Error"));
            this.errorProvider.SetIconAlignment(lblLayoutname, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLayoutname.IconAlignment"))));
            this.errorProvider.SetIconPadding(lblLayoutname, ((int)(resources.GetObject("lblLayoutname.IconPadding"))));
            lblLayoutname.Name = "lblLayoutname";
            // 
            // butSave
            // 
            resources.ApplyResources(this.butSave, "butSave");
            this.errorProvider.SetError(this.butSave, resources.GetString("butSave.Error"));
            this.errorProvider.SetIconAlignment(this.butSave, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butSave.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butSave, ((int)(resources.GetObject("butSave.IconPadding"))));
            this.butSave.Name = "butSave";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.ButSave_Click);
            // 
            // butCancel
            // 
            resources.ApplyResources(this.butCancel, "butCancel");
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider.SetError(this.butCancel, resources.GetString("butCancel.Error"));
            this.errorProvider.SetIconAlignment(this.butCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butCancel.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butCancel, ((int)(resources.GetObject("butCancel.IconPadding"))));
            this.butCancel.Name = "butCancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.ButCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // chkDiagonalBracingDescriptionBlack
            // 
            resources.ApplyResources(this.chkDiagonalBracingDescriptionBlack, "chkDiagonalBracingDescriptionBlack");
            this.errorProvider.SetError(this.chkDiagonalBracingDescriptionBlack, resources.GetString("chkDiagonalBracingDescriptionBlack.Error"));
            this.errorProvider.SetIconAlignment(this.chkDiagonalBracingDescriptionBlack, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("chkDiagonalBracingDescriptionBlack.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.chkDiagonalBracingDescriptionBlack, ((int)(resources.GetObject("chkDiagonalBracingDescriptionBlack.IconPadding"))));
            this.chkDiagonalBracingDescriptionBlack.Name = "chkDiagonalBracingDescriptionBlack";
            this.chkDiagonalBracingDescriptionBlack.UseVisualStyleBackColor = true;
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