using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using gArkanoid.Miscelaneous;

namespace gArkanoid
{
    public partial class frmHowTo : Form
    {
        int step = 0;
        readonly string path = "";

        private readonly Pen pen1;
        private readonly Pen pen2;
        private readonly Pen pen3;

        Image image;
        Graphics graphics;

        public frmHowTo()
        {
            InitializeComponent();
            this.LoadLanguajeTexts();

            this.pen1 = new Pen(Color.White, 6)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.ArrowAnchor
            };

            this.pen2 = new Pen(Color.White, 6)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.ArrowAnchor,
                DashStyle = DashStyle.Dot
            };

            this.pen3 = new Pen(Color.White, 2)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.ArrowAnchor,
                DashStyle = DashStyle.Dot
            };

            this.path = ConfigurationManager.AppSettings["pathImages"];
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
            step = 0;

            this.btnBack.Visible = false;
            this.btnRepeat.Visible = false;
            this.lblLeyend.Visible = false;

            this.tmrTick.Enabled = true;
        }

        private void tmrTick_Tick(object sender, EventArgs e)
        {
            step++;
            this.DrawHowTo();
        }

        private void DrawHowTo()
        {
            this.graphics = this.CreateGraphics();
            this.graphics.SmoothingMode = SmoothingMode.HighQuality;

            switch (step)
            {
                case 1:
                    this.image = Image.FromFile(path + @"HowTo\Screen01.png");
                    graphics.DrawImage(image, new Rectangle(0, 0, this.Width, this.Height));
                    break;
                case 3:
                    graphics.DrawLine(pen1, new Point(425, 270), new Point(470, 255));
                    graphics.DrawString(Languaje.GetFrmHowToStep(1), new Font("Verdana", 10), Brushes.White, 80, 270);
                    break;
                case 8:
                    graphics.DrawString(Languaje.GetFrmHowToStep(2), new Font("Verdana", 10), Brushes.White, 80, 290);
                    break;
                case 13:
                    this.image = Image.FromFile(path + @"HowTo\Screen02.png");
                    graphics.DrawImage(image, new Rectangle(0, 0, this.Width, this.Height));
                    break;
                case 15:
                    graphics.DrawLine(pen1, new Point(100, 400), new Point(70, 450));
                    graphics.DrawString(Languaje.GetFrmHowToStep(3), new Font("Verdana", 10), Brushes.White, 115, 385);
                    break;
                case 20:
                    image = Image.FromFile(path + @"HowTo\Screen03.png");
                    graphics.DrawImage(image, new Rectangle(0, 0, this.Width, this.Height));
                    break;
                case 22:
                    graphics.DrawLine(pen1, new Point(580, 355), new Point(520, 415));
                    break;
                case 24:
                    graphics.DrawLine(pen2, new Point(485, 415), new Point(425, 355));
                    graphics.DrawString(Languaje.GetFrmHowToStep(4), new Font("Verdana", 10), Brushes.White, 90, 350);
                    break;
                case 29:
                    image = Image.FromFile(path + @"HowTo\Screen04.png");
                    graphics.DrawImage(image, new Rectangle(0, 0, this.Width, this.Height));
                    break;
                case 31:
                    graphics.DrawLine(pen1, new Point(580, 355), new Point(520, 415));
                    break;
                case 33:
                    graphics.DrawLine(pen3, new Point(493, 405), new Point(485, 335));
                    graphics.DrawLine(pen3, new Point(493, 405), new Point(493, 335));
                    graphics.DrawLine(pen3, new Point(493, 405), new Point(501, 335));
                    graphics.DrawString(Languaje.GetFrmHowToStep(5), new Font("Verdana", 10), Brushes.White, 90, 300);
                    break;
                case 38:
                    this.image = Image.FromFile(path + @"HowTo\Screen05.png");
                    graphics.DrawImage(image, new Rectangle(0, 0, this.Width, this.Height));
                    graphics.DrawString(Languaje.GetFrmHowToStep(5), new Font("Verdana", 10), Brushes.White, 90, 300);
                    break;
                case 40:
                    graphics.DrawLine(pen1, new Point(480, 355), new Point(420, 415));
                    break;
                case 42:
                    graphics.DrawLine(pen3, new Point(390, 410), new Point(330, 345));
                    graphics.DrawLine(pen3, new Point(390, 410), new Point(330, 355));
                    graphics.DrawLine(pen3, new Point(390, 410), new Point(330, 365));
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
                this.lblLeyend.Text = Languaje.GetFrmHowToLblLeyend();
                this.btnBack.Text = Languaje.GetFrmHowToBtnBack();
                this.btnRepeat.Text = Languaje.GetFrmHowToBtnRepeat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
