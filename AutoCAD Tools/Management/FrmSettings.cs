using AutoCADTools.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoCADTools.Management
{
    /// <summary>
    /// Provides a GUI for changing basic settings like for the database connection
    /// </summary>
    public partial class FrmSettings : Form
    {
        #region Load/Unload

        /// <summary>
        /// Initializes the GUI and loads the data from the current application settings.
        /// </summary>
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            Settings currentSettings = Settings.Default;
            txtDatabasePath.Text = currentSettings.SqlConnectionPath;
            txtDatabasePort.Text = currentSettings.SqlConnectionPort;
            txtDatabaseLogin.Text = currentSettings.SqlConnectionLogin;
            txtDatabasePassword.Text = currentSettings.SqlConnectionPassword;
            txtDatabaseTimeout.Text = currentSettings.SqlConnectionTimeout;
            txtDatabaseDatabase.Text = currentSettings.SqlConnectionDatabase;

            txtLayoutname.Text = currentSettings.DefaultLayoutName;
            txtPrinterA4.Text = currentSettings.DefaultPrinterA4;
            txtPrinterA3.Text = currentSettings.DefaultPrinterA3;
            txtPrinterCustom.Text = currentSettings.DefaultPrinterCustom;

            chkDiagonalBracingDescriptionBlack.Checked = Settings.Default.DiagonalBracingDescriptionBlack;
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Saves the changes persistent to the application settings and closes the window.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void ButSave_Click(object sender, EventArgs e)
        {
            Settings currentSettings = Settings.Default;

            currentSettings.SqlConnectionPath = txtDatabasePath.Text;
            currentSettings.SqlConnectionPort = txtDatabasePort.Text;
            currentSettings.SqlConnectionLogin = txtDatabaseLogin.Text;
            currentSettings.SqlConnectionPassword = txtDatabasePassword.Text;
            currentSettings.SqlConnectionTimeout = txtDatabaseTimeout.Text;
            currentSettings.SqlConnectionDatabase = txtDatabaseDatabase.Text;

            currentSettings.DefaultLayoutName = txtLayoutname.Text;
            currentSettings.DefaultPrinterA4 = txtPrinterA4.Text;
            currentSettings.DefaultPrinterA3 = txtPrinterA3.Text;
            currentSettings.DefaultPrinterCustom = txtPrinterCustom.Text;

            currentSettings.DiagonalBracingDescriptionBlack = chkDiagonalBracingDescriptionBlack.Checked;

            currentSettings.Save();
            this.Close();
        }

        /// <summary>
        /// Closes the window without saving.
        /// </summary>
        /// <param name="sender">using</param>
        /// <param name="e">using</param>
        private void ButCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Validates the input port for being a number.
        /// If there is no number input, the error provider is actived and leaving the field is averted.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">the event arguments for validation</param>
        private void TxtPortValidate(object sender, CancelEventArgs e)
        {
            if (int.TryParse(txtDatabasePort.Text, out _))
            {
                errorProvider.SetError(txtDatabasePort, String.Empty);
            }
            else
            {
                errorProvider.SetError(txtDatabasePort, LocalData.ErrorNoNumber);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Validates the input timeout value for being a number.
        /// If there is no number input, the error provider is actived and leaving the field is averted.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">the event arguments for validation</param>
        private void TxtTimeoutValidate(object sender, CancelEventArgs e)
        {
            if (int.TryParse(txtDatabaseTimeout.Text, out _))
            {
                errorProvider.SetError(txtDatabaseTimeout, String.Empty);
            }
            else
            {
                errorProvider.SetError(txtDatabaseTimeout, LocalData.ErrorNoNumber);
                e.Cancel = true;
            }
        }

        #endregion


    }
}
