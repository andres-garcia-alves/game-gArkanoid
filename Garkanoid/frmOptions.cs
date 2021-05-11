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
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
            LoadLanguajeTexts();
            LoadFormData();

            // add the events handlers at this point, to avoid handle previously events
            this.cboLanguajes.SelectedIndexChanged += new System.EventHandler(this.DisplayRestartMessage);
            this.cboLives.SelectedIndexChanged += new System.EventHandler(this.DisplayRestartMessage);
            this.chkMusic.CheckedChanged += new System.EventHandler(this.DisplayRestartMessage);
            this.radKeyboard.CheckedChanged += new System.EventHandler(this.DisplayRestartMessage);
        }

        private void frmOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveFormData();
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
                this.lblLanguaje.Text = cLanguaje.GetFrmOptionsLblLanguaje();
                this.lblLives.Text = cLanguaje.GetFrmOptionsLblLives();
                this.lblMusic.Text = cLanguaje.GetFrmOptionsLblMusic();
                this.lblInput.Text = cLanguaje.GetFrmOptionsLblInput();
                this.lblMessaje.Text = cLanguaje.GetFrmOptionsLblMenssaje();
                this.btnOK.Text = cLanguaje.GetFrmOptionsBtnOK();
                this.btnCancel.Text = cLanguaje.GetFrmOptionsBtnCancel();
                this.btnRestart.Text = cLanguaje.GetFrmOptionsBtnRestart();
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
                string sPath = ConfigurationManager.AppSettings["pathLanguajes"];

                DirectoryInfo oDirectoryInfo = new DirectoryInfo(@sPath);
                FileInfo[] arrFileInfo = oDirectoryInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);

                foreach (FileInfo o in arrFileInfo)
                    this.cboLanguajes.Items.Add(o.Name.Replace(o.Extension, ""));
                this.cboLanguajes.SelectedItem = ConfigurationManager.AppSettings["languaje"];
                this.cboLives.SelectedItem = ConfigurationManager.AppSettings["lives"];

                string sMusic = ConfigurationManager.AppSettings["music"];
                if (sMusic == "ON") this.chkMusic.Checked = true;

                string sInput = ConfigurationManager.AppSettings["input"];
                if (sInput == "Mouse") this.radMouse.Checked = true;
                else this.radKeyboard.Checked = true;
            }
            catch (Exception ex) { throw ex; }
        }

        private void SaveFormData()
        {
            try
            {
                this.btnOK.Enabled = false;

                ExeConfigurationFileMap oFileMap = new ExeConfigurationFileMap();
                oFileMap.ExeConfigFilename = "Garkanoid.exe.config";

                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(oFileMap, ConfigurationUserLevel.None);

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
