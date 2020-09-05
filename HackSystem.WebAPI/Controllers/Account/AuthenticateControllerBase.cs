using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HackSystem.WebAPI.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackSystem.WebAPI.Controllers.Account
{
    public abstract class AuthenticateControllerBase : Controller
    {
        protected readonly RoleManager<HackSystemRole> roleManager;
        protected readonly UserManager<HackSystemUser> userManager;

        public AuthenticateControllerBase(
            RoleManager<HackSystemRole> roleManager,
            UserManager<HackSystemUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new System.ArgumentException($"“{nameof(userName)}”不能为 Null 或空白", nameof(userName));
            }

            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new System.ArgumentException($"“{nameof(userName)}”不能为 Null 或空白", nameof(userName));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var userClaims = await this.userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var roleNames = await this.userManager.GetRolesAsync(user);
            claims.AddRange(roleNames.Select(role => new Claim(ClaimTypes.Role, role)));
            foreach (var roleName in roleNames)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                var roleClaims = await roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            return claims;
        }
    }
}
