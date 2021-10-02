using HackSystem.Web.Authentication.AuthorizationStateHandlers;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Options;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.TokenHandlers;

public class HackSystemAuthenticationTokenRefresher : IHackSystemAuthenticationTokenRefresher
{
    private readonly ILogger<HackSystemAuthenticationTokenRefresher> logger;
    private readonly IHackSystemAuthenticationStateUpdater hackSystemAuthenticationStateHandler;
    private readonly IHackSystemAuthenticationTokenHandler hackSystemAuthenticationTokenHandler;
    private readonly Timer timer;
    private readonly IOptionsMonitor<HackSystemAuthenticationOptions> options;
    private readonly HttpClient httpClient;
    private readonly int period;

    public HackSystemAuthenticationTokenRefresher(
        ILogger<HackSystemAuthenticationTokenRefresher> logger,
        IServiceScopeFactory serviceScopeFactory,
        IOptionsMonitor<HackSystemAuthenticationOptions> options)
    {
        this.logger = logger;
        this.options = options;
        this.period = options.CurrentValue.TokenRefreshInMinutes * 1000 * 60;
        this.timer = new Timer(new TimerCallback(this.RefreshTokenCallBack), null, Timeout.Infinite, period);

        var provider = serviceScopeFactory.CreateScope().ServiceProvider;
        this.httpClient = provider.GetService<HttpClient>();
        this.hackSystemAuthenticationStateHandler = provider.GetService<AuthenticationStateProvider>() as IHackSystemAuthenticationStateUpdater;
        this.hackSystemAuthenticationTokenHandler = provider.GetService<IHackSystemAuthenticationTokenHandler>();
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
        this.logger.LogDebug($"Hack System refresh Token ...");
        var currentToken = await this.hackSystemAuthenticationTokenHandler.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(currentToken))
        {
            this.logger.LogDebug($"Current Token is empty, skipping refresh token ...");
            return default;
        }

        this.httpClient.AddAuthorizationHeader(this.options.CurrentValue.AuthenticationScheme, currentToken);
        var response = await httpClient.GetAsync("api/token/refresh");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            this.logger.LogWarning($"Failed to refresh Token: ({(int)response.StatusCode}) {response.StatusCode} => {content}");
            return default;
        }

        await this.hackSystemAuthenticationStateHandler.UpdateAuthenticattionStateAsync(content);
        return content;
    }
}
