namespace HackSystem.Web.Authentication.TokenHandlers;

public interface IHackSystemAuthenticationTokenRefresher
{
    bool IsRunning { get; }

    void StartRefresher();

    void StopRefresher();

    Task<string> RefreshTokenAsync();
}
