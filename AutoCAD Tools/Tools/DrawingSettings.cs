using System;
using System.Drawing;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using AutoCADTools.Data;
using AutoCADTools.Management;
using AutoCADTools.PrintLayout;
using Database = AutoCADTools.Data.Database;
using AcDatabase = Autodesk.AutoCAD.DatabaseServices.Database;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class creates a window to edit the drawing properties of the current document.
    /// Projects and employers from the database can be loaded. The data input in this windows
    /// are stored in the current document and are used to fill the textfields of the layouts.
    /// </summary>
    public partial class DrawingSettings : Form
    {
        #region Attributes

        /// <summary>
        /// The fields saved in the drawing's settings
        /// </summary>
        private static String[] fields = {"Auftraggeber", "BV1", "BV2", "BV3", "BV4", "BVK", "Statiknummer",
                                           "Bauteil", "Plannummer", "CheckErstellungsdatum", "Erstellungsdatum", 
                                           "CheckAE1", "AE1Name", "AE1Datum", "AE1Vermerk", "CheckAE2", "AE2Name", 
                                           "AE2Datum", "AE2Vermerk", "Zeichnungseinheit"};
        
        /// <summary>
        /// Saves the marked drawing unit
        /// </summary>
        private RadioButton markedUnit;

        /// <summary>
        /// The sql connection object
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// A flag indicating if there is a server connection or not
        /// </summary>
        private bool connected;

        /// <summary>
        /// The table containing the project data
        /// </summary>
        private Database.ProjectDataTable projectsTable;

        /// <summary>
        /// The table containing the employers data
        /// </summary>
        private Database.EmployerDataTable employersTable;

        /// <summary>
        /// Indicated if closing is caused by causing child window "projects" to avoid closing this window
        /// </summary>
        private bool closeByProjects;

        /// <summary>
        /// Indicates wheter data are currently updated from database to prevent from overriding data
        /// </summary>
        private bool updating;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiates a new window to edit the drawing properties.
        /// The information added here are used in the textfields for the layouts.
        /// Projects and employers are loaded from the database.
        /// </summary>
        public DrawingSettings()
        {
            InitializeComponent();
            markedUnit = rbMeters;

            // Try to connect to database
            try
            {
                connection = new SqlConnection();
                connected = true;
                connection.FillProjects(projectsTable);
                connection.FillEmployers(employersTable);
            }
            catch (Exception)
            {
                connected = false;
                butEditProjects.Enabled = false;
            }
            closeByProjects = false;
            updating = false;

            projectsTable = new Database.ProjectDataTable();
            employersTable = new Database.EmployerDataTable();
            projectsTable.Columns.Add("Descr", typeof(String), "number + ' - ' + employer + ' - ' + descriptionShort");

            // Update the data bindings
            UpdateConnectedDate();

            // Load the data already stored for this file
            var data = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            cmbEmployers.Text = data.Employer;
            txtDescription1.Text = data.ProjectDescription1;
            txtDescription2.Text = data.ProjectDescription2;
            txtDescription3.Text = data.ProjectDescription3;
            txtDescription4.Text = data.ProjectDescription4;
            txtDescriptionShort.Text = data.ProjectDescriptionShort;
            txtProjectnumber.Text = data.ProjectNumber;
            txtSegment.Text = data.DrawingDescription;
            txtPage.Text = data.DrawingNumber;
            dtCreation.Value = data.CreationTime;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the data by calling current information from database.
        /// If there is no database connection nothing is done
        /// </summary>
        private void UpdateConnectedDate()
        {
            if (!connected) return;

            // Mark updating
            updating = true;

            // Update projects
            projectsTable.Clear();
            connection.FillProjects(projectsTable);
            
            cmbProjects.BeginUpdate();
            cmbProjects.DataSource = null;
            cmbProjects.DataSource = projectsTable;
            cmbProjects.DisplayMember = "Descr";
            cmbProjects.EndUpdate();
            cmbProjects.SelectedIndex = -1;

            // Update employers
            String saveEmployer = cmbEmployers.Text;
            employersTable.Clear();
            connection.FillEmployers(employersTable);

            cmbEmployers.BeginUpdate();
            cmbEmployers.DataSource = null;
            cmbEmployers.DataSource = employersTable;
            cmbEmployers.DisplayMember = "name";
            cmbEmployers.EndUpdate();
            cmbEmployers.Text = saveEmployer;

            // Unmark updating
            updating = false;
        }

        #endregion

        #region ButtonHandler
        
        /// <summary>
        /// If the OK-button is clicked, empty textboxes are filled with whitespace to prevent
        /// textfields showing "---" and the data are stored in the SummaryInfo of the active document.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void butOK_Click(object sender, EventArgs e)
        {
            var data = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.UserData[DrawingData.DICTIONARY_NAME] as DrawingData;
            data.CreationTime = dtCreation.Value;
            data.DrawingDescription = txtSegment.Text;
            data.DrawingNumber = txtPage.Text;
            data.Employer = cmbEmployers.Text;
            data.ProjectDescription1 = txtDescription1.Text;
            data.ProjectDescription2 = txtDescription2.Text;
            data.ProjectDescription3 = txtDescription3.Text;
            data.ProjectDescription4 = txtDescription4.Text;
            data.ProjectDescriptionShort = txtDescriptionShort.Text;
            data.ProjectNumber = txtProjectnumber.Text;

            data.SaveValues();
            
            this.Close();

            // Regenerate textfields
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.Regen();
        }

        /// <summary>
        /// Exits the dialog when pressing the Cancel-button.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Opens the manegement to edit the projects.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void butEditProjects_Click(object sender, EventArgs e)
        {
            using (FrmManageProjects management = new FrmManageProjects())
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(management);
            }

            closeByProjects = true;
            UpdateConnectedDate();
        }



        
        /// <summary>
        /// If a project is chosen in the ComboBox the data of this project are loaded
        /// and put into the fields for the drawing properties.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void cmbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProjects.SelectedIndex >= 0 && !updating)
            {
                cmbEmployers.Text = (employersTable.Rows.Find(projectsTable[cmbProjects.SelectedIndex].employer) as Database.EmployerRow).name;
                txtDescription1.Text = projectsTable[cmbProjects.SelectedIndex].description1;
                txtDescription2.Text = projectsTable[cmbProjects.SelectedIndex].description2;
                txtDescription3.Text = projectsTable[cmbProjects.SelectedIndex].description3;
                txtDescription4.Text = projectsTable[cmbProjects.SelectedIndex].description4;
                txtDescriptionShort.Text = projectsTable[cmbProjects.SelectedIndex].descriptionShort;
                txtProjectnumber.Text = projectsTable[cmbProjects.SelectedIndex].number;
            }
        }


        /// <summary>
        /// Handles the click on one of the radio buttons for the drawing unit.
        /// If the click changes the sleection a messageBox is shown which asks the user to go on,
        /// because then the current drawing frame (if existing) will be deleted.
        /// </summary>
        /// <param name="sender">the radio button being clicked</param>
        /// <param name="e">unused</param>
        private void Unit_Click(object sender, EventArgs e)
        {
            var document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var drawingAreaWrapper = document.UserData[DrawingAreaDocumentWrapper.DICTIONARY_NAME] as DrawingAreaDocumentWrapper;
            DrawingArea drawingArea = drawingAreaWrapper.DrawingArea;

            if (drawingArea != null)
            {
                if (markedUnit != sender)
                {
                    // Ask to delete the current drawing frame or flag event as handled
                    if (MessageBox.Show(LocalData.DeleteDrawingAreaText, LocalData.DeleteDrawingAreaTitle,
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Delete the current drawing frame
                        drawingArea.Delete();

                        // Set new markedUnit
                        markedUnit = (RadioButton)sender;
                    }
                    else
                    {
                        markedUnit.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Resets segment and position to standard values.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void butStandard_Click(object sender, EventArgs e)
        {
            txtSegment.Text = LocalData.StandardSegment;
            txtPage.Text = LocalData.StandardPosition;
        }

        #endregion

        #region Other Handlers
        
        /// <summary>
        /// Removes the texts if the change annotation is deactivated or selects the first person if it is activated.
        /// Also the fields are enabled.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void chkChanged1Active_CheckedChanged(object sender, EventArgs e)
        {
            dtChanged1Date.Enabled = chkChanged1Active.Checked;
            cmbChanged1Name.Enabled = chkChanged1Active.Checked;
            txtChanged1Note.Enabled = chkChanged1Active.Checked;
            if (!chkChanged1Active.Checked)
            {
                cmbChanged1Name.Text = "";
                txtChanged1Note.Text = "";
            }
            else if (cmbChanged1Name.Items.Count > 0)
            {
                cmbChanged1Name.SelectedIndex = 0;
            }
        }
        
        /// <summary>
        /// Removes the texts if the change annotation is deactivated or selects the first person if it is activated.
        /// Also the fields are enabled
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void chkChanged2Active_CheckedChanged(object sender, EventArgs e)
        {
            dtChanged2Date.Enabled = chkChanged2Active.Checked;
            txtChanged2Name.Enabled = chkChanged2Active.Checked;
            txtChanged2Note.Enabled = chkChanged2Active.Checked;
            if (!chkChanged2Active.Checked)
            {
                txtChanged2Name.Text = "";
                txtChanged2Note.Text = "";
            }
            else if (txtChanged2Name.Items.Count > 0)
            {
                txtChanged2Name.SelectedIndex = 0;
            }
        }
        
        /// <summary>
        /// Enabled the creation date field depending on the appendant check box.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCreation.Enabled = chkDate.Checked;
        }

        /// <summary>
        /// Handles closing this form. If there were the projects opened an unkown failure
        /// causes this form to close. This is interrupted.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">the event arguments of the closing event</param>
        private void DrawingSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeByProjects)
            {
                closeByProjects = false;
                e.Cancel = true;
            }
        }

        #endregion

    }

}
