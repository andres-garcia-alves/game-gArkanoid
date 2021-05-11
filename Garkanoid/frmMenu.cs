/* 
 *  Created by:
 *      Juan Andrés Garcia Alves de Borba
 *  
 *  Date & Version:
 *      Feb-2010, version 1.1
 * 
 *  Contact:
 *      andres_garcia_ao@hotmail.com
 *      andres.garcia.ao@gmail.com
 *  
 *  Curse:
 *      Sistemas de Procesamiento de Datos
 *      Tecnicatura Superior en Programación
 *      Universidad Tecnológica Nacional (UTN) FRBA - Argentina
 *
 *  Licensing:
 *      This software is Open Source and are available under de GNU LGPL license.
 *      You can found a copy of the license at http://www.gnu.org/copyleft/lesser.html
 *  
 *  Enjoy playing!
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using Garkanoid.Entities;
using Garkanoid.Miscelaneous;

namespace Garkanoid
{
    public partial class frmMenu : Form
    {
        cMusic oMusic;

        public frmMenu()
        {
            InitializeComponent();

            cLanguaje.Initialize();
            LoadLanguajeTexts();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            try
            {
                string sPath = ConfigurationManager.AppSettings["pathImages"];
                this.BackgroundImage = Image.FromFile(sPath + "MainBackground.png", false);

                string sMusic = ConfigurationManager.AppSettings["music"];
                if (sMusic == "ON") {
                    oMusic = new cMusic();
                    oMusic.Play();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmGame oGame = new frmGame();
            oGame.Location = this.Location;
            oGame.ShowDialog();

            this.Visible = true;
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmHowTo oHowTo = new frmHowTo();
            oHowTo.Location = this.Location;
            oHowTo.ShowDialog();

            this.Visible = true;
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmHighScores oHighScores = new frmHighScores();
            oHighScores.Location = this.Location;
            oHighScores.ShowDialog();

            this.Visible = true;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmOptions oOptions = new frmOptions();
            oOptions.Location = this.Location;

            DialogResult oDR = oOptions.ShowDialog();

            this.Visible = true;

            if (oDR == DialogResult.Abort)
                Application.Restart();
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            try
            {
                string sPath = ConfigurationManager.AppSettings["pathMapEditor"];

                AppDomain oAppDomain = System.AppDomain.CreateDomain("AD");
                oAppDomain.ExecuteAssembly(sPath);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCredits_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmCredits oCredits = new frmCredits();
            oCredits.Location = this.Location;
            oCredits.ShowDialog();

            this.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (oMusic != null) oMusic.Stop();
            Application.Exit();
        }

        private void LoadLanguajeTexts()
        {
            try
            {
                this.btnPlay.Text = cLanguaje.GetFrmMenuBtnPlay();
                this.btnHowTo.Text = cLanguaje.GetFrmMenuBtnHowTo();
                this.btnHighScores.Text = cLanguaje.GetFrmMenuBtnHighScores();
                this.btnOptions.Text = cLanguaje.GetFrmMenuBtnOptions();
                this.btnEditor.Text = cLanguaje.GetFrmMenuBtnEditor();
                this.btnCredits.Text = cLanguaje.GetFrmMenuBtnCredits();
                this.btnExit.Text = cLanguaje.GetFrmMenuBtnExits();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region HighlightSelected
        private void btnPlay_GotFocus(object sender, EventArgs e)
        {
            HighlightSelected();
        }

        private void btnHowToPlay_GotFocus(object sender, EventArgs e)
        {
            HighlightSelected();
        }

        private void btnHighScores_GotFocus(object sender, EventArgs e)
        {
            HighlightSelected();
        }

        private void btnOptions_GotFocus(object sender, EventArgs e)
        {
            HighlightSelected();
        }

        private void btnEditor_GotFocus(object sender, EventArgs e)
        {
            HighlightSelected();
        }

        private void btnCredits_GotFocus(object sender, EventArgs e)
        {
            HighlightSelected();
        }

        private void btnExit_GotFocus(object sender, EventArgs e)
        {
            HighlightSelected();
        }

        private void HighlightSelected()
        {
            Font oFontNormal = new Font("Verdana", 8);
            Font oFontHightlighted = new Font("Verdana", 12, FontStyle.Bold);

            for (int i = 0; i < this.Controls.Count; i++)
            {
                //if (this.Controls[i].GetType() == Type.GetType("System.Windows.Forms.Button"))
                if (this.Controls[i].Name.StartsWith("btn"))
                {
                    if (((System.Windows.Forms.Button)this.Controls[i]).Focused)
                        this.Controls[i].Font = oFontHightlighted;
                    else
                        this.Controls[i].Font = oFontNormal;
                }
            }
        }
        #endregion
    }
}
