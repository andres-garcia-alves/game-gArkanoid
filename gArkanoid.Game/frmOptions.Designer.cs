namespace gArkanoid
{
    partial class frmOptions
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
            this.cboLanguajes = new System.Windows.Forms.ComboBox();
            this.lblLanguaje = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblLives = new System.Windows.Forms.Label();
            this.cboLives = new System.Windows.Forms.ComboBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.lblMessaje = new System.Windows.Forms.Label();
            this.chkMusic = new System.Windows.Forms.CheckBox();
            this.lblMusic = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.radKeyboard = new System.Windows.Forms.RadioButton();
            this.radMouse = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cboLanguajes
            // 
            this.cboLanguajes.BackColor = System.Drawing.Color.Black;
            this.cboLanguajes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguajes.ForeColor = System.Drawing.Color.White;
            this.cboLanguajes.FormattingEnabled = true;
            this.cboLanguajes.Location = new System.Drawing.Point(285, 110);
            this.cboLanguajes.Name = "cboLanguajes";
            this.cboLanguajes.Size = new System.Drawing.Size(140, 21);
            this.cboLanguajes.TabIndex = 0;
            // 
            // lblLanguaje
            // 
            this.lblLanguaje.AutoSize = true;
            this.lblLanguaje.BackColor = System.Drawing.Color.Transparent;
            this.lblLanguaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguaje.ForeColor = System.Drawing.Color.White;
            this.lblLanguaje.Location = new System.Drawing.Point(160, 110);
            this.lblLanguaje.Name = "lblLanguaje";
            this.lblLanguaje.Size = new System.Drawing.Size(59, 15);
            this.lblLanguaje.TabIndex = 1;
            this.lblLanguaje.Text = "Languaje";
            // 
            // btnOK
            // 
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(140, 370);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(260, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Return";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblLives
            // 
            this.lblLives.AutoSize = true;
            this.lblLives.BackColor = System.Drawing.Color.Transparent;
            this.lblLives.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLives.ForeColor = System.Drawing.Color.White;
            this.lblLives.Location = new System.Drawing.Point(160, 150);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(35, 15);
            this.lblLives.TabIndex = 1;
            this.lblLives.Text = "Lives";
            // 
            // cboLives
            // 
            this.cboLives.BackColor = System.Drawing.Color.Black;
            this.cboLives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLives.ForeColor = System.Drawing.Color.White;
            this.cboLives.FormattingEnabled = true;
            this.cboLives.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cboLives.Location = new System.Drawing.Point(285, 150);
            this.cboLives.Name = "cboLives";
            this.cboLives.Size = new System.Drawing.Size(50, 21);
            this.cboLives.TabIndex = 1;
            // 
            // btnRestart
            // 
            this.btnRestart.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRestart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnRestart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnRestart.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.Location = new System.Drawing.Point(380, 370);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(100, 30);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // lblMessaje
            // 
            this.lblMessaje.AutoSize = true;
            this.lblMessaje.BackColor = System.Drawing.Color.Transparent;
            this.lblMessaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessaje.ForeColor = System.Drawing.Color.White;
            this.lblMessaje.Location = new System.Drawing.Point(160, 315);
            this.lblMessaje.Name = "lblMessaje";
            this.lblMessaje.Size = new System.Drawing.Size(211, 15);
            this.lblMessaje.TabIndex = 4;
            this.lblMessaje.Text = "Changes will apply after game restart.";
            this.lblMessaje.Visible = false;
            // 
            // chkMusic
            // 
            this.chkMusic.AutoSize = true;
            this.chkMusic.ForeColor = System.Drawing.Color.White;
            this.chkMusic.Location = new System.Drawing.Point(285, 190);
            this.chkMusic.Name = "chkMusic";
            this.chkMusic.Size = new System.Drawing.Size(73, 17);
            this.chkMusic.TabIndex = 5;
            this.chkMusic.Text = "(ON/OFF)";
            this.chkMusic.UseVisualStyleBackColor = true;
            // 
            // lblMusic
            // 
            this.lblMusic.AutoSize = true;
            this.lblMusic.BackColor = System.Drawing.Color.Transparent;
            this.lblMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMusic.ForeColor = System.Drawing.Color.White;
            this.lblMusic.Location = new System.Drawing.Point(160, 190);
            this.lblMusic.Name = "lblMusic";
            this.lblMusic.Size = new System.Drawing.Size(40, 15);
            this.lblMusic.TabIndex = 6;
            this.lblMusic.Text = "Music";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.BackColor = System.Drawing.Color.Transparent;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput.ForeColor = System.Drawing.Color.White;
            this.lblInput.Location = new System.Drawing.Point(160, 230);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(34, 15);
            this.lblInput.TabIndex = 7;
            this.lblInput.Text = "Input";
            // 
            // radKeyboard
            // 
            this.radKeyboard.AutoSize = true;
            this.radKeyboard.ForeColor = System.Drawing.Color.White;
            this.radKeyboard.Location = new System.Drawing.Point(285, 230);
            this.radKeyboard.Name = "radKeyboard";
            this.radKeyboard.Size = new System.Drawing.Size(70, 17);
            this.radKeyboard.TabIndex = 8;
            this.radKeyboard.TabStop = true;
            this.radKeyboard.Text = "Keyboard";
            this.radKeyboard.UseVisualStyleBackColor = true;
            // 
            // radMouse
            // 
            this.radMouse.AutoSize = true;
            this.radMouse.ForeColor = System.Drawing.Color.White;
            this.radMouse.Location = new System.Drawing.Point(380, 230);
            this.radMouse.Name = "radMouse";
            this.radMouse.Size = new System.Drawing.Size(57, 17);
            this.radMouse.TabIndex = 9;
            this.radMouse.TabStop = true;
            this.radMouse.Text = "Mouse";
            this.radMouse.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.radMouse);
            this.Controls.Add(this.radKeyboard);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.lblMusic);
            this.Controls.Add(this.chkMusic);
            this.Controls.Add(this.lblMessaje);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblLives);
            this.Controls.Add(this.lblLanguaje);
            this.Controls.Add(this.cboLives);
            this.Controls.Add(this.cboLanguajes);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOptions_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboLanguajes;
        private System.Windows.Forms.Label lblLanguaje;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblLives;
        private System.Windows.Forms.ComboBox cboLives;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label lblMessaje;
        private System.Windows.Forms.CheckBox chkMusic;
        private System.Windows.Forms.Label lblMusic;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.RadioButton radKeyboard;
        private System.Windows.Forms.RadioButton radMouse;

    }
}

