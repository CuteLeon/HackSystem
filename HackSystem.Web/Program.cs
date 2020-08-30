using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Authentication.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.InitService();

            await builder.Build().RunAsync();
        }

        public static WebAssemblyHostBuilder InitService(this WebAssemblyHostBuilder builder)
        {
            // ��־����
#if DEBUG
            builder.Services.AddLogging(options => options.SetMinimumLevel(LogLevel.Debug));
#else
            services.AddLogging(options => options.SetMinimumLevel(LogLevel.Warning));
#endif
            // JWT ������
            builder.Services.AddScoped<IJWTParser, JWTParser>();
            // �ڱ��ش洢 Tokwn
            builder.Services.AddBlazoredLocalStorage();
            // ���������֤����
            builder.Services.AddAuthorizationCore();
            // ע���û������֤״̬�ṩ��
            builder.Services.AddScoped<AuthenticationStateProvider, HackSystemAuthenticationStateProvider>();
            // ע��ָ��Ӧ�õ� HttpClient ����
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // ע����֤������ HttpClient ����
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                httpClient.BaseAddress = new Uri(configuration["APIURL"]);
            });

            return builder;
        }
    }
}
