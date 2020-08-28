using System;

namespace HackSystem.Host.EventHandlers
{
    public static class AppDomainExceptionHandler
    {
        public static void OnEvent(object sender, UnhandledExceptionEventArgs e)
            => Console.WriteLine($"{nameof(ApplicationThreadExceptionHandler)}: {nameof(OnEvent)} => {(e.ExceptionObject as Exception).Message}");
    }
}
