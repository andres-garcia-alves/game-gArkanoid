using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using gArkanoid.Base;
using gArkanoid.Entities;

namespace MapEditor
{
    public partial class frmMain : Form
    {
        #region Enumerations

        enum eAxis { X, Y }
        enum eFormStatus { Normal, CreatingItem }

        #endregion

        bool isFileModified = false;
        int brickCount = 0;
        eFormStatus eStatus = eFormStatus.Normal;

        private static List<Brick> bricks = new List<Brick>();

        public frmMain()
        {
            InitializeComponent();

            this.Text = "gArkanoid MapEditor v" + Application.ProductVersion;

            string path = ConfigurationManager.AppSettings["pathImages"];
            this.pnlGame.BackgroundImage = Image.FromFile(path + "GameBackground.png", false);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            #region Load combos items

            this.cboColor.Items.Add(Brick.eColor.Black);
            this.cboColor.Items.Add(Brick.eColor.Blue);
            this.cboColor.Items.Add(Brick.eColor.Green);
            this.cboColor.Items.Add(Brick.eColor.Gray);
            this.cboColor.Items.Add(Brick.eColor.Pink);
            this.cboColor.Items.Add(Brick.eColor.Red);
            this.cboColor.Items.Add(Brick.eColor.White);
            this.cboColor.Items.Add(Brick.eColor.Yellow);
            this.cboColor.SelectedIndex = 0;

            this.cboType.Items.Add(Brick.eBrickType.Normal);
            this.cboType.Items.Add(Brick.eBrickType.DoubleHit);
            this.cboType.Items.Add(Brick.eBrickType.Indestructible);
            this.cboType.SelectedIndex = 0;

            this.cboReward.Items.Add(Reward.eRewardType.None);
            this.cboReward.Items.Add("------");
            this.cboReward.Items.Add(Reward.eRewardType.SlowBall);
            this.cboReward.Items.Add(Reward.eRewardType.DemolitionBall);
            this.cboReward.Items.Add(Reward.eRewardType.DoubleBall);
            this.cboReward.Items.Add(Reward.eRewardType.TripleBall);
            this.cboReward.Items.Add("------");
            this.cboReward.Items.Add(Reward.eRewardType.FirePad);
            this.cboReward.Items.Add(Reward.eRewardType.WidePad);
            this.cboReward.Items.Add("------");
            this.cboReward.Items.Add(Reward.eRewardType.WinLevel);
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
                this.eStatus = eFormStatus.Normal;
                this.Cursor = Cursors.Default;
                this.toolStripStatusLabel.Text = "";
            }
            else if (e.KeyData == Keys.Delete && eStatus == eFormStatus.Normal)
            {
                foreach (Control control in this.Controls["pnlGame"].Controls)
                    if (control.Focused)
                        this.Controls["pnlGame"].Controls.Remove(control);
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
            this.isFileModified = true;

            this.toolStripStatusLabel.Text = " Choose the position for the new brick. Press 'Esc' to cancel.";
            this.eStatus = eFormStatus.CreatingItem;
            this.Cursor = Cursors.No;
        }

        private void pnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.eStatus == eFormStatus.CreatingItem)
            {
                this.CreateNewBrick(e.Location);
            }
        }

        private void CreateNewBrick(Point oPoint)
        {
            Button button = new Button()
            {
                Name = "Brick" + brickCount.ToString(),
                Location = oPoint,
                Width = Brick.BRICK_WIDTH,
                Height = Brick.BRICK_HEIGHT,
                TabStop = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromName(this.cboColor.SelectedItem.ToString()),
                Tag = new Tag((Brick.eBrickType)this.cboType.SelectedItem, (Reward.eRewardType)this.cboReward.SelectedItem)
            };
            button.FlatAppearance.BorderColor = Color.Black;
            button.FlatAppearance.MouseDownBackColor = Color.FromName(this.cboColor.SelectedItem.ToString());
            button.FlatAppearance.MouseOverBackColor = Color.FromName(this.cboColor.SelectedItem.ToString());

            button.GotFocus += new EventHandler(button_GotFocus);
            button.LostFocus += new EventHandler(gbPosition_LostFocus);

            this.pnlGame.Controls.Add(button);

            brickCount++;
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
            this.isFileModified = true;

            if (!this.ValidateNumeric(this.txtPosX.Text) || !this.ValidateNumeric(this.txtPosY.Text)) return;
            if (!this.ValidatePosition(this.txtPosX.Text, eAxis.X) || !this.ValidatePosition(this.txtPosY.Text, eAxis.Y)) return;

            Point point = new Point(Convert.ToInt32(this.txtPosX.Text), Convert.ToInt32(this.txtPosY.Text));
            this.pnlGame.Controls[this.lblId.Text].Location = point;
        }

        #endregion

        #region Show/Hide gbPosition

        private void button_GotFocus(object sender, EventArgs e)
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
            if (this.isFileModified == true)
            {
                string message = "Current file not saved. ¿Save now?";

                DialogResult dialogResult = MessageBox.Show(message, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    dialogResult = this.saveFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                        SaveFormData();
                }

                this.DiscardFormData();
            }
            else { this.DiscardFormData(); }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            
            // check to save current edited file
            if (this.isFileModified == true)
            {
                string message = "Current file not saved. ¿Save now?";

                dialogResult = MessageBox.Show(message, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    dialogResult = this.saveFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                        this.SaveFormData();
                }
            }

            // file to open
            dialogResult = this.openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.DiscardFormData();
                this.LoadFormData(this.openFileDialog.FileName);
                this.DisplayFormData();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // check to save current edited file
            if (this.isFileModified == true)
            {
                string message = "Current file not saved. ¿Save now?";

                DialogResult dialogResult = MessageBox.Show(message, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                    this.SaveFormData();
            }

            this.DiscardFormData();
            this.DisplayFormData();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = this.saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.SaveFormData();
                this.isFileModified = false;
            }
        }

        private void loadScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            // check to save current edited file
            if (this.isFileModified == true)
            {
                string message = "Current file not saved. ¿Save now?";

                dialogResult = MessageBox.Show(message, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    dialogResult = this.saveFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                        this.SaveFormData();
                }
            }

            // file to open
            openFileDialog.Filter = "Script files (*.dat)|*.dat|All files (*.*)|*.*"; 
            
            dialogResult = this.openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.DiscardFormData();
                this.LoadScriptData(this.openFileDialog.FileName);
                this.DisplayFormData();
            }

            openFileDialog.Filter = "Level files (*.lvl)|*.lvl|All files (*.*)|*.*";
        }

        private void saveScriptbetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            // check to save current edited file
            if (this.isFileModified == true)
            {
                string message = "Current file not saved. ¿Save now?";

                dialogResult = MessageBox.Show(message, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    dialogResult = this.saveFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                        this.SaveFormData();
                }
            }

            // file to generate
            saveFileDialog.Filter = "Script files (*.dat)|*.dat|All files (*.*)|*.*";

            dialogResult = this.saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.SaveScriptData(this.saveFileDialog.FileName);
            }

            saveFileDialog.Filter = "Level files (*.lvl)|*.lvl|All files (*.*)|*.*";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.isFileModified == true)
            {
                string message = "Current file not saved. ¿Save now?";
                
                DialogResult dialogResult = MessageBox.Show(message, "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    dialogResult = this.saveFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                        this.SaveFormData();
                }
            }

            this.Close();
        }

        private void aboutMapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void DiscardFormData()
        {
            this.isFileModified = false;
            this.brickCount = 0;

            bricks.Clear();
            this.pnlGame.Controls.Clear();
        }

        private void LoadFormData(string fileName)
        {
            this.isFileModified = false;
            this.brickCount = 0;

            frmMain.bricks.Clear();
            this.pnlGame.Controls.Clear();

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();

                frmMain.bricks = (List<Brick>)bf.Deserialize(fs);

                fs.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoadScriptData(string fileName)
        {
            this.isFileModified = false;
            this.brickCount = 0;

            frmMain.bricks.Clear();
            this.pnlGame.Controls.Clear();

            string line;
            try
            {
                StreamReader sr = new StreamReader(fileName);

                do {
                    line = sr.ReadLine();
                    this.ProcessScriptLine(line);
                } while (!sr.EndOfStream);

                sr.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ProcessScriptLine(string line)
        {
            if (line.Trim() == "") { return; }
            if (line.StartsWith("//")) { return; }

            Brick brick;
            string[] data = line.Split(new char[] { ',' }, StringSplitOptions.None); // 6, 

            if (data[0] == "BRICK")
            {
                int pos_x = Convert.ToInt32(data[1]);
                int pos_y = Convert.ToInt32(data[2]);
                Brick.eColor eColor = (Brick.eColor)Color.FromName(data[3]).ToKnownColor();
                Brick.eBrickType eType = (Brick.eBrickType)Convert.ToInt32(data[4]);
                Reward.eRewardType eReward = (Reward.eRewardType)Convert.ToInt32(data[5]);

                brick = new Brick(pos_x, pos_y, eType, eReward, eColor);
                bricks.Add(brick);
            }
        }

        private void SaveScriptData(string fileName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Brick brick in bricks)
                sb.AppendLine($"BRICK,{ brick.X },{ brick.Y },{ brick.Color },{ (int)brick.BrickType },{ (int)brick.RewardType }");

            try
            {
                StreamWriter sw = new StreamWriter(fileName);
                sw.Write(sb.ToString());
                sw.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DisplayFormData()
        {
            this.pnlGame.Controls.Clear();

            foreach (Brick brick in bricks)
            {
                Button button = new Button()
                {
                    Name = "Brick" + brickCount.ToString(),
                    Location = brick.Location,
                    Width = Brick.BRICK_WIDTH,
                    Height = Brick.BRICK_HEIGHT,
                    TabStop = false,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromName(brick.Color.ToString()),
                    Tag = new Tag(brick.BrickType, brick.RewardType)
                };
                button.FlatAppearance.BorderColor = Color.Black;
                button.FlatAppearance.MouseDownBackColor = Color.FromName(brick.Color.ToString());
                button.FlatAppearance.MouseOverBackColor = Color.FromName(brick.Color.ToString());

                button.GotFocus += new EventHandler(button_GotFocus);
                button.LostFocus += new EventHandler(gbPosition_LostFocus);

                this.pnlGame.Controls.Add(button);
                brickCount++;
            }
        }

        private void SaveFormData()
        {
            Brick brick;
            bricks.Clear();

            try
            {
                foreach (Control control in this.pnlGame.Controls)
                {
                    brick = new Brick(control.Location.X, control.Location.Y, ((Tag)control.Tag).Type, ((Tag)control.Tag).Reward, (Brick.eColor)control.BackColor.ToKnownColor());
                    bricks.Add(brick);
                }

                FileStream fs = new FileStream(this.saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, bricks);
                fs.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        #endregion

        #region Validations

        private bool ValidateNumeric(string sText)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(sText);
        }

        private bool ValidatePosition(string text, eAxis axis)
        {
            int num = Convert.ToInt32(text);

            if (axis == eAxis.X)
                if (num < 0 || num > (CollisionBase.SCREEN_WIDTH - Brick.BRICK_WIDTH)) { return false; }
            else if (axis == eAxis.Y)
                if (num < 0 || num > (CollisionBase.SCREEN_HEIGHT - Brick.BRICK_HEIGHT)) { return false; }

            return true;
        }

        #endregion
    }
}
