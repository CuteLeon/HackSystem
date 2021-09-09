using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.ProgramDevelopmentKit.Authentication;

public interface IDevelopmentKitAuthenticationStateProvider
{
    AuthenticationState AuthenticationState { get; set; }
}
