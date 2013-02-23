using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AutoCADTools.Data;

namespace AutoCADTools.Tools
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

            dataBound = false;
            connection = new SqlConnection();

             // Initialize annotation categories and annotation table
            annotationCategoriesTable = new Database.AnnotationCategoriesDataTable();
            annotationsTable = new Database.AnnotationsDataTable();
            ListAnnotations.Columns.Add("Name", 240);

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
            int categoryId = 0;
            int.TryParse(CmbAnnotationCategories.SelectedValue.ToString(), out categoryId);
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

                TxtContent.DataBindings.Add("Text", annotationsTable, "content");
            }
            else if (!newSelection && dataBound)
            {
                // if a project was deselected, update the controls and unbind them from data sources
                dataBound = false;
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
                butClipboard.Enabled = true;
                UpdateDataBindings(true);
            }
            else
            {
                butClipboard.Enabled = false;
                UpdateDataBindings(false);
            }
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

        /// <summary>
        /// Copies the content of the currently selected annotation to the clipboard.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void butClipboard_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetDataObject(TxtContent.Text, true);
            SetForegroundWindow(Autodesk.AutoCAD.ApplicationServices.Application.NonInPlaceMainWindow.Handle.ToInt32());
        }

        /// <summary>
        /// Closes this form.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region WinAPI SetForeground

        /// <summary>
        /// This method provided by the COM-API allows to set the window with the given handle active
        /// </summary>
        /// <param name="hWnd">the handle of the window to set active</param>
        /// <returns>something</returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int SetForegroundWindow(int hWnd);

        #endregion
    }
}
