using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HackSystem.Web.Authentication.Services;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using Microsoft.AspNetCore.Components.Authorization;
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

        public static WebAssemblyHostBuilder InitConfig(this WebAssemblyHostBuilder builder)
        {
            // TODO: ʹ�����÷���������Ӧ������json�ļ��ڵ�����
            const string ConfigFileName = "appsettings.json";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(ConfigFileName)
                .Build();
            builder.Services.AddSingleton(configuration);
            return builder;
        }

        public static WebAssemblyHostBuilder InitService(this WebAssemblyHostBuilder builder)
        {
            // ��־����
#if DEBUG
            builder.Services.AddLogging(options => options.SetMinimumLevel(LogLevel.Debug));
#else
            services.AddLogging(options => options.SetMinimumLevel(LogLevel.Warning));
#endif

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
                httpClient.BaseAddress = new Uri("https://localhost:4237");
            });

            return builder;
        }
    }
}
