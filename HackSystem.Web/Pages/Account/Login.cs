using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebDTO.Account;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Pages.Account
{
    public partial class Login
    {
        public Login()
        {
        }

        private readonly LoginDTO loginModel = new LoginDTO();
        private bool ShowErrors;
        private string Error = "";

        private async Task HandleLogin()
        {
            this.ShowErrors = false;
            this.logger.LogInformation($"页面使用登录服务登录...");
            var result = await this.authenticationService.Login(this.loginModel);

            this.logger.LogInformation($"页面登录结果：{result.Successful}");
            this.logger.LogInformation($"当前Cookie内的Token: {await this.authenticationStateProvider.GetCurrentTokenAsync()}");
            var state = await this.authenticationStateProvider.GetAuthenticationStateAsync();
            this.logger.LogInformation($"当前认证状态的声明：{string.Join("\n", state.User.Claims.Select(c => $"{c.Type} = {c.Value}"))}");
            this.logger.LogInformation($"当前认证状态：{state.User?.Identity?.IsAuthenticated}");
            this.logger.LogInformation($"当前Hacker角色状态：{state.User?.IsInRole("Hacker")}");

            if (result.Successful)
            {
                this.logger.LogInformation($"准备转跳到桌面组件");
                this.navigationManager.NavigateTo("/Desktop");
            }
            else
            {
                this.Error = result.Error;
                this.ShowErrors = true;
            }
        }
    }
}
