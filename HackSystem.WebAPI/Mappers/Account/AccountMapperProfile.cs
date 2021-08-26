using AutoMapper;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebDataTransfer.Account;

namespace HackSystem.WebAPI.Mappers.Account;

    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            this.CreateMap<HackSystemUser, AccountInfoDTO>();
        }
    }
