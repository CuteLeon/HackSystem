using System;
using HackSystem.Web.Services.API.Authentication;
using HackSystem.Web.Services.API.Program;
using HackSystem.Web.Services.Authentication;
using HackSystem.Web.Services.Configurations;
using HackSystem.Web.Services.Program;
using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.Web.Services.Extensions;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, WebServiceOptions webServiceOptions)
        {
            services.AddHttpClient<IAuthenticationService, AuthenticationService>(httpClient => httpClient.BaseAddress = new Uri(webServiceOptions.APIHost));
            services.AddHttpClient<IBasicProgramService, BasicProgramService>(httpClient => httpClient.BaseAddress = new Uri(webServiceOptions.APIHost));
            return services;
        }
    }
