﻿namespace AutoCADTools.Tools
{
    partial class FrmAnnotations
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
            System.Windows.Forms.Label lblCurrentAnnotation;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnnotations));
            System.Windows.Forms.ColumnHeader Dummy;
            this.txtContent = new System.Windows.Forms.TextBox();
            this.cboAnnotationCategories = new System.Windows.Forms.ComboBox();
            this.lvwAnnotations = new System.Windows.Forms.ListView();
            this.butClipboard = new System.Windows.Forms.Button();
            this.butClose = new System.Windows.Forms.Button();
            lblCurrentAnnotation = new System.Windows.Forms.Label();
            Dummy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lblCurrentAnnotation
            // 
            resources.ApplyResources(lblCurrentAnnotation, "lblCurrentAnnotation");
            lblCurrentAnnotation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            lblCurrentAnnotation.Name = "lblCurrentAnnotation";
            // 
            // txtContent
            // 
            resources.ApplyResources(this.txtContent, "txtContent");
            this.txtContent.BackColor = System.Drawing.Color.OldLace;
            this.txtContent.Name = "txtContent";
            this.txtContent.ReadOnly = true;
            // 
            // cboAnnotationCategories
            // 
            resources.ApplyResources(this.cboAnnotationCategories, "cboAnnotationCategories");
            this.cboAnnotationCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnnotationCategories.FormattingEnabled = true;
            this.cboAnnotationCategories.Name = "cboAnnotationCategories";
            this.cboAnnotationCategories.SelectedIndexChanged += new System.EventHandler(this.cboAnnotationCategories_SelectedIndexChanged);
            // 
            // lvwAnnotations
            // 
            resources.ApplyResources(this.lvwAnnotations, "lvwAnnotations");
            this.lvwAnnotations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            Dummy});
            this.lvwAnnotations.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwAnnotations.HideSelection = false;
            this.lvwAnnotations.MultiSelect = false;
            this.lvwAnnotations.Name = "lvwAnnotations";
            this.lvwAnnotations.UseCompatibleStateImageBehavior = false;
            this.lvwAnnotations.View = System.Windows.Forms.View.Details;
            this.lvwAnnotations.SelectedIndexChanged += new System.EventHandler(this.lvwAnnotations_SelectedIndexChanged);
            this.lvwAnnotations.DoubleClick += new System.EventHandler(this.butClipboard_Click);
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
            // FrmAnnotations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butClose;
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butClipboard);
            this.Controls.Add(lblCurrentAnnotation);
            this.Controls.Add(this.lvwAnnotations);
            this.Controls.Add(this.cboAnnotationCategories);
            this.Controls.Add(this.txtContent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAnnotations";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnnotations_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnnotations_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.ComboBox cboAnnotationCategories;
        private System.Windows.Forms.ListView lvwAnnotations;
        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.Button butClipboard;
    }
}