using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebDataTransfer.Account;

namespace HackSystem.Web.Pages.Account
{
    public partial class RegisterComponent
    {
        private readonly RegisterDTO RegisterModel = new RegisterDTO();
        private bool ShowErrors;
        private IEnumerable<string> Errors;

        public RegisterComponent()
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
