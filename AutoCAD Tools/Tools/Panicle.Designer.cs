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
            this.cmbDescription = new System.Windows.Forms.ComboBox();
            this.butCreate = new System.Windows.Forms.Button();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.radThirdsPoint = new System.Windows.Forms.RadioButton();
            this.radMiddlePoint = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.SuspendLayout();
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
            // butCreate
            // 
            resources.ApplyResources(this.butCreate, "butCreate");
            this.butCreate.Name = "butCreate";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // numCount
            // 
            resources.ApplyResources(this.numCount, "numCount");
            this.numCount.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCount.ValueChanged += new System.EventHandler(this.numCount_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtDistance
            // 
            resources.ApplyResources(this.txtDistance, "txtDistance");
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistance_KeyPress);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // radThirdsPoint
            // 
            resources.ApplyResources(this.radThirdsPoint, "radThirdsPoint");
            this.radThirdsPoint.Checked = true;
            this.radThirdsPoint.Name = "radThirdsPoint";
            this.radThirdsPoint.TabStop = true;
            this.radThirdsPoint.UseVisualStyleBackColor = true;
            // 
            // radMiddlePoint
            // 
            resources.ApplyResources(this.radMiddlePoint, "radMiddlePoint");
            this.radMiddlePoint.Name = "radMiddlePoint";
            this.radMiddlePoint.UseVisualStyleBackColor = true;
            // 
            // Panicle
            // 
            this.AcceptButton = this.butCreate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.radMiddlePoint);
            this.Controls.Add(this.radThirdsPoint);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDescription);
            this.Controls.Add(this.butCreate);
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
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDescription;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radThirdsPoint;
        private System.Windows.Forms.RadioButton radMiddlePoint;

    }
}