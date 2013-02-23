using System;
using System.Windows.Forms;
using AutoCADTools.Data;


namespace AutoCADTools.Management
{
    /// <summary>
    /// A GUI representing a DataGridView to modify and manage the employers database.
    /// </summary>
    public partial class ManageEmployers : Form
    {
        #region Attributes
        
        /// <summary>
        /// The SqlConnection object doing the server connection stuff.
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// The Table containing the employers.
        /// </summary>
        private Database.EmployerDataTable employersTable;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Initiates a new GUI to manage employers.
        /// Initializes the Sql connection and fills the table.
        /// </summary>
        public ManageEmployers()
        {
            InitializeComponent();

            connection = new SqlConnection();

            // Fille the employers table
            employersTable = new Database.EmployerDataTable();
            connection.FillEmployers(employersTable);

            // Initialize the DataGridView
            DgEmployers.DataSource = employersTable;
            DgEmployers.Columns[0].HeaderText = LocalData.EmployerShort;
            DgEmployers.Columns[0].Width = 120;
            DgEmployers.Columns[1].HeaderText = LocalData.Employer;
            DgEmployers.Columns[1].Width = 260;
        }

        #endregion

        #region EventHandler
        
        /// <summary>
        /// Asks user to save changes when closing the window.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ManageEmployers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (employersTable.GetChanges() != null &&
                MessageBox.Show(LocalData.SaveChangesQuestion, LocalData.SaveChangesTitle, 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                connection.UpdateEmployers(employersTable);
            }
        }

        /// <summary>
        /// Saves changes in global database when clicking the Save-Button.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButSave_Click(object sender, EventArgs e)
        {
            connection.UpdateEmployers(employersTable);
        }

        /// <summary>
        /// Rejects the changes on the database when clicking the Reject-Button.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButDiscard_Click(object sender, EventArgs e)
        {
            employersTable.RejectChanges();
        }

        /// <summary>
        /// Updates the local database by copying the global one.
        /// Asks user to save changes first to prevent from data loss.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButUpdate_Click(object sender, EventArgs e)
        {
            if (employersTable.GetChanges() != null &&
                MessageBox.Show(LocalData.SaveChangesQuestion, LocalData.SaveChangesTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                connection.UpdateEmployers(employersTable);
            }

            employersTable.Clear();
            connection.FillEmployers(employersTable);
        }

        #endregion

        #region ErrorHandling
        
        /// <summary>
        /// Handles the data errors in the DataGridView and show a MessageBox to the user
        /// describing what he did wrong.
        /// </summary>
        /// <param name="sender">the ssender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void DgEmployers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show(LocalData.DataErrorMessage + Environment.NewLine + e.Exception.Message,
                LocalData.DataErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion

    }
}
