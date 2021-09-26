namespace HackSystem.Web.Authentication.TokenHandlers;

public interface IHackSystemAuthenticationTokenHandler
{
    ValueTask<string> GetTokenAsync();

    Task UpdateTokenAsync(string token);

    Task RemoveTokenAsync();
}
