using System;
using System.Threading;

namespace HackSystem.Host.EventHandlers
{
    public static class ApplicationThreadExceptionHandler
    {
        public static void OnEvent(object sender, ThreadExceptionEventArgs e)
            => Console.WriteLine($"{nameof(ApplicationThreadExceptionHandler)}: {nameof(OnEvent)} => {e.Exception.Message}");
    }
}
