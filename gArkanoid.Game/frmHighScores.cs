using System;
using System.Collections.Generic;
using System.Windows.Forms;

using gArkanoid.Miscelaneous;

namespace gArkanoid
{
    public partial class frmHighScores : Form
    {
        public frmHighScores()
        {
            InitializeComponent();
            this.LoadFormData();
        }

        private void frmHighScores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string title = Languaje.GetFrmHighScoresHighScores()[0];
            string message = Languaje.GetFrmHighScoresHighScores()[1];
            DialogResult dialogResult = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.OK)
            {
                HighScores highScores = new HighScores();
                highScores.ResetHightScores();
                this.LoadFormData();
            }
        }

        private void LoadFormData()
        {
            this.lblPlayers.Text = "";
            this.lblPoints.Text = "";

            try
            {
                HighScores highScores = new HighScores();
                List<HighScores.HighScoreItem> highScoresItems = highScores.GetHighScores();

                for (int i = 0; i < highScoresItems.Count; i++)
                {
                    this.lblPlayers.Text += highScoresItems[i].Name + "\n";
                    this.lblPoints.Text += highScoresItems[i].Points.ToString().PadLeft(5, '0') + "\n";
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
