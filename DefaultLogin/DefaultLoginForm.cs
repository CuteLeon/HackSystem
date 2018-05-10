using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LoginTemplate;

namespace DefaultLogin
{
    public partial class DefaultLoginForm : Form
    {
        public LoginTemplateClass ParentLogin = null;

        public DefaultLoginForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler((Leon, Mathilda) => { ParentLogin?.OnLoginFinished(EventArgs.Empty); });
            this.Text = string.Format("{0} - {1}", LoginTemplateClass.UserName, LoginTemplateClass.Password);
            this.BackgroundImage = LoginTemplateClass.HeadPortrait;
        }
    }
}
