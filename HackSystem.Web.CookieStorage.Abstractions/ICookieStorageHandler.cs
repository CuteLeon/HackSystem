namespace HackSystem.Web.CookieStorage;

public interface ICookieStorageHandler
{
    event EventHandler<CookieChangedEventArgs> CookieChanged;

    ValueTask RemoveCookieAsync(string name);

    void RemoveCookie(string name);

    ValueTask<Dictionary<string, string>> GetAllAsync();

    Dictionary<string, string> GetAll();

    ValueTask SaveCookieAsync(string name, string value, long expiresInSecond = -1);

    void SaveCookie(string name, string value, long expiresInSecond = -1);

    ValueTask<string> GetCookieAsync(string name);

    string GetCookie(string name);
}
