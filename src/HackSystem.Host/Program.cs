using System;
using System.Windows.Forms;
using HackSystem.Host.EventHandlers;

namespace HackSystem.Host
{
    static class Program
    {
        /// <summary>
        /// Main entry of program
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ApplicationExit += ApplicationExitHandler.DoApplicationExit;
            Application.ThreadException += ApplicationThreadExceptionHandler.DoApplicationThreadException;
            AppDomain.CurrentDomain.UnhandledException += AppDomainExceptionHandler.DoAppDomainException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HostForm());
        }
    }
}
