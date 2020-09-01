using System.Threading.Tasks;
using HackSystem.WebDTO.Account;

namespace HackSystem.Web.Services
{
    public interface IAuthenticationService
    {
        Task<RegisterResultDTO> Register(RegisterDTO registerModel);

        Task<LoginResultDTO> Login(LoginDTO loginModel);

        Task<string> GetAccountInfo();

        Task Logout();
    }
}
