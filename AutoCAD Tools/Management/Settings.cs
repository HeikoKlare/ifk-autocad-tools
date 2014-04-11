using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoCADTools.Management
{
    /// <summary>
    /// Provides a GUI for changing basic settings like for the database connection
    /// </summary>
    public partial class Settings : Form
    {
        #region Constructors
        
        /// <summary>
        /// Initializes the GUI and loads the data from the current application settings.
        /// </summary>
        public Settings()
        {
            InitializeComponent();

            txtDbPath.Text = Properties.Settings.Default.SqlConnectionPath;
            txtDbPort.Text = Properties.Settings.Default.SqlConnectionPort;
            txtDbLogin.Text = Properties.Settings.Default.SqlConnectionLogin;
            txtDbPassword.Text = Properties.Settings.Default.SqlConnectionPassword;
            txtDbTimeout.Text = Properties.Settings.Default.SqlConnectionTimeout;
            txtDbDatabase.Text = Properties.Settings.Default.SqlConnectionDatabase;
            cbPanicleDescriptionBlack.Checked = Properties.Settings.Default.PanicleDescriptionBlack;
        }

        #endregion

        #region EventHandler
        
        /// <summary>
        /// Saves the changes persistent to the application settings and closes the window.
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void butSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SqlConnectionPath= txtDbPath.Text;
            Properties.Settings.Default.SqlConnectionPort = txtDbPort.Text;
            Properties.Settings.Default.SqlConnectionLogin = txtDbLogin.Text;
            Properties.Settings.Default.SqlConnectionPassword = txtDbPassword.Text;
            Properties.Settings.Default.SqlConnectionTimeout = txtDbTimeout.Text;
            Properties.Settings.Default.SqlConnectionDatabase = txtDbDatabase.Text;
            Properties.Settings.Default.PanicleDescriptionBlack = cbPanicleDescriptionBlack.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }

        /// <summary>
        /// Closes the window without saving.
        /// </summary>
        /// <param name="sender">using</param>
        /// <param name="e">using</param>
        private void butCancel_Click(object sender, EventArgs e)
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
            int dummy;
            if (int.TryParse(txtDbPort.Text, out dummy))
            {
                errorProvider.SetError(txtDbPort, "");
            }
            else
            {
                errorProvider.SetError(txtDbPort, LocalData.ErrorNoNumber);
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
            int dummy;
            if (int.TryParse(txtDbTimeout.Text, out dummy))
            {
                errorProvider.SetError(txtDbTimeout, "");
            }
            else
            {
                errorProvider.SetError(txtDbTimeout, LocalData.ErrorNoNumber);
                e.Cancel = true;
            }
        }

        #endregion


    }
}
