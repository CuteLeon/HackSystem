using System;
using System.IO;
using System.Text;
using CefSharp;
using HackSystem.Host.Extensions;

namespace HackSystem.Host.EventHandlers
{
    public static class ChromiumRegisterResourceHandler
    {
        public static void RegisterStartUpPageResource(
            this IWebBrowser webBrowser,
            string url,
            string title,
            string message = "",
            string mimeType = "text/html",
            bool oneTimeUse = false)
        {
            if (webBrowser is null)
            {
                throw new ArgumentNullException(nameof(webBrowser));
            }

            var content = GetStartUpPageHtml(title, message);
            using (var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                webBrowser.RegisterResourceHandler(url, contentStream, mimeType, oneTimeUse);
            }
        }

        public static string GetStartUpPageHtml(string title, string message = "")
        {
            var logoImageBase64 = HostResource.LogoImage.ToBase64();
            var content = string.Format(HostResource.StartUpPage, logoImageBase64, title, message);
            return content;
        }
    }
}
