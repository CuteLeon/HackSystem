using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StartUpTemplate
{
    public abstract class StartUpTemplateClass : IDisposable
    {
        /// <summary>
        /// 图标
        /// </summary>
        public static Icon StartUpIcon = null;
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
        /// <summary>
        /// 启动名称
        /// </summary>
        public string Name { get; protected set; } = string.Empty;
        /// <summary>
        /// 启动描述
        /// </summary>
        public string Description { get; protected set; } = string.Empty;
        /// <summary>
        /// 程序集所在文件名称
        /// </summary>
        public abstract string FileName { get; }
        
        private Image _preview = StartUpTemplateResource.DefaultStartUpPreview;
        /// <summary>
        /// 启动预览图 (图像尺寸为 160 x 90 px)
        /// </summary>
        public Image Preview
        {
            get => _preview;
            protected set
            {
                if (value == null)
                    _preview = StartUpTemplateResource.DefaultStartUpPreview;
                else
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
            /* 
             * 不要在 StartUpTemplate 子类的构造函数里 new 出 Form，否则会自动创建出 StartUpForm 占用很多内存
             * 只需要实现 CreateStartUpForm() 方法，第一次访问 Form 时创建对象，不用时 Dispose 掉，防止过多占用内存；
             */
            get
            {
                if (_startUpForm == null)
                    _startUpForm = CreateStartUpForm();
                if (_startUpForm != null) _startUpForm.Icon = StartUpIcon;
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
            this?.StartUpFinished?.Invoke(this, e);
            
            //启动完成后自动释放启动画面内存；
            _startUpForm?.Dispose();
            StartUpForm = null;
        }

        /// <summary>
        /// 构造启动窗口
        /// </summary>
        /// <returns>启动窗口</returns>
        protected abstract Form CreateStartUpForm();

        ~StartUpTemplateClass()
        {
            (this as IDisposable).Dispose();
        }

        void IDisposable.Dispose()
        {
            System.Diagnostics.Debug.Print("Dispose : {0}", this.Name);
            _preview?.Dispose();
            _startUpForm?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
