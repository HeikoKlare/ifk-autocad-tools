using System;
using System.Data;

using System.Windows.Forms;
using AutoCADTools.Data;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// Represents a GUI to modify, add and manage annotations.
    /// It can be navigated through categories and annotations, they can be modifed and added.
    /// </summary>
    public partial class FrmAnnotations : Form
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

        #region Load/Unload
        
        /// <summary>
        /// Initates a new GUI for managing projects and the needed database connection and data tables.
        /// </summary>
        public FrmAnnotations()
        {
            InitializeComponent();
        }

        private void FrmAnnotations_Load(object sender, EventArgs e)
        {
            dataBound = false;
            connection = new SqlConnection();

            // Initialize annotation categories and annotation table
            annotationCategoriesTable = new Database.AnnotationCategoriesDataTable();
            annotationsTable = new Database.AnnotationsDataTable();

            AnnotationCategories_Refresh();
            Annotations_Refresh();
        }

        private void FrmAnnotations_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Dispose();
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
            if (!int.TryParse(cboAnnotationCategories.SelectedValue.ToString(), out int categoryId))
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

                rtfContent.DataBindings.Add("Rtf", annotationsTable, "content");
            }
            else if (!newSelection && dataBound)
            {
                // if a project was deselected, update the controls and unbind them from data sources
                dataBound = false;
                rtfContent.DataBindings.Clear();
                rtfContent.Text = String.Empty;
            }
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Handles changing the selection of a annotation in the list and updates the data bindings.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void LvwAnnotations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwAnnotations.SelectedIndices.Count > 0)
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
        private void CboAnnotationCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Annotations_Refresh();
        }

        /// <summary>
        /// Copies the content of the currently selected annotation to the clipboard.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void ButClipboard_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(rtfContent.Rtf, TextDataFormat.Rtf);
            Utils.NativeMethods.SetWindowsToForeground(Autodesk.AutoCAD.ApplicationServices.Application.NonInPlaceMainWindow.Handle.ToInt32());
        }

        private void FrmAnnotations_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                this.Close();
            }
        }

        #endregion

        private void RrtfContent_TextChanged(object sender, EventArgs e)
        {
            rtfContent.SelectAll();
            rtfContent.SelectionColor = System.Drawing.Color.Black;
            rtfContent.Select(0, 0);
        }

    }
}
