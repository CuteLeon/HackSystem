using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using HackSystem.LogonTemplate;

namespace Leon.DefaultLogon
{
    public partial class DefaultLogonForm : Form
    {
        public LogonTemplateClass ParentLogon = null;
        TextBox InnerTextBox = new TextBox() { MaxLength = 12, Visible = true, BorderStyle= BorderStyle.None, Size= Size.Empty };
        bool AllowToClose = false;

        public DefaultLogonForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            LogonPanel.Controls.Add(InnerTextBox);
            InnerTextBox.Location = PasswordInputBox.Location;
            this.FormClosing += new FormClosingEventHandler(
                (Leon, Mathilda) => {
                    if (!AllowToClose)
                        Mathilda.Cancel = true;
                    else
                        ParentLogon?.OnLogonFinished(EventArgs.Empty);
                });
            HeadPortraitPictureBox.BackgroundImage = new Bitmap(LogonTemplateClass.CircularHeadPortrait, 159, 159);

            LogonButton.MouseEnter += delegate { LogonButton.Image = DefaultLogonResource.LogonButton_2; };
            LogonButton.MouseDown += delegate { LogonButton.Image = DefaultLogonResource.LogonButton_3; };
            LogonButton.MouseUp += delegate { LogonButton.Image = DefaultLogonResource.LogonButton_2; };
            LogonButton.MouseLeave += delegate { LogonButton.Image = DefaultLogonResource.LogonButton_1; };

            PasswordInputBox.Click += delegate { InnerTextBox.Focus(); };
            PasswordInputBox.MouseEnter += delegate { PasswordInputBox.Image = DefaultLogonResource.PasswordInputBox_Enter; };
            PasswordInputBox.MouseDown += delegate { PasswordInputBox.Image = DefaultLogonResource.PasswordInputBox_Down; };
            PasswordInputBox.MouseUp += delegate { PasswordInputBox.Image = DefaultLogonResource.PasswordInputBox_Enter; };
            PasswordInputBox.MouseLeave += delegate { PasswordInputBox.Image = DefaultLogonResource.PasswordInputBox_Normal; };

            InnerTextBox.TextChanged += delegate { PasswordInputBox.Text = string.Empty.PadRight(InnerTextBox.Text.Length, '♋'); };
            InnerTextBox.KeyDown += new KeyEventHandler((s, e) => { if (e.KeyCode == Keys.Enter) { CheckPassword(); } });
        }

        private void LogonButton_Click(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void CheckPassword()
        {
            if (InnerTextBox.Text != DefaultLogonClass.Password)
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
