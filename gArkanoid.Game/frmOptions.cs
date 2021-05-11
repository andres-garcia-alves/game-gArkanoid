using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

using gArkanoid.Miscelaneous;

namespace gArkanoid
{
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
            this.LoadLanguajeTexts();
            this.LoadFormData();

            // add the events handlers at this point, to avoid handle previously events
            this.cboLanguajes.SelectedIndexChanged += new EventHandler(this.DisplayRestartMessage);
            this.cboLives.SelectedIndexChanged += new EventHandler(this.DisplayRestartMessage);
            this.chkMusic.CheckedChanged += new EventHandler(this.DisplayRestartMessage);
            this.radKeyboard.CheckedChanged += new EventHandler(this.DisplayRestartMessage);
        }

        private void frmOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveFormData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        private void DisplayRestartMessage(object sender, EventArgs e)
        {
            this.lblMessaje.Visible = true;
        }

        private void LoadLanguajeTexts()
        {
            try
            {
                this.lblLanguaje.Text = Languaje.GetFrmOptionsLblLanguaje();
                this.lblLives.Text = Languaje.GetFrmOptionsLblLives();
                this.lblMusic.Text = Languaje.GetFrmOptionsLblMusic();
                this.lblInput.Text = Languaje.GetFrmOptionsLblInput();
                this.lblMessaje.Text = Languaje.GetFrmOptionsLblMenssaje();
                this.btnOK.Text = Languaje.GetFrmOptionsBtnOK();
                this.btnCancel.Text = Languaje.GetFrmOptionsBtnCancel();
                this.btnRestart.Text = Languaje.GetFrmOptionsBtnRestart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFormData()
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathLanguajes"];

                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                FileInfo[] filesInfo = directoryInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);

                foreach (FileInfo fileInfo in filesInfo)
                    this.cboLanguajes.Items.Add(fileInfo.Name.Replace(fileInfo.Extension, ""));
                this.cboLanguajes.SelectedItem = ConfigurationManager.AppSettings["languaje"];
                this.cboLives.SelectedItem = ConfigurationManager.AppSettings["lives"];

                string music = ConfigurationManager.AppSettings["music"];
                if (music == "ON") this.chkMusic.Checked = true;

                string input = ConfigurationManager.AppSettings["input"];
                if (input == "Mouse") this.radMouse.Checked = true;
                else this.radKeyboard.Checked = true;
            }
            catch (Exception ex) { throw ex; }
        }

        private void SaveFormData()
        {
            try
            {
                this.btnOK.Enabled = false;

                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = "Garkanoid.exe.config"
                };

                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                config.AppSettings.Settings.Remove("languaje");
                config.AppSettings.Settings.Add("languaje", this.cboLanguajes.SelectedItem.ToString());
                config.AppSettings.Settings.Remove("lives");
                config.AppSettings.Settings.Add("lives", this.cboLives.SelectedItem.ToString());
                config.AppSettings.Settings.Remove("music");
                config.AppSettings.Settings.Add("music", (this.chkMusic.Checked ? "ON" : "OFF"));
                config.AppSettings.Settings.Remove("input");
                config.AppSettings.Settings.Add("input", (this.radMouse.Checked ? "Mouse" : "Keyboard"));

                config.Save(ConfigurationSaveMode.Full);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
