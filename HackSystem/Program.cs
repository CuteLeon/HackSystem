using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StartUpTemplate;

namespace HackSystem
{
    static class Program
    {
        private volatile static StartUpTemplateClass _startUp = null;
        /// <summary>
        /// 系统的启动画面
        /// </summary>
        public static StartUpTemplateClass StartUp
        {
            get => _startUp;
            set
            {
                _startUp = value;
                UnityModule.DebugPrint("系统当前 StartUp : {0} ({1})", value.Name, value.Description);
            }
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //为 Program 监听全局异常捕获；
            Application.ThreadException += Application_ThreadException;
            Application.ApplicationExit += Application_ApplicationExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //初始化目录
            CheckDirectory();
            //补齐默认配置
            ConfigController.LoadDefaultConfig();

            //加载启动画面

            /* TODO : 放到登录窗口
            StartUpTemplateClass.UserName = ConfigController.GetConfig("UserName");
            StartUpTemplateClass.Password = ConfigController.GetConfig("Password");
            StartUpTemplateClass.HeadPortrait = Base64Controller.Base64ToImage(ConfigController.GetConfig("HeadPortrait"));
             */
            StartUp = StartUpController.GetStartUpPlugin(
                FileController.PathCombine(UnityModule.StartUpDirectory, ConfigController.GetConfig("StartUpFile")),
                ConfigController.GetConfig("StartUpName")
            );
            if (StartUp == null)
            {
                MessageBox.Show("空的 StartUp 插件，系统即将退出。");
                Application.Exit();
            }
            if (StartUp.StartUpForm == null)
            {
                MessageBox.Show("无法创建 StartUpForm，系统即将退出。");
                Application.Exit();
            }
            StartUp.StartUpFinished += new EventHandler<EventArgs>((s, e) => { SwitchToLogin(s, e); });
            StartUp.StartUpForm.ShowDialog();

            Application.Run(new DesktopForm());
        }

        /// <summary>
        /// 初始化目录
        /// </summary>
        private static void CheckDirectory()
        {
            //TODO : 程序需要的目录在这里初始化
            try
            {
                if (!Directory.Exists(UnityModule.StartUpDirectory))
                    Directory.CreateDirectory(UnityModule.StartUpDirectory);
            }catch(Exception ex)
            {
                UnityModule.DebugPrint("创建工作目录遇到异常：{0}", ex.Message);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception UnhandledException = e.ExceptionObject as Exception;
            MessageBox.Show(string.Format("捕获到未处理异常：{0}\r\n异常信息：{1}\r\n异常堆栈：{2}\r\nCLR即将退出：{3}", UnhandledException.GetType(), UnhandledException.Message, UnhandledException.StackTrace, e.IsTerminating));
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ThreadException = e.Exception;
            MessageBox.Show(string.Format("捕获到未处理异常：{0}\r\n异常信息：{1}\r\n异常堆栈：{2}", ThreadException.GetType(), ThreadException.Message, ThreadException.StackTrace));
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            UnityModule.DebugPrint("程序退出 ...");
        }

        private static void SwitchToLogin(object s, EventArgs e)
        {
            UnityModule.DebugPrint("启动完成！");
            
        }

    }
}
