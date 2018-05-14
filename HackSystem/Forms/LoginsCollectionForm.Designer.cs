namespace HackSystem
{
    partial class LoginsCollectionForm
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
            this.LoginsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // LoginsLayoutPanel
            // 
            this.LoginsLayoutPanel.AutoScroll = true;
            this.LoginsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LoginsLayoutPanel.Name = "LoginsLayoutPanel";
            this.LoginsLayoutPanel.Size = new System.Drawing.Size(884, 561);
            this.LoginsLayoutPanel.TabIndex = 0;
            // 
            // LoginsCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.LoginsLayoutPanel);
            this.DoubleBuffered = true;
            this.Name = "LoginsCollectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StartUpsCollectionForm";
            this.Shown += new System.EventHandler(this.LoginsCollectionForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel LoginsLayoutPanel;
    }
}