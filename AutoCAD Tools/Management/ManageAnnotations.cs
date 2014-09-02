﻿using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using AutoCADTools.Data;


namespace AutoCADTools.Management
{
    /// <summary>
    /// Represents a GUI to modify, add and manage annotations.
    /// It can be navigated through categories and annotations, they can be modifed and added.
    /// </summary>
    public partial class Annotations : Form
    {
        #region Attributes
        
        /// <summary>
        /// The Sql connection doing the connection stuff to the global database.
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// The table containing the annotation categories.
        /// </summary>
        private Database.AnnotationCategoriesDataTable annotationCategoriesTable;
        
        /// <summary>
        /// The table containing the annotations.
        /// </summary>
        private Database.AnnotationsDataTable annotationsTable;

        /// <summary>
        /// A flag indicating in which state management is currently.
        /// </summary>
        private EditState state;

        /// <summary>
        /// A flag indicating if data are bound to the fields or not.
        /// </summary>
        private bool dataBound;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Initates a new GUI for managing projects and the needed database connection and data tables.
        /// </summary>
        public Annotations()
        {
            InitializeComponent();

            state = EditState.input;
            dataBound = false;
            connection = new SqlConnection();

             // Initialize annotation categories and annotation table
            annotationCategoriesTable = new Database.AnnotationCategoriesDataTable();
            annotationsTable = new Database.AnnotationsDataTable();
            ListAnnotations.Columns.Add(LocalData.Name, 240);

            AnnotationCategories_Refresh();
            Annotations_Refresh();
        }
       
        #endregion

        #region Methods

        /// <summary>
        /// Refreshes the annotation categories table by getting recent updates from the global database
        /// and updates the data bindings for an up-to-date GUI presentation.
        /// Last chosen category is restored.
        /// </summary>
        private void AnnotationCategories_Refresh()
        {
            // Save category, clear table and refill category table
            String saveCategory = CmbAnnotationCategories.Text;
            annotationCategoriesTable.Clear();
            connection.FillAnnotationCategories(annotationCategoriesTable);

            // Reset data binding of categories list
            CmbAnnotationCategories.BeginUpdate();
            CmbAnnotationCategories.DataSource = null;
            CmbAnnotationCategories.DataSource = annotationCategoriesTable;
            CmbAnnotationCategories.ValueMember = "id";
            CmbAnnotationCategories.DisplayMember = "name";
            CmbAnnotationCategories.EndUpdate();

            // Restore last chosen category
            CmbAnnotationCategories.Text = saveCategory;
        }

        /// <summary>
        /// Refreshes the annotations table by getting recent updates from the global database
        /// and updates the data bindings for an up-to-date GUI presentation.
        /// Last chosen annotation is restored if possible.
        /// </summary>
        private void Annotations_Refresh()
        {
            // If no category is selected, just clear the annotations list
            if (CmbAnnotationCategories.SelectedIndex == -1)
            {
                ListAnnotations.Items.Clear();
                return;
            }

            // Clear table and refill annotations table
            annotationsTable.Clear();
            int categoryId = int.Parse(CmbAnnotationCategories.SelectedValue.ToString());
            connection.FillAnnotations(annotationsTable, categoryId);

            // Reset data binding of annotations list
            ListAnnotations.BeginUpdate();
            ListAnnotations.Clear();
            foreach (Database.AnnotationsRow row in annotationsTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(row.name);
                    ListAnnotations.Items.Add(lvi);
                }
            }
            ListAnnotations.EndUpdate();
        }

        /// <summary>
        /// Updates the control states depending on the current state of the management,
        /// so there are just the right controls active.
        /// </summary>
        private void UpdateControlStates()
        {
            TxtName.ReadOnly = (state == EditState.annotationSelected);
            TxtContent.ReadOnly = (state == EditState.annotationSelected);
            ButModify.Enabled = (state != EditState.input);
            ButUseForNew.Enabled = (state == EditState.annotationSelected);
            ButRemove.Enabled = (state == EditState.annotationSelected || state == EditState.editing);
            switch (state) 
            {
                case EditState.annotationSelected:
                    ButModify.Text = LocalData.ModifyEdit;
                    break;
                case EditState.editing:
                    ButModify.Text = LocalData.ModifySubmit;
                    break;
                default:
                    ButModify.Text = LocalData.ModifyAdd;
                    break;
            }
        }

        /// <summary>
        /// Clears the content of each field.
        /// </summary>
        private void ClearFields()
        {
            TxtName.Text = "";
            TxtContent.Text = "";
        }

        /// <summary>
        /// Adds the current input data as a new annotation to the annotations table and updates the
        /// global database.
        /// </summary>
        private void AddAnnotation()
        {
            // Initiate a new row and add it to the projects table
            Database.AnnotationsRow newRow = annotationsTable.NewAnnotationsRow();
            newRow.name = TxtName.Text;
            newRow.content = TxtContent.Text;
            newRow.categoryId = (int)CmbAnnotationCategories.SelectedValue;
            annotationsTable.AddAnnotationsRow(newRow);

            // Update the global database
            connection.UpdateAnnotations(annotationsTable);

            // Refresh projects and select the added project
            Annotations_Refresh();
        }

        /// <summary>
        /// Updates the data bindings depending on the current and previous selection.
        /// </summary>
        /// <param name="newSelection">defines a new annotation was selected</param>
        private void UpdateDataBindings(bool newSelection)
        {
            if (newSelection && !dataBound)
            {
                // if there is a project selected, update the controls and bind them to data sources
                dataBound = true;

                CurrencyManager cm = this.BindingContext[annotationsTable] as CurrencyManager;
                cm.Position = ListAnnotations.SelectedIndices[0];

                TxtName.DataBindings.Add("Text", annotationsTable, "name");
                TxtContent.DataBindings.Add("Text", annotationsTable, "content");
            }
            else if (!newSelection && dataBound)
            {
                // if a project was deselected, update the controls and unbind them from data sources
                dataBound = false;
                TxtName.DataBindings.Clear();
                TxtContent.DataBindings.Clear();
            }
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Handles changing the selection of a annotation in the list and updates the data bindings.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ListAnnotations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListAnnotations.SelectedIndices.Count > 0)
            {
                state = EditState.annotationSelected;
                UpdateControlStates();
                UpdateDataBindings(true);
                errorProvider.SetError(TxtName, String.Empty);
                errorProvider.SetError(TxtContent, String.Empty);
            }
            else
            {
                state = EditState.input;
                UpdateControlStates();
                UpdateDataBindings(false);
                ClearFields();
                this.ValidateFields();
            }
        }

        /// <summary>
        /// Adds the current input data as a new annotation to the annotations table and updates the
        /// global database.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButModify_Click(object sender, EventArgs e)
        {
            // Validate the fields
            if (!this.ValidateFields()) return;

            if (state == EditState.addable)
            {
                // If there is no project selected a new one shell be added
                AddAnnotation();
                state = EditState.annotationSelected;
                UpdateControlStates();
            }
            else if (state == EditState.annotationSelected)
            {
                // If a project is selected and there is none being edited, start it
                state = EditState.editing;
                UpdateControlStates();
            }
            else if (state == EditState.editing)
            {
                // If a project is selected and being edited, submit its changes to local and global database
                // and refresh controls
                state = EditState.input;
                UpdateControlStates();
                annotationsTable.Rows[ListAnnotations.SelectedIndices[0]].EndEdit();
                connection.UpdateAnnotations(annotationsTable);
                Annotations_Refresh();
                ClearFields();
                ListAnnotations.SelectedIndices.Clear();
                UpdateDataBindings(false);
            }
        }

        /// <summary>
        /// Handles removing the currently selected row and updates the global database.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButRemove_Click(object sender, EventArgs e)
        {
            // Delete the selected row, update data binding and update global database and controls
            state = EditState.input;
            CurrencyManager cm = this.BindingContext[annotationsTable] as CurrencyManager;
            annotationsTable.Rows[cm.Position].Delete();
            UpdateDataBindings(false);
            ClearFields();
            connection.UpdateAnnotations(annotationsTable);
            Annotations_Refresh();
            ListAnnotations.SelectedIndices.Clear();
            this.ValidateFields();
        }

        /// <summary>
        /// Handles clicking the "New" button to start inserting a new annotation.
        /// The content of all fields is cleared.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButNew_Click(object sender, EventArgs e)
        {
            state = EditState.input;
            ClearFields();
            ListAnnotations.SelectedIndices.Clear();
            UpdateDataBindings(false);
            UpdateControlStates();
            this.ValidateFields();
        }

        /// <summary>
        /// Handles clicking the "Use as new" button, which uses the data of the currently selected
        /// annotation to define a new one.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButUseForNew_Click(object sender, EventArgs e)
        {
            state = EditState.input;
            UpdateControlStates();
            UpdateDataBindings(false);
            ListAnnotations.SelectedIndices.Clear();
            this.ValidateFields();
        }

        /// <summary>
        /// Opens the ManageAnnotationCategories dialog when clicking this button and refreshes the annotation categories
        /// table and controls afterwards.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButEditAnnotationCategories_Click(object sender, EventArgs e)
        {
            using (FrmManageAnnotationCategories management = new FrmManageAnnotationCategories())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
            AnnotationCategories_Refresh();
        }

        /// <summary>
        /// Refreshes the annotations when the selected category changes.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void CmbAnnotationCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Annotations_Refresh();
        }

        #endregion

        #region ErrorHandling
        
        /// <summary>
        /// Calls the validate method of this formular when name or content have to be validated.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void TxtNameContent_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateFields();
        }

        /// <summary>
        /// Calls the validate mathod of this formular when the name or content is changed.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void TxtNameContent_TextChanged(object sender, EventArgs e)
        {
            this.ValidateFields();
        }

        /// <summary>
        /// This method validates the data in this formular, especially the input project number,
        /// which has to be not null and does not have to be already used.
        /// </summary>
        /// <returns>true if everything is okay, false otherweise</returns>
        private bool ValidateFields()
        {
            bool result = true;

            errorProvider.SetError(TxtName, String.Empty);
            errorProvider.SetError(TxtContent, String.Empty);

            if ((state == EditState.addable || state== EditState.input) && String.IsNullOrEmpty(TxtName.Text))
            {
                errorProvider.SetError(TxtName, LocalData.ErrorEmptyName);
                state = EditState.input;
                UpdateControlStates();
                result = false;
            }
            else if ((state == EditState.addable || state == EditState.input) && String.IsNullOrEmpty(TxtContent.Text))
            {
                errorProvider.SetError(TxtContent, LocalData.ErrorEmptyContent);
                state = EditState.input;
                UpdateControlStates();
                result = false;
            }
            else if (state == EditState.addable || state == EditState.input)
            {
                state = EditState.addable;
                UpdateControlStates();
            }

            return result;
        }

        #endregion

        #region Enums

        /// <summary>
        /// This enum represents the current state of this management GUI.
        /// Is defines wheter a annotation is being "input", has a valid project number and so is "addable",
        /// if a there is a "annotationSelected" or if the user is "editing" an alrady added project.
        /// </summary>
        private enum EditState
        {
            input,
            addable,
            annotationSelected,
            editing
        }

        #endregion

    }
}
