namespace RingsStartUp
{
    partial class RainbowRingForm
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
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.RainbowPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoEllipsis = true;
            this.ProgressLabel.BackColor = System.Drawing.Color.Transparent;
            this.ProgressLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProgressLabel.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgressLabel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.ProgressLabel.Location = new System.Drawing.Point(50, 427);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(799, 73);
            this.ProgressLabel.TabIndex = 3;
            this.ProgressLabel.Text = "Hack System Loading ...";
            this.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RainbowPictureBox
            // 
            this.RainbowPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RainbowPictureBox.Image = global::RingsStartUp.RingsStartUpResource.RainbowRing;
            this.RainbowPictureBox.Location = new System.Drawing.Point(299, 163);
            this.RainbowPictureBox.MaximumSize = new System.Drawing.Size(300, 225);
            this.RainbowPictureBox.MinimumSize = new System.Drawing.Size(300, 225);
            this.RainbowPictureBox.Name = "RainbowPictureBox";
            this.RainbowPictureBox.Size = new System.Drawing.Size(300, 225);
            this.RainbowPictureBox.TabIndex = 4;
            this.RainbowPictureBox.TabStop = false;
            // 
            // RainbowRingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(899, 550);
            this.Controls.Add(this.RainbowPictureBox);
            this.Controls.Add(this.ProgressLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RainbowRingForm";
            this.Padding = new System.Windows.Forms.Padding(50);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "e";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.RainbowRingForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.RainbowPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.PictureBox RainbowPictureBox;
    }
}