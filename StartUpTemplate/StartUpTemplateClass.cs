using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StartUpTemplate
{
    public abstract class StartUpTemplateClass : IDisposable
    {
        /// <summary>
        /// 用户名 (传送至 StartUp 插件)
        /// </summary>
        public static string UserName = string.Empty;
        /// <summary>
        /// 密码 (传送至 StartUp 插件)
        /// </summary>
        public static string Password = string.Empty;
        /// <summary>
        /// 头像 (传送至 StartUp 插件)
        /// </summary>
        public static Image HeadPortrait = null;

        private string _name = string.Empty;
        /// <summary>
        /// 启动名称
        /// </summary>
        public string Name
        {
            get => _name;
            protected set => _name = value;
        }

        private string _description = string.Empty;
        /// <summary>
        /// 启动描述
        /// </summary>
        public string Description
        {
            get => _description;
            protected set => _description = value;
        }

        private Image _preview = StartUpTemplateResource.DefaultStartUpPreview;
        /// <summary>
        /// 启动预览图 (图像尺寸为 160 x 90 px)
        /// </summary>
        public Image Preview
        {
            get => _preview;
            protected set
            {
                _preview = new Bitmap(value, 160, 90);
            }
        }

        // 使用 volatile 关键字，防止多线程对对象造成不可预期的影响
        private volatile Form _startUpForm = null;

        /// <summary>
        /// 启动窗口
        /// </summary>
        public Form StartUpForm
        {
            get => _startUpForm;
            protected set => _startUpForm = value;
        }

        /// <summary>
        /// 构造启动窗口
        /// </summary>
        /// <returns>启动窗口</returns>
        protected abstract Form CreateStartUpForm();

        void IDisposable.Dispose()
        {
            _startUpForm?.Dispose();
        }

    }
}
