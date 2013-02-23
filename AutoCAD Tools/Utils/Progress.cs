using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCADTools.Utils
{
    /// <summary>
    /// A class representing a progress bar especially for creating a detail.
    /// </summary>
    public partial class UFProgress : Form
    {
        /// <summary>
        /// A progress bar especially for creating a detail.
        /// </summary>
        public UFProgress()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Sets the progress to the given value from 0 to 100.
        /// If the value is higher or lower than allowed it is set to minumum or maximum.
        /// </summary>
        /// <param name="progress">the progress value between 0 and 100</param>
        public void setProgress(int progress)
        {
            if (progress < 0) progress = 0;
            if (progress > 100) progress = 100;
            this.progressBar1.Value = progress;
            this.Update();
        }


        /// <summary>
        /// Sets the main text of the progress window.
        /// </summary>
        /// <param name="text">the text to set</param>
        public void setMain(String text)
        {
            if (text != null)
            {
                lblMain.Text = text;
            }
        }


        /// <summary>
        /// Sets the description text of the progress window.
        /// </summary>
        /// <param name="text">the text to set</param>
        public void setDescription(String text)
        {
            if (text != null)
            {
                lblDescription.Text = text;
            }
        }


        /// <summary>
        /// Sets the name of the progress window
        /// </summary>
        /// <param name="text">the text to set</param>
        public void setName(String text)
        {
            if (text != null)
            {
                this.Text = text;
            }
        }


        /// <summary>
        /// Provides user from closing this window.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void UFProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

    }
}
