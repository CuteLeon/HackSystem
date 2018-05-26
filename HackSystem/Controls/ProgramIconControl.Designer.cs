namespace HackSystem
{
    partial class ProgramIconControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.NameLabel = new System.Windows.Forms.Label();
            this.IconPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.AutoEllipsis = true;
            this.NameLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.NameLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NameLabel.ForeColor = System.Drawing.Color.DimGray;
            this.NameLabel.Location = new System.Drawing.Point(0, 64);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(64, 24);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "钢铁侠";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IconPictureBox
            // 
            this.IconPictureBox.BackgroundImage = global::HackSystem.UnityResource.DefaultProgramIcon;
            this.IconPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.IconPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IconPictureBox.Location = new System.Drawing.Point(0, 0);
            this.IconPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.IconPictureBox.Name = "IconPictureBox";
            this.IconPictureBox.Size = new System.Drawing.Size(64, 64);
            this.IconPictureBox.TabIndex = 2;
            this.IconPictureBox.TabStop = false;
            // 
            // ProgramIconControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.IconPictureBox);
            this.Controls.Add(this.NameLabel);
            this.Name = "ProgramIconControl";
            this.Size = new System.Drawing.Size(64, 88);
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.PictureBox IconPictureBox;
    }
}
