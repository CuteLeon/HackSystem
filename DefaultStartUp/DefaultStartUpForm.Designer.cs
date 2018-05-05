namespace DefaultStartUp
{
    partial class DefaultStartUpForm
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
            this.FrameTimer = new System.Windows.Forms.Timer(this.components);
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.FrameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FrameTimer
            // 
            this.FrameTimer.Tick += new System.EventHandler(this.FrameTimer_Tick);
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoEllipsis = true;
            this.ProgressLabel.BackColor = System.Drawing.Color.Transparent;
            this.ProgressLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProgressLabel.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgressLabel.Location = new System.Drawing.Point(50, 327);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(700, 73);
            this.ProgressLabel.TabIndex = 1;
            this.ProgressLabel.Text = "Hack System Loading ...";
            this.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrameLabel
            // 
            this.FrameLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.FrameLabel.Location = new System.Drawing.Point(284, 67);
            this.FrameLabel.Margin = new System.Windows.Forms.Padding(20);
            this.FrameLabel.MaximumSize = new System.Drawing.Size(240, 240);
            this.FrameLabel.MinimumSize = new System.Drawing.Size(240, 240);
            this.FrameLabel.Name = "FrameLabel";
            this.FrameLabel.Padding = new System.Windows.Forms.Padding(20);
            this.FrameLabel.Size = new System.Drawing.Size(240, 240);
            this.FrameLabel.TabIndex = 2;
            this.FrameLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // DefaultStartUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::DefaultStartUp.StartUpResource.StartingUIWallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FrameLabel);
            this.Controls.Add(this.ProgressLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DefaultStartUpForm";
            this.Padding = new System.Windows.Forms.Padding(50);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DefaultStartUpForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DefaultStartUpForm_Load);
            this.Shown += new System.EventHandler(this.DefaultStartUpForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer FrameTimer;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.Label FrameLabel;
    }
}