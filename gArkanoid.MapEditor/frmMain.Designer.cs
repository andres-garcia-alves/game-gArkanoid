namespace MapEditor
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMapEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlGame = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbBrick = new System.Windows.Forms.GroupBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cboReward = new System.Windows.Forms.ComboBox();
            this.lblReward = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnNewBrick = new System.Windows.Forms.Button();
            this.cboColor = new System.Windows.Forms.ComboBox();
            this.gbPosition = new System.Windows.Forms.GroupBox();
            this.lblIdTitle = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.btnSetPosition = new System.Windows.Forms.Button();
            this.lblPosY = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.lblPosX = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblMark1A = new System.Windows.Forms.Label();
            this.lblMark2A = new System.Windows.Forms.Label();
            this.lblMark1B = new System.Windows.Forms.Label();
            this.lblMark2B = new System.Windows.Forms.Label();
            this.saveScriptbetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.gbBrick.SuspendLayout();
            this.gbPosition.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.MouseEnter += new System.EventHandler(this.menuStrip_MouseEnter);
            this.menuStrip.MouseLeave += new System.EventHandler(this.menuStrip_MouseLeave);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.loadScriptToolStripMenuItem,
            this.saveScriptbetaToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // loadScriptToolStripMenuItem
            // 
            this.loadScriptToolStripMenuItem.Name = "loadScriptToolStripMenuItem";
            this.loadScriptToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadScriptToolStripMenuItem.Text = "&Load Script (beta)";
            this.loadScriptToolStripMenuItem.Click += new System.EventHandler(this.loadScriptToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutMapEditorToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // aboutMapEditorToolStripMenuItem
            // 
            this.aboutMapEditorToolStripMenuItem.Name = "aboutMapEditorToolStripMenuItem";
            this.aboutMapEditorToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.aboutMapEditorToolStripMenuItem.Text = "About &MapEditor";
            this.aboutMapEditorToolStripMenuItem.Click += new System.EventHandler(this.aboutMapEditorToolStripMenuItem_Click);
            // 
            // pnlGame
            // 
            this.pnlGame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGame.Location = new System.Drawing.Point(137, 39);
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.Size = new System.Drawing.Size(640, 480);
            this.pnlGame.TabIndex = 5;
            this.pnlGame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlGame_MouseClick);
            this.pnlGame.MouseEnter += new System.EventHandler(this.pnlGame_MouseEnter);
            this.pnlGame.MouseLeave += new System.EventHandler(this.pnlGame_MouseLeave);
            // 
            // statusStrip
            // 
            this.statusStrip.AllowMerge = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 534);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.AutoSize = false;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(400, 17);
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbBrick
            // 
            this.gbBrick.Controls.Add(this.cboType);
            this.gbBrick.Controls.Add(this.lblType);
            this.gbBrick.Controls.Add(this.cboReward);
            this.gbBrick.Controls.Add(this.lblReward);
            this.gbBrick.Controls.Add(this.lblColor);
            this.gbBrick.Controls.Add(this.btnNewBrick);
            this.gbBrick.Controls.Add(this.cboColor);
            this.gbBrick.Location = new System.Drawing.Point(9, 39);
            this.gbBrick.Name = "gbBrick";
            this.gbBrick.Size = new System.Drawing.Size(115, 212);
            this.gbBrick.TabIndex = 0;
            this.gbBrick.TabStop = false;
            this.gbBrick.Text = "Brick";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(10, 81);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(90, 21);
            this.cboType.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(10, 65);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(27, 13);
            this.lblType.TabIndex = 11;
            this.lblType.Text = "type";
            // 
            // cboReward
            // 
            this.cboReward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReward.FormattingEnabled = true;
            this.cboReward.Location = new System.Drawing.Point(10, 126);
            this.cboReward.Name = "cboReward";
            this.cboReward.Size = new System.Drawing.Size(90, 21);
            this.cboReward.TabIndex = 2;
            this.cboReward.SelectedIndexChanged += new System.EventHandler(this.cboReward_SelectedIndexChanged);
            // 
            // lblReward
            // 
            this.lblReward.AutoSize = true;
            this.lblReward.Location = new System.Drawing.Point(10, 110);
            this.lblReward.Name = "lblReward";
            this.lblReward.Size = new System.Drawing.Size(39, 13);
            this.lblReward.TabIndex = 9;
            this.lblReward.Text = "reward";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(10, 20);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(30, 13);
            this.lblColor.TabIndex = 8;
            this.lblColor.Text = "color";
            // 
            // btnNewBrick
            // 
            this.btnNewBrick.Location = new System.Drawing.Point(12, 166);
            this.btnNewBrick.Name = "btnNewBrick";
            this.btnNewBrick.Size = new System.Drawing.Size(90, 23);
            this.btnNewBrick.TabIndex = 3;
            this.btnNewBrick.Text = "Create";
            this.btnNewBrick.UseVisualStyleBackColor = true;
            this.btnNewBrick.Click += new System.EventHandler(this.btnNewBrick_Click);
            // 
            // cboColor
            // 
            this.cboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColor.FormattingEnabled = true;
            this.cboColor.Location = new System.Drawing.Point(10, 36);
            this.cboColor.Name = "cboColor";
            this.cboColor.Size = new System.Drawing.Size(90, 21);
            this.cboColor.TabIndex = 0;
            // 
            // gbPosition
            // 
            this.gbPosition.Controls.Add(this.lblIdTitle);
            this.gbPosition.Controls.Add(this.lblId);
            this.gbPosition.Controls.Add(this.btnSetPosition);
            this.gbPosition.Controls.Add(this.lblPosY);
            this.gbPosition.Controls.Add(this.txtPosY);
            this.gbPosition.Controls.Add(this.txtPosX);
            this.gbPosition.Controls.Add(this.lblPosX);
            this.gbPosition.Enabled = false;
            this.gbPosition.Location = new System.Drawing.Point(9, 267);
            this.gbPosition.Name = "gbPosition";
            this.gbPosition.Size = new System.Drawing.Size(115, 190);
            this.gbPosition.TabIndex = 6;
            this.gbPosition.TabStop = false;
            this.gbPosition.Text = "Position";
            // 
            // lblIdTitle
            // 
            this.lblIdTitle.AutoSize = true;
            this.lblIdTitle.Location = new System.Drawing.Point(10, 20);
            this.lblIdTitle.Name = "lblIdTitle";
            this.lblIdTitle.Size = new System.Drawing.Size(16, 13);
            this.lblIdTitle.TabIndex = 14;
            this.lblIdTitle.Text = "Id";
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(32, 20);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(68, 13);
            this.lblId.TabIndex = 13;
            // 
            // btnSetPosition
            // 
            this.btnSetPosition.Location = new System.Drawing.Point(10, 143);
            this.btnSetPosition.Name = "btnSetPosition";
            this.btnSetPosition.Size = new System.Drawing.Size(90, 23);
            this.btnSetPosition.TabIndex = 6;
            this.btnSetPosition.Text = "Set Position";
            this.btnSetPosition.UseVisualStyleBackColor = true;
            this.btnSetPosition.Click += new System.EventHandler(this.btnSetPosition_Click);
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Location = new System.Drawing.Point(10, 87);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(12, 13);
            this.lblPosY.TabIndex = 11;
            this.lblPosY.Text = "y";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(10, 103);
            this.txtPosY.MaxLength = 3;
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(90, 20);
            this.txtPosY.TabIndex = 5;
            this.txtPosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPosY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPosY_KeyDown);
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(10, 58);
            this.txtPosX.MaxLength = 3;
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(90, 20);
            this.txtPosX.TabIndex = 4;
            this.txtPosX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPosX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPosX_KeyDown);
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Location = new System.Drawing.Point(10, 42);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(12, 13);
            this.lblPosX.TabIndex = 9;
            this.lblPosX.Text = "x";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Level files (*.lvl)|*.lvl|All files (*.*)|*.*";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Level files (*.lvl)|*.lvl|All files (*.*)|*.*";
            // 
            // lblMark1A
            // 
            this.lblMark1A.AutoSize = true;
            this.lblMark1A.BackColor = System.Drawing.Color.Transparent;
            this.lblMark1A.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMark1A.ForeColor = System.Drawing.Color.Red;
            this.lblMark1A.Location = new System.Drawing.Point(124, 65);
            this.lblMark1A.Name = "lblMark1A";
            this.lblMark1A.Size = new System.Drawing.Size(13, 13);
            this.lblMark1A.TabIndex = 7;
            this.lblMark1A.Text = "_";
            // 
            // lblMark2A
            // 
            this.lblMark2A.AutoSize = true;
            this.lblMark2A.BackColor = System.Drawing.Color.Transparent;
            this.lblMark2A.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMark2A.ForeColor = System.Drawing.Color.Red;
            this.lblMark2A.Location = new System.Drawing.Point(124, 460);
            this.lblMark2A.Name = "lblMark2A";
            this.lblMark2A.Size = new System.Drawing.Size(13, 13);
            this.lblMark2A.TabIndex = 7;
            this.lblMark2A.Text = "_";
            // 
            // lblMark1B
            // 
            this.lblMark1B.AutoSize = true;
            this.lblMark1B.BackColor = System.Drawing.Color.Transparent;
            this.lblMark1B.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMark1B.ForeColor = System.Drawing.Color.Red;
            this.lblMark1B.Location = new System.Drawing.Point(779, 65);
            this.lblMark1B.Name = "lblMark1B";
            this.lblMark1B.Size = new System.Drawing.Size(13, 13);
            this.lblMark1B.TabIndex = 7;
            this.lblMark1B.Text = "_";
            // 
            // lblMark2B
            // 
            this.lblMark2B.AutoSize = true;
            this.lblMark2B.BackColor = System.Drawing.Color.Transparent;
            this.lblMark2B.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMark2B.ForeColor = System.Drawing.Color.Red;
            this.lblMark2B.Location = new System.Drawing.Point(779, 460);
            this.lblMark2B.Name = "lblMark2B";
            this.lblMark2B.Size = new System.Drawing.Size(13, 13);
            this.lblMark2B.TabIndex = 7;
            this.lblMark2B.Text = "_";
            // 
            // saveScriptbetaToolStripMenuItem
            // 
            this.saveScriptbetaToolStripMenuItem.Name = "saveScriptbetaToolStripMenuItem";
            this.saveScriptbetaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveScriptbetaToolStripMenuItem.Text = "Save Script (beta)";
            this.saveScriptbetaToolStripMenuItem.Click += new System.EventHandler(this.saveScriptbetaToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 556);
            this.Controls.Add(this.lblMark2B);
            this.Controls.Add(this.lblMark2A);
            this.Controls.Add(this.gbPosition);
            this.Controls.Add(this.lblMark1B);
            this.Controls.Add(this.lblMark1A);
            this.Controls.Add(this.gbBrick);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pnlGame);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Garkanoid MapEditor v1.0";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.gbBrick.ResumeLayout(false);
            this.gbBrick.PerformLayout();
            this.gbPosition.ResumeLayout(false);
            this.gbPosition.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutMapEditorToolStripMenuItem;
        private System.Windows.Forms.Panel pnlGame;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.GroupBox gbBrick;
        private System.Windows.Forms.Button btnNewBrick;
        private System.Windows.Forms.ComboBox cboColor;
        private System.Windows.Forms.ComboBox cboReward;
        private System.Windows.Forms.Label lblReward;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.GroupBox gbPosition;
        private System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.Label lblPosY;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Button btnSetPosition;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblIdTitle;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblMark1A;
        private System.Windows.Forms.Label lblMark2A;
        private System.Windows.Forms.Label lblMark1B;
        private System.Windows.Forms.Label lblMark2B;
        private System.Windows.Forms.ToolStripMenuItem loadScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveScriptbetaToolStripMenuItem;
    }
}

