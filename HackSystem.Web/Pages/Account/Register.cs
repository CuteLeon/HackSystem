using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.Web.Services;
using HackSystem.WebDTO.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Pages.Account
{
    public partial class Register
    {
        private readonly ILogger<Register> logger;
        private readonly IAuthenticationService authenticationService;
        private readonly NavigationManager navigationManager;

        private RegisterDTO RegisterModel = new RegisterDTO();
        private bool ShowErrors;
        private IEnumerable<string> Errors;

        public Register(
            ILogger<Register> logger,
            IAuthenticationService authenticationService,
            NavigationManager navigationManager)
        {
            this.logger = logger;
            this.authenticationService = authenticationService;
            this.navigationManager = navigationManager;
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
