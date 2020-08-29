using System.Threading;
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
                Name = "HostWebBrowser",
                MenuHandler = new ChromiumDisableMenuHandler()
            };

            this.Controls.Add(this.WebBrowser);
            this.WebBrowser.ConsoleMessage += ChromiumWebBrowserMessageHandler.DoConsoleMessage;
            this.WebBrowser.JavascriptMessageReceived += ChromiumWebBrowserMessageHandler.DoJavascriptMessage;
            this.WebBrowser.LoadingStateChanged += ChromiumWebBrowserLoadHandler.DoLoadingStateChanged;
            this.WebBrowser.LoadError += ChromiumWebBrowserLoadHandler.DoLoadError;
            this.WebBrowser.FrameLoadStart += ChromiumWebBrowserLoadHandler.DoFrameLoadStart;
            this.WebBrowser.FrameLoadEnd += ChromiumWebBrowserLoadHandler.DoFrameLoadEnd;

            this.WebBrowser.RegisterStartUpPageResource("https://StartUpPage.HackSystem.com", "Loading ...");
        }

        private void HostForm_Shown(object sender, System.EventArgs e)
        {
            this.LoadRemoteURL();
        }

        private void LoadRemoteURL()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((x) =>
            {
                this.WebBrowser.Load("https://StartUpPage.HackSystem.com");
                Thread.Sleep(1000);
                this.WebBrowser.Load($"{HostConfigs.RemoteURL}/{HostConfigs.StartURI}");
            }));
        }
    }
}
