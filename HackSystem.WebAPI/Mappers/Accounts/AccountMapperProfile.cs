using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.DataTransferObjects.Accounts;

namespace HackSystem.WebAPI.Mappers.Accounts;

public class AccountMapperProfile : Profile
{
    public AccountMapperProfile()
    {
        this.CreateMap<HackSystemUser, UserResponse>();
    }
}
