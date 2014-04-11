namespace AutoCADTools.Management
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDbPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDbDatabase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDbTimeout = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDbPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDbLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDbPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butSave = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbPanicleDescriptionBlack = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // txtDbPath
            // 
            resources.ApplyResources(this.txtDbPath, "txtDbPath");
            this.errorProvider.SetError(this.txtDbPath, resources.GetString("txtDbPath.Error"));
            this.errorProvider.SetIconAlignment(this.txtDbPath, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDbPath.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDbPath, ((int)(resources.GetObject("txtDbPath.IconPadding"))));
            this.txtDbPath.Name = "txtDbPath";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtDbDatabase);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDbTimeout);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDbPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDbLogin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDbPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDbPath);
            this.groupBox1.Controls.Add(this.label1);
            this.errorProvider.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtDbDatabase
            // 
            resources.ApplyResources(this.txtDbDatabase, "txtDbDatabase");
            this.errorProvider.SetError(this.txtDbDatabase, resources.GetString("txtDbDatabase.Error"));
            this.errorProvider.SetIconAlignment(this.txtDbDatabase, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDbDatabase.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDbDatabase, ((int)(resources.GetObject("txtDbDatabase.IconPadding"))));
            this.txtDbDatabase.Name = "txtDbDatabase";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.errorProvider.SetError(this.label6, resources.GetString("label6.Error"));
            this.errorProvider.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            // 
            // txtDbTimeout
            // 
            resources.ApplyResources(this.txtDbTimeout, "txtDbTimeout");
            this.errorProvider.SetError(this.txtDbTimeout, resources.GetString("txtDbTimeout.Error"));
            this.errorProvider.SetIconAlignment(this.txtDbTimeout, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDbTimeout.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDbTimeout, ((int)(resources.GetObject("txtDbTimeout.IconPadding"))));
            this.txtDbTimeout.Name = "txtDbTimeout";
            this.txtDbTimeout.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTimeoutValidate);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider.SetError(this.label5, resources.GetString("label5.Error"));
            this.errorProvider.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            // 
            // txtDbPassword
            // 
            resources.ApplyResources(this.txtDbPassword, "txtDbPassword");
            this.errorProvider.SetError(this.txtDbPassword, resources.GetString("txtDbPassword.Error"));
            this.errorProvider.SetIconAlignment(this.txtDbPassword, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDbPassword.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDbPassword, ((int)(resources.GetObject("txtDbPassword.IconPadding"))));
            this.txtDbPassword.Name = "txtDbPassword";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider.SetError(this.label4, resources.GetString("label4.Error"));
            this.errorProvider.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            // 
            // txtDbLogin
            // 
            resources.ApplyResources(this.txtDbLogin, "txtDbLogin");
            this.errorProvider.SetError(this.txtDbLogin, resources.GetString("txtDbLogin.Error"));
            this.errorProvider.SetIconAlignment(this.txtDbLogin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDbLogin.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDbLogin, ((int)(resources.GetObject("txtDbLogin.IconPadding"))));
            this.txtDbLogin.Name = "txtDbLogin";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            // 
            // txtDbPort
            // 
            resources.ApplyResources(this.txtDbPort, "txtDbPort");
            this.errorProvider.SetError(this.txtDbPort, resources.GetString("txtDbPort.Error"));
            this.errorProvider.SetIconAlignment(this.txtDbPort, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDbPort.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.txtDbPort, ((int)(resources.GetObject("txtDbPort.IconPadding"))));
            this.txtDbPort.Name = "txtDbPort";
            this.txtDbPort.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPortValidate);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            // 
            // butSave
            // 
            resources.ApplyResources(this.butSave, "butSave");
            this.errorProvider.SetError(this.butSave, resources.GetString("butSave.Error"));
            this.errorProvider.SetIconAlignment(this.butSave, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("butSave.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.butSave, ((int)(resources.GetObject("butSave.IconPadding"))));
            this.butSave.Name = "butSave";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
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
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // cbPanicleDescriptionBlack
            // 
            resources.ApplyResources(this.cbPanicleDescriptionBlack, "cbPanicleDescriptionBlack");
            this.errorProvider.SetError(this.cbPanicleDescriptionBlack, resources.GetString("cbPanicleDescriptionBlack.Error"));
            this.errorProvider.SetIconAlignment(this.cbPanicleDescriptionBlack, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbPanicleDescriptionBlack.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.cbPanicleDescriptionBlack, ((int)(resources.GetObject("cbPanicleDescriptionBlack.IconPadding"))));
            this.cbPanicleDescriptionBlack.Name = "cbPanicleDescriptionBlack";
            this.cbPanicleDescriptionBlack.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.Controls.Add(this.cbPanicleDescriptionBlack);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDbPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDbPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDbLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDbPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDbDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDbTimeout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox cbPanicleDescriptionBlack;
    }
}