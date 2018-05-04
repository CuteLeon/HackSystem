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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //初始化目录
            CheckDirectory();
            //补齐默认配置
            ConfigController.LoadDefaultConfig();

            //加载启动画面
            /* 放到登录窗口
            StartUpTemplateClass.UserName = ConfigController.GetConfig("UserName");
            StartUpTemplateClass.Password = ConfigController.GetConfig("Password");
            StartUpTemplateClass.HeadPortrait = Base64Controller.Base64ToImage(ConfigController.GetConfig("HeadPortrait"));
             */
            StartUpController.GetStartUpPlugin(UnityModule.StartUpDirectory);

            Application.Run(new StartUpForm());
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

    }
}
