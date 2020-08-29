using System.Threading.Tasks;
using HackSystem.WebDTO.Account;

namespace HackSystem.Web.Authentication.Services
{
    public interface IAuthService
    {
        Task<RegisterResultDTO> Register(RegisterDTO registerModel);

        Task<LoginResultDTO> Login(LoginDTO loginModel);

        Task Logout();
    }
}
