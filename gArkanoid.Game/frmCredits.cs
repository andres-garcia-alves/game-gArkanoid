using System;
using System.Drawing;
using System.Windows.Forms;

using gArkanoid.Miscelaneous;

namespace gArkanoid
{
    public partial class frmCredits : Form
    {
        int tick = 0;
        int efectStep = 0;

        readonly string[] titles = Languaje.GetFrmCreditsTitles();
        readonly string[] values = { "Andres Garcia Alves",
                                     "andres.garcia.alves@gmail.com",
                                     "Sistema de Procesamiento de Datos",
                                     "Universidad Tecnológica Nacional (UTN) - FRBA - Argentina",
                                     "Available under GNU GPL v3.0 (Open Source)",
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
            tick++;

            switch (tick)
            {
                case 1:
                    this.lblTitle.Text = titles[0];
                    this.lblValue.Text = values[0];
                    StartEfect();
                    break;
                case 7:
                    this.lblTitle.Text = titles[1];
                    this.lblValue.Text = values[1];
                    StartEfect();
                    break;
                case 13:
                    this.lblTitle.Text = titles[2];
                    this.lblValue.Text = values[2];
                    StartEfect();
                    break;
                case 19:
                    this.lblTitle.Text = titles[3];
                    this.lblValue.Text = values[3];
                    StartEfect();
                    break;
                case 25:
                    this.lblTitle.Text = titles[4];
                    this.lblValue.Text = values[4];
                    StartEfect();
                    break;
                case 31:
                    this.lblTitle.Text = titles[5];
                    this.lblValue.Text = values[5];
                    StartEfect();
                    break;
                case 37:
                    this.lblTitle.Text = titles[6];
                    this.lblValue.Text = values[6];
                    this.lblTop.Visible = false;
                    tmrChangeItem.Enabled = false;
                    break;
            }
        }

        private void tmrEfect_Tick(object sender, EventArgs e)
        {
            this.efectStep++;

            if (this.efectStep < 50) {
                int iPosX = (this.efectStep * 568 / 50) + 32;
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
            this.efectStep = 0;
            this.tmrEfect.Enabled = true;
        }
    }
}
