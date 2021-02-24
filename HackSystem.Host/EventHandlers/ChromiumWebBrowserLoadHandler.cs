using System;
using CefSharp;

namespace HackSystem.Host.EventHandlers
{
    public static class ChromiumWebBrowserLoadHandler
    {
        public static void DoLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            // Console.WriteLine($"{nameof(ChromiumWebBrowserLoadHandler)}: {nameof(DoLoadingStateChanged)} => {e.Browser.MainFrame?.Url} {(e.IsLoading ? "is" : "not")} loading");
        }

        public static void DoFrameLoadStart(object sender, FrameLoadStartEventArgs e)
            => Console.WriteLine($"{nameof(ChromiumWebBrowserLoadHandler)}: {nameof(DoFrameLoadStart)} => {e.Url} {e.TransitionType}");

        public static void DoFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
            => Console.WriteLine($"{nameof(ChromiumWebBrowserLoadHandler)}: {nameof(DoFrameLoadEnd)} => {e.Url} {e.HttpStatusCode}");

        public static void DoLoadError(object sender, LoadErrorEventArgs e)
        {
            if (e.ErrorCode == CefErrorCode.Aborted) return;

            Console.WriteLine($"{nameof(ChromiumWebBrowserLoadHandler)}: {nameof(DoLoadError)} => {e.FailedUrl} {e.ErrorCode} {e.ErrorText}");
            // Show error message in static page when load failed.
            var htmlContent = ChromiumRegisterResourceHandler.GetStartUpPageHtml($"Load Error. (Code = {e.ErrorCode})", $"{e.FailedUrl}<br/>{e.ErrorText}");
            e.Frame.LoadHtml(htmlContent, true);
        }
    }
}
