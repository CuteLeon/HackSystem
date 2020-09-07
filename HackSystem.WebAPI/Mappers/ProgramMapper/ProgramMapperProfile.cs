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
            this.CreateMap<BasicProgram, QueryBasicProgramDTO>();

            this.CreateMap<CreateBasicProgramDTO, BasicProgram>();
            this.CreateMap<UpdateBasicProgramDTO, BasicProgram>();

            this.CreateMap<UserProgramMap, UserProgramMapDTO>();
        }
    }
}
