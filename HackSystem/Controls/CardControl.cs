using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HackSystem
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
            get => _isActived;
            set
            {
                _isActived = value;
                if (value)
                {
                    DescriptionLabel.ForeColor = Color.OrangeRed;
                    DescriptionLabel.Text = "正在使用";
                }
                else
                {
                    DescriptionLabel.ForeColor = Color.Gray;
                    DescriptionLabel.Text = Description;
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
            get => NameLabel.Text;
            set => NameLabel.Text = value;
        }
        private string _description = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                if (!IsActived)
                    DescriptionLabel.Text = value;
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
            InitializeComponent();

            FileName = fileName;
            ClassName = className;
            Name = name;
            Description = description;
            Preview = preview;
        }

        private void CardControl_Load(object sender, EventArgs e)
        {
            EventHandler CardMouseEnter = new EventHandler((s, v) =>
            {
                NameLabel.ForeColor = Color.OrangeRed;
            });
            EventHandler CardMouseLeave = new EventHandler((s, v) =>
            {
                NameLabel.ForeColor = Color.Black;
            });
            MouseEventHandler CardMouseDown = new MouseEventHandler((s, v) =>
            {
                NameLabel.ForeColor = Color.Red;
            });
            MouseEventHandler CardMouseUp = new MouseEventHandler((s, v) =>
            {
                NameLabel.ForeColor = Color.OrangeRed;
            });

            this.MouseEnter += CardMouseEnter;
            NameLabel.MouseEnter += CardMouseEnter;
            DescriptionLabel.MouseEnter += CardMouseEnter;

            this.MouseLeave += CardMouseLeave;
            NameLabel.MouseLeave += CardMouseLeave;
            DescriptionLabel.MouseLeave += CardMouseLeave;

            this.MouseDown += CardMouseDown;
            NameLabel.MouseDown += CardMouseDown;
            DescriptionLabel.MouseDown += CardMouseDown;

            this.MouseUp += CardMouseUp;
            NameLabel.MouseUp += CardMouseUp;
            DescriptionLabel.MouseUp += CardMouseUp;

            base.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
            NameLabel.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
            DescriptionLabel.Click += delegate { Click?.Invoke(this, EventArgs.Empty); };
        }
        
        private void CardControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DodgerBlue, new Rectangle(0, 0, 159, 109));
        }

    }
}
