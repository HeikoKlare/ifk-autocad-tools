using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using AutoCADTools.Data;


namespace AutoCADTools.Management
{
    /// <summary>
    /// Represents a GUI to modify, add and manage projects.
    /// There is an overview as well as a region to add new projects provided for the user.
    /// </summary>
    public partial class ManageProjects : Form
    {
        #region Attributes
        
        /// <summary>
        /// The Sql connection doing the connection stuff to the global database.
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// The table containing the employers.
        /// </summary>
        private Database.EmployerDataTable employersTable;

        /// <summary>
        /// The table containing the projects.
        /// </summary>
        private Database.ProjectDataTable projectsTable;

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
        public ManageProjects()
        {
            InitializeComponent();

            state = EditState.input;
            dataBound = false;
            connection = new SqlConnection();

            // Initialize employer and project tables
            employersTable = new Database.EmployerDataTable();
            projectsTable = new Database.ProjectDataTable();

            // Add colums to the projects list
            ListProjects.Columns.Add(LocalData.Projectnumber, 50);
            ListProjects.Columns.Add(LocalData.Employer, 70);
            ListProjects.Columns.Add(LocalData.Description, 170);

            // Refresh (fill) employer and project tables
            Employers_Refresh();
            Projects_Refresh();
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Refreshes the employers table by getting recent updates from the global database
        /// and updates the data bindings for an up-to-date GUI presentation.
        /// Last chosen employer is restored.
        /// </summary>
        private void Employers_Refresh()
        {
            // Save employer, clear table and refill employers table
            String saveEmployer = CmbEmployer.Text;
            employersTable.Clear();
            connection.FillEmployers(employersTable);
            
            // Reset data binding of employers list
            CmbEmployer.BeginUpdate();
            CmbEmployer.DataSource = null;
            CmbEmployer.DataSource = employersTable;
            CmbEmployer.DisplayMember = "name";
            CmbEmployer.ValueMember = "id";
            CmbEmployer.EndUpdate();

            // Restore last chosen employer
            CmbEmployer.Text = saveEmployer;
        }

        /// <summary>
        /// Refreshed the projects table by getting recent updates from the global database
        /// and updates the data bindings for an up-to.date GUI presentation.
        /// </summary>
        private void Projects_Refresh()
        {
            // Save last project, clear table and refill it
            String lastProject = null;
            if (state == EditState.projectSelected || state == EditState.editing) lastProject = TxtNumber.Text;
            projectsTable.Clear();
            connection.FillProjects(projectsTable);
            
            // Refill the projects list
            ListProjects.BeginUpdate();
            ListProjects.Items.Clear();
            foreach (Database.ProjectRow row in projectsTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(row.number);
                    lvi.SubItems.Add(row.employer);
                    lvi.SubItems.Add(row.descriptionShort);
                    ListProjects.Items.Add(lvi);
                }
            }
            ListProjects.EndUpdate();
            
            // Restore last chosen project
            if (state == EditState.projectSelected || state == EditState.editing) 
                ListProjects.SelectedIndices.Add(projectsTable.Rows.IndexOf(projectsTable.Rows.Find(lastProject)));
        }

        /// <summary>
        /// Updates the control states depending on the current state of the management,
        /// so there are just the right controls active.
        /// </summary>
        private void UpdateControlStates()
        {
            TxtNumber.ReadOnly = (state == EditState.projectSelected || state == EditState.editing);
            TxtDescription1.ReadOnly = (state == EditState.projectSelected);
            TxtDescription2.ReadOnly = (state == EditState.projectSelected);
            TxtDescription3.ReadOnly = (state == EditState.projectSelected);
            TxtDescription4.ReadOnly = (state == EditState.projectSelected);
            TxtDescriptionShort.ReadOnly = (state == EditState.projectSelected);
            CmbEmployer.Enabled = (state != EditState.projectSelected);
            ButModify.Enabled = (state != EditState.input);
            ButUseForNew.Enabled = (state == EditState.projectSelected);
            ButRemove.Enabled = (state == EditState.projectSelected || state == EditState.editing);
            switch (state) 
            {
                case EditState.projectSelected:
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
            TxtNumber.Text = String.Empty;
            TxtDescription1.Text = String.Empty;
            TxtDescription2.Text = String.Empty;
            TxtDescription3.Text = String.Empty;
            TxtDescription4.Text = String.Empty;
            TxtDescriptionShort.Text = String.Empty;
            LblProjectCreatedAt.Text = String.Empty;
            CmbEmployer.SelectedIndex = 0;
        }

        /// <summary>
        /// Adds the current input data as a new project to the projects table and updates the
        /// global database.
        /// </summary>
        private void AddProject()
        {
            // Save the new projects number
            String saveNumber = TxtNumber.Text;

            // Initiate a new row and add it to the projects table
            Database.ProjectRow newRow = projectsTable.NewProjectRow();
            newRow.number = TxtNumber.Text;
            newRow.description1 = TxtDescription1.Text;
            newRow.description2 = TxtDescription2.Text;
            newRow.description3 = TxtDescription3.Text;
            newRow.description4 = TxtDescription4.Text;
            newRow.descriptionShort = TxtDescriptionShort.Text;
            newRow.employer = CmbEmployer.SelectedValue as String;
            newRow.createdAt = DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString();
            projectsTable.AddProjectRow(newRow);

            // Update the global database
            connection.UpdateProjects(projectsTable);

            // Refresh projects and select the added project
            Projects_Refresh();
            ListProjects.SelectedIndices.Clear();
            ListProjects.SelectedIndices.Add(projectsTable.Rows.IndexOf(projectsTable.Rows.Find(saveNumber)));
        }

        /// <summary>
        /// Updates the data bindings depending on the current and previous selection.
        /// </summary>
        /// <param name="newSelection">defines a new project was selected</param>
        private void UpdateDataBindings(bool newSelection)
        {
            if (newSelection && !dataBound)
            {
                // if there is a project selected, update the controls and bind them to data sources
                dataBound = true;

                CurrencyManager cm = this.BindingContext[projectsTable] as CurrencyManager;
                cm.Position = ListProjects.SelectedIndices[0];

                TxtNumber.DataBindings.Add("Text", projectsTable, "number");
                TxtDescription1.DataBindings.Add("Text", projectsTable, "description1");
                TxtDescription2.DataBindings.Add("Text", projectsTable, "description2");
                TxtDescription3.DataBindings.Add("Text", projectsTable, "description3");
                TxtDescription4.DataBindings.Add("Text", projectsTable, "description4");
                TxtDescriptionShort.DataBindings.Add("Text", projectsTable, "descriptionShort");
                CmbEmployer.DataBindings.Add("SelectedValue", projectsTable, "employer");
                LblProjectCreatedAt.DataBindings.Add("Text", projectsTable, "createdAt");
            }
            else if (!newSelection && dataBound)
            {
                // if a project was deselected, update the controls and unbind them from data sources
                dataBound = false;
                TxtNumber.DataBindings.Clear();
                TxtDescription1.DataBindings.Clear();
                TxtDescription2.DataBindings.Clear();
                TxtDescription3.DataBindings.Clear();
                TxtDescription4.DataBindings.Clear();
                TxtDescriptionShort.DataBindings.Clear();
                CmbEmployer.DataBindings.Clear();
                LblProjectCreatedAt.DataBindings.Clear();
            }
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Handles changing the selection of a project in the list and updates the data bindings.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ListProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListProjects.SelectedIndices.Count > 0)
            {
                state = EditState.projectSelected;
                UpdateControlStates();
                UpdateDataBindings(true);
                errorProvider.SetError(TxtNumber, String.Empty);
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
        /// Adds the current input data as a new project to the projects table and updates the
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
                AddProject();
                state = EditState.projectSelected;
                UpdateControlStates();
            }
            else if (state == EditState.projectSelected)
            {
                // If a project is selected and there is none being edited, start it
                state = EditState.editing;
                UpdateControlStates();
            }
            else if (state == EditState.editing)
            {
                // If a project is selected and being edited, submit its changes to local and global database
                // and refresh controls
                state = EditState.projectSelected;
                UpdateControlStates();
                projectsTable.Rows[ListProjects.SelectedIndices[0]].EndEdit();
                connection.UpdateProjects(projectsTable);
                Projects_Refresh();
            }
        }

        /// <summary>
        /// Handles removing the currently selected row and updates the global database
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButRemove_Click(object sender, EventArgs e)
        {
            // Delete the selected row, update data binding and update global database and controls
            state = EditState.input;
            projectsTable.Rows.Find(TxtNumber.Text).Delete();
            UpdateDataBindings(false);
            ClearFields();
            connection.UpdateProjects(projectsTable);
            Projects_Refresh();
            ListProjects.SelectedIndices.Clear();
            this.ValidateFields();
        }

        /// <summary>
        /// Handles clicking the "New" button to start inserting a new project.
        /// The content of all fields is cleared.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButNew_Click(object sender, EventArgs e)
        {
            state = EditState.input;
            ClearFields();
            ListProjects.SelectedIndices.Clear();
            UpdateDataBindings(false);
            UpdateControlStates();
            this.ValidateFields();
        }

        /// <summary>
        /// Handles clicking the "Use as new" button, which uses the data of the currently selected
        /// project except the projectnumber to define a new project.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButUseForNew_Click(object sender, EventArgs e)
        {
            state = EditState.input;
            UpdateControlStates();
            UpdateDataBindings(false);
            TxtNumber.Text = "";
            this.ValidateFields();
        }

        /// <summary>
        /// Opens the ManageEmployers dialog when clicking this button and refreshes the employers
        /// table and controls afterwards.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButEditEmployers_Click(object sender, EventArgs e)
        {
            using (ManageEmployers management = new ManageEmployers())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
            Employers_Refresh();
        }

        #endregion

        #region ErrorHandling
        
        /// <summary>
        /// Calls the validate method of this formular when the project number has to be validated.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void TxtNumber_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateFields();
        }

        /// <summary>
        /// Calls the validate mathod of this formular when the project number is changed.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void TxtNumber_TextChanged(object sender, EventArgs e)
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

            errorProvider.SetError(TxtNumber, String.Empty);

            if ((state == EditState.addable || state== EditState.input) && String.IsNullOrEmpty(TxtNumber.Text))
            {
                errorProvider.SetError(TxtNumber, LocalData.ErrorEmptyProjectnumber);
                state = EditState.input;
                UpdateControlStates();
                result = false;
            }
            else if ((state == EditState.addable || state== EditState.input) && projectsTable.Rows.Contains(TxtNumber.Text))
            {
                errorProvider.SetError(TxtNumber, LocalData.ErrorUsedProjectnumber);
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
        /// Is defines wheter a project is being "input", has a valid project number and so is "addable",
        /// if a there is a "projectSelected" or if the user is "editing" an alrady added project.
        /// </summary>
        private enum EditState
        {
            input,
            addable,
            projectSelected,
            editing
        }

        #endregion
    }
}
