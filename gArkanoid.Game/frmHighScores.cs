using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

using Garkanoid.Miscelaneous;

namespace Garkanoid
{
    public partial class frmHighScores : Form
    {
        public frmHighScores()
        {
            InitializeComponent();
            LoadFormData();
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
            string sTitle = cLanguaje.GetFrmHighScoresHighScores()[0];
            string sMsg = cLanguaje.GetFrmHighScoresHighScores()[1];
            DialogResult oDialogResult = MessageBox.Show(sMsg, sTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (oDialogResult == DialogResult.OK)
            {
                cHighScores oHighScores = new cHighScores();
                oHighScores.ResetHightScores();
                LoadFormData();
            }
        }

        private void LoadFormData()
        {
            this.lblPlayers.Text = "";
            this.lblPoints.Text = "";
            List<cHighScores.cHighScoreItem> lstHighScores;

            try
            {
                cHighScores oHighScores = new cHighScores();
                lstHighScores = oHighScores.GetHighScores();

                for (int i = 0; i < lstHighScores.Count; i++)
                {
                    this.lblPlayers.Text += lstHighScores[i].Name + "\n";
                    this.lblPoints.Text += lstHighScores[i].Points.ToString().PadLeft(5, '0') + "\n";
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
