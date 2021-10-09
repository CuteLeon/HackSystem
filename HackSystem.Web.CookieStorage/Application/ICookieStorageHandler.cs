namespace HackSystem.Web.CookieStorage;

public interface ICookieStorageHandler : IAsyncDisposable
{
    event EventHandler<CookieChangedEventArgs> CookieChanged;

    ValueTask RemoveCookieAsync(string name);

    ValueTask<Dictionary<string, string>> GetCookiesAsync();

    ValueTask SaveCookieAsync(string name, string value, long expiresInSecond = -1);

    ValueTask<string> GetCookieAsync(string name);
}
