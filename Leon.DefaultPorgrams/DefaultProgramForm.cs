using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Leon.DefaultPorgrams
{
    public partial class DefaultProgramForm : Form
    {
        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();
        private const int HT_CAPTION = 0x2;
        private const int WM_NCLBUTTONDOWN = 0xA1;

        bool CloseProgram = false;

        public new string Text {
            get => TitleLabel.Text;
            set
            {
                TitleLabel.Text = value;
                base.Text = value;
            }
        }

        private Bitmap _icon = DefaultProgramResource.DefaultIcon;
        public new Image Icon
        {
            get => _icon;
            set
            {
                if (value == null)
                {
                    _icon = DefaultProgramResource.DefaultIcon;
                }
                else
                {
                    _icon = new Bitmap(value, 24, 24);
                }
                base.Icon = System.Drawing.Icon.FromHandle(_icon.GetHicon());
                IconLabel.Image = _icon;
            }
        }

        private Image _imageResource = DefaultProgramResource.HackSystemLogo;
        public Image ImageResource
        {
            get => _imageResource;
            set
            {
                if (value == null)
                    _imageResource = DefaultProgramResource.HackSystemLogo;
                else
                    _imageResource = value;

                MainPictureBox.Image = _imageResource;
                MainPictureBox.MaximumSize = _imageResource.Size;
                MainPictureBox.MinimumSize = _imageResource.Size;
            }
        }

        private DefaultProgramForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            AddEventHandler();
        }

        public DefaultProgramForm(string text, Image icon, Image imageResource)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            AddEventHandler();

            Text = text;
            Icon = icon;
            ImageResource = imageResource;
        }

        private void DefaultProgramForm_Load(object sender, EventArgs e)
        {
            //this.Text = this.GetHashCode().ToString("X");
        }

        private void DefaultProgramForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DimGray, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void AddEventHandler()
        {
            CloseButton.MouseEnter += delegate {
                CloseButton.Image = DefaultProgramResource.Close_Enter;
            };
            CloseButton.MouseLeave += delegate {
                CloseButton.Image = DefaultProgramResource.Close_Normal;
            };
            CloseButton.MouseDown += delegate {
                CloseButton.Image = DefaultProgramResource.Close_Down;
            };
            CloseButton.MouseUp += delegate {
                CloseButton.Image = DefaultProgramResource.Close_Enter;
            };

            CloseButton.Click += delegate { this.Close(); };

            this.FormClosed += delegate {
                MainPictureBox.Image?.Dispose();
                MainPictureBox.Image = null;
                this.Dispose(true);
                GC.Collect();
            };

            IconLabel.MouseDown += MoveFormViaMouse;
            TitleLabel.MouseDown += MoveFormViaMouse;
            MainPictureBox.MouseDown += MoveFormViaMouse;
        }

        public void MoveFormViaMouse(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage((sender is Form ? (sender as Form).Handle : (sender as Control).FindForm().Handle), WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void DefaultProgramForm_Activated(object sender, EventArgs e) => TitlePanel.Height = 24;

        private void DefaultProgramForm_Deactivate(object sender, EventArgs e) => TitlePanel.Height = 0;

        private void DefaultProgramForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* TODO : 很奇怪的BUG，在主桌面的窗口触发Form_Closing()的时候，窗口关闭事件就会执行到这里来
             * 但是，此处直接 return 时就不会发生此BUG.
             */

            return;
            if (!CloseProgram)
            {
                e.Cancel = true;
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate {
                    try
                    {
                        while (this.Opacity > 0)
                        {
                            this.Opacity -= 0.1;
                            Thread.Sleep(20);
                        }
                        CloseProgram = true;
                        this.Close();
                    }
                    catch { }
                }));
            }
        }
    }
}
