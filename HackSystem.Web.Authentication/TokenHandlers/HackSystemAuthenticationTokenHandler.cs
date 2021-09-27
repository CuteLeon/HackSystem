using HackSystem.Web.Authentication.Options;
using HackSystem.Web.CookieStorage;

namespace HackSystem.Web.Authentication.TokenHandlers;

public class HackSystemAuthenticationTokenHandler : IHackSystemAuthenticationTokenHandler
{

    private readonly ILogger<HackSystemAuthenticationTokenHandler> logger;
    private readonly ICookieStorageHandler cookieStorageHandler;
    private readonly IOptionsSnapshot<HackSystemAuthenticationOptions> options;

    public HackSystemAuthenticationTokenHandler(
        ILogger<HackSystemAuthenticationTokenHandler> logger,
        IOptionsSnapshot<HackSystemAuthenticationOptions> options,
        ICookieStorageHandler cookieStorageHandler)
    {
        this.logger = logger;
        this.options = options;
        this.cookieStorageHandler = cookieStorageHandler;
    }

    public async ValueTask<string> GetTokenAsync()
    {
        this.logger.LogInformation("HackSystem Get Token ...");
        return await this.cookieStorageHandler.GetCookieAsync(this.options.Value.AuthTokenName);
    }

    public async Task UpdateTokenAsync(string token)
    {
        this.logger.LogInformation("HackSystem Update Token ...");
        await this.cookieStorageHandler.SaveCookieAsync(this.options.Value.AuthTokenName, token, this.options.Value.TokenExpiryInMinutes * 60);
    }

    public async Task RemoveTokenAsync()
    {
        this.logger.LogInformation("HackSystem Remove Token ...");
        await this.cookieStorageHandler.RemoveCookieAsync(this.options.Value.AuthTokenName);
    }
}
