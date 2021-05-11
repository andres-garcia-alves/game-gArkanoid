using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;

using Garkanoid.Miscelaneous;

namespace Garkanoid
{
    public partial class frmHowTo : Form
    {
        int iStep = 0;
        string sPath = "";

        Image oImage;
        Pen oPen1, oPen2, oPen3;
        Graphics oGraphics;

        public frmHowTo()
        {
            InitializeComponent();
            LoadLanguajeTexts();

            oPen1 = new Pen(Color.White, 6);
            oPen1.StartCap = LineCap.Round;
            oPen1.EndCap = LineCap.ArrowAnchor;

            oPen2 = new Pen(Color.White, 6);
            oPen2.StartCap = LineCap.Round;
            oPen2.EndCap = LineCap.ArrowAnchor;
            oPen2.DashStyle = DashStyle.Dot;

            oPen3 = new Pen(Color.White, 2);
            oPen3.StartCap = LineCap.Round;
            oPen3.EndCap = LineCap.ArrowAnchor;
            oPen3.DashStyle = DashStyle.Dot;

            sPath = ConfigurationManager.AppSettings["pathImages"];
        }

        private void frmHowTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            iStep = 0;

            this.btnBack.Visible = false;
            this.btnRepeat.Visible = false;
            this.lblLeyend.Visible = false;

            this.tmrTick.Enabled = true;
        }

        private void tmrTick_Tick(object sender, EventArgs e)
        {
            iStep++;
            DrawHowTo();
        }

        private void DrawHowTo()
        {
            oGraphics = this.CreateGraphics();
            oGraphics.SmoothingMode = SmoothingMode.HighQuality;

            switch (iStep)
            {
                case 1:
                    oImage = Image.FromFile(@sPath + @"HowTo\Screen01.png");
                    oGraphics.DrawImage(oImage, new Rectangle(0, 0, this.Width, this.Height));
                    break;
                case 3:
                    oGraphics.DrawLine(oPen1, new Point(425, 270), new Point(470, 255));
                    oGraphics.DrawString(cLanguaje.GetFrmHowToStep(1), new Font("Verdana", 10), Brushes.White, 80, 270);
                    break;
                case 8:
                    oGraphics.DrawString(cLanguaje.GetFrmHowToStep(2), new Font("Verdana", 10), Brushes.White, 80, 290);
                    break;
                case 13:
                    oImage = Image.FromFile(@sPath + @"HowTo\Screen02.png");
                    oGraphics.DrawImage(oImage, new Rectangle(0, 0, this.Width, this.Height));
                    break;
                case 15:
                    oGraphics.DrawLine(oPen1, new Point(100, 400), new Point(70, 450));
                    oGraphics.DrawString(cLanguaje.GetFrmHowToStep(3), new Font("Verdana", 10), Brushes.White, 115, 385);
                    break;
                case 20:
                    oImage = Image.FromFile(@sPath + @"HowTo\Screen03.png");
                    oGraphics.DrawImage(oImage, new Rectangle(0, 0, this.Width, this.Height));
                    break;
                case 22:
                    oGraphics.DrawLine(oPen1, new Point(580, 355), new Point(520, 415));
                    break;
                case 24:
                    oGraphics.DrawLine(oPen2, new Point(485, 415), new Point(425, 355));
                    oGraphics.DrawString(cLanguaje.GetFrmHowToStep(4), new Font("Verdana", 10), Brushes.White, 90, 350);
                    break;
                case 29:
                    oImage = Image.FromFile(@sPath + @"HowTo\Screen04.png");
                    oGraphics.DrawImage(oImage, new Rectangle(0, 0, this.Width, this.Height));
                    break;
                case 31:
                    oGraphics.DrawLine(oPen1, new Point(580, 355), new Point(520, 415));
                    break;
                case 33:
                    oGraphics.DrawLine(oPen3, new Point(493, 405), new Point(485, 335));
                    oGraphics.DrawLine(oPen3, new Point(493, 405), new Point(493, 335));
                    oGraphics.DrawLine(oPen3, new Point(493, 405), new Point(501, 335));
                    oGraphics.DrawString(cLanguaje.GetFrmHowToStep(5), new Font("Verdana", 10), Brushes.White, 90, 300);
                    break;
                case 38:
                    oImage = Image.FromFile(@sPath + @"HowTo\Screen05.png");
                    oGraphics.DrawImage(oImage, new Rectangle(0, 0, this.Width, this.Height));
                    oGraphics.DrawString(cLanguaje.GetFrmHowToStep(5), new Font("Verdana", 10), Brushes.White, 90, 300);
                    break;
                case 40:
                    oGraphics.DrawLine(oPen1, new Point(480, 355), new Point(420, 415));
                    break;
                case 42:
                    oGraphics.DrawLine(oPen3, new Point(390, 410), new Point(330, 345));
                    oGraphics.DrawLine(oPen3, new Point(390, 410), new Point(330, 355));
                    oGraphics.DrawLine(oPen3, new Point(390, 410), new Point(330, 365));
                    break;
                case 47:
                    this.tmrTick.Enabled = false;
                    this.btnBack.Visible = true;
                    this.btnRepeat.Visible = true;
                    this.lblLeyend.Visible = true;
                    break;
            }
        }

        private void LoadLanguajeTexts()
        {
            try
            {
                this.lblLeyend.Text = cLanguaje.GetFrmHowToLblLeyend();
                this.btnBack.Text = cLanguaje.GetFrmHowToBtnBack();
                this.btnRepeat.Text = cLanguaje.GetFrmHowToBtnRepeat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
