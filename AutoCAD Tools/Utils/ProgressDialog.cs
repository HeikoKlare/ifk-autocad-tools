using System.Windows.Forms;

namespace AutoCADTools.Utils
{
    /// <summary>
    /// A dialog for presenting a progress bar for an action.
    /// </summary>
    public partial class ProgressDialog : Form, IProgressMonitor
    {
        private double progress;
        double IProgressMonitor.Progress
        {
            get { return progress; }
            set
            {
                progress = value < 0 ? 0 : value > 1 ? 1 : value;
                progressBar.Value = ProgressInPercent();
                UpdateWindowTitle();
            }
        }

        private int ProgressInPercent()
        {
            return (int)(progress * 100);
        }

        private string windowTitle;
        string IProgressMonitor.Title
        {
            set
            {
                windowTitle = value;
                UpdateWindowTitle();
            }
        }

        private void UpdateWindowTitle()
        {
            this.Text = windowTitle + " (" + ProgressInPercent() + " %)";
            Update();
        }

        string IProgressMonitor.MainText
        {
            set
            {
                lblMain.Text = value;
                Update();

            }
        }

        string IProgressMonitor.CurrentActionDescription
        {
            set
            {
                lblDescription.Text = value;
                Update();
            }
        }

        /// <summary>
        /// Initialises the dialog.
        /// </summary>
        public ProgressDialog()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        public void Start()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(this);
            Update();
        }

        /// <summary>
        /// Prevents user from closing this window.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

    }
}
