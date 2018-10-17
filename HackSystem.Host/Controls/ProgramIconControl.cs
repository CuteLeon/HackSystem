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
            get => NameLabel.Text;
            set => NameLabel.Text = value;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public Image Icon
        {
            get => IconPictureBox.BackgroundImage;
            set
            {
                if (value == null)
                {
                    IconPictureBox.BackgroundImage = UnityResource.DefaultProgramIcon;
                }
                else
                {
                    IconPictureBox.BackgroundImage = new Bitmap(value, new Size(64, 64));
                }
            }
        }

        private ProgramIconControl()
        {
            InitializeComponent();
        }

        public ProgramIconControl(string name, Image icon, ProgramTemplateClass parentProgram)
        {
            InitializeComponent();
            AddEventHandler();
            
            Name = name;
            Icon = icon;
            ParentProgram = parentProgram;
        }

        private void AddEventHandler()
        {
            EventHandler CardMouseEnter = new EventHandler((s, v) =>
            {
                NameLabel.ForeColor = Color.OrangeRed;
                IconPictureBox.Image = UnityResource.ProgramIconMask_0;
            });
            EventHandler CardMouseLeave = new EventHandler((s, v) =>
            {
                NameLabel.ForeColor = Color.DimGray;
                IconPictureBox.Image = null;
            });
            MouseEventHandler CardMouseDown = new MouseEventHandler((s, v) =>
            {
                NameLabel.ForeColor = Color.Red;
                IconPictureBox.Image = UnityResource.ProgramIconMask_1;
            });
            MouseEventHandler CardMouseUp = new MouseEventHandler((s, v) =>
            {
                NameLabel.ForeColor = Color.OrangeRed;
                IconPictureBox.Image = UnityResource.ProgramIconMask_0;
            });

            this.MouseEnter += CardMouseEnter;
            NameLabel.MouseEnter += CardMouseEnter;
            IconPictureBox.MouseEnter += CardMouseEnter;

            this.MouseLeave += CardMouseLeave;
            NameLabel.MouseLeave += CardMouseLeave;
            IconPictureBox.MouseLeave += CardMouseLeave;

            this.MouseDown += CardMouseDown;
            NameLabel.MouseDown += CardMouseDown;
            IconPictureBox.MouseDown += CardMouseDown;

            this.MouseUp += CardMouseUp;
            NameLabel.MouseUp += CardMouseUp;
            IconPictureBox.MouseUp += CardMouseUp;

            base.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
            NameLabel.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
            IconPictureBox.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
        }

    }
}
