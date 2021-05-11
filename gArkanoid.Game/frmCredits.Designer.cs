namespace gArkanoid
{
    partial class frmCredits
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.tmrChangeItem = new System.Windows.Forms.Timer(this.components);
            this.tmrEfect = new System.Windows.Forms.Timer(this.components);
            this.lblTop = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(32, 137);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(568, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblValue
            // 
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.ForeColor = System.Drawing.Color.White;
            this.lblValue.Location = new System.Drawing.Point(32, 184);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(568, 23);
            this.lblValue.TabIndex = 1;
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrChangeItem
            // 
            this.tmrChangeItem.Enabled = true;
            this.tmrChangeItem.Interval = 1000;
            this.tmrChangeItem.Tick += new System.EventHandler(this.tmrChangeItem_Tick);
            // 
            // tmrEfect
            // 
            this.tmrEfect.Tick += new System.EventHandler(this.tmrEfect_Tick);
            // 
            // lblTop
            // 
            this.lblTop.BackColor = System.Drawing.Color.Black;
            this.lblTop.Location = new System.Drawing.Point(32, 184);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(568, 23);
            this.lblTop.TabIndex = 2;
            // 
            // frmCredits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCredits";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Credits";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCredits_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Timer tmrChangeItem;
        private System.Windows.Forms.Timer tmrEfect;
        private System.Windows.Forms.Label lblTop;
    }
}