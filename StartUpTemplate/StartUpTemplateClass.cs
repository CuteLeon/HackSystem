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
            //TODO : 不要在 StartUpTemplate 子类的构造函数里 new 出 Form，否则扫描插件列表时就会创建出所有的 StartUpForm 占用很多内存
            //第一次访问 Form 时创建对象，不用时 Dispose 掉，防止过多占用内存；
            get
            {
                if (_startUpForm == null)
                    _startUpForm = CreateStartUpForm();
                return _startUpForm;
            }
            protected set => _startUpForm = value;
        }

        /// <summary>
        /// 启动完成事件（用于系统订阅）
        /// </summary>
        public event EventHandler<EventArgs> StartUpFinished; //{add{}remove{}}

        /// <summary>
        /// 触发启动完成事件
        /// </summary>
        /// <param name="e"></param>
        public void OnStartUpFinished(EventArgs e)
        {
            StartUpFinished(this, e);

            //启动完成后自动释放启动画面内存；
            _startUpForm?.Close();
            _startUpForm?.Dispose();
            StartUpForm = null;
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
