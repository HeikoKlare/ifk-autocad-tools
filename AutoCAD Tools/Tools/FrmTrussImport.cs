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
            trussImport = Tools.TrussImport.getInstance();

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

            chkBearings.Checked = trussImport.isLayerChecked(Layer.Bearings);
            chkBracings.Checked = trussImport.isLayerChecked(Layer.Bracings);
            chkMember.Checked = trussImport.isLayerChecked(Layer.Member);
            chkDimensions.Checked = trussImport.isLayerChecked(Layer.Dimensions);
            chkPlates.Checked = trussImport.isLayerChecked(Layer.Plates);
        }

        #endregion

        #region Handle Input
        /// <summary>
        /// Opens a dialog to select the file to import the truss from.
        /// </summary>
        /// <param name="sender">the object sending invoke to execute this function</param>
        /// <param name="e">the event arguments</param>
        private void butSearchSource_Click(object sender, EventArgs e)
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
        private void butImport_Click(object sender, EventArgs e)
        {
            // Look if selected file exists, otherwise show error and return
            if (!File.Exists(trussImport.FileName))
            {
                MessageBox.Show(LocalData.TrussImportFileNotFoundText + ": " + trussImport.FileName, LocalData.TrussImportFileNotFoundTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
            
            //if (!trussImport.Import())
            //{
            //    MessageBox.Show(LocalData.TrussImportErrorText, LocalData.TrussImportErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            trussImport.FileName = txtSource.Text;
        }

        private void txtLayerPrefix_TextChanged(object sender, EventArgs e)
        {
            trussImport.LayerPrefix = txtLayerPrefix.Text;
        }

        private void butRotate_CheckedChanged(object sender, EventArgs e)
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

        private void layerChecked_CheckedChanged(object sender, EventArgs e)
        {
            trussImport.setLayerChecked(Layer.Member, chkMember.Checked);
            trussImport.setLayerChecked(Layer.Bearings, chkBearings.Checked);
            trussImport.setLayerChecked(Layer.Bracings, chkBracings.Checked);
            trussImport.setLayerChecked(Layer.Dimensions, chkDimensions.Checked);
            trussImport.setLayerChecked(Layer.Plates, chkPlates.Checked);
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
