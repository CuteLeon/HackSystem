using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebDTO.Account;

namespace HackSystem.Web.Pages.Account
{
    public partial class Register
    {
        private RegisterDTO RegisterModel = new RegisterDTO();
        private bool ShowErrors;
        private IEnumerable<string> Errors;

        public Register()
        {
        }

        private async Task HandleRegistration()
        {
            ShowErrors = false;

            var result = await authenticationService.Register(RegisterModel);

            if (result.Successful)
            {
                navigationManager.NavigateTo("/StartUp");
            }
            else
            {
                Errors = result.Errors;
                ShowErrors = true;
            }
        }
    }
}
