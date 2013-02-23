using System;
using System.Windows.Forms;
using AutoCADTools.Data;


namespace AutoCADTools.Management
{
    /// <summary>
    /// Defines a GUI for manging annotation categories.
    /// </summary>
    public partial class ManageAnnotationCategories : Form
    {
        #region Attributes
        
        /// <summary>
        /// The SqlConnection object doing the server connection stuff.
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// The table containing the annotation categories.
        /// </summary>
        private Database.AnnotationCategoriesDataTable annotationCategoriesTable;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Initiates a new GUI to manage annotation categories.
        /// Initializes the Sql connection and fills the table.
        /// </summary>
        public ManageAnnotationCategories()
        {
            InitializeComponent();

            connection = new SqlConnection();
          
            // Fille the annotation categories table
            annotationCategoriesTable = new Database.AnnotationCategoriesDataTable();
            connection.FillAnnotationCategories(annotationCategoriesTable);

            // Initialize the DataGridView
            DgAnnotationCategories.DataSource = annotationCategoriesTable;
            DgAnnotationCategories.Columns[0].Visible = false;
            DgAnnotationCategories.Columns[1].HeaderText = LocalData.Category;
            DgAnnotationCategories.Columns[1].Width = 400;
        }

        #endregion

        #region EventHandler
        
        /// <summary>
        /// Asks user to save changes when closing the window.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ManageAnnotationCategories_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (annotationCategoriesTable.GetChanges() != null &&
                MessageBox.Show(LocalData.SaveChangesQuestion, LocalData.SaveChangesTitle, 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                connection.UpdateAnnotationCategories(annotationCategoriesTable);
            }
        }

        /// <summary>
        /// Saves changes in global database when clicking the Save-Button.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButSave_Click(object sender, EventArgs e)
        {
            connection.UpdateAnnotationCategories(annotationCategoriesTable);
        }

        /// <summary>
        /// Rejects the changes on the database when clicking the Reject-Button.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButDiscard_Click(object sender, EventArgs e)
        {
            annotationCategoriesTable.RejectChanges();
        }

        /// <summary>
        /// Updates the local database by copying the global one.
        /// Asks user to save changes first to prevent from data loss.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButUpdate_Click(object sender, EventArgs e)
        {
            if (annotationCategoriesTable.GetChanges() != null &&
                MessageBox.Show(LocalData.SaveChangesQuestion, LocalData.SaveChangesTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                connection.UpdateAnnotationCategories(annotationCategoriesTable);
            }

            annotationCategoriesTable.Clear();
            connection.FillAnnotationCategories(annotationCategoriesTable);
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
