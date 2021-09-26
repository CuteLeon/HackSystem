using System.Security.Claims;

namespace HackSystem.Web.Authentication.ClaimsIdentityHandlers;

public interface IHackSystemClaimsIdentityValidator
{
    bool ValidateClaimsIdentity(ClaimsIdentity claimsIdentity);
}
