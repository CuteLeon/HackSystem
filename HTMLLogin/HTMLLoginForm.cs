using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LoginTemplate;

namespace HTMLLogin
{
    //必须设置COM可见
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class HTMLLoginForm : Form
    {
        bool AllowToClose = false;

        /// <summary>
        /// HTML内容流
        /// </summary>
        public MemoryStream HTMLStream
        {
            get => (MemoryStream)MainWebBrowser.DocumentStream;
            set
            {
                MainWebBrowser.DocumentStream = value ?? default(MemoryStream);
                MainWebBrowser.ObjectForScripting = this;
                MainWebBrowser.IsWebBrowserContextMenuEnabled = false;
                MainWebBrowser.ScrollBarsEnabled = false;
                MainWebBrowser.ScriptErrorsSuppressed = false;
            }
        }

        public LoginTemplateClass ParentLogin = null;

        public HTMLLoginForm()
        {
            InitializeComponent();
        }

        private void HTMLLoginForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(
                (Leon, Mathilda) => {
                    if (!AllowToClose)
                        Mathilda.Cancel = true;
                    else
                        ParentLogin?.OnLoginFinished(EventArgs.Empty);
                });
        }

        public void CheckLogin(string UserName, string Password)
        {
            //用户信息通过后，置AllowToClose为true
            if (UserName == LoginTemplateClass.UserName && Password == LoginTemplateClass.Password)
            {
                //Client 调用 Browser 代码；
                MainWebBrowser.Document.InvokeScript("LoginSuccessfully",
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
                        AllowToClose = true;
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
