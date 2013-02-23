using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoCADTools.Data;
using AutoCADTools.Utils;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.PlottingServices;
using Database = AutoCADTools.Data.Database;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// Represents a GUI to modify, add and manage annotations.
    /// It can be navigated through categories and annotations, they can be modifed and added.
    /// </summary>
    public partial class Details : Form
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

        #region Constructors
        
        /// <summary>
        /// Initates a new GUI for managing details and the needed database connection and data tables.
        /// </summary>
        public Details()
        {
            InitializeComponent();

            connection = new SqlConnection();

             // Initialize annotation categories and annotation table
            detailCategoriesTable = new Database.DetailCategoriesDataTable();
            detailsTable = new Database.DetailsDataTable();
            ListDetails.Columns.Add(LocalData.Name, 280);

            DetailCategories_Refresh();
            Details_Refresh();
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
            String saveCategory = CmbDetailCategories.Text;
            detailCategoriesTable.Clear();
            connection.FillDetailCategories(detailCategoriesTable);

            // Reset data binding of categories list
            CmbDetailCategories.BeginUpdate();
            CmbDetailCategories.DataSource = null;
            CmbDetailCategories.DataSource = detailCategoriesTable;
            CmbDetailCategories.ValueMember = "id";
            CmbDetailCategories.DisplayMember = "name";
            CmbDetailCategories.EndUpdate();

            // Restore last chosen category
            CmbDetailCategories.Text = saveCategory;
        }

        /// <summary>
        /// Refreshes the details table by getting recent updates from the global database
        /// and updates the data bindings for an up-to-date GUI presentation.
        /// Last chosen annotation is restored if possible.
        /// </summary>
        private void Details_Refresh()
        {
            // If no category is selected, just clear the annotations list
            if (CmbDetailCategories.SelectedIndex == -1)
            {
                ListDetails.Items.Clear();
                return;
            }

            // Clear table and refill annotations table
            detailsTable.Clear();
            int categoryId = int.Parse(CmbDetailCategories.SelectedValue.ToString());
            connection.FillDetails(detailsTable, categoryId);

            // Reset data binding of annotations list
            ListDetails.BeginUpdate();
            ListDetails.Clear();
            foreach (Database.DetailsRow row in detailsTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(row.name);
                    ListDetails.Items.Add(lvi);
                }
            }
            ListDetails.EndUpdate();
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Handles changing the selection of a detail in the list.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ListDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListDetails.SelectedIndices.Count == 0)
            {
                PicPng.Image = null;
                ButOpen.Enabled = false;
            }
            else
            {
                ButOpen.Enabled = true;

                Database.DetailsDataTable table = connection.GetDetail(detailsTable[ListDetails.SelectedIndices[0]].id);
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

                PicPng.Image = im;
            }
        }

        /// <summary>
        /// Opens the selected detail.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButOpen_Click(object sender, EventArgs e)
        {
            Database.DetailsDataTable table = connection.GetDetail(detailsTable[ListDetails.SelectedIndices[0]].id);
            if (table.Rows.Count != 1) return;

            String file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + Properties.Settings.Default.TemplateFileName;
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
        private void CmbAnnotationCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Details_Refresh();
        }

        /// <summary>
        /// Rotates the image on mouse clicks.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void PicPng_MouseClick(object sender, MouseEventArgs e)
        {
            if (PicPng.Image != null)
            {
                System.Drawing.Image im = PicPng.Image;
                if (e.Button == MouseButtons.Left)
                {
                    im.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    im.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                PicPng.Image = im;
            }
        }

        #endregion

    }
}
