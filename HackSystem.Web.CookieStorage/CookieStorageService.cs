using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HackSystem.Web.CookieStorage
{
    /// <summary>
    /// Cookie storage service
    /// </summary>
    /// <remarks> Should work with blazor.cookie.js </remarks>
    public class CookieStorageService : ICookieStorageService
    {
        public event EventHandler<CookieChangedEventArgs> CookieChanged;
        private readonly IJSInProcessRuntime jsInProcessRuntime;

        public CookieStorageService(IJSRuntime jsRuntime)
        {
            this.jsInProcessRuntime = (jsRuntime as IJSInProcessRuntime);
        }

        /// <summary>
        /// Get all Cookie
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAll()
        {
            var cookies = this.jsInProcessRuntime.Invoke<Dictionary<string, string>>("cookies.getAll");
            return cookies;
        }

        /// <summary>
        /// Get all Cookie
        /// </summary>
        /// <returns></returns>
        public async ValueTask<Dictionary<string, string>> GetAllAsync()
        {
            var cookies = await this.jsInProcessRuntime.InvokeAsync<Dictionary<string, string>>("cookies.getAll");
            return cookies;
        }

        /// <summary>
        /// Get Cookie
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns></returns>
        public string GetCookie(string name)
        {
            var cookie = this.jsInProcessRuntime.Invoke<string>("cookies.getCookie", name);
            return cookie;
        }

        /// <summary>
        /// Get Cookie
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns></returns>
        public async ValueTask<string> GetCookieAsync(string name)
        {
            var cookie = await this.jsInProcessRuntime.InvokeAsync<string>("cookies.getCookie", name);
            return cookie;
        }

        /// <summary>
        /// Remove Cookie
        /// </summary>
        /// <param name="name">Name</param>
        public void RemoveCookie(string name)
        {
            this.jsInProcessRuntime.InvokeVoid("cookies.removeCookie", name);
        }

        /// <summary>
        /// Remove Cookie
        /// </summary>
        /// <param name="name">Name</param>
        public async ValueTask RemoveCookieAsync(string name)
        {
            await this.jsInProcessRuntime.InvokeVoidAsync("cookies.removeCookie", name);
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
        public void SaveCookie(string name, string value, long expiresInSecond = -1)
        {
            this.jsInProcessRuntime.InvokeVoid("cookies.saveCookie", name, value, expiresInSecond);

            var oldValue = this.GetCookie(name);
            this.RaiseOnChanged(name, oldValue, value);
        }

        /// <summary>
        /// Save Cookie
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="value">Date</param>
        /// <param name="expiresInSecond">Expiry in second</param>
        public async ValueTask SaveCookieAsync(string name, string value, long expiresInSecond = -1)
        {
            await this.jsInProcessRuntime.InvokeVoidAsync("cookies.saveCookie", name, value, expiresInSecond);
        }

        private void RaiseOnChanged(string name, string oldValue, string newValue)
        {
            var eventArgs = new CookieChangedEventArgs
            {
                Name = name,
                OldValue = oldValue,
                NewValue = newValue
            };

            this.CookieChanged?.Invoke(this, eventArgs);
        }
    }
}
