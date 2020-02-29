using System;
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
    public partial class FrmManageAnnotations : Form
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

        #region Load/Unload
        
        /// <summary>
        /// Initates a new GUI for managing projects and the needed database connection and data tables.
        /// </summary>
        public FrmManageAnnotations()
        {
            InitializeComponent();
        }
        
        private void FrmManageAnnotations_Load(object sender, EventArgs e)
        {
            state = EditState.input;
            dataBound = false;
            try
            {
                connection = new SqlConnection();
            }
            catch (Exception)
            {
                this.Close();
                return;
            }

            // Initialize annotation categories and annotation table
            annotationCategoriesTable = new Database.AnnotationCategoriesDataTable();
            annotationsTable = new Database.AnnotationsDataTable();
            lvwAnnotations.Columns.Add(new ColumnHeader());

            AnnotationCategories_Refresh();
            Annotations_Refresh();
        }

        private void FrmManageAnnotations_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null) connection.Dispose();
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
            if (connection == null || !connection.IsEstablished()) return;

            // Save category, clear table and refill category table
            String saveCategory = cboAnnotationCategories.Text;
            annotationCategoriesTable.Clear();
            connection.FillAnnotationCategories(annotationCategoriesTable);

            // Reset data binding of categories list
            cboAnnotationCategories.BeginUpdate();
            cboAnnotationCategories.DataSource = null;
            cboAnnotationCategories.DataSource = annotationCategoriesTable;
            cboAnnotationCategories.ValueMember = "id";
            cboAnnotationCategories.DisplayMember = "name";
            cboAnnotationCategories.EndUpdate();

            // Restore last chosen category
            cboAnnotationCategories.Text = saveCategory;
        }

        /// <summary>
        /// Refreshes the annotations table by getting recent updates from the global database
        /// and updates the data bindings for an up-to-date GUI presentation.
        /// Last chosen annotation is restored if possible.
        /// </summary>
        private void Annotations_Refresh()
        {
            lvwAnnotations.SelectedIndices.Clear();
            lvwAnnotations.Items.Clear();
            // If no category is selected, just clear the annotations list
            if (cboAnnotationCategories.SelectedIndex == -1)
            {
                return;
            }

            // Clear table and refill annotations table
            annotationsTable.Clear();
            int categoryId = 0;
            if (!int.TryParse(cboAnnotationCategories.SelectedValue.ToString(), out categoryId))
            {
                return;
            }
            connection.FillAnnotations(annotationsTable, categoryId);

            // Reset data binding of annotations list
            lvwAnnotations.BeginUpdate();
            foreach (Database.AnnotationsRow row in annotationsTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(row.name);
                    lvwAnnotations.Items.Add(lvi);
                }
            }
            lvwAnnotations.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvwAnnotations.EndUpdate();
        }

        /// <summary>
        /// Updates the control states depending on the current state of the management,
        /// so there are just the right controls active.
        /// </summary>
        private void UpdateControlStates()
        {
            txtAnnotationName.ReadOnly = (state == EditState.annotationSelected);
            rtfAnnotationContent.ReadOnly = (state == EditState.annotationSelected);
            butModify.Enabled = (state != EditState.input);
            butUseForNew.Enabled = (state == EditState.annotationSelected);
            butRemove.Enabled = (state == EditState.annotationSelected || state == EditState.editing);
            switch (state) 
            {
                case EditState.annotationSelected:
                    butModify.Text = LocalData.ModifyEdit;
                    break;
                case EditState.editing:
                    butModify.Text = LocalData.ModifySubmit;
                    break;
                default:
                    butModify.Text = LocalData.ModifyAdd;
                    break;
            }
        }

        /// <summary>
        /// Clears the content of each field.
        /// </summary>
        private void ClearFields()
        {
            txtAnnotationName.Text = String.Empty;
            rtfAnnotationContent.Text = String.Empty;
        }

        /// <summary>
        /// Adds the current input data as a new annotation to the annotations table and updates the
        /// global database.
        /// </summary>
        private void AddAnnotation()
        {
            // Initiate a new row and add it to the projects table
            Database.AnnotationsRow newRow = annotationsTable.NewAnnotationsRow();
            newRow.name = txtAnnotationName.Text;
            newRow.content = rtfAnnotationContent.Rtf;
            newRow.categoryId = (int)cboAnnotationCategories.SelectedValue;
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
                cm.Position = lvwAnnotations.SelectedIndices[0];

                txtAnnotationName.DataBindings.Add("Text", annotationsTable, "name");
                rtfAnnotationContent.DataBindings.Add("Rtf", annotationsTable, "content");
            }
            else if (!newSelection && dataBound)
            {
                // if a project was deselected, update the controls and unbind them from data sources
                dataBound = false;
                txtAnnotationName.DataBindings.Clear();
                rtfAnnotationContent.DataBindings.Clear();
                txtAnnotationName.Text = String.Empty;
                rtfAnnotationContent.Text = String.Empty;
            }
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Handles changing the selection of a annotation in the list and updates the data bindings.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void lvwAnnotations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwAnnotations.SelectedIndices.Count > 0)
            {
                state = EditState.annotationSelected;
                UpdateControlStates();
                UpdateDataBindings(true);
                errorProvider.SetError(txtAnnotationName, String.Empty);
                errorProvider.SetError(rtfAnnotationContent, String.Empty);
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
        private void butModify_Click(object sender, EventArgs e)
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
                annotationsTable.Rows[lvwAnnotations.SelectedIndices[0]].EndEdit();
                connection.UpdateAnnotations(annotationsTable);
                Annotations_Refresh();
                ClearFields();
                lvwAnnotations.SelectedIndices.Clear();
                UpdateDataBindings(false);
            }
        }

        /// <summary>
        /// Handles removing the currently selected row and updates the global database.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void butRemove_Click(object sender, EventArgs e)
        {
            // Delete the selected row, update data binding and update global database and controls
            state = EditState.input;
            CurrencyManager cm = this.BindingContext[annotationsTable] as CurrencyManager;
            annotationsTable.Rows[cm.Position].Delete();
            UpdateDataBindings(false);
            ClearFields();
            connection.UpdateAnnotations(annotationsTable);
            Annotations_Refresh();
            lvwAnnotations.SelectedIndices.Clear();
            this.ValidateFields();
        }

        /// <summary>
        /// Handles clicking the "New" button to start inserting a new annotation.
        /// The content of all fields is cleared.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void butNew_Click(object sender, EventArgs e)
        {
            state = EditState.input;
            ClearFields();
            lvwAnnotations.SelectedIndices.Clear();
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
        private void butUseForNew_Click(object sender, EventArgs e)
        {
            state = EditState.input;
            UpdateControlStates();
            UpdateDataBindings(false);
            lvwAnnotations.SelectedIndices.Clear();
            this.ValidateFields();
        }

        /// <summary>
        /// Opens the ManageAnnotationCategories dialog when clicking this button and refreshes the annotation categories
        /// table and controls afterwards.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void butEditAnnotationCategories_Click(object sender, EventArgs e)
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
        private void cboAnnotationCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Annotations_Refresh();
        }

        private void FrmManageAnnotations_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                this.Close();
            }
        }

        #endregion

        #region ErrorHandling
        
        /// <summary>
        /// Calls the validate method of this formular when name or content have to be validated.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void txtNameContent_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateFields();
        }

        /// <summary>
        /// Calls the validate mathod of this formular when the name or content is changed.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.ValidateFields();
        }


        private void rtfContent_TextChanged(object sender, EventArgs e)
        {
            int selection = rtfAnnotationContent.SelectionStart;
            rtfAnnotationContent.SelectAll();
            rtfAnnotationContent.SelectionColor = System.Drawing.Color.Black;
            rtfAnnotationContent.Select(selection, 0);

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

            errorProvider.SetError(txtAnnotationName, String.Empty);
            errorProvider.SetError(rtfAnnotationContent, String.Empty);

            if ((state == EditState.addable || state== EditState.input) && String.IsNullOrEmpty(txtAnnotationName.Text))
            {
                errorProvider.SetError(txtAnnotationName, LocalData.ErrorEmptyName);
                state = EditState.input;
                UpdateControlStates();
                result = false;
            }
            else if ((state == EditState.addable || state == EditState.input) && String.IsNullOrEmpty(rtfAnnotationContent.Text))
            {
                errorProvider.SetError(rtfAnnotationContent, LocalData.ErrorEmptyContent);
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
