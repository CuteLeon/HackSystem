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
    public partial class StartUpCardControl : UserControl
    {
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
                    this.BackColor = Color.Gray;
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
        /// StartUp 名称
        /// </summary>
        public new string Name
        {
            get => NameLabel.Text;
            set => NameLabel.Text = value;
        }
        /// <summary>
        /// StartUp 描述
        /// </summary>
        public string Description
        {
            get => DescriptionLabel.Text;
            set => DescriptionLabel.Text = value;
        }
        /// <summary>
        /// 预览图
        /// </summary>
        public Image Preview
        {
            get => this.BackgroundImage;
            set => this.BackgroundImage = value;
        }

        public StartUpCardControl(string fileName, string className,string name,string description, Image preview)
        {
            InitializeComponent();

            FileName = fileName;
            ClassName = className;
            Name = name;
            Description = description;
            Preview = preview;
        }

        private void StartUpCardControl_Load(object sender, EventArgs e)
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

            EventHandler CardClick = new EventHandler((s, v) =>
            {
                ActiveStartUp();
            });

            this.Click += CardClick;
            NameLabel.Click += CardClick;
            DescriptionLabel.Click += CardClick;
        }
        
        private void StartUpCardControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DodgerBlue, new Rectangle(0, 0, 159, 109));
        }

        private void ActiveStartUp()
        {
            if (MessageBox.Show(string.Format("是否使用启动画面 {0} ？", Name), "使用启动画面？", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                ConfigController.SetConfig("StartUpFile", FileName);
                ConfigController.SetConfig("StartUpName", ClassName);
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("更新 StartUp 配置遇到异常：{0}", ex.Message);
                MessageBox.Show(string.Format("更新 StartUp 配置遇到异常：{0}", ex.Message));
                return;
            }
            MessageBox.Show(string.Format("更新 StartUp 配置成功，重启即可查看效果"));
            Application.Restart();
        }

    }
}
