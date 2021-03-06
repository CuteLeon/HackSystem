﻿using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.Web.CookieStorage
{
    public static class CookieStorageExtension
    {
        public static IServiceCollection AddCookieStorage(this IServiceCollection services)
            => services.AddScoped<ICookieStorageService, CookieStorageService>();
    }
}
