namespace HackSystem
{
    partial class StartUpsCollectionForm
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
            this.StartUpsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // StartUpsLayoutPanel
            // 
            this.StartUpsLayoutPanel.AutoScroll = true;
            this.StartUpsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StartUpsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.StartUpsLayoutPanel.Name = "StartUpsLayoutPanel";
            this.StartUpsLayoutPanel.Size = new System.Drawing.Size(884, 561);
            this.StartUpsLayoutPanel.TabIndex = 0;
            // 
            // StartUpsCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.StartUpsLayoutPanel);
            this.DoubleBuffered = true;
            this.Name = "StartUpsCollectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StartUpsCollectionForm";
            this.Shown += new System.EventHandler(this.StartUpsCollectionForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel StartUpsLayoutPanel;
    }
}