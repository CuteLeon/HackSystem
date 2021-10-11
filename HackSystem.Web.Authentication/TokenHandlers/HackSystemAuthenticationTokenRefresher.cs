using HackSystem.Web.Authentication.AuthorizationStateHandlers;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.WebServices;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.TokenHandlers;

public class HackSystemAuthenticationTokenRefresher : IHackSystemAuthenticationTokenRefresher
{
    private readonly int period;
    private readonly Timer timer;
    private readonly HttpClient httpClient;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<HackSystemAuthenticationTokenRefresher> logger;
    private readonly IOptionsMonitor<HackSystemAuthenticationOptions> options;
    private readonly IHackSystemAuthenticationStateUpdater hackSystemAuthenticationStateHandler;

    public HackSystemAuthenticationTokenRefresher(
        ILogger<HackSystemAuthenticationTokenRefresher> logger,
        IOptionsMonitor<HackSystemAuthenticationOptions> options,
        IServiceScopeFactory serviceScopeFactory,
        IHttpClientFactory httpClientFactory)
    {
        this.logger = logger;
        this.options = options;
        this.httpClientFactory = httpClientFactory;
        this.period = options.CurrentValue.TokenRefreshInMinutes * 1000 * 60;
        this.timer = new Timer(new TimerCallback(this.RefreshTokenCallBack), null, Timeout.Infinite, period);

        var provider = serviceScopeFactory.CreateScope().ServiceProvider;
        this.httpClient = httpClientFactory.CreateClient(AuthenticatedServiceBase.AuthenticatedClientName);
        this.hackSystemAuthenticationStateHandler = provider.GetService<AuthenticationStateProvider>() as IHackSystemAuthenticationStateUpdater;
    }

    public bool IsRunning { get; private set; }

    public void StartRefresher()
    {
        this.IsRunning = true;

        // Set first paramter as 0 to invoke once immediately.
        this.timer.Change(5 * 1000, period);
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
        this.logger.LogInformation($"Hack System refresh Token ...");
        var response = await httpClient.GetAsync("api/token/refresh");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            this.logger.LogWarning($"Failed to refresh Token: ({(int)response.StatusCode}) {response.StatusCode} => {content}");
            return default;
        }

        await this.hackSystemAuthenticationStateHandler.UpdateAuthenticattionStateAsync(content);
        this.logger.LogInformation($"Hack System refresh and update Token successfully.");
        return content;
    }
}
