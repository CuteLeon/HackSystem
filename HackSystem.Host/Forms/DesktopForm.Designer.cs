namespace HackSystem.Host
{
    partial class DesktopForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ShowStartUpsButton = new System.Windows.Forms.Button();
            this.ShowLogonsButton = new System.Windows.Forms.Button();
            this.ProgramLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // ShowStartUpsButton
            // 
            this.ShowStartUpsButton.Location = new System.Drawing.Point(506, 175);
            this.ShowStartUpsButton.Name = "ShowStartUpsButton";
            this.ShowStartUpsButton.Size = new System.Drawing.Size(204, 84);
            this.ShowStartUpsButton.TabIndex = 0;
            this.ShowStartUpsButton.Text = "Show StartUps Collaction";
            this.ShowStartUpsButton.UseVisualStyleBackColor = true;
            this.ShowStartUpsButton.Click += new System.EventHandler(this.ShowStartUpsButton_Click);
            // 
            // ShowLogonsButton
            // 
            this.ShowLogonsButton.Location = new System.Drawing.Point(506, 288);
            this.ShowLogonsButton.Name = "ShowLogonsButton";
            this.ShowLogonsButton.Size = new System.Drawing.Size(204, 84);
            this.ShowLogonsButton.TabIndex = 1;
            this.ShowLogonsButton.Text = "Show Logons Collaction";
            this.ShowLogonsButton.UseVisualStyleBackColor = true;
            this.ShowLogonsButton.Click += new System.EventHandler(this.ShowLogonsButton_Click);
            // 
            // ProgramLayoutPanel
            // 
            this.ProgramLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProgramLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ProgramLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.ProgramLayoutPanel.Name = "ProgramLayoutPanel";
            this.ProgramLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.ProgramLayoutPanel.Size = new System.Drawing.Size(969, 661);
            this.ProgramLayoutPanel.TabIndex = 2;
            // 
            // DesktopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 661);
            this.Controls.Add(this.ShowLogonsButton);
            this.Controls.Add(this.ShowStartUpsButton);
            this.Controls.Add(this.ProgramLayoutPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DesktopForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hack System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesktopForm_FormClosing);
            this.Shown += new System.EventHandler(this.DesktopForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ShowStartUpsButton;
        private System.Windows.Forms.Button ShowLogonsButton;
        private System.Windows.Forms.FlowLayoutPanel ProgramLayoutPanel;
    }
}

