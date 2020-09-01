using System.Linq;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services;
using HackSystem.WebDTO.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Pages.Account
{
    public partial class Login
    {
        private readonly ILogger<Login> logger;
        private readonly IAuthenticationService authenticationService;
        private readonly IHackSystemAuthenticationStateProvider authenticationStateProvider;
        private readonly NavigationManager navigationManager;

        public Login(
            ILogger<Login> logger,
            IAuthenticationService authenticationService,
            IHackSystemAuthenticationStateProvider authenticationStateProvider,
            NavigationManager navigationManager)
        {
            this.logger = logger;
            this.authenticationService = authenticationService;
            this.authenticationStateProvider = authenticationStateProvider;
            this.navigationManager = navigationManager;
        }

        private LoginDTO loginModel = new LoginDTO();
        private bool ShowErrors;
        private string Error = "";

        private async Task HandleLogin()
        {
            ShowErrors = false;
            logger.LogInformation($"页面使用登录服务登录...");
            var result = await authenticationService.Login(loginModel);

            logger.LogInformation($"页面登录结果：{result.Successful}");
            logger.LogInformation($"当前Cookie内的Token: {await authenticationStateProvider.GetCurrentTokenAsync()}");
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            logger.LogInformation($"当前认证状态的声明：{string.Join("\n", state.User.Claims.Select(c => $"{c.Type} = {c.Value}"))}");
            logger.LogInformation($"当前认证状态：{state.User.Identity.IsAuthenticated}");
            logger.LogInformation($"当前Hacker角色状态：{state.User.IsInRole("Hacker")}");

            if (result.Successful)
            {
                logger.LogInformation($"准备转跳到桌面组件");
                navigationManager.NavigateTo("/Desktop");
            }
            else
            {
                Error = result.Error;
                ShowErrors = true;
            }
        }
    }
}
