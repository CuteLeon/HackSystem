namespace HackSystem.Web.Authentication.AuthorizationStateHandlers;

public interface IHackSystemAuthenticationStateUpdater
{
    Task UpdateAuthenticattionStateAsync(string token);
}
