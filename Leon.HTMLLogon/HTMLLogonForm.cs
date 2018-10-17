using System;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

using HackSystem.LogonTemplate;

namespace Leon.HTMLLogon
{
    //必须设置COM可见
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class HTMLLogonForm : Form
    {
        bool AllowToClose = false;

        /// <summary>
        /// HTML内容流
        /// </summary>
        public MemoryStream HTMLStream
        {
            get => (MemoryStream)this.MainWebBrowser.DocumentStream;
            set
            {
                this.MainWebBrowser.DocumentStream = value ?? default(MemoryStream);
                this.MainWebBrowser.ObjectForScripting = this;
                this.MainWebBrowser.IsWebBrowserContextMenuEnabled = false;
                this.MainWebBrowser.ScrollBarsEnabled = false;
                this.MainWebBrowser.ScriptErrorsSuppressed = false;
            }
        }

        public LogonTemplateClass ParentLogon = null;

        public HTMLLogonForm()
        {
            this.InitializeComponent();
        }

        private void HTMLLogonForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(
                (Leon, Mathilda) => {
                    if (!this.AllowToClose)
                    {
                        Mathilda.Cancel = true;
                    }
                    else
                        this.
                        ParentLogon?.OnLogonFinished(EventArgs.Empty);
                });
        }

        public void CheckLogon(string UserName, string Password)
        {
            //用户信息通过后，置AllowToClose为true
            if (UserName == LogonTemplateClass.UserName && Password == LogonTemplateClass.Password)
            {

                //Client 调用 Browser 代码；
                this.MainWebBrowser.Document.InvokeScript("LogonSuccessfully",
                new String[] { "登录成功，欢迎访问！" });
                ThreadPool.QueueUserWorkItem(new WaitCallback(
                    (ILoveU) =>
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            while (this.Opacity > 0)
                            {
                                Thread.Sleep(100);
                                this.Opacity -= 0.1;
                            }
                        }
                        catch { }
                        this.AllowToClose = true;
                        this.Close();
                    }));
            }
            else
            {
                MessageBox.Show("您的用户名或密码输入错误，请重新输入 ...");
            }
        }
    }
}
