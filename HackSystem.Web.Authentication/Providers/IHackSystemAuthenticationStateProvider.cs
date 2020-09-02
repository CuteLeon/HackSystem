using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.Providers
{
    /// <summary>
    /// HackSystem 认证状态提供者接口
    /// </summary>
    /// <remarks>
    /// 不要尝试将此接口注入DI，并以此接口获取 HackSystemAuthenticationStateProvider 服务，
    /// 否则将在认证时发生莫名的错误（Provider的覆写方法可以返回正确的认证状态，但所有的认证视图组件依旧我行我素）,
    /// 继续使用 AuthenticationStateProvider 类型注册和获取 HackSystemAuthenticationStateProvider 服务,
    /// 因为这个问题上熬夜到 01/09/2020 3:00 AM
    /// </remarks>
    public interface IHackSystemAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();

        ValueTask<string> GetCurrentTokenAsync();

        void NotifyAuthenticationStateChanged(AuthenticationState authenticationState);

        ClaimsIdentity ParseClaimsIdentity(string token);

        bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity);
    }
}
