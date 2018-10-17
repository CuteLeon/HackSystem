using System;
using System.Drawing;
using System.Windows.Forms;

namespace HackSystem.Host
{
    public partial class CardControl : UserControl
    {
        /// <summary>
        /// 点击事件
        /// </summary>
        public new event EventHandler Click;

        private bool _isActived = false;
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActived
        {
            get => this._isActived;
            set
            {
                this._isActived = value;
                if (value)
                {
                    this.DescriptionLabel.ForeColor = Color.OrangeRed;
                    this.DescriptionLabel.Text = "正在使用";
                }
                else
                {
                    this.DescriptionLabel.ForeColor = Color.Gray;
                    this.DescriptionLabel.Text = this.Description;
                }
            }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; protected set; } = string.Empty;
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; protected set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        public new string Name
        {
            get => this.NameLabel.Text;
            set => this.NameLabel.Text = value;
        }
        private string _description = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get => this._description;
            set
            {
                this._description = value;
                if (!this.IsActived)
                    this.DescriptionLabel.Text = value;
            }
        }
        /// <summary>
        /// 预览图
        /// </summary>
        public Image Preview
        {
            get => this.BackgroundImage;
            set => this.BackgroundImage = value;
        }

        public CardControl(string fileName, string className,string name,string description, Image preview)
        {
            this.InitializeComponent();

            this.FileName = fileName;
            this.ClassName = className;
            this.Name = name;
            this.Description = description;
            this.Preview = preview;
        }

        private void CardControl_Load(object sender, EventArgs e)
        {
            this.AddEventHandler();
        }
        
        private void CardControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DodgerBlue, new Rectangle(0, 0, 159, 109));
        }

        private void AddEventHandler()
        {
            EventHandler CardMouseEnter = new EventHandler((s, v) =>
            {
                this.NameLabel.ForeColor = Color.OrangeRed;
            });
            EventHandler CardMouseLeave = new EventHandler((s, v) =>
            {
                this.NameLabel.ForeColor = Color.Black;
            });
            MouseEventHandler CardMouseDown = new MouseEventHandler((s, v) =>
            {
                this.NameLabel.ForeColor = Color.Red;
            });
            MouseEventHandler CardMouseUp = new MouseEventHandler((s, v) =>
            {
                this.NameLabel.ForeColor = Color.OrangeRed;
            });

            this.MouseEnter += CardMouseEnter;
            this.NameLabel.MouseEnter += CardMouseEnter;
            this.DescriptionLabel.MouseEnter += CardMouseEnter;

            this.MouseLeave += CardMouseLeave;
            this.NameLabel.MouseLeave += CardMouseLeave;
            this.DescriptionLabel.MouseLeave += CardMouseLeave;

            this.MouseDown += CardMouseDown;
            this.NameLabel.MouseDown += CardMouseDown;
            this.DescriptionLabel.MouseDown += CardMouseDown;

            this.MouseUp += CardMouseUp;
            this.NameLabel.MouseUp += CardMouseUp;
            this.DescriptionLabel.MouseUp += CardMouseUp;

            base.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
            this.NameLabel.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
            this.DescriptionLabel.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
        }

    }
}
