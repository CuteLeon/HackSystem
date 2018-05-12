using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LoginTemplate;

namespace DefaultLogin
{
    public partial class DefaultLoginForm : Form
    {
        public LoginTemplateClass ParentLogin = null;
        TextBox InnerTextBox = new TextBox() { MaxLength = 12, Visible = true, BorderStyle= BorderStyle.None, Size= Size.Empty };
        bool AllowToClose = false;

        public DefaultLoginForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            LoginPanel.Controls.Add(InnerTextBox);
            InnerTextBox.Location = PasswordInputBox.Location;
            this.FormClosing += new FormClosingEventHandler(
                (Leon, Mathilda) => {
                    if (!AllowToClose)
                        Mathilda.Cancel = true;
                    else
                        ParentLogin?.OnLoginFinished(EventArgs.Empty);
                });
            HeadPortraitPictureBox.BackgroundImage = new Bitmap(LoginTemplateClass.CircularHeadPortrait, 159, 159);

            LoginButton.MouseEnter += delegate { LoginButton.Image = DefaultLoginResource.LoginButton_2; };
            LoginButton.MouseDown += delegate { LoginButton.Image = DefaultLoginResource.LoginButton_3; };
            LoginButton.MouseUp += delegate { LoginButton.Image = DefaultLoginResource.LoginButton_2; };
            LoginButton.MouseLeave += delegate { LoginButton.Image = DefaultLoginResource.LoginButton_1; };

            PasswordInputBox.Click += delegate { InnerTextBox.Focus(); };
            PasswordInputBox.MouseEnter += delegate { PasswordInputBox.Image = DefaultLoginResource.PasswordInputBox_Enter; };
            PasswordInputBox.MouseDown += delegate { PasswordInputBox.Image = DefaultLoginResource.PasswordInputBox_Down; };
            PasswordInputBox.MouseUp += delegate { PasswordInputBox.Image = DefaultLoginResource.PasswordInputBox_Enter; };
            PasswordInputBox.MouseLeave += delegate { PasswordInputBox.Image = DefaultLoginResource.PasswordInputBox_Normal; };

            InnerTextBox.TextChanged += delegate { PasswordInputBox.Text = string.Empty.PadRight(InnerTextBox.Text.Length, '♋'); };
            InnerTextBox.KeyDown += new KeyEventHandler((s, e) => { if (e.KeyCode == Keys.Enter) { CheckPassword(); } });
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void CheckPassword()
        {
            if (InnerTextBox.Text != DefaultLoginClass.Password)
            {
                AllowToClose = false;
                //TODO: 等系统提供了弹窗或浮窗的API，使用系统弹出错误提示
                MessageBox.Show("密码不正确！");
                InnerTextBox.Focus();
            }
            else
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(
                    (ILoveU) => {
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
        }

    }

    class PanelEx : Panel
    {
        public PanelEx()
        {
            SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
        }
    }

}
