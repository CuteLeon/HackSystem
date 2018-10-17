using System;
using System.Drawing;
using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace HackSystem.Host
{
    public partial class ProgramIconControl : UserControl
    {
        /// <summary>
        /// 点击事件
        /// </summary>
        public new event EventHandler Click;

        /// <summary>
        /// 父程序对象
        /// </summary>
        public ProgramTemplateClass ParentProgram { get; protected set; } = null;

        /// <summary>
        /// 名称
        /// </summary>
        public new string Name {
            get => this.NameLabel.Text;
            set => this.NameLabel.Text = value;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public Image Icon
        {
            get => this.IconPictureBox.BackgroundImage;
            set
            {
                if (value == null)
                {
                    this.IconPictureBox.BackgroundImage = UnityResource.DefaultProgramIcon;
                }
                else
                {
                    this.IconPictureBox.BackgroundImage = new Bitmap(value, new Size(64, 64));
                }
            }
        }

        private ProgramIconControl()
        {
            this.InitializeComponent();
        }

        public ProgramIconControl(string name, Image icon, ProgramTemplateClass parentProgram)
        {
            this.InitializeComponent();
            this.AddEventHandler();

            this.Name = name;
            this.Icon = icon;
            this.ParentProgram = parentProgram;
        }

        private void AddEventHandler()
        {
            EventHandler CardMouseEnter = new EventHandler((s, v) =>
            {
                this.NameLabel.ForeColor = Color.OrangeRed;
                this.IconPictureBox.Image = UnityResource.ProgramIconMask_0;
            });
            EventHandler CardMouseLeave = new EventHandler((s, v) =>
            {
                this.NameLabel.ForeColor = Color.DimGray;
                this.IconPictureBox.Image = null;
            });
            MouseEventHandler CardMouseDown = new MouseEventHandler((s, v) =>
            {
                this.NameLabel.ForeColor = Color.Red;
                this.IconPictureBox.Image = UnityResource.ProgramIconMask_1;
            });
            MouseEventHandler CardMouseUp = new MouseEventHandler((s, v) =>
            {
                this.NameLabel.ForeColor = Color.OrangeRed;
                this.IconPictureBox.Image = UnityResource.ProgramIconMask_0;
            });

            this.MouseEnter += CardMouseEnter;
            this.NameLabel.MouseEnter += CardMouseEnter;
            this.IconPictureBox.MouseEnter += CardMouseEnter;

            this.MouseLeave += CardMouseLeave;
            this.NameLabel.MouseLeave += CardMouseLeave;
            this.IconPictureBox.MouseLeave += CardMouseLeave;

            this.MouseDown += CardMouseDown;
            this.NameLabel.MouseDown += CardMouseDown;
            this.IconPictureBox.MouseDown += CardMouseDown;

            this.MouseUp += CardMouseUp;
            this.NameLabel.MouseUp += CardMouseUp;
            this.IconPictureBox.MouseUp += CardMouseUp;

            base.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
            this.NameLabel.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
            this.IconPictureBox.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
        }

    }
}
