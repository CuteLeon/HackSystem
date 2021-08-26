using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Account;

public partial class LoginComponent
{
    public LoginComponent()
    {
    }

    private async Task OnLogin()
    {
        this.Logging = true;
        this.ShowErrors = false;
        this.logger.LogDebug($"User try to Login: {this.loginDto.UserName}");

        try
        {
            var result = await this.authenticationService.Login(this.loginDto);
            this.logger.LogDebug($"Login result: {(result.Successful ? "Success" : "Fail")}");

            if (result.Successful)
            {
                this.navigationManager.NavigateTo("/Desktop");
            }
            else
            {
                this.logger.LogWarning($"Login Error: {result.Error}");
                this.Error = result.Error;
                this.ShowErrors = true;
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError($"Login Exception: {ex.Message}");
            this.Error = ex.Message;
            this.ShowErrors = true;
        }

        this.Logging = false;
    }
}
