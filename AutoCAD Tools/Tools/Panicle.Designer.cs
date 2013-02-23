namespace AutoCADTools.Tools
{
    partial class Panicle
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2010")]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Panicle));
            this.chkVertical = new System.Windows.Forms.CheckBox();
            this.chkDouble = new System.Windows.Forms.CheckBox();
            this.cmbDescription = new System.Windows.Forms.ComboBox();
            this.butcreate = new System.Windows.Forms.Button();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkVertical
            // 
            resources.ApplyResources(this.chkVertical, "chkVertical");
            this.chkVertical.Name = "chkVertical";
            this.chkVertical.UseVisualStyleBackColor = true;
            // 
            // chkDouble
            // 
            resources.ApplyResources(this.chkDouble, "chkDouble");
            this.chkDouble.Name = "chkDouble";
            this.chkDouble.UseVisualStyleBackColor = true;
            this.chkDouble.CheckedChanged += new System.EventHandler(this.chkDouble_CheckedChanged);
            // 
            // cmbDescription
            // 
            resources.ApplyResources(this.cmbDescription, "cmbDescription");
            this.cmbDescription.FormattingEnabled = true;
            this.cmbDescription.Items.AddRange(new object[] {
            resources.GetString("cmbDescription.Items"),
            resources.GetString("cmbDescription.Items1"),
            resources.GetString("cmbDescription.Items2"),
            resources.GetString("cmbDescription.Items3")});
            this.cmbDescription.Name = "cmbDescription";
            // 
            // butcreate
            // 
            resources.ApplyResources(this.butcreate, "butcreate");
            this.butcreate.Name = "butcreate";
            this.butcreate.UseVisualStyleBackColor = true;
            this.butcreate.Click += new System.EventHandler(this.butCreate_Click);
            // 
            // txtPosition
            // 
            resources.ApplyResources(this.txtPosition, "txtPosition");
            this.txtPosition.Name = "txtPosition";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Panicle
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.chkVertical);
            this.Controls.Add(this.chkDouble);
            this.Controls.Add(this.cmbDescription);
            this.Controls.Add(this.butcreate);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Panicle";
            this.ShowInTaskbar = false;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Panicle_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkVertical;
        private System.Windows.Forms.CheckBox chkDouble;
        private System.Windows.Forms.ComboBox cmbDescription;
        private System.Windows.Forms.Button butcreate;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}