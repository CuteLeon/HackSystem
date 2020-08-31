using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackSystem.Web.Services.Storage
{
    public interface ICookieStorageService
    {
        event EventHandler<CookieChangedEventArgs> CookieChanged;

        ValueTask RemoveCookieAsync(string name);

        void RemoveCookie(string name);

        ValueTask<Dictionary<string, string>> GetAllAsync();

        Dictionary<string, string> GetAll();

        ValueTask SaveCookieAsync(string name, string value, int expiresInSecond = -1);

        void SaveCookie(string name, string value, int expiresInSecond = -1);

        ValueTask<string> GetCookieAsync(string name);

        string GetCookie(string name);
    }
}
