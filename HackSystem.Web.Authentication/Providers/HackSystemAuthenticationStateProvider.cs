using System.Security.Claims;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.CookieStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackSystem.Web.Authentication.Providers;

    /// <summary>
    /// User authentication state provider
    /// </summary>
    public class HackSystemAuthenticationStateProvider : AuthenticationStateProvider, IHackSystemAuthenticationStateProvider
    {
        private readonly ILogger<HackSystemAuthenticationStateProvider> logger;
        private readonly IJWTParserService jwtParser;
        private readonly HackSystemAuthenticationOptions options;
        private readonly ICookieStorageService cookieStorageService;

        public HackSystemAuthenticationStateProvider(
            ILogger<HackSystemAuthenticationStateProvider> logger,
            IOptionsMonitor<HackSystemAuthenticationOptions> options,
            IJWTParserService jwtParser,
            ICookieStorageService cookieStorageService)
        {
            this.logger = logger;
            this.options = options.CurrentValue;
            this.jwtParser = jwtParser;
            this.cookieStorageService = cookieStorageService;
        }

        #region Get Authentication Information

        /// <summary>
        /// Get current user's authentication state
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            this.logger.LogInformation("HackSystem Get Authentication State...");
            var savedToken = await this.GetCurrentTokenAsync();
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return this.ReturnAnonymousState();
            }

            var claimsIdentity = this.ParseClaimsIdentity(savedToken);
            if (!this.CheckClaimsIdentity(claimsIdentity))
            {
                return this.ReturnAnonymousState();
            }

            var user = new ClaimsPrincipal(claimsIdentity);
            return this.ReturnAuthebticatedState(user);
        }

        /// <summary>
        /// Get current token
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetCurrentTokenAsync()
        {
            this.logger.LogDebug("HackSystem Get Current Token.");
            return await this.cookieStorageService.GetCookieAsync(this.options.AuthTokenName);
        }
        #endregion

        #region Return authentication state

        /// <summary>
        /// Return anonymous state
        /// </summary>
        /// <returns></returns>
        private AuthenticationState ReturnAnonymousState()
        {
            this.logger.LogInformation("HackSystem Return Anonymous State.");
            return this.options.AnonymousState;
        }

        /// <summary>
        /// Return authebticated state
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        private AuthenticationState ReturnAuthebticatedState(ClaimsPrincipal claimsPrincipal)
        {
            this.logger.LogInformation("HackSystem Return Authebticated State.");
            return new AuthenticationState(claimsPrincipal);
        }

        #endregion

        #region Update authentication information

        /// <summary>
        /// Notify authentication state changed
        /// </summary>
        /// <param name="authenticationState"></param>
        /// <returns></returns>
        public void NotifyAuthenticationStateChanged(AuthenticationState authenticationState)
        {
            this.logger.LogInformation($"HackSystem Notify Authentication State Change to {authenticationState.User?.Identity?.Name ?? "[null]"}");
            this.NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }
        #endregion

        #region Parse and check

        /// <summary>
        /// Parse claims identity
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsIdentity ParseClaimsIdentity(string token)
        {
            this.logger.LogDebug("HackSystem Get Claims Identity from Token...");
            var claims = this.jwtParser.ParseJWTToken(token);
            return new ClaimsIdentity(claims, this.options.AuthenticationType);
        }

        /// <summary>
        /// Check claims identity
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        public bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity)
        {
            this.logger.LogDebug("HackSystem Check Claims Identity...");
            return !claimsIdentity.Claims.IsExpired(this.options.ExpiryClaimType);
        }
        #endregion
    }
