using System;
using System.Windows.Forms;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class provides windows and methods to create a new bracing.
    /// </summary>
    public partial class ReinforcingBondUI : Form
    {
        private ReinforcingBond bond;

        #region Initialisation

        /// <summary>
        /// Inititates a new windows to create a bracing.
        /// </summary>
        public ReinforcingBondUI()
        {
            InitializeComponent();

            bond = ReinforcingBond.getInstance();
            txtDistanceRidge.Text = bond.DistanceToRidge.ToString();
            txtDistanceEave.Text = bond.DistanceToEave.ToString();
            numHalfFields.Value = bond.HalfFieldCount;
            txtPosition.Text = bond.Position;
            butDrawVerticalMembers.Checked = bond.DrawVerticalMembers;
            butDrawChords.Checked = bond.DrawChords;
            butDrawChords.Enabled = bond.DrawVerticalMembers;
        }

        #endregion

        #region Handler

        /// <summary>
        /// Closes the dialog on pressing the escape key.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void ReinforcedBondUi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void butDrawVerticalMembers_CheckedChanged(object sender, EventArgs e)
        {
            bond.DrawVerticalMembers = butDrawVerticalMembers.Checked;
            txtDistanceEave.Enabled = butDrawVerticalMembers.Checked;
            txtDistanceRidge.Enabled = butDrawVerticalMembers.Checked;
            butDrawChords.Enabled = butDrawVerticalMembers.Checked;
            if (!butDrawVerticalMembers.Checked) butDrawChords.Checked = false;
        }

        private void txtDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 8 || ((int)e.KeyChar >= 48 && (int)e.KeyChar < 58)
                || ((int)e.KeyChar == 46))
            {
                return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtDistanceEave_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDistanceEave.Text)) txtDistanceEave.Text = "0.10";
        }

        private void txtDistanceRidge_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDistanceRidge.Text)) txtDistanceRidge.Text = "0.10";
        }

        private void txtDistance_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDistanceEave.Text))
            {
                bond.DistanceToEave = double.Parse(txtDistanceEave.Text);
            }
            if (!String.IsNullOrEmpty(txtDistanceRidge.Text))
            {
                bond.DistanceToRidge = double.Parse(txtDistanceRidge.Text);
            }
        }

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {
            bond.Position = txtPosition.Text;
        }

        private void numHalfFields_ValueChanged(object sender, EventArgs e)
        {
            bond.HalfFieldCount = (int)numHalfFields.Value;
        }

        private void butDraw_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (!bond.Draw())
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void butDrawChords_CheckedChanged(object sender, EventArgs e)
        {
            bond.DrawChords = butDrawChords.Checked;
        }

        #endregion

    }
}