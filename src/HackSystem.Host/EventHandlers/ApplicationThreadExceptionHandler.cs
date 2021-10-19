using System;
using System.Threading;

namespace HackSystem.Host.EventHandlers
{
    public static class ApplicationThreadExceptionHandler
    {
        public static void DoApplicationThreadException(object sender, ThreadExceptionEventArgs e)
            => Console.WriteLine($"{nameof(ApplicationThreadExceptionHandler)}: {nameof(DoApplicationThreadException)} => {e.Exception.Message}");
    }
}
