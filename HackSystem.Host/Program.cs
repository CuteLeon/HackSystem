using System;
using System.Windows.Forms;
using HackSystem.Host.EventHandlers;

namespace HackSystem.Host
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ApplicationExit += ApplicationExitHandler.OnEvent;
            Application.ThreadException += ApplicationThreadExceptionHandler.OnEvent;

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HostForm());
        }
    }
}
