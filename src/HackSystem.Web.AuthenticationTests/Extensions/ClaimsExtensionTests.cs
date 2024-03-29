﻿using System.Security.Claims;
using HackSystem.Web.Authentication.Options;
using Xunit;

namespace HackSystem.Web.Authentication.Extensions.Tests;

public class ClaimsExtensionTests
{
    [Fact()]
    public void IsUnexpiredTest()
    {
        var options = new HackSystemAuthenticationOptions();

        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "Leon") });
        Assert.False(claimsIdentity.Claims.IsExpired(options.ExpiryClaimType), $"{string.Join(";", claimsIdentity.Claims.Select(c => $"{c.Type}:{c.Value}"))} Should not be expiried.");

        claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(options.ExpiryClaimType, Convert.ToInt64((DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1)).TotalSeconds).ToString())
        });
        Assert.False(claimsIdentity.Claims.IsExpired(options.ExpiryClaimType), $"{string.Join(";", claimsIdentity.Claims.Select(c => $"{c.Type}:{c.Value}"))} Should not be expiried.");

        claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(options.ExpiryClaimType, "!@#$%^&*()_+")
        });
        Assert.True(claimsIdentity.Claims.IsExpired(options.ExpiryClaimType), $"{string.Join(";", claimsIdentity.Claims.Select(c => $"{c.Type}:{c.Value}"))} Should be expiried.");

        claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(options.ExpiryClaimType, Convert.ToInt64((DateTime.UtcNow.AddDays(-1) - new DateTime(1970, 1, 1)).TotalSeconds).ToString())
        });
        Assert.True(claimsIdentity.Claims.IsExpired(options.ExpiryClaimType), $"{string.Join(";", claimsIdentity.Claims.Select(c => $"{c.Type}:{c.Value}"))} Should be expiried.");
    }
}
