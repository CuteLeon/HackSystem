using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HackSystem.Web.CookieStorage
{
    /// <summary>
    /// Cookie 存储服务
    /// </summary>
    /// <remarks> 需要搭配 blazor.cookie.js 使用 </remarks>
    public class CookieStorageService : ICookieStorageService
    {
        public event EventHandler<CookieChangedEventArgs> CookieChanged;
        private readonly IJSInProcessRuntime jsInProcessRuntime;

        public CookieStorageService(IJSRuntime jsRuntime)
        {
            this.jsInProcessRuntime = (jsRuntime as IJSInProcessRuntime);
        }

        /// <summary>
        /// 获取全部 Cookie
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAll()
        {
            var cookies = this.jsInProcessRuntime.Invoke<Dictionary<string, string>>("cookies.getAll");
            return cookies;
        }

        /// <summary>
        /// 获取全部 Cookie
        /// </summary>
        /// <returns></returns>
        public async ValueTask<Dictionary<string, string>> GetAllAsync()
        {
            var cookies = await this.jsInProcessRuntime.InvokeAsync<Dictionary<string, string>>("cookies.getAll");
            return cookies;
        }

        /// <summary>
        /// 获取 Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public string GetCookie(string name)
        {
            var cookie = this.jsInProcessRuntime.Invoke<string>("cookies.getCookie", name);
            return cookie;
        }

        /// <summary>
        /// 获取 Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public async ValueTask<string> GetCookieAsync(string name)
        {
            var cookie = await this.jsInProcessRuntime.InvokeAsync<string>("cookies.getCookie", name);
            return cookie;
        }

        /// <summary>
        /// 移除 Cookie
        /// </summary>
        /// <param name="name">名称</param>
        public void RemoveCookie(string name)
        {
            this.jsInProcessRuntime.InvokeVoid("cookies.removeCookie", name);
        }

        /// <summary>
        /// 移除 Cookie
        /// </summary>
        /// <param name="name">名称</param>
        public async ValueTask RemoveCookieAsync(string name)
        {
            await this.jsInProcessRuntime.InvokeVoidAsync("cookies.removeCookie", name);
        }

        /// <summary>
        /// 保存 Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">数据</param>
        /// <param name="expiresInSecond">期限(秒)</param>
        /// <remarks>
        /// Cookie 数据类型为字符串
        /// Cookie 不设置过期时间则永远有效，知道会话结束
        /// Cookie 的 Name 对大小写敏感，需要正确指定大小写
        /// Cookie 的 Name 前后的空格将被忽略
        /// Cookie 的 Name 和 Value 不允许含有英文分号和等于号
        /// </remarks>
        public void SaveCookie(string name, string value, long expiresInSecond = -1)
        {
            this.jsInProcessRuntime.InvokeVoid("cookies.saveCookie", name, value, expiresInSecond);

            var oldValue = this.GetCookie(name);
            this.RaiseOnChanged(name, oldValue, value);
        }

        /// <summary>
        /// 保存 Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">数据</param>
        /// <param name="expiresInSecond">期限(秒)</param>
        /// <remarks>
        /// Cookie 数据类型为字符串
        /// Cookie 不设置过期时间则永远有效，知道会话结束
        /// Cookie 的 Name 对大小写敏感，需要正确指定大小写
        /// Cookie 的 Name 前后的空格将被忽略
        /// Cookie 的 Name 和 Value 不允许含有英文分号和等于号
        /// </remarks>
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
