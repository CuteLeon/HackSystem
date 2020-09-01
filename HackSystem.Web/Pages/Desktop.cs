using System.Linq;
using System.Threading.Tasks;
using HackSystem.Web.Configurations;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace HackSystem.Web.Pages
{
    public partial class Desktop
    {
        private readonly ILogger<Desktop> logger;
        private readonly IJSRuntime jsRuntime;
        private readonly APIConfiguration configuration;
        private readonly IAuthenticationService authenticationService;
        private readonly ICookieStorageService cookieStorageService;

        public Desktop(
            ILogger<Desktop> logger,
            IJSRuntime jsRuntime,
            IOptionsMonitor<APIConfiguration> optionsMonitor,
            IAuthenticationService authenticationService,
            ICookieStorageService cookieStorageService)
        {
            this.logger = logger;
            this.jsRuntime = jsRuntime;
            this.configuration = optionsMonitor.CurrentValue;
            this.authenticationService = authenticationService;
            this.cookieStorageService = cookieStorageService;
        }

        private async Task GetAccountInfo()
        {
            accountInfo = await this.authenticationService.GetAccountInfo();
        }

        private async Task GetAll()
        {
            var cookies = await this.cookieStorageService.GetAllAsync();
            this.logger.LogInformation($"所有 Cookie: {string.Join("; ", cookies.Select(pair => $"{pair.Key} = {pair.Value}"))}");
        }

        private async Task Save()
        {
            await this.cookieStorageService.SaveCookieAsync("leon", "cute");
            await this.cookieStorageService.SaveCookieAsync("Leon", "Cute");
            await this.cookieStorageService.SaveCookieAsync("Leon ", "C ute");
            await this.cookieStorageService.SaveCookieAsync("Le on ", "Cu te");
            await this.cookieStorageService.SaveCookieAsync("1", "1");
            await this.cookieStorageService.SaveCookieAsync("2", "2", -1);
            await this.cookieStorageService.SaveCookieAsync("3", "3", -2);
            await this.cookieStorageService.SaveCookieAsync("4", "4", 10);
            await this.cookieStorageService.SaveCookieAsync("5", "5", 60 * 60 * 24 * 3);
        }

        private async Task Remove()
        {
            await this.cookieStorageService.RemoveCookieAsync("leon");
            await this.cookieStorageService.RemoveCookieAsync("Leon");
            await this.cookieStorageService.RemoveCookieAsync("Le on");
        }

        private async Task Get()
        {
            var cookie = await this.cookieStorageService.GetCookieAsync("Leon ");
            this.logger.LogInformation($"获得Cookie: Leon = {cookie}");
            cookie = await this.cookieStorageService.GetCookieAsync(" leon");
            this.logger.LogInformation($"获得Cookie: leon = {cookie}");
            cookie = await this.cookieStorageService.GetCookieAsync("cute");
            this.logger.LogInformation($"获得Cookie: cute = {cookie}");
        }
    }
}
