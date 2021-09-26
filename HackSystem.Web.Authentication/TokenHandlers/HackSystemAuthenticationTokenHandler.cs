using HackSystem.Web.Authentication.Options;
using HackSystem.Web.CookieStorage;

namespace HackSystem.Web.Authentication.TokenHandlers;

public class HackSystemAuthenticationTokenHandler : IHackSystemAuthenticationTokenHandler
{

    private readonly ILogger<HackSystemAuthenticationTokenHandler> logger;
    private readonly ICookieStorageService cookieStorageService;
    private readonly IOptionsSnapshot<HackSystemAuthenticationOptions> options;

    public HackSystemAuthenticationTokenHandler(
        ILogger<HackSystemAuthenticationTokenHandler> logger,
        IOptionsSnapshot<HackSystemAuthenticationOptions> options,
        ICookieStorageService cookieStorageService)
    {
        this.logger = logger;
        this.options = options;
        this.cookieStorageService = cookieStorageService;
    }

    public async ValueTask<string> GetTokenAsync()
    {
        this.logger.LogInformation("HackSystem Get Token ...");
        return await this.cookieStorageService.GetCookieAsync(this.options.Value.AuthTokenName);
    }

    public async Task UpdateTokenAsync(string token)
    {
        this.logger.LogInformation("HackSystem Update Token ...");
        await this.cookieStorageService.SaveCookieAsync(this.options.Value.AuthTokenName, token, this.options.Value.TokenExpiryInMinutes * 60);
    }

    public async Task RemoveTokenAsync()
    {
        this.logger.LogInformation("HackSystem Remove Token ...");
        await this.cookieStorageService.RemoveCookieAsync(this.options.Value.AuthTokenName);
    }
}
