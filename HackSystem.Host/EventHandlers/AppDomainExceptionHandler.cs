using System;

namespace HackSystem.Host.EventHandlers
{
    public static class AppDomainExceptionHandler
    {
        public static void DoAppDomainException(object sender, UnhandledExceptionEventArgs e)
            => Console.WriteLine($"{nameof(ApplicationThreadExceptionHandler)}: {nameof(DoAppDomainException)} => {(e.ExceptionObject as Exception).Message}");
    }
}
