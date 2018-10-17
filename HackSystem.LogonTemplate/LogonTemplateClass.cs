using System;
using System.Drawing;
using System.Windows.Forms;

namespace HackSystem.LogonTemplate
{
    public abstract class LogonTemplateClass : IDisposable
    {
        /// <summary>
        /// 图标
        /// </summary>
        public static Icon LogonIcon = null;
        /// <summary>
        /// 用户名 (传送至 Logon 插件)
        /// </summary>
        public static string UserName = string.Empty;
        /// <summary>
        /// 密码 (传送至 Logon 插件)
        /// </summary>
        public static string Password = string.Empty;
        /// <summary>
        /// 圆形头像
        /// </summary>
        public static Image CircularHeadPortrait { get; protected set; }
        private static Image _headPortrait = LogonTemplateResource.DefaultHeadPortrait;
        /// <summary>
        /// 头像 (传送至 Logon 插件)
        /// </summary>
        public static Image HeadPortrait
        {
            get => _headPortrait;
            set
            {
                if (value == null)
                    _headPortrait = LogonTemplateResource.DefaultHeadPortrait;
                else
                    _headPortrait = value;

                CircularHeadPortrait = HeadPortraitController.GetCircularHeadPortrait(value as Bitmap);
            }
        }
        /// <summary>
        /// 启动名称
        /// </summary>
        public string Name { get; protected set; } = "登录";
        /// <summary>
        /// 启动描述
        /// </summary>
        public string Description { get; protected set; } = "描述";
        /// <summary>
        /// 程序集所在文件名称
        /// </summary>
        public abstract string FileName { get; }

        private Image _preview = LogonTemplateResource.DefaultLogonPreview;
        /// <summary>
        /// 启动预览图 (图像尺寸为 160 x 90 px)
        /// </summary>
        public Image Preview
        {
            get => this._preview;
            protected set
            {
                if (value == null)
                    this._preview = LogonTemplateResource.DefaultLogonPreview;
                else
                    this._preview = new Bitmap(value, 160, 90);
            }
        }

        // 使用 volatile 关键字，防止多线程对对象造成不可预期的影响
        private volatile Form _LogonForm = null;

        /// <summary>
        /// 启动窗口
        /// </summary>
        public Form LogonForm
        {
            /* 
             * 不要在子类的构造函数里 new 出 Form，否则会自动创建出 Form 占用很多内存
             * 只需要实现 CreateXXXForm() 方法，第一次访问 Form 时创建对象，不用时 Dispose 掉，防止过多占用内存；
             */
            get
            {
                if (this._LogonForm == null)
                    this._LogonForm = this.CreateLogonForm();
                if (this._LogonForm != null) this._LogonForm.Icon = LogonIcon;
                return this._LogonForm;
            }
            protected set => this._LogonForm = value;
        }

        /// <summary>
        /// 启动完成事件（用于系统订阅）
        /// </summary>
        public event EventHandler<EventArgs> LogonFinished; //{add{}remove{}}

        /// <summary>
        /// 触发登录完成事件
        /// </summary>
        /// <param name="e"></param>
        public void OnLogonFinished(EventArgs e)
        {
            this?.LogonFinished?.Invoke(this, e);

            //启动完成后自动释放启动画面内存；
            this._LogonForm?.Dispose();
            this.LogonForm = null;
        }

        /// <summary>
        /// 构造启动窗口
        /// </summary>
        /// <returns>启动窗口</returns>
        protected abstract Form CreateLogonForm();

        ~LogonTemplateClass()
        {
            (this as IDisposable).Dispose();
        }

        void IDisposable.Dispose()
        {
            System.Diagnostics.Debug.Print("Dispose Logon : {0}", this.Name);
            this._preview?.Dispose();
            this._LogonForm?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
