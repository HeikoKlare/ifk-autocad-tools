using System;
using System.Windows.Forms;
using System.Diagnostics.Contracts;

namespace AutoCADTools.Utils
{
    /// <summary>
    /// A dialog for presenting a progress bar for an action.
    /// </summary>
    public partial class ProgressDialog : Form, IProgressMonitor
    {
        private readonly string _windowTitle;

        private double _progress;
        double IProgressMonitor.Progress
        {
            get { return _progress; }
            set {
                _progress = value < 0 ? 0 : value > 1 ? 1 : value;
                var progressInPercent = (int)(_progress * 100);
                this.Text = _windowTitle + " (" + progressInPercent + " %)";
                progressBar.Value = progressInPercent;
                Update();
            }
        }

        /// <summary>
        /// Initialises the dialog with the given window name and text.
        /// </summary>
        /// <param name="windowTitle">the title of the windows, must not be <code>null</code></param>
        /// <param name="mainText">the main text to be presented as top content of the dialog, must not be <code>null</code></param>
        public ProgressDialog(string windowTitle, string mainText)
        {
            Contract.Requires(windowTitle != null, "title of the progress dialog window must not be null");
            Contract.Requires(mainText != null, "main text of the progress dialog window must not be null");
            InitializeComponent();
            this._windowTitle = windowTitle;
            lblMain.Text = mainText;
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(this);
        }

        public void SetCurrentActionDescription(String text)
        {
            Contract.Requires(text != null);
            lblDescription.Text = text;
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
