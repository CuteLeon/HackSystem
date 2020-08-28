using System;
using System.Windows.Forms;
using HackSystem.Host.EventHandlers;

namespace HackSystem.Host
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ApplicationExit += ApplicationExitHandler.OnEvent;
            Application.ThreadException += ApplicationThreadExceptionHandler.OnEvent;
            AppDomain.CurrentDomain.UnhandledException += AppDomainExceptionHandler.OnEvent;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HostForm());
        }
    }
}
