using System;
using CefSharp;

namespace HackSystem.Host.EventHandlers
{
    public static class ChromiumWebBrowserMessageHandler
    {
        public static void DoConsoleMessage(object sender, ConsoleMessageEventArgs e)
            => Console.WriteLine($"{e.Source} [{e.Level}] {e.Line} => {e.Message}");

        public static void DoJavascriptMessage(object sender, JavascriptMessageReceivedEventArgs e)
            => Console.WriteLine($"{e.Browser} => {e.Frame.Name} {e.Message}");
    }
}
