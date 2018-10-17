namespace Leon.HTMLLogon
{
    partial class HTMLLogonForm
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
            this.MainWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // MainWebBrowser
            // 
            this.MainWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainWebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.MainWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.MainWebBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.MainWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.MainWebBrowser.Name = "MainWebBrowser";
            this.MainWebBrowser.Size = new System.Drawing.Size(800, 450);
            this.MainWebBrowser.TabIndex = 0;
            this.MainWebBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // HTMLLogonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainWebBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HTMLLogonForm";
            this.Text = "Hack System Loading ...";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HTMLLogonForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser MainWebBrowser;
    }
}