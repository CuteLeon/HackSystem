// 跳过启动画面
#define SkipStartUp
// 跳过登录界面
#define SkipLogon
// 捕获全局异常
#undef CatchException

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using HackSystem.LogonTemplate;
using HackSystem.StartUpTemplate;

namespace HackSystem.Host
{
    static class Program
    {
        /// <summary>
        /// 允许运行此进程
        /// </summary>
        private static bool CreateNew = true;
        /// <summary>
        /// 创建互斥体，使程序仅可单实例运行；
        /// </summary>
        private static readonly Mutex UnityMutex = new Mutex(true, Application.ProductName, out CreateNew);

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

        private volatile static LogonTemplateClass _Logon = null;
        /// <summary>
        /// 系统的登录界面
        /// </summary>
        public static LogonTemplateClass Logon
        {
            get => _Logon;
            set
            {
                _Logon = value;
                LogController.Debug("系统当前 StartUp : {0} ({1})", value.Name, value.Description);
            }
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //检查单实例进程运行
            if (!CreateNew)
            {
                Debug.Print("已经有实例运行，程序即将退出...");
                MessageBox.Show("已经有实例运行，程序即将退出...");
                return;
            }
            Application.AddMessageFilter(new UnityMessageFilter());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;
#if (CatchException)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif

            //创建日志监听器
            LogController.CreateLogListener(UnityModule.LogDirectory, Application.ProductName);

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

#if (!SkipStartUp)
            //初始化目录
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
#endif

#if (!SkipLogon)
            //初始化登录界面
            try
            {
                Logon = InitializeLogon();
            }
            catch (Exception ex)
            {
                LogController.Fatal("初始化 Logon 遇到异常 : {0}", ex.Message);
                MessageBox.Show("初始化 Logon 遇到异常 : " + ex.Message);
                return;
            }

            //Logon.LogonForm.TopMost = true;
            Logon.LogonForm.ShowDialog();
            GC.Collect();
#endif
            
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
                UnityModule.LogonDirectory,
                UnityModule.ProgramDirectory,
            })
            {
                LogController.Debug("检查目录：{0}", TargetDirectory);
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
            LogController.Debug("释放互斥体...");
            UnityMutex?.ReleaseMutex();
            LogController.Info("程序退出 ...");
            LogController.CloseLogListener();
        }

        private static void SwitchToLogon(object s, EventArgs e)
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

            StartUpInstance.StartUpFinished += new EventHandler<EventArgs>((s, e) => { SwitchToLogon(s, e); });
            return StartUpInstance;
        }

        /// <summary>
        /// 初始化登录界面
        /// </summary>
        private static LogonTemplateClass InitializeLogon()
        {
            LogonTemplateClass.LogonIcon = UnityResource.HackSystemLogoIcon;

            LogonTemplateClass.UserName = ConfigController.GetConfig("UserName");
            LogonTemplateClass.Password = ConfigController.GetConfig("Password");
            LogonTemplateClass.HeadPortrait = Base64Controller.Base64ToImage( ConfigController.GetConfig("HeadPortrait"));

            string LogonPath = FileController.PathCombine(UnityModule.LogonDirectory, ConfigController.GetConfig("LogonFile"));
            LogonTemplateClass LogonInstance = LogonController.GetLogonPlugin(
                LogonPath,
                ConfigController.GetConfig("LogonName"));

            //无法创建指定DLL内指定CLASS的Logon对象时，尝试扫描整个目录
            if (LogonInstance == null)
            {
                LogController.Error("无法创建指定的 Logon，尝试扫描 Logon 目录 ...", LogController.LogTypes.ERROR);
                LogonInstance = LogonController.ScanLogonPlugins(UnityModule.LogonDirectory).FirstOrDefault();
            }

            if (LogonInstance == null)
                throw new Exception("无法创建 Logon 对象");
            if (LogonInstance.LogonForm == null)
                throw new Exception("无法创建 Logon.LogonForm 对象");

            LogonInstance.LogonFinished += new EventHandler<EventArgs>((s, e) => { SwitchToLogon(s, e); });
            return LogonInstance;
        }

    }
}
