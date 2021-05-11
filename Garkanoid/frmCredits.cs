using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Garkanoid.Miscelaneous;

namespace Garkanoid
{
    public partial class frmCredits : Form
    {
        int iTick = 0;
        int iEfectStep = 0;

        string[] sTitles = cLanguaje.GetFrmCreditsTitles();
        string[] sValues = { "Juan Andres Garcia Alves de Borba",
                             "andres_garcia_ao@hotmail.com",
                             "Sistema de Procesamiento de Datos",
                             "Universidad Tecnológica Nacional (UTN) - FRBA - Argentina",
                             "Available under GNU LGPL license (Open Source)",
                             "Enjoy playing!",
                             "Press 'Esc' to go back." };
        
        public frmCredits()
        {
            InitializeComponent();
        }

        private void frmCredits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void tmrChangeItem_Tick(object sender, EventArgs e)
        {
            iTick++;

            switch (iTick)
            {
                case 1:
                    this.lblTitle.Text = sTitles[0];
                    this.lblValue.Text = sValues[0];
                    StartEfect();
                    break;
                case 7:
                    this.lblTitle.Text = sTitles[1];
                    this.lblValue.Text = sValues[1];
                    StartEfect();
                    break;
                case 13:
                    this.lblTitle.Text = sTitles[2];
                    this.lblValue.Text = sValues[2];
                    StartEfect();
                    break;
                case 19:
                    this.lblTitle.Text = sTitles[3];
                    this.lblValue.Text = sValues[3];
                    StartEfect();
                    break;
                case 25:
                    this.lblTitle.Text = sTitles[4];
                    this.lblValue.Text = sValues[4];
                    StartEfect();
                    break;
                case 31:
                    this.lblTitle.Text = sTitles[5];
                    this.lblValue.Text = sValues[5];
                    StartEfect();
                    break;
                case 37:
                    this.lblTitle.Text = sTitles[6];
                    this.lblValue.Text = sValues[6];
                    this.lblTop.Visible = false;
                    tmrChangeItem.Enabled = false;
                    break;
            }
        }

        private void tmrEfect_Tick(object sender, EventArgs e)
        {
            iEfectStep++;

            if (iEfectStep < 50) {
                int iPosX = (iEfectStep * 568 / 50) + 32;
                this.lblTop.Location = new Point(iPosX, 184);

            } else {
                this.tmrEfect.Enabled = false;
                this.lblTitle.Text = "";
                this.lblValue.Text = "";
                this.lblTop.Location = new Point(32, 184);
            }
        }

        private void StartEfect()
        {
            iEfectStep = 0;
            this.tmrEfect.Enabled = true;
        }
    }
}
