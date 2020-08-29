using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HackSystem.Web.Authentication.Services;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // �ڱ��ش洢 Tokwn
            builder.Services.AddBlazoredLocalStorage();
            // ���������֤����
            builder.Services.AddAuthorizationCore();
            // ע���û������֤״̬�ṩ��
            builder.Services.AddScoped<AuthenticationStateProvider, HackSystemAuthenticationStateProvider>();
            // ע��ָ��Ӧ�õ� HttpClient ����
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // Ϊ Auth ����ע��ִ�� API �� HttpClient ����
            builder.Services.AddScoped(serviceProvider => new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // ע����֤����
            builder.Services.AddHttpClient<IAuthService, AuthService>(httpClient => httpClient.BaseAddress = new Uri("https://localhost:4237"));

            await builder.Build().RunAsync();
        }
    }
}
