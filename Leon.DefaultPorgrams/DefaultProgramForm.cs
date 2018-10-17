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
            get => this.TitleLabel.Text;
            set
            {
                this.TitleLabel.Text = value;
                base.Text = value;
            }
        }

        private Bitmap _icon = DefaultProgramResource.DefaultIcon;
        public new Image Icon
        {
            get => this._icon;
            set
            {
                if (value == null)
                {
                    this._icon = DefaultProgramResource.DefaultIcon;
                }
                else
                {
                    this._icon = new Bitmap(value, 24, 24);
                }
                base.Icon = System.Drawing.Icon.FromHandle(this._icon.GetHicon());
                this.IconLabel.Image = this._icon;
            }
        }

        private Image _imageResource = DefaultProgramResource.HackSystemLogo;
        public Image ImageResource
        {
            get => this._imageResource;
            set
            {
                if (value == null)
                    this._imageResource = DefaultProgramResource.HackSystemLogo;
                else
                    this._imageResource = value;

                this.MainPictureBox.Image = this._imageResource;
                this.MainPictureBox.MaximumSize = this._imageResource.Size;
                this.MainPictureBox.MinimumSize = this._imageResource.Size;
            }
        }

        private DefaultProgramForm()
        {
            this.InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.AddEventHandler();
        }

        public DefaultProgramForm(string text, Image icon, Image imageResource)
        {
            this.InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.AddEventHandler();

            this.Text = text;
            this.Icon = icon;
            this.ImageResource = imageResource;
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
            this.CloseButton.MouseEnter += delegate {
                this.CloseButton.Image = DefaultProgramResource.Close_Enter;
            };
            this.CloseButton.MouseLeave += delegate {
                this.CloseButton.Image = DefaultProgramResource.Close_Normal;
            };
            this.CloseButton.MouseDown += delegate {
                this.CloseButton.Image = DefaultProgramResource.Close_Down;
            };
            this.CloseButton.MouseUp += delegate {
                this.CloseButton.Image = DefaultProgramResource.Close_Enter;
            };

            this.CloseButton.Click += delegate { this.Close(); };

            this.FormClosed += delegate {
                this.MainPictureBox.Image?.Dispose();
                this.MainPictureBox.Image = null;
                this.Dispose(true);
                GC.Collect();
            };

            this.IconLabel.MouseDown += this.MoveFormViaMouse;
            this.TitleLabel.MouseDown += this.MoveFormViaMouse;
            this.MainPictureBox.MouseDown += this.MoveFormViaMouse;
        }

        public void MoveFormViaMouse(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage((sender is Form ? (sender as Form).Handle : (sender as Control).FindForm().Handle), WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void DefaultProgramForm_Activated(object sender, EventArgs e) => this.TitlePanel.Height = 24;

        private void DefaultProgramForm_Deactivate(object sender, EventArgs e) => this.TitlePanel.Height = 0;

        private void DefaultProgramForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CloseProgram)
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
                        this.CloseProgram = true;
                        this.Close();
                    }
                    catch { }
                }));
            }
        }
    }
}
