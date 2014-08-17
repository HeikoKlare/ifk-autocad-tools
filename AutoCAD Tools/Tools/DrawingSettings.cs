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
            String today = String.Format("{0,2:00}.{1,2:00}.{2,2:00}", DateTime.Today.Day.ToString(),
                DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());
            dtCreation.Text = DateTime.Today.ToShortDateString();
            dtChanged1Date.Text = today;
            dtChanged2Date.Text = today;
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
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            foreach (String value in fields)
            {
                System.Collections.IDictionaryEnumerator enumer = acDoc.Database.SummaryInfo.CustomProperties;
                while (enumer.MoveNext())
                {
                    if (enumer.Key.ToString() == value)
                    {
                        String val = enumer.Value.ToString().Trim();
                        switch (value)
                        {
                            case "Auftraggeber": cmbEmployers.Text = val; break;
                            case "BV1": txtDescription1.Text = val; break;
                            case "BV2": txtDescription2.Text = val; break;
                            case "BV3": txtDescription3.Text = val; break;
                            case "BV4": txtDescription4.Text = val; break;
                            case "BVK": txtDescriptionShort.Text = val; break;
                            case "Statiknummer": txtProjectnumber.Text = val; break;
                            case "Bauteil": txtSegment.Text = val; break;
                            case "Plannummer": txtPage.Text = val; break;
                            case "CheckErstellungsdatum": 
                                if (val == "0") chkDate.Checked = false; 
                                break;
                            case "Erstellungsdatum": dtCreation.Text = val; break;
                            case "CheckAE1":
                                if (val == "1") chkChanged1Active.Checked = true; 
                                break;
                            case "AE1Name": cmbChanged1Name.Text = val; break;
                            case "AE1Datum": dtChanged1Date.Text = val; break;
                            case "AE1Vermerk": txtChanged1Note.Text = val; break;
                            case "CheckAE2":
                                if (val == "1") chkChanged2Active.Checked = true;
                                break;
                            case "AE2Name": txtChanged2Name.Text = val; break;
                            case "AE2Datum": dtChanged2Date.Text = val; break;
                            case "AE2Vermerk": txtChanged2Note.Text = val; break;
                            case "Zeichnungseinheit":
                                if (val == "1")
                                {
                                    rbMillimeters.Checked = true;
                                    markedUnit = rbMillimeters;
                                }
                                else if (val == "10")
                                {
                                    rbCentimeters.Checked = true;
                                    markedUnit = rbCentimeters;
                                }
                                else
                                {
                                    rbMeters.Checked = true;
                                    markedUnit = rbMeters;
                                }
                                break;
                        }
                    }
                }
            }

            if (!chkDate.Checked) dtCreation.Enabled = false;
            if (!chkChanged1Active.Checked)
            {
                cmbChanged1Name.Enabled = false;
                dtChanged1Date.Enabled = false;
                txtChanged1Note.Enabled = false;
            }
            if (!chkChanged2Active.Checked)
            {
                txtChanged2Name.Enabled = false;
                dtChanged2Date.Enabled = false;
                txtChanged2Note.Enabled = false;
            }
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
            // If textboxes are empty fill with a whitespace so there is no "---" in the textfields
            if (String.IsNullOrEmpty(cmbEmployers.Text)) cmbEmployers.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtDescription1.Text)) txtDescription1.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtDescription2.Text)) txtDescription2.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtDescription3.Text)) txtDescription3.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtDescription4.Text)) txtDescription4.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtDescriptionShort.Text)) txtDescriptionShort.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtProjectnumber.Text)) txtProjectnumber.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtSegment.Text)) txtSegment.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtPage.Text)) txtPage.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(cmbChanged1Name.Text)) cmbChanged1Name.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtChanged1Note.Text)) txtChanged1Note.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtChanged2Name.Text)) txtChanged2Name.Text = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(txtChanged2Note.Text)) txtChanged2Note.Text = LocalData.EmptyDrawingSetting;
            
            // Save data in SummaryInfo
            DatabaseSummaryInfoBuilder dbSumBuilder = new DatabaseSummaryInfoBuilder();
            System.Collections.IDictionary prop = dbSumBuilder.CustomPropertyTable;
            prop.Add("Auftraggeber", cmbEmployers.Text);
            prop.Add("BV1", txtDescription1.Text);
            prop.Add("BV2", txtDescription2.Text);
            prop.Add("BV3", txtDescription3.Text);
            prop.Add("BV4", txtDescription4.Text);
            prop.Add("BVK", txtDescriptionShort.Text);
            if (chkDate.Checked)
            {
                prop.Add("CheckErstellungsdatum", "1");
                prop.Add("Erstellungsdatum", dtCreation.Text);
            }
            else
            {
                prop.Add("CheckErstellungsdatum", "0");
                prop.Add("Erstellungsdatum", " ");
            }
            prop.Add("Statiknummer", txtProjectnumber.Text);
            prop.Add("Bauteil", txtSegment.Text);
            prop.Add("Plannummer", txtPage.Text);
            prop.Add("AE1Name", cmbChanged1Name.Text);
            if (chkChanged1Active.Checked)
            {
                prop.Add("CheckAE1", "1");
                prop.Add("AE1Datum", dtChanged1Date.Text);
            }
            else
            {
                prop.Add("CheckAE1", "0");
                prop.Add("AE1Datum", " ");
            }
            prop.Add("AE1Vermerk", txtChanged1Note.Text);
            prop.Add("AE2Name", txtChanged2Name.Text);
            if (chkChanged2Active.Checked)
            {
                prop.Add("CheckAE2", "1");
                prop.Add("AE2Datum", dtChanged2Date.Text);
            }
            else
            {
                prop.Add("CheckAE2", "0");
                prop.Add("AE2Datum", " ");
            }
            prop.Add("AE2Vermerk", txtChanged2Note.Text);
            String unit = "1000";
            if (rbMillimeters.Checked)
            {
                unit = "1";
            }
            else if(rbCentimeters.Checked)
            {
                unit = "10";
            }
            prop.Add("Zeichnungseinheit", unit);
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.SummaryInfo 
                = dbSumBuilder.ToDatabaseSummaryInfo();
            
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
            using (ManageProjects management = new ManageProjects())
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
