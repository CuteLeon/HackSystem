using Microsoft.Extensions.DependencyInjection;
using HackSystem.Web.Authentication.Providers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HackSystem.Web.Extensions
{
    /// <summary>
    /// Launch basic services
    /// </summary>
    /// <remarks>
    /// Each of tab of browser has a WebAssembly runtime which the Blazor WebAssembly application runs in.
    /// A new WebAssembly runtime instance will be create when start a new tab of browser or refresh current tab.
    /// Navigating to a new page address of current applicaiotn will keep current WebAssembly runtime instance in this tab.
    /// </remarks>
    public static class LaunchBasicServicesExtension
    {
        public static WebAssemblyHost LaunchBasicServices(this WebAssemblyHost host)
        {
            var refresher = host.Services.GetService<IHackSystemAuthenticationTokenRefresher>();
            refresher.StartRefresher();

            return host;
        }
    }
}
