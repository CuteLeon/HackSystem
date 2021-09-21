using HackSystem.WebAPI.Domain.Identity;

namespace HackSystem.WebAPI.Services.API.Account;

public interface IAccountService
{
    Task InitialUser(HackSystemUser user);
}
