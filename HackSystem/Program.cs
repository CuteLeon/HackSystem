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
                LogController.Debug("系统当前 StartUp : {0} ({1})", value.Name, value.Description);
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
            Application.ApplicationExit += Application_ApplicationExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //创建日志监听器
            LogController.CreateLogListener(UnityModule.LogDirectory, Application.ProductName);

            //初始化目录
            try
            {
                CheckDirectory();
            }
            catch (Exception ex)
            {
                LogController.Fatal("创建工作目录遇到异常：{0}", ex.Message);
                MessageBox.Show("创建工作目录遇到异常："+ex.Message);
                Application.Exit();
            }

            //补齐默认配置
            ConfigController.LoadDefaultConfig();

            /* TODO : 放到登录窗口
            StartUpTemplateClass.UserName = ConfigController.GetConfig("UserName");
            StartUpTemplateClass.Password = ConfigController.GetConfig("Password");
            StartUpTemplateClass.HeadPortrait = Base64Controller.Base64ToImage(ConfigController.GetConfig("HeadPortrait"));
             */

            //初始化启动画面
            try
            {
                StartUp = InitializeStartUp();
            }
            catch (Exception ex)
            {
                LogController.Fatal("初始化 StartUp 遇到异常 : {0}", ex.Message);
                MessageBox.Show("初始化 StartUp 遇到异常 : "+ ex.Message);
                return;
            }

            //StartUp.StartUpForm.TopMost = true;
            StartUp.StartUpForm.ShowDialog();
            GC.Collect();

            Application.Run(new DesktopForm());
        }

        /// <summary>
        /// 初始化目录
        /// </summary>
        private static void CheckDirectory()
        {
            LogController.Info("检查工作目录 ...");

            //TODO : 程序需要的目录在这里初始化
            foreach (string TargetDirectory in new string[] {
                UnityModule.StartUpDirectory,
            })
            {
                try
                {
                    if (!Directory.Exists(TargetDirectory))
                        Directory.CreateDirectory(TargetDirectory);
                }catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception UnhandledException = e.ExceptionObject as Exception;
            string ExceptionDescription = string.Format(
                "应用域内发现未被捕获的异常：\r\n"+
                "   异常类型 : {0}\r\n" +
                "   异常地址 : {1}\r\n" +
                "   异常信息 : {2}\r\n" +
                "   调用堆栈 : \r\n{3}\r\n" +
                "   即将终止 : {4}",
                UnhandledException.GetType().ToString(),
                UnhandledException.Source,
                UnhandledException.Message,
                UnhandledException.StackTrace,
                e.IsTerminating
                );
            LogController.Fatal(ExceptionDescription);
            MessageBox.Show(string.Format(
                "{0} \n——————————————\n日志文件：{1}\n请联系作者：{2}\n或把问题提交在 : {3}\n\t感谢您的帮助！" ,
                ExceptionDescription,
                LogController.GetLogPath(),
                "Leon.ID@QQ.COM",
                "http://www.GitHub.com/CuteLeon/HackSystem"));
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            LogController.Info("程序退出 ...");
            LogController.CloseLogListener();
        }

        private static void SwitchToLogin(object s, EventArgs e)
        {
            LogController.Info("启动完成！");
            
        }

        /// <summary>
        /// 初始化启动画面
        /// </summary>
        private static StartUpTemplateClass InitializeStartUp()
        {
            StartUpTemplateClass.StartUpIcon = UnityResource.HackSystemLogoIcon;
            string StartUpPath = FileController.PathCombine(UnityModule.StartUpDirectory, ConfigController.GetConfig("StartUpFile"));
            StartUpTemplateClass StartUpInstance = StartUpController.GetStartUpPlugin(
                StartUpPath,
                ConfigController.GetConfig("StartUpName"));

            //无法创建指定DLL内指定CLASS的StartUp对象时，尝试扫描整个目录
            if (StartUpInstance == null)
            {
                LogController.Error("无法创建指定的 StartUp，尝试扫描 StartUp 目录 ...", LogController.LogTypes.ERROR);
                StartUpInstance = StartUpController.ScanStartUpPlugins(UnityModule.StartUpDirectory).FirstOrDefault();
            }

            if (StartUpInstance == null)
                throw new Exception("无法创建 StartUp 对象");
            if (StartUpInstance.StartUpForm == null)
                throw new Exception("无法创建 StartUp.StartUpForm 对象");

            StartUpInstance.StartUpFinished += new EventHandler<EventArgs>((s, e) => { SwitchToLogin(s, e); });
            return StartUpInstance;
        }

    }
}
