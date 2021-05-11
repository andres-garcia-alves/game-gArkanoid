using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

using gArkanoid.Miscelaneous;

namespace gArkanoid
{
    public partial class frmMenu : Form
    {
        Music music;

        public frmMenu()
        {
            InitializeComponent();

            Languaje.Initialize();
            this.LoadLanguajeTexts();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathImages"];
                this.BackgroundImage = Image.FromFile(path + "MainBackground.png", false);

                string music = ConfigurationManager.AppSettings["music"];
                if (music == "ON") {
                    this.music = new Music();
                    this.music.Play();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmGame game = new frmGame() { Location = this.Location };
            game.ShowDialog();

            this.Visible = true;
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmHowTo howTo = new frmHowTo() { Location = this.Location };
            howTo.ShowDialog();

            this.Visible = true;
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmHighScores highScores = new frmHighScores() { Location = this.Location };
            highScores.ShowDialog();

            this.Visible = true;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmOptions options = new frmOptions() { Location = this.Location };

            DialogResult dialogResult = options.ShowDialog();

            this.Visible = true;

            if (dialogResult == DialogResult.Abort)
                Application.Restart();
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathMapEditor"];

                AppDomain appDomain = AppDomain.CreateDomain("AD");
                appDomain.ExecuteAssembly(path);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCredits_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmCredits credits = new frmCredits() { Location = this.Location };
            credits.ShowDialog();

            this.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (music != null) { music.Stop(); }
            Application.Exit();
        }

        private void LoadLanguajeTexts()
        {
            try
            {
                this.btnPlay.Text = Languaje.GetFrmMenuBtnPlay();
                this.btnHowTo.Text = Languaje.GetFrmMenuBtnHowTo();
                this.btnHighScores.Text = Languaje.GetFrmMenuBtnHighScores();
                this.btnOptions.Text = Languaje.GetFrmMenuBtnOptions();
                this.btnEditor.Text = Languaje.GetFrmMenuBtnEditor();
                this.btnCredits.Text = Languaje.GetFrmMenuBtnCredits();
                this.btnExit.Text = Languaje.GetFrmMenuBtnExits();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region HighlightSelected

        private void btnPlay_GotFocus(object sender, EventArgs e)
        {
            this.HighlightSelected();
        }

        private void btnHowToPlay_GotFocus(object sender, EventArgs e)
        {
            this.HighlightSelected();
        }

        private void btnHighScores_GotFocus(object sender, EventArgs e)
        {
            this.HighlightSelected();
        }

        private void btnOptions_GotFocus(object sender, EventArgs e)
        {
            this.HighlightSelected();
        }

        private void btnEditor_GotFocus(object sender, EventArgs e)
        {
            this.HighlightSelected();
        }

        private void btnCredits_GotFocus(object sender, EventArgs e)
        {
            this.HighlightSelected();
        }

        private void btnExit_GotFocus(object sender, EventArgs e)
        {
            this.HighlightSelected();
        }

        private void HighlightSelected()
        {
            Font fontNormal = new Font("Verdana", 8);
            Font fontHightlighted = new Font("Verdana", 12, FontStyle.Bold);

            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name.StartsWith("btn"))
                {
                    if (((Button)this.Controls[i]).Focused)
                        this.Controls[i].Font = fontHightlighted;
                    else
                        this.Controls[i].Font = fontNormal;
                }
            }
        }

        #endregion
    }
}
