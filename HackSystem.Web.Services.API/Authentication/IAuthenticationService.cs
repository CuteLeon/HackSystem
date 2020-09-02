using System.Threading.Tasks;
using HackSystem.WebDataTransfer.Account;

namespace HackSystem.Web.Services.API.Authentication
{
    public interface IAuthenticationService
    {
        Task<RegisterResultDTO> Register(RegisterDTO registerModel);

        Task<LoginResultDTO> Login(LoginDTO loginModel);

        Task<string> GetAccountInfo();

        Task Logout();
    }
}
