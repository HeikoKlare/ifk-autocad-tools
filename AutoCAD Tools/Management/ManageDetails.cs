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
    public partial class ManageDetails : Form
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
        public ManageDetails()
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
            newRow.name = TxtName.Text;
            newRow.categoryId = (int)CmbDetailCategories.SelectedValue;

            // Get the temporary file paths
            String templateFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) 
                + Properties.Settings.Default.TemplateFileName;
            String presentationFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + Properties.Settings.Default.PresentationFileName;
            
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
            TxtName.Text = "";
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
        private void ListDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListDetails.SelectedIndices.Count == 0)
            {
                PicPng.Image = null;
                ButRemove.Enabled = false;
            }
            else
            {
                ButRemove.Enabled = true;

                Database.DetailsDataTable table = connection.GetDetail(detailsTable[ListDetails.SelectedIndices[0]].id);
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

                PicPng.Image = im;
            }
        }

        /// <summary>
        /// Adds the current input data as a new detail to the details table and updates the
        /// global database.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButAdd_Click(object sender, EventArgs e)
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
        private void ButRemove_Click(object sender, EventArgs e)
        {
            // Delete the selected row, update data binding and update global database and controls
            CurrencyManager cm = this.BindingContext[detailsTable] as CurrencyManager;
            detailsTable.Rows[ListDetails.SelectedIndices[0]].Delete();
            connection.UpdateDetails(detailsTable);
            Details_Refresh();
            ListDetails.SelectedIndices.Clear();
            ListDetails_SelectedIndexChanged(this, null);
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
        /// Opens the dialog to edit detail categories.
        /// </summary>
        /// <param name="sender">the sender invoking this method</param>
        /// <param name="e">the event arguments</param>
        private void ButEditCategories_Click(object sender, EventArgs e)
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

        #region ErrorHandling
        
        /// <summary>
        /// This method validates the data in this formular, especially the input project number,
        /// which has to be not null and does not have to be already used.
        /// </summary>
        /// <returns>true if everything is okay, false otherweise</returns>
        private bool ValidateFields()
        {
            bool result = true;

            errorProvider.SetError(TxtName, String.Empty);

            if (String.IsNullOrEmpty(TxtName.Text))
            {
                errorProvider.SetError(TxtName, LocalData.ErrorEmptyName);
                result = false;
            }

            return result;
        }

        #endregion

        #region ChangeDetail
        
        /// <summary>
        /// Handles clicking the edit detail button in the context strip of the details list.
        /// Opens a new dialog to change the data of the selected detail.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void TsiEditCategory_Click(object sender, EventArgs e)
        {
            if (ListDetails.SelectedIndices.Count > 0)
            {
                using (EditCategory editForm = new EditCategory(detailsTable, detailCategoriesTable,
                    ListDetails.SelectedIndices[0], CmbDetailCategories.SelectedIndex))
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(editForm);
                }

                connection.UpdateDetails(detailsTable);
                Details_Refresh();
            }
        }

        /// <summary>
        /// A GUI representing some elements for modifying a detail.
        /// </summary>
        private class EditCategory : Form
        {
            Database.DetailsDataTable detailsTable;
            Database.DetailCategoriesDataTable detailCategoriesTable;

            TextBox txtName;
            ComboBox cmbEdit;
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
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

                txtName = new TextBox();
                txtName.Size = new Size(240, 20);
                txtName.Location = new Point(30, 20);
                txtName.Text = detailsTable[selectedDetail].name;
                this.Controls.Add(txtName);

                cmbEdit = new ComboBox();
                cmbEdit.DataSource = detailCategoriesTable;
                cmbEdit.DisplayMember = "name";
                cmbEdit.ValueMember = "id";
                cmbEdit.Size = new Size(240, 20);
                cmbEdit.Location = new Point(30, 55);
                cmbEdit.DropDownStyle = ComboBoxStyle.DropDownList;
                this.Controls.Add(cmbEdit);
                

                butOk = new Button();
                butOk.Size = new Size(100, 20);
                butOk.Location = new Point(30, 90);
                butOk.Text = LocalData.OK;
                butOk.Click += new EventHandler(EditCategory_ButOk_Click);
                this.Controls.Add(butOk);

                butCancel = new Button();
                butCancel.Size = new Size(100, 20);
                butCancel.Location = new Point(170, 90);
                butCancel.Text = LocalData.Cancel;
                butCancel.Click += new EventHandler(EditCategory_ButCancel_Click);
                this.Controls.Add(butCancel);

                this.Text = LocalData.EditDetailTitle;
                this.Load += EditCategory_Load;
            }

            private void EditCategory_Load(Object sender, EventArgs e)
            {
                cmbEdit.SelectedValue = detailsTable[selectedIndex].categoryId;
            }

            private void EditCategory_ButOk_Click(Object sender, EventArgs e)
            {
                detailsTable[selectedIndex].categoryId = (int)cmbEdit.SelectedValue;
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
