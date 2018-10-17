namespace HackSystem.Host
{
    partial class LogonsCollectionForm
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
            this.LogonsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // LogonsLayoutPanel
            // 
            this.LogonsLayoutPanel.AutoScroll = true;
            this.LogonsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogonsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LogonsLayoutPanel.Name = "LogonsLayoutPanel";
            this.LogonsLayoutPanel.Size = new System.Drawing.Size(884, 561);
            this.LogonsLayoutPanel.TabIndex = 0;
            // 
            // LogonsCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.LogonsLayoutPanel);
            this.DoubleBuffered = true;
            this.Name = "LogonsCollectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StartUpsCollectionForm";
            this.Shown += new System.EventHandler(this.LogonsCollectionForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel LogonsLayoutPanel;
    }
}