using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace HackSystem.Web.Authentication.Extensions
{
    public static class ClaimsExtension
    {
        public static bool IsUnexpired(this IEnumerable<Claim> claims, string expiryClaimType)
        {
            var expiryClaim = claims.FirstOrDefault(claim => string.Equals(claim.Type, expiryClaimType, StringComparison.OrdinalIgnoreCase));
            if (expiryClaim == null)
            {
                return true;
            }

            if (!long.TryParse(expiryClaim.Value, out var expiryTimeStamp))
            {
                return true;
            }

            var localExpiredTime = TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(1970, 1, 1) + TimeSpan.FromSeconds(expiryTimeStamp),
                TimeZoneInfo.Local);
            return localExpiredTime >= DateTime.Now;
        }
    }
}
