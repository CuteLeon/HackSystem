using Microsoft.Extensions.DependencyInjection;
using HackSystem.Web.Authentication.Providers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HackSystem.Web.Extensions
{
    /// <summary>
    /// 启动基础服务
    /// </summary>
    /// <remarks>
    /// 浏览器的每个标签内运行一个 WebAssembly 运行时，并在运行时内运行.Net Blazor WASM 程序
    /// 新建浏览器标签或刷新当前浏览器标签都将会创建新的运行时环境和应用程序进程
    /// 在当前标签转跳同一应用程序的页面并将会保持当前运行的应用程序进程
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
