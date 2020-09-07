using AutoMapper;
using HackSystem.WebAPI.Model.Map.UserMap;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebDataTransfer.Program;

namespace HackSystem.WebAPI.Mappers.ProgramMapper
{
    public class ProgramMapperProfile : Profile
    {
        public ProgramMapperProfile()
        {
            this.CreateMap<QueryBasicProgramDTO, BasicProgram>();
            this.CreateMap<BasicProgram, QueryBasicProgramDTO>()
                .ForMember(dto => dto.IconUrl, map => map.MapFrom(program => $"~/image/Icon/{program.Id}.png"));

            this.CreateMap<UserProgramMap, QueryUserProgramMapDTO>();
        }
    }
}
