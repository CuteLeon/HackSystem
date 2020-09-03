using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackSystem.Web.Authentication.Providers
{
    public class HackSystemAuthenticationTokenRefresher : IHackSystemAuthenticationTokenRefresher
    {
        private readonly ILogger<HackSystemAuthenticationTokenRefresher> logger;
        private readonly IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler;
        private readonly Timer timer;
        private readonly HackSystemAuthenticationOptions configuration;
        private readonly HttpClient httpClient;
        private readonly int period;

        public HackSystemAuthenticationTokenRefresher(
            ILogger<HackSystemAuthenticationTokenRefresher> logger,
            IServiceScopeFactory serviceScopeFactory,
            IOptionsMonitor<HackSystemAuthenticationOptions> configure)
        {
            this.logger = logger;
            this.configuration = configure.CurrentValue;

            var scope = serviceScopeFactory.CreateScope();
            this.hackSystemAuthenticationStateHandler = scope.ServiceProvider.GetService<IHackSystemAuthenticationStateHandler>();
            this.httpClient = scope.ServiceProvider.GetService<HttpClient>();

            this.period = this.configuration.TokenRefreshInMinutes * 1000 * 60;
            this.timer = new Timer(new TimerCallback(this.RefreshTokenCallBack), null, Timeout.Infinite, period);
        }

        public bool IsRunning { get; private set; }

        public void StartRefresher()
        {
            this.IsRunning = true;

            // Zero to invoke once immediately.
            this.timer.Change(0, period);
        }

        public void StopRefresher()
        {
            this.IsRunning = false;
            this.timer.Change(Timeout.Infinite, period);
        }

        protected void RefreshTokenCallBack(object state)
        {
            _ = this.RefreshTokenAsync().ConfigureAwait(false);
        }

        public virtual async Task<string> RefreshTokenAsync()
        {
            this.logger.LogInformation($"尝试刷新 Token ...");
            var currentToken = await this.hackSystemAuthenticationStateHandler.GetCurrentTokenAsync();
            if (string.IsNullOrWhiteSpace(currentToken))
            {
                this.logger.LogInformation($"获取当前 Token 为空，跳过刷新 ...");
                return default;
            }

            this.httpClient.AddAuthorizationHeader(this.configuration.AuthenticationScheme, currentToken);
            var response = await httpClient.GetAsync("api/token/refresh");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                this.logger.LogWarning($"刷新 Token 失败：({(int)response.StatusCode}) {response.StatusCode}\n\t{content}");
                return default;
            }

            await this.hackSystemAuthenticationStateHandler.UpdateAuthenticattionStateAsync(content);
            return content;
        }
    }
}
