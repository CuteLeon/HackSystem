namespace DefaultLogin
{
    partial class DefaultLoginForm
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
            this.LoginPanel = new DefaultLogin.PanelEx();
            this.PasswordInputBox = new System.Windows.Forms.Label();
            this.HeadPortraitPictureBox = new System.Windows.Forms.PictureBox();
            this.LoginButton = new System.Windows.Forms.Label();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.LoginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeadPortraitPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoginPanel.BackColor = System.Drawing.Color.Transparent;
            this.LoginPanel.BackgroundImage = global::DefaultLogin.DefaultLoginResource.LoginArea;
            this.LoginPanel.Controls.Add(this.UserNameLabel);
            this.LoginPanel.Controls.Add(this.PasswordInputBox);
            this.LoginPanel.Controls.Add(this.HeadPortraitPictureBox);
            this.LoginPanel.Controls.Add(this.LoginButton);
            this.LoginPanel.Location = new System.Drawing.Point(155, 341);
            this.LoginPanel.MaximumSize = new System.Drawing.Size(643, 185);
            this.LoginPanel.MinimumSize = new System.Drawing.Size(643, 185);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(643, 185);
            this.LoginPanel.TabIndex = 0;
            // 
            // PasswordInputBox
            // 
            this.PasswordInputBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PasswordInputBox.ForeColor = System.Drawing.Color.Tomato;
            this.PasswordInputBox.Image = global::DefaultLogin.DefaultLoginResource.PasswordInputBox_Normal;
            this.PasswordInputBox.Location = new System.Drawing.Point(274, 103);
            this.PasswordInputBox.Name = "PasswordInputBox";
            this.PasswordInputBox.Size = new System.Drawing.Size(220, 40);
            this.PasswordInputBox.TabIndex = 2;
            this.PasswordInputBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HeadPortraitPictureBox
            // 
            this.HeadPortraitPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.HeadPortraitPictureBox.Image = global::DefaultLogin.DefaultLoginResource.HeadMask;
            this.HeadPortraitPictureBox.Location = new System.Drawing.Point(-5, -3);
            this.HeadPortraitPictureBox.Name = "HeadPortraitPictureBox";
            this.HeadPortraitPictureBox.Size = new System.Drawing.Size(191, 191);
            this.HeadPortraitPictureBox.TabIndex = 1;
            this.HeadPortraitPictureBox.TabStop = false;
            // 
            // LoginButton
            // 
            this.LoginButton.Image = global::DefaultLogin.DefaultLoginResource.LoginButton_1;
            this.LoginButton.Location = new System.Drawing.Point(508, 48);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(94, 95);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserNameLabel.ForeColor = System.Drawing.Color.White;
            this.UserNameLabel.Location = new System.Drawing.Point(192, 18);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(302, 67);
            this.UserNameLabel.TabIndex = 3;
            this.UserNameLabel.Text = "Leon";
            this.UserNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DefaultLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DefaultLogin.DefaultLoginResource.Wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(951, 587);
            this.Controls.Add(this.LoginPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DefaultLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Hack System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.LoginPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeadPortraitPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PanelEx LoginPanel;
        private System.Windows.Forms.Label LoginButton;
        private System.Windows.Forms.PictureBox HeadPortraitPictureBox;
        private System.Windows.Forms.Label PasswordInputBox;
        private System.Windows.Forms.Label UserNameLabel;
    }
}