namespace HackSystem.Web.CookieStorage;

/// <summary>
/// Cookie storage service
/// </summary>
/// <remarks> Should work with blazor.cookie.js </remarks>
public class CookieStorageHandler : ICookieStorageHandler
{
    public event EventHandler<CookieChangedEventArgs> CookieChanged;
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public CookieStorageHandler(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
           "import", "./_content/HackSystem.Web.CookieStorage/blazor.cookie.js").AsTask());
    }

    /// <summary>
    /// Get all Cookie
    /// </summary>
    /// <returns></returns>
    public async ValueTask<Dictionary<string, string>> GetCookiesAsync()
    {
        var module = await moduleTask.Value;
        var cookies = await module.InvokeAsync<Dictionary<string, string>>("getCookies");
        return cookies;
    }

    /// <summary>
    /// Get Cookie
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns></returns>
    public async ValueTask<string> GetCookieAsync(string name)
    {
        var module = await moduleTask.Value;
        var cookie = await module.InvokeAsync<string>("getCookie", name);
        return cookie;
    }

    /// <summary>
    /// Remove Cookie
    /// </summary>
    /// <param name="name">Name</param>
    public async ValueTask RemoveCookieAsync(string name)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("removeCookie", name);
    }

    /// <summary>
    /// Save Cookie
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="value">Date</param>
    /// <param name="expiresInSecond">Expiry in second</param>
    /// <remarks>
    /// Cookie's data type should be string.
    /// Cookie with no expiry will never expires, unitil session terminated.
    /// Cookie's name is case sensitive, use exactly the same name.
    /// Space before or after Cookie's name will be ignored.
    /// Cookie's name and value should contain ';' or '='.
    /// </remarks>
    public async ValueTask SaveCookieAsync(string name, string value, long expiresInSecond = -1)
    {
        var oldValue = await this.GetCookieAsync(name);
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("saveCookie", name, value, expiresInSecond);
        this.RaiseOnChanged(name, oldValue, value);
    }

    private void RaiseOnChanged(string name, string oldValue, string newValue)
    {
        var eventArgs = new CookieChangedEventArgs(name, newValue, oldValue);
        this.CookieChanged?.Invoke(this, eventArgs);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
