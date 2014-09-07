using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoCADTools.Data;
using Database = AutoCADTools.Data.Database;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// Represents a GUI to modify, add and manage annotations.
    /// It can be navigated through categories and annotations, they can be modifed and added.
    /// </summary>
    public partial class FrmDetails : Form
    {
        #region Attributes

        /// <summary>
        /// The Sql connection doing the connection stuff to the global database.
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// The table containing the detail categories.
        /// </summary>
        private Database.DetailCategoriesDataTable detailCategoriesTable;
        
        /// <summary>
        /// The table containing the details.
        /// </summary>
        private Database.DetailsDataTable detailsTable;

        #endregion

        #region Load/Unload
        
        /// <summary>
        /// Initates a new GUI for managing details and the needed database connection and data tables.
        /// </summary>
        public FrmDetails()
        {
            InitializeComponent();
        }

        private void FrmDetails_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection();

            // Initialize annotation categories and annotation table
            detailCategoriesTable = new Database.DetailCategoriesDataTable();
            detailsTable = new Database.DetailsDataTable();

            DetailCategories_Refresh();
            Details_Refresh();
        }

        private void FrmDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Dispose();
        }


        #endregion

        #region Methods

        /// <summary>
        /// Refreshes the detail categories table by getting recent updates from the global database
        /// and updates the data bindings for an up-to-date GUI presentation.
        /// Last chosen category is restored.
        /// </summary>
        private void DetailCategories_Refresh()
        {
            // Save category, clear table and refill category table
            String saveCategory = cboDetailCategories.Text;
            detailCategoriesTable.Clear();
            connection.FillDetailCategories(detailCategoriesTable);

            // Reset data binding of categories list
            cboDetailCategories.BeginUpdate();
            cboDetailCategories.DataSource = null;
            cboDetailCategories.DataSource = detailCategoriesTable;
            cboDetailCategories.ValueMember = "id";
            cboDetailCategories.DisplayMember = "name";
            cboDetailCategories.EndUpdate();

            // Restore last chosen category
            cboDetailCategories.Text = saveCategory;
        }

        /// <summary>
        /// Refreshes the details table by getting recent updates from the global database
        /// and updates the data bindings for an up-to-date GUI presentation.
        /// Last chosen annotation is restored if possible.
        /// </summary>
        private void Details_Refresh()
        {
            lvwDetails.SelectedIndices.Clear();
            lvwDetails.Items.Clear();
            // If no category is selected, just clear the annotations list
            if (cboDetailCategories.SelectedIndex == -1)
            {
                return;
            }

            // Clear table and refill annotations table
            detailsTable.Clear();
            int categoryId = 0;
            if (!int.TryParse(cboDetailCategories.SelectedValue.ToString(), out categoryId))
            {
                return;
            }
            connection.FillDetails(detailsTable, categoryId);

            // Reset data binding of annotations list
            lvwDetails.BeginUpdate();
            foreach (Database.DetailsRow row in detailsTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(row.name);
                    lvwDetails.Items.Add(lvi);
                }
            }
            lvwDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvwDetails.EndUpdate();
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Handles changing the selection of a detail in the list.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void lvwDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            butOpen.Enabled = lvwDetails.SelectedIndices.Count != 0;
            if (lvwDetails.SelectedIndices.Count == 0)
            {
                picPng.Image = null;
            }
            else
            {
                Database.DetailsDataTable table = connection.GetDetail(detailsTable[lvwDetails.SelectedIndices[0]].id);
                if (table.Rows.Count != 1) return;

                System.Drawing.Image im;
                using (Stream presentationFileStream = new MemoryStream(table[0].presentationFile))
                {
                    im = System.Drawing.Image.FromStream(presentationFileStream);
                }

                if ((im.Width == 4200) || (im.Height == 4200))
                {
                    im.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else
                {
                    im.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }

                picPng.Image = im;
            }
        }

        /// <summary>
        /// Opens the selected detail.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void butOpen_Click(object sender, EventArgs e)
        {
            Database.DetailsDataTable table = connection.GetDetail(detailsTable[lvwDetails.SelectedIndices[0]].id);
            if (table.Rows.Count != 1) return;

            String file = Path.GetTempPath() + "Detail.dwt";
            File.Delete(file);
            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                fs.Write(table[0].templateFile, 0, table[0].templateFile.Length);
            }

            AutoCADTools.AcadTools.FileToOpen = file;
            this.Close();
        }

        /// <summary>
        /// Refreshes the annotations when the selected category changes.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void cboAnnotationCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Details_Refresh();
        }

        /// <summary>
        /// Rotates the image on mouse clicks.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void picPng_MouseClick(object sender, MouseEventArgs e)
        {
            if (picPng.Image != null)
            {
                System.Drawing.Image im = picPng.Image;
                if (e.Button == MouseButtons.Left)
                {
                    im.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    im.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                picPng.Image = im;
            }
        }

        private void FrmDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                this.Close();
            }
        }

        #endregion

    }
}
