using System;

namespace HackSystem.Host.EventHandlers
{
    public static class ApplicationExitHandler
    {
        public static void OnEvent(object sender, EventArgs e)
            => Console.WriteLine($"{nameof(ApplicationExitHandler)}: {nameof(OnEvent)}");
    }
}
