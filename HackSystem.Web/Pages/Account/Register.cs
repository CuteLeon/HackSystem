using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebDataTransfer.Account;

namespace HackSystem.Web.Pages.Account
{
    public partial class Register
    {
        private readonly RegisterDTO RegisterModel = new RegisterDTO();
        private bool ShowErrors;
        private IEnumerable<string> Errors;

        public Register()
        {
        }

        private async Task HandleRegistration()
        {
            this.ShowErrors = false;

            var result = await this.authenticationService.Register(this.RegisterModel);

            if (result.Successful)
            {
                this.navigationManager.NavigateTo("/StartUp");
            }
            else
            {
                this.Errors = result.Errors;
                this.ShowErrors = true;
            }
        }
    }
}
