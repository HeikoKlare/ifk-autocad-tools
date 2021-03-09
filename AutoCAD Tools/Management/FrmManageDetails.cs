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

namespace AutoCADTools.Management
{
    /// <summary>
    /// Represents a GUI to modify, add and manage annotations.
    /// It can be navigated through categories and annotations, they can be modifed and added.
    /// </summary>
    public partial class FrmManageDetails : Form
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
        public FrmManageDetails()
        {
            InitializeComponent();
        }

        private void FrmManageDetails_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection();

            // Initialize annotation categories and annotation table
            detailCategoriesTable = new Database.DetailCategoriesDataTable();
            detailsTable = new Database.DetailsDataTable();

            lvwDetails.Columns.Add(new ColumnHeader());

            DetailCategories_Refresh();
            Details_Refresh();
        }

        private void FrmManageDetails_FormClosing(object sender, FormClosingEventArgs e)
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
            if (!int.TryParse(cboDetailCategories.SelectedValue.ToString(), out int categoryId))
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

        /// <summary>
        /// Delegate for parameterless thread method
        /// </summary>
        delegate void ThreadMethod();

        /// <summary>
        /// Adds the current drawing with the input name as a new detail to the details table and updates the
        /// global database.
        /// </summary>
        private void AddDetail()
        {
            // Initiate a new row
            Database.DetailsRow newRow = detailsTable.NewDetailsRow();
            newRow.name = txtName.Text;
            newRow.categoryId = (int)cboDetailCategories.SelectedValue;

            // Get the temporary file paths
            String templateFile = Path.GetTempPath() + "Detail.dwt";
            String presentationFile = Path.GetTempPath() + "Detail.png";

            // Delete template and presentation file if existing
            File.Delete(templateFile);
            File.Delete(presentationFile);

            // Get the active document and save the template file
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            LayoutManager.Current.CurrentLayout = "Model";
            acDoc.Database.SaveAs(templateFile, DwgVersion.Current);
            Byte[] dwtBytes = null;
            Byte[] pdfBytes = null;

            // Add template file to database
            using (FileStream fs = new FileStream(templateFile, FileMode.Open))
            {
                dwtBytes = new Byte[fs.Length];
                fs.Read(dwtBytes, 0, Convert.ToInt32(fs.Length));
            }
            File.Delete(templateFile);
            newRow.templateFile = dwtBytes;

            // Try to print the presentation png
            if (!PrintPng(presentationFile)) return;

            // Add presentation file to database
            using (FileStream fs = new FileStream(presentationFile, FileMode.Open, FileAccess.Read))
            {
                pdfBytes = new Byte[fs.Length];
                fs.Read(pdfBytes, 0, Convert.ToInt32(fs.Length));
            }
            File.Delete(presentationFile);
            newRow.presentationFile = pdfBytes;

            // Add row to database and update global database
            detailsTable.AddDetailsRow(newRow);
            connection.UpdateDetailsComplete(detailsTable);

            // Refresh projects and show window again
            Details_Refresh();
            txtName.Text = "";
            this.Show();
        }

        /// <summary>
        /// Provides functionality for printing the current drawing as png to the specified file.
        /// </summary>
        /// <param name="file">the file to print to</param>
        /// <returns>true if successfully printed, false if not</returns>
        private bool PrintPng(String file)
        {
            // If there is a plot running, stop with message
            if (PlotFactory.ProcessPlotState != ProcessPlotState.NotPlotting)
            {
                // Show error if a plot is running
                MessageBox.Show(LocalData.PlotRunningText, LocalData.PlotRunningTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            this.Hide();

            using (UFProgress progress = new UFProgress())
            {
                progress.setName(LocalData.PNGPlotTitle);
                progress.setMain(LocalData.PNGPlotText);
                progress.setDescription(LocalData.PNGPlotCreateLayout);
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(progress);
                progress.setProgress(0);
                progress.Update();

                // If not in model space, switch there
                if (LayoutManager.Current.CurrentLayout != "Model") LayoutManager.Current.CurrentLayout = "Model";

                // Create the layout using the function of the UFlayout form
                progress.setProgress(5);
                progress.Update();

                if (!PrintLayout.QuickLayoutCreation.CreatePngLayout())
                {
                    progress.Hide();
                    this.Show();
                    return false;
                }


                progress.setDescription(LocalData.PNGPlotInitialize);
                progress.setProgress(20);
                progress.Update();

                // Get the active document
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                using (Transaction acTrans = acDoc.Database.TransactionManager.StartTransaction())
                {
                    BlockTableRecord btr = (BlockTableRecord)acTrans.GetObject(acDoc.Database.CurrentSpaceId, OpenMode.ForRead);
                    Layout lo = (Layout)acTrans.GetObject(btr.LayoutId, OpenMode.ForRead);

                    using (PlotInfo pi = new PlotInfo())
                    {
                        pi.Layout = btr.LayoutId;

                        using (PlotSettings ps = new PlotSettings(lo.ModelType))
                        {
                            ps.CopyFrom(lo);
                            pi.OverrideSettings = ps;
                        }

                        using (PlotInfoValidator piv = new PlotInfoValidator())
                        {
                            piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                            piv.Validate(pi);
                        }

                        using (PlotEngine pe = PlotFactory.CreatePublishEngine())
                        {
                            progress.setProgress(30);
                            progress.Update();

                            pe.BeginPlot(null, null);

                            // We'll be plotting a single document
                            pe.BeginDocument(pi, "PNG", null, 1, true, file);

                            using (PlotPageInfo ppi = new PlotPageInfo())
                            {
                                // Print the only page
                                pe.BeginPage(ppi, pi, true, null);
                                pe.BeginGenerateGraphics(null);
                                pe.EndGenerateGraphics(null);
                                pe.EndPage(null);
                            }

                            // End the plot
                            pe.EndDocument(null);
                            pe.EndPlot(null);
                        }
                    }
                }
                // Start the waiting thread and disable changing details, show the main window

                progress.setDescription(LocalData.PNGPlotWaiting);
                int progressValue = 40;
                bool accessable = false;
                while (!accessable)
                {
                    if (File.Exists(file))
                    {
                        try
                        {
                            File.OpenRead(file).Close();
                            accessable = true;
                        }
                        catch (Exception) { }
                    }
                    progressValue++;
                    progress.setProgress(progressValue);
                    progress.Update();
                    System.Threading.Thread.Sleep(500);
                }

                LayoutManager.Current.CurrentLayout = "Model";
                LayoutManager.Current.DeleteLayout("PNG");

                progress.Hide();
            }

            return true;
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
            butRemove.Enabled = lvwDetails.SelectedIndices.Count != 0;
            butEdit.Enabled = lvwDetails.SelectedIndices.Count != 0;
            if (lvwDetails.SelectedIndices.Count == 0)
            {
                picPng.Image = null;
            }
            else
            {
                Database.DetailsDataTable table = connection.GetDetail(detailsTable[lvwDetails.SelectedIndices[0]].id);
                if (table.Rows.Count != 1) return;

                System.Drawing.Image im = null;
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
        /// Adds the current input data as a new detail to the details table and updates the
        /// global database.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void butAdd_Click(object sender, EventArgs e)
        {
            // Validate the fields
            if (!this.ValidateFields()) return;

            ThreadMethod thread = AddDetail;
            this.Invoke(thread, null);
        }

        /// <summary>
        /// Handles removing the currently selected row and updates the global database.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void butRemove_Click(object sender, EventArgs e)
        {
            // Delete the selected row, update data binding and update global database and controls
            detailsTable.Rows[lvwDetails.SelectedIndices[0]].Delete();
            connection.UpdateDetails(detailsTable);
            Details_Refresh();
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
        /// Opens the dialog to edit detail categories.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void butEditCategories_Click(object sender, EventArgs e)
        {
            using (FrmManageDetailCategories management = new FrmManageDetailCategories())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }
            DetailCategories_Refresh();
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

        private void FrmanageDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            if (lvwDetails.SelectedIndices.Count > 0)
            {
                using (EditCategory editForm = new EditCategory(detailsTable, detailCategoriesTable,
                    lvwDetails.SelectedIndices[0], cboDetailCategories.SelectedIndex))
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(editForm);
                }

                connection.UpdateDetails(detailsTable);
                Details_Refresh();
            }
        }

        #endregion

        #region ErrorHandling

        /// <summary>
        /// This method validates the data in this formular, especially the input project number,
        /// which has to be not null and does not have to be already used.
        /// </summary>
        /// <returns>true if everything is okay, false otherweise</returns>
        private bool ValidateFields()
        {
            bool result = true;

            errorProvider.SetError(txtName, String.Empty);

            if (String.IsNullOrEmpty(txtName.Text))
            {
                errorProvider.SetError(txtName, LocalData.ErrorEmptyName);
                result = false;
            }

            return result;
        }

        #endregion

        #region ChangeDetail

        /// <summary>
        /// A GUI representing some elements for modifying a detail.
        /// </summary>
        private class EditCategory : Form
        {
            Database.DetailsDataTable detailsTable;
            Database.DetailCategoriesDataTable detailCategoriesTable;

            TextBox txtName;
            ComboBox cboEdit;
            Button butOk;
            Button butCancel;
            int selectedIndex;
            int selectedCategory;

            public EditCategory(Database.DetailsDataTable detailsTable,
                Database.DetailCategoriesDataTable detailCategoriesTable,
                int selectedDetail, int selectedCategory)
            {
                this.detailsTable = detailsTable;
                this.detailCategoriesTable = detailCategoriesTable;
                this.selectedIndex = selectedDetail;
                this.selectedCategory = selectedCategory;

                this.StartPosition = FormStartPosition.CenterParent;
                this.Size = new Size(300, 150);
                this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                this.AutoSize = true;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

                txtName = new TextBox();
                txtName.Size = new Size(400, 26);
                txtName.Location = new Point(12, 12);
                txtName.Text = detailsTable[selectedDetail].name;
                txtName.Margin = new Padding(3);
                this.Controls.Add(txtName);

                cboEdit = new ComboBox();
                cboEdit.FlatStyle = FlatStyle.System;
                cboEdit.DataSource = detailCategoriesTable;
                cboEdit.DisplayMember = "name";
                cboEdit.ValueMember = "id";
                cboEdit.Size = new Size(400, 28);
                cboEdit.Location = new Point(12, 44);
                cboEdit.Margin = new Padding(3);
                cboEdit.DropDownStyle = ComboBoxStyle.DropDownList;
                this.Controls.Add(cboEdit);

                butOk = new Button();
                butOk.FlatStyle = FlatStyle.System;
                butOk.Size = new Size(197, 35);
                butOk.Location = new Point(12, 78);
                butOk.Margin = new Padding(3);
                butOk.Text = LocalData.OK;
                butOk.Click += new EventHandler(EditCategory_ButOk_Click);
                this.Controls.Add(butOk);

                butCancel = new Button();
                butCancel.FlatStyle = FlatStyle.System;
                butCancel.Size = new Size(197, 35);
                butCancel.Location = new Point(215, 78);
                butCancel.Text = LocalData.Cancel;
                butCancel.Margin = new Padding(3, 3, 12, 12);
                butCancel.Click += new EventHandler(EditCategory_ButCancel_Click);
                this.Controls.Add(butCancel);

                this.CancelButton = butCancel;
                this.AcceptButton = butOk;
                this.Text = LocalData.EditDetailTitle;
                this.Load += EditCategory_Load;
            }

            private void EditCategory_Load(Object sender, EventArgs e)
            {
                cboEdit.SelectedValue = detailsTable[selectedIndex].categoryId;
            }

            private void EditCategory_ButOk_Click(Object sender, EventArgs e)
            {
                detailsTable[selectedIndex].categoryId = (int)cboEdit.SelectedValue;
                detailsTable[selectedIndex].name = txtName.Text;
                detailsTable[selectedIndex].EndEdit();
                this.Close();
            }

            private void EditCategory_ButCancel_Click(Object sender, EventArgs e)
            {
                this.Close();
            }
        }

        #endregion

    }
}
