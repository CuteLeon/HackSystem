using System.Windows.Forms;
using CefSharp.WinForms;
using HackSystem.Host.Configs;
using HackSystem.Host.EventHandlers;

namespace HackSystem.Host
{
    public partial class HostForm : Form
    {
        protected ChromiumWebBrowser WebBrowser { get; set; }

        public HostForm()
        {
            this.Text = $"{HostConfigs.Title} {Application.ProductVersion}";
            this.Icon = HostResource.Logo;

            this.InitializeComponent();

            this.InitWebBrowser();
            this.LoadRemoteURL();
        }

        private void InitWebBrowser()
        {
            this.WebBrowser = new ChromiumWebBrowser(string.Empty, null)
            {
                Name = "HostWebBrowser"
            };
            this.Controls.Add(this.WebBrowser);
            this.WebBrowser.ConsoleMessage += ChromiumWebBrowserMessageHandler.DoConsoleMessage;
            this.WebBrowser.JavascriptMessageReceived += ChromiumWebBrowserMessageHandler.DoJavascriptMessage;
        }

        private void LoadRemoteURL()
        {
            this.WebBrowser.Load(HostConfigs.RemoteURL);
        }
    }
}
