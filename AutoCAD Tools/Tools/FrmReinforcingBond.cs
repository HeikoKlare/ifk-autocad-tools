using System;
using System.Windows.Forms;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class provides windows and methods to create a new bracing.
    /// </summary>
    public partial class FrmReinforcingBond : Form
    {
        private ReinforcingBond bond;

        #region Loading

        /// <summary>
        /// Inititates a new windows to create a bracing.
        /// </summary>
        public FrmReinforcingBond()
        {
            InitializeComponent();
        }

        private void FrmReinforcingBond_Load(object sender, EventArgs e)
        {
            bond = ReinforcingBond.Instance;
            txtDistanceRidge.Text = bond.DistanceToRidge.ToString();
            txtDistanceEave.Text = bond.DistanceToEave.ToString();
            updHalfFields.Value = bond.HalfFieldCount;
            txtPosition.Text = bond.Position;
            chkDrawVerticalMembers.Checked = bond.DrawVerticalMembers;
            chkDrawChords.Checked = bond.DrawChords;
            chkDrawChords.Enabled = bond.DrawVerticalMembers;
        }

        #endregion

        #region Handler

        /// <summary>
        /// Closes the dialog on pressing the escape key.
        /// </summary>
        /// <param name="sender">the object sending invoke to start this command</param>
        /// <param name="e">the event arguments</param>
        private void FrmReinforcedBond_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private void chkDrawVerticalMembers_CheckedChanged(object sender, EventArgs e)
        {
            bond.DrawVerticalMembers = chkDrawVerticalMembers.Checked;
            txtDistanceEave.Enabled = chkDrawVerticalMembers.Checked;
            txtDistanceRidge.Enabled = chkDrawVerticalMembers.Checked;
            chkDrawChords.Enabled = chkDrawVerticalMembers.Checked;
            if (!chkDrawVerticalMembers.Checked) chkDrawChords.Checked = false;
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
            if (String.IsNullOrEmpty(txtDistanceEave.Text)) txtDistanceEave.Text = LocalData.ReinforcingBondDefaultDistance;
        }

        private void txtDistanceRidge_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDistanceRidge.Text)) txtDistanceRidge.Text = LocalData.ReinforcingBondDefaultDistance;
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

        private void updHalfFields_ValueChanged(object sender, EventArgs e)
        {
            bond.HalfFieldCount = (int)updHalfFields.Value;
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
            bond.DrawChords = chkDrawChords.Checked;
        }

        #endregion

    }
}