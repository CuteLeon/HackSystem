using System.Threading.Tasks;
using HackSystem.WebAPI.Model.Identity;

namespace HackSystem.WebAPI.Services.API.Account;

    public interface IAccountService
    {
        Task InitialUser(HackSystemUser user);
    }
