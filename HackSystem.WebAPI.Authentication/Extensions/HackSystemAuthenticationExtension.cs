using System;
using System.Text;
using System.Threading.Tasks;
using HackSystem.WebAPI.Authentication.Configurations;
using HackSystem.WebAPI.Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HackSystem.Web.Authentication.Extensions
{
    public static class HackSystemAuthenticationExtension
    {
        public static IServiceCollection AddAPIAuthentication(
            this IServiceCollection services,
            JwtAuthenticationOptions configuration)
        {
            services
                .Configure(new Action<JwtAuthenticationOptions>(options =>
                {
                    options.JwtAudience = configuration.JwtAudience;
                    options.JwtExpiryInMinutes = configuration.JwtExpiryInMinutes;
                    options.JwtIssuer = configuration.JwtIssuer;
                    options.JwtSecurityKey = configuration.JwtSecurityKey;
                }))
                .AddScoped<ITokenGenerator, TokenGenerator>()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents()
                    {
                        OnForbidden = context => Task.CompletedTask,
                        OnAuthenticationFailed = context => Task.CompletedTask,
                    };
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // 修正过期时间偏移
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.JwtIssuer,
                        ValidAudience = configuration.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JwtSecurityKey))
                    };
                });

            return services;
        }
    }
}
