using System;
using System.Drawing;
using System.Windows.Forms;

namespace HackSystem.StartUpTemplate
{
    public abstract class StartUpTemplateClass : IDisposable
    {
        /// <summary>
        /// 图标
        /// </summary>
        public static Icon StartUpIcon = null;
        /// <summary>
        /// 启动名称
        /// </summary>
        public string Name { get; protected set; } = "启动";
        /// <summary>
        /// 启动描述
        /// </summary>
        public string Description { get; protected set; } = "描述";
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
            get => this._preview;
            protected set
            {
                if (value == null)
                    this._preview = StartUpTemplateResource.DefaultStartUpPreview;
                else
                    this._preview = new Bitmap(value, 160, 90);
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
             * 不要在子类的构造函数里 new 出 Form，否则会自动创建出 Form 占用很多内存
             * 只需要实现 CreateXXXForm() 方法，第一次访问 Form 时创建对象，不用时 Dispose 掉，防止过多占用内存；
             */
            get
            {
                if (this._startUpForm == null)
                    this._startUpForm = this.CreateStartUpForm();
                if (this._startUpForm != null) this._startUpForm.Icon = StartUpIcon;
                return this._startUpForm;
            }
            protected set => this._startUpForm = value;
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
            this._startUpForm?.Dispose();
            this.StartUpForm = null;
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
            System.Diagnostics.Debug.Print("Dispose StartUp : {0}", this.Name);
            this._preview?.Dispose();
            this._startUpForm?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
