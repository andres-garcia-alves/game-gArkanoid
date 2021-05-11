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
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Garkanoid.Base;
using Garkanoid.Entities;

namespace MapEditor
{
    public partial class frmMain : Form
    {
        #region Enumerations

        enum eAxis { X, Y }
        enum eFormStatus { Normal, CreatingItem }

        #endregion

        bool bFileModified = false;
        int iBrickCount = 0;
        eFormStatus eStatus = eFormStatus.Normal;

        private static List<cBrick> lstBricks = new List<cBrick>();


        public frmMain()
        {
            InitializeComponent();

            string sPath = ConfigurationManager.AppSettings["pathImages"];
            this.pnlGame.BackgroundImage = Image.FromFile(@sPath + "GameBackground.png", false);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            #region Load combos items

            this.cboColor.Items.Add(cBrick.eColor.Black);
            this.cboColor.Items.Add(cBrick.eColor.Blue);
            this.cboColor.Items.Add(cBrick.eColor.Green);
            this.cboColor.Items.Add(cBrick.eColor.Gray);
            this.cboColor.Items.Add(cBrick.eColor.Pink);
            this.cboColor.Items.Add(cBrick.eColor.Red);
            this.cboColor.Items.Add(cBrick.eColor.White);
            this.cboColor.Items.Add(cBrick.eColor.Yellow);
            this.cboColor.SelectedIndex = 0;

            this.cboType.Items.Add(cBrick.eBrickType.Normal);
            this.cboType.Items.Add(cBrick.eBrickType.DoubleHit);
            this.cboType.Items.Add(cBrick.eBrickType.Indestructible);
            this.cboType.SelectedIndex = 0;

            this.cboReward.Items.Add(cReward.eRewardType.None);
            this.cboReward.Items.Add("------");
            this.cboReward.Items.Add(cReward.eRewardType.SlowBall);
            this.cboReward.Items.Add(cReward.eRewardType.DemolitionBall);
            this.cboReward.Items.Add(cReward.eRewardType.DoubleBall);
            this.cboReward.Items.Add(cReward.eRewardType.TripleBall);
            this.cboReward.Items.Add("------");
            this.cboReward.Items.Add(cReward.eRewardType.FirePad);
            this.cboReward.Items.Add(cReward.eRewardType.WidePad);
            this.cboReward.Items.Add("------");
            this.cboReward.Items.Add(cReward.eRewardType.WinLevel);
            this.cboReward.SelectedIndex = 0;

            #endregion

            #region gbPosition event handlers

            this.gbPosition.LostFocus += new EventHandler(gbPosition_LostFocus);
            this.txtPosX.LostFocus += new EventHandler(gbPosition_LostFocus);
            this.txtPosY.LostFocus += new EventHandler(gbPosition_LostFocus);
            this.btnSetPosition.LostFocus += new EventHandler(gbPosition_LostFocus);

            #endregion
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape && eStatus == eFormStatus.CreatingItem)
            {
                eStatus = eFormStatus.Normal;
                this.Cursor = Cursors.Default;
                this.toolStripStatusLabel.Text = "";
            }
            else if (e.KeyData == Keys.Delete && eStatus == eFormStatus.Normal)
            {
                foreach (Control oControl in this.Controls["pnlGame"].Controls)
                    if (oControl.Focused)
                        this.Controls["pnlGame"].Controls.Remove(oControl);
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                btnNewBrick_Click(null, null);
            }
        }

        private void cboReward_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboReward.SelectedItem.ToString() == "------")
                this.cboReward.SelectedIndex = 0;
        }

        #region New Brick

        private void btnNewBrick_Click(object sender, EventArgs e)
        {
            this.bFileModified = true;

            this.toolStripStatusLabel.Text = " Choose the position for the new brick. Press 'Esc' to cancel.";
            this.eStatus = eFormStatus.CreatingItem;
            this.Cursor = Cursors.No;
        }

        private void pnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.eStatus == eFormStatus.CreatingItem)
            {
                //this.eStatus = eFormStatus.Normal;
                //this.Cursor = Cursors.Default;
                //this.toolStripStatusLabel.Text = "";

                CreateNewBrick(e.Location);
            }
        }

        private void CreateNewBrick(Point oPoint)
        {
            Button oButton = new Button();
            oButton.Name = "Brick" + iBrickCount.ToString();
            oButton.Location = oPoint;
            oButton.Width = Garkanoid.Entities.cBrick.BRICK_WIDTH;
            oButton.Height = Garkanoid.Entities.cBrick.BRICK_HEIGHT;
            oButton.TabStop = false;
            oButton.FlatStyle = FlatStyle.Flat;
            oButton.FlatAppearance.BorderColor = Color.Black;
            oButton.BackColor = Color.FromName(this.cboColor.SelectedItem.ToString());
            oButton.FlatAppearance.MouseDownBackColor = Color.FromName(this.cboColor.SelectedItem.ToString());
            oButton.FlatAppearance.MouseOverBackColor = Color.FromName(this.cboColor.SelectedItem.ToString());
            oButton.Tag = new cTag((cBrick.eBrickType)this.cboType.SelectedItem, (cReward.eRewardType)this.cboReward.SelectedItem);

            oButton.GotFocus += new EventHandler(oButton_GotFocus);
            oButton.LostFocus += new EventHandler(gbPosition_LostFocus);

            this.pnlGame.Controls.Add(oButton);

            iBrickCount++;
        }

        #endregion

        #region Set Position

        private void txtPosX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnSetPosition_Click(null, null);
        }

        private void txtPosY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnSetPosition_Click(null, null);
        }

        private void btnSetPosition_Click(object sender, EventArgs e)
        {
            this.bFileModified = true;

            if (!ValidateNumeric(this.txtPosX.Text) || !ValidateNumeric(this.txtPosY.Text)) return;
            if (!ValidatePosition(this.txtPosX.Text, eAxis.X) || !ValidatePosition(this.txtPosY.Text, eAxis.Y)) return;

            Point oPoint = new Point(Convert.ToInt32(this.txtPosX.Text), Convert.ToInt32(this.txtPosY.Text));
            this.pnlGame.Controls[this.lblId.Text].Location = oPoint;
        }

        #endregion

        #region Show/Hide gbPosition

        private void oButton_GotFocus(object sender, EventArgs e)
        {
            this.lblId.Text = ((Button)sender).Name;
            this.txtPosX.Text = ((Button)sender).Location.X.ToString();
            this.txtPosY.Text = ((Button)sender).Location.Y.ToString();
            this.gbPosition.Enabled = true;
        }

        private void gbPosition_LostFocus(object sender, EventArgs e)
        {
            if (this.gbPosition.Focused == false && this.btnSetPosition.Focused == false &&
            this.txtPosX.Focused == false && this.txtPosY.Focused == false)
                this.gbPosition.Enabled = false;
        }

        #endregion

        #region Form mode cursor type (Normal/CreatingItem)

        private void pnlGame_MouseEnter(object sender, EventArgs e)
        {
            if (this.eStatus == eFormStatus.CreatingItem)
                this.Cursor = Cursors.Default;
        }

        private void pnlGame_MouseLeave(object sender, EventArgs e)
        {
            if (this.eStatus == eFormStatus.CreatingItem)
                this.Cursor = Cursors.No;
        }

        private void menuStrip_MouseEnter(object sender, EventArgs e)
        {
            if (this.eStatus == eFormStatus.CreatingItem)
                this.Cursor = Cursors.Default;
        }

        private void menuStrip_MouseLeave(object sender, EventArgs e)
        {
            if (this.eStatus == eFormStatus.CreatingItem)
                this.Cursor = Cursors.No;
        }

        #endregion

        #region Menues items

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult oDialogResult;

            if (this.bFileModified == true)
            {
                string sMsg = "Current file not saved. ¿Save now?";
                oDialogResult = MessageBox.Show(sMsg, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (oDialogResult == DialogResult.OK)
                {
                    oDialogResult = this.saveFileDialog.ShowDialog();
                    if (oDialogResult == DialogResult.OK)
                        SaveFormData();
                }
                DiscardFormData();
            }
            else { DiscardFormData(); }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult oDialogResult;
            
            // check to save current edited file
            if (this.bFileModified == true)
            {
                string sMsg = "Current file not saved. ¿Save now?";
                oDialogResult = MessageBox.Show(sMsg, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (oDialogResult == DialogResult.OK)
                {
                    oDialogResult = this.saveFileDialog.ShowDialog();
                    if (oDialogResult == DialogResult.OK)
                        SaveFormData();
                }
            }

            // file to open
            oDialogResult = this.openFileDialog.ShowDialog();
            if (oDialogResult == DialogResult.OK)
            {
                DiscardFormData();
                LoadFormData();
                DisplayFormData();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult oDialogResult;

            // check to save current edited file
            if (this.bFileModified == true)
            {
                string sMsg = "Current file not saved. ¿Save now?";
                oDialogResult = MessageBox.Show(sMsg, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (oDialogResult == DialogResult.OK)
                    SaveFormData();
            }

            DiscardFormData();
            DisplayFormData();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult oDialogResult;
            
            oDialogResult = this.saveFileDialog.ShowDialog();
            if (oDialogResult == DialogResult.OK)
            {
                SaveFormData();
                this.bFileModified = false;
            }
        }

        private void loadScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult oDialogResult;

            // check to save current edited file
            if (this.bFileModified == true)
            {
                string sMsg = "Current file not saved. ¿Save now?";
                oDialogResult = MessageBox.Show(sMsg, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (oDialogResult == DialogResult.OK)
                {
                    oDialogResult = this.saveFileDialog.ShowDialog();
                    if (oDialogResult == DialogResult.OK)
                        SaveFormData();
                }
            }

            // file to open
            openFileDialog.Filter = "Script files (*.dat)|*.dat|All files (*.*)|*.*"; 
            oDialogResult = this.openFileDialog.ShowDialog();
            if (oDialogResult == DialogResult.OK)
            {
                DiscardFormData();
                LoadScriptData();
                DisplayFormData();
            }
            openFileDialog.Filter = "Level files (*.lvl)|*.lvl|All files (*.*)|*.*";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult oDialogResult;

            if (this.bFileModified == true)
            {
                string sMsg = "Current file not saved. ¿Save now?";
                oDialogResult = MessageBox.Show(sMsg, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (oDialogResult == DialogResult.OK)
                {
                    oDialogResult = this.saveFileDialog.ShowDialog();
                    if (oDialogResult == DialogResult.OK)
                        SaveFormData();
                }
            }

            this.Close();
        }

        private void aboutMapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout oAbout = new frmAbout();
            oAbout.ShowDialog();
        }

        private void DiscardFormData()
        {
            this.bFileModified = false;
            this.iBrickCount = 0;

            lstBricks.Clear();
            this.pnlGame.Controls.Clear();
        }

        private void LoadFormData()
        {
            this.bFileModified = false;
            this.iBrickCount = 0;

            lstBricks.Clear();
            this.pnlGame.Controls.Clear();

            try
            {
                FileStream fs = new FileStream(this.openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                lstBricks = (List<cBrick>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoadScriptData()
        {
            this.bFileModified = false;
            this.iBrickCount = 0;

            lstBricks.Clear();
            this.pnlGame.Controls.Clear();

            string sLine;
            try
            {
                StreamReader sr = new StreamReader(this.openFileDialog.FileName);

                do {
                    sLine = sr.ReadLine();
                    ProcessLine(sLine);
                } while (!sr.EndOfStream);

                sr.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ProcessLine(string sLine)
        {
            if (sLine.Trim() == "") return;
            if (sLine.StartsWith("//")) return;

            cBrick oBrick;
            string[] arrData = sLine.Split(new char[] { ',' }, 6, StringSplitOptions.None);

            if (arrData[0] == "BRICK")
            {
                int iPosX = Convert.ToInt32(arrData[1]);
                int iPosY = Convert.ToInt32(arrData[2]);
                cBrick.eBrickType eType = (cBrick.eBrickType)Convert.ToInt32(arrData[4]);
                cReward.eRewardType eReward = (cReward.eRewardType)Convert.ToInt32(arrData[5]);
                cBrick.eColor eColor = (cBrick.eColor)Color.FromName(arrData[3]).ToKnownColor();

                oBrick = new cBrick(iPosX, iPosY, eType, eReward, eColor);

                lstBricks.Add(oBrick);
            }
        }

        private void DisplayFormData()
        {
            this.pnlGame.Controls.Clear();

            foreach (cBrick oBrick in lstBricks)
            {
                Button oButton = new Button();
                oButton.Name = "Brick" + iBrickCount.ToString();
                oButton.Location = oBrick.Location;
                oButton.Width = Garkanoid.Entities.cBrick.BRICK_WIDTH;
                oButton.Height = Garkanoid.Entities.cBrick.BRICK_HEIGHT;
                oButton.TabStop = false;
                oButton.FlatStyle = FlatStyle.Flat;
                oButton.FlatAppearance.BorderColor = Color.Black;
                oButton.BackColor = Color.FromName(oBrick.Color.ToString());
                oButton.FlatAppearance.MouseDownBackColor = Color.FromName(oBrick.Color.ToString());
                oButton.FlatAppearance.MouseOverBackColor = Color.FromName(oBrick.Color.ToString());
                oButton.Tag = new cTag(oBrick.BrickType, oBrick.RewardType);

                oButton.GotFocus += new EventHandler(oButton_GotFocus);
                oButton.LostFocus += new EventHandler(gbPosition_LostFocus);

                this.pnlGame.Controls.Add(oButton);
                iBrickCount++;
            }
        }

        private void SaveFormData()
        {
            cBrick oBrick;
            lstBricks.Clear();

            try
            {
                foreach (Control c in this.pnlGame.Controls)
                {
                    oBrick = new cBrick(c.Location.X, c.Location.Y, ((cTag)c.Tag).Type, ((cTag)c.Tag).Reward, (cBrick.eColor)c.BackColor.ToKnownColor());
                    lstBricks.Add(oBrick);
                }

                FileStream fs = new FileStream(this.saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, lstBricks);
                fs.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        #endregion

        #region Validations

        private bool ValidateNumeric(string sText)
        {
            Regex oRegex = new Regex("^[0-9]+$");
            return oRegex.IsMatch(sText);
        }

        private bool ValidatePosition(string sText, eAxis eAx)
        {
            int iNum = Convert.ToInt32(sText);

            if (eAx == eAxis.X)
                if (iNum < 0 || iNum > (cCollisionBase.SCREEN_WIDTH - cBrick.BRICK_WIDTH)) return false;
            else if (eAx == eAxis.Y)
                if (iNum < 0 || iNum > (cCollisionBase.SCREEN_HEIGHT - cBrick.BRICK_HEIGHT)) return false;

            return true;
        }

        #endregion
    }
}
