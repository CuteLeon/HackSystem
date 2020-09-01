using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.Providers
{
    public interface IHackSystemAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();

        ValueTask<string> GetCurrentTokenAsync();

        Task UpdateAuthenticattionStateAsync(string token);
    }
}
