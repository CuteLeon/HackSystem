using HackSystem.DataTransferObjects.Accounts;

namespace HackSystem.Web.Services.API.Authentication;

public interface IAuthenticationService
{
    Task<RegisterResponse> Register(RegisterRequest registerModel);

    Task<LoginResponse> Login(LoginRequest loginModel);

    Task<string> GetAccountInfo();

    Task Logout();
}
