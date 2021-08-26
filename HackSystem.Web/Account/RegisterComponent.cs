using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Account;

    public partial class RegisterComponent
    {
        public RegisterComponent()
        {
        }

        private async Task OnRegister()
        {
            this.ShowErrors = false;

            this.ShowErrors = false;
            this.logger.LogDebug($"Try to Register: {this.registerDto.UserName}");

            try
            {
                var result = await this.authenticationService.Register(this.registerDto);
                this.logger.LogDebug($"Register result: {(result.Successful ? "Success" : "Fail")}");

                if (result.Successful)
                {
                    this.navigationManager.NavigateTo("/Login");
                }
                else
                {
                    this.logger.LogWarning($"Register Error: {string.Join("; ", result.Errors)}");
                    this.Errors = result.Errors;
                    this.ShowErrors = true;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Register Exception: {ex.Message}");
                this.Errors = new[] { ex.Message };
                this.ShowErrors = true;
            }
        }
    }
