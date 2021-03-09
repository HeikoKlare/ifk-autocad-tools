using System;
using System.Windows.Forms;
using System.IO;

using Direction = AutoCADTools.Tools.TrussImport.Direction;
using Layer = AutoCADTools.Tools.TrussImport.Layer;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class provides the graphical user interface and the functions to import a TrussCon- or MiTeK- Truss-Drawing
    /// to the currently open drawing.
    /// The rotation and the layers to import can be selected
    /// </summary>
    public partial class FrmTrussImport : Form
    {
        #region Attributes

        private Tools.TrussImport trussImport;

        #endregion

        #region Loading

        /// <summary>
        /// Initializes the graphical user interface for the TrussCon-Truss-Drawing import function
        /// </summary>
        public FrmTrussImport()
        {
            InitializeComponent();
        }

        private void FrmTrussImport_Load(object sender, EventArgs e)
        {
            trussImport = TrussImport.Instance;

            txtLayerPrefix.Text = trussImport.LayerPrefix;
            switch (trussImport.Rotation)
            {
                case Direction.LeftRotate:
                    optRotateLeft.Checked = true;
                    break;
                case Direction.Standard:
                    optRotateNo.Checked = true;
                    break;
                case Direction.RightRotate:
                    optRotateLeft.Checked = true;
                    break;
            }

            chkBearings.Checked = trussImport.IsLayerChecked(Layer.Bearings);
            chkBracings.Checked = trussImport.IsLayerChecked(Layer.Bracings);
            chkMember.Checked = trussImport.IsLayerChecked(Layer.Member);
            chkDimensions.Checked = trussImport.IsLayerChecked(Layer.Dimensions);
            chkPlates.Checked = trussImport.IsLayerChecked(Layer.Plates);
        }

        #endregion

        #region Handle Input
        /// <summary>
        /// Opens a dialog to select the file to import the truss from.
        /// </summary>
        /// <param name="sender">the object sending invoke to execute this function</param>
        /// <param name="e">the event arguments</param>
        private void ButSearchSource_Click(object sender, EventArgs e)
        {
            if (ofdSource.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = ofdSource.FileName;
                trussImport.FileName = ofdSource.FileName;
            }
        }

        /// <summary>
        /// Imports the objects on the selected layers of the specified drawing to the currently opened drawing.
        /// The user is asked to place the imported objects.
        /// </summary>
        /// <param name="sender">the object sending invoke to execute this function</param>
        /// <param name="e">the event arguments</param>
        private void ButImport_Click(object sender, EventArgs e)
        {
            // Look if selected file exists, otherwise show error and return
            if (!File.Exists(trussImport.FileName))
            {
                MessageBox.Show(LocalData.TrussImportFileNotFoundText + trussImport.FileName, LocalData.TrussImportFileNotFoundTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
            
            //if (!trussImport.Import())
            //{
            //    MessageBox.Show(LocalData.TrussImportErrorText, LocalData.TrussImportErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void TxtSource_TextChanged(object sender, EventArgs e)
        {
            trussImport.FileName = txtSource.Text;
        }

        private void TxtLayerPrefix_TextChanged(object sender, EventArgs e)
        {
            trussImport.LayerPrefix = txtLayerPrefix.Text;
        }

        private void ButRotate_CheckedChanged(object sender, EventArgs e)
        {
            if (optRotateLeft.Checked)
            {
                trussImport.Rotation = Direction.LeftRotate;
            }
            else if (optRotateRight.Checked)
            {
                trussImport.Rotation = Direction.RightRotate;
            }
            else
            {
                trussImport.Rotation = Direction.Standard;
            }
        }

        private void LayerChecked_CheckedChanged(object sender, EventArgs e)
        {
            trussImport.SetLayerChecked(Layer.Member, chkMember.Checked);
            trussImport.SetLayerChecked(Layer.Bearings, chkBearings.Checked);
            trussImport.SetLayerChecked(Layer.Bracings, chkBracings.Checked);
            trussImport.SetLayerChecked(Layer.Dimensions, chkDimensions.Checked);
            trussImport.SetLayerChecked(Layer.Plates, chkPlates.Checked);
        }


        /// <summary>
        /// Handles the escape and enter keypress to exit the window or start drawing Rispen.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void FrmTrussImport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #endregion

    }

}
