using System.Security.Claims;
using HackSystem.Web.Authentication.ClaimsIdentityHandlers;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.TokenHandlers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.AuthorizationStateHandlers
{
    public class HackSystemAuthenticationStateUpdater : IHackSystemAuthenticationStateUpdater
    {
        private readonly ILogger<HackSystemAuthenticationStateUpdater> logger;
        private readonly IOptionsSnapshot<HackSystemAuthenticationOptions> options;
        private readonly IJsonWebTokenParser jsonWebTokenParser;
        private readonly IHackSystemAuthenticationStateProvider authenticationStateProvider;
        private readonly IHackSystemAuthenticationTokenHandler hackSystemAuthenticationTokenHandler;
        private readonly IHackSystemClaimsIdentityValidator hackSystemClaimsIdentityValidator;

        public HackSystemAuthenticationStateUpdater(
            ILogger<HackSystemAuthenticationStateUpdater> logger,
            IOptionsSnapshot<HackSystemAuthenticationOptions> options,
            IJsonWebTokenParser jsonWebTokenParser,
            AuthenticationStateProvider authenticationStateProvider,
            IHackSystemAuthenticationTokenHandler hackSystemAuthenticationTokenHandler,
            IHackSystemClaimsIdentityValidator hackSystemClaimsIdentityValidator)
        {
            this.logger = logger;
            this.options = options;
            this.hackSystemAuthenticationTokenHandler = hackSystemAuthenticationTokenHandler;
            this.jsonWebTokenParser = jsonWebTokenParser;
            this.authenticationStateProvider = authenticationStateProvider as IHackSystemAuthenticationStateProvider;
            this.hackSystemClaimsIdentityValidator = hackSystemClaimsIdentityValidator;
        }

        protected bool ParseValidateClaimsIdentity(string token, out ClaimsIdentity claimsIdentity)
        {
            var claims = this.jsonWebTokenParser.ParseJWTToken(token);
            claimsIdentity = new ClaimsIdentity(claims, this.options.Value.AuthenticationType);
            return this.hackSystemClaimsIdentityValidator.ValidateClaimsIdentity(claimsIdentity);
        }

        /// <summary>
        /// Update current user's authentication state
        /// </summary>
        /// <param name="token"></param>
        public async Task UpdateAuthenticattionStateAsync(string token)
        {
            this.logger.LogInformation("HackSystem Update Authentication State...");
            if (string.IsNullOrWhiteSpace(token) ||
                !ParseValidateClaimsIdentity(token, out var claimsIdentity))
            {
                await this.AuthenticateFailed();
                return;
            }

            await this.AuthenticateSuccessfully(token, claimsIdentity);
        }

        #region Update authentication information

        /// <summary>
        /// Authenticate successfully
        /// </summary>
        private async Task AuthenticateSuccessfully(string token, ClaimsIdentity claimsIdentity)
        {
            this.logger.LogInformation("HackSystem Authenticate Successfully !");
            await this.hackSystemAuthenticationTokenHandler.UpdateTokenAsync(token);
            var authenticationState = new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
            this.authenticationStateProvider.NotifyAuthenticationStateChanged(authenticationState);
        }

        /// <summary>
        /// Authenticate failed
        /// </summary>
        private async Task AuthenticateFailed()
        {
            this.logger.LogWarning("HackSystem Authenticate Failed !");
            await this.hackSystemAuthenticationTokenHandler.RemoveTokenAsync();
            this.authenticationStateProvider.NotifyAuthenticationStateChanged(this.options.Value.AnonymousState);
        }
        #endregion
    }
}
