using System;

namespace HackSystem.Host.EventHandlers
{
    public static class ApplicationExitHandler
    {
        public static void DoApplicationExit(object sender, EventArgs e)
            => Console.WriteLine($"{nameof(ApplicationExitHandler)}: {nameof(DoApplicationExit)}");
    }
}
