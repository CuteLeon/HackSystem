using AutoMapper;
using HackSystem.WebAPI.Model.Map.UserMap;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebDataTransfer.Program;

namespace HackSystem.WebAPI.Mappers.Program;

public class ProgramMapperProfile : Profile
{
    public ProgramMapperProfile()
    {
        this.CreateMap<BasicProgramDTO, BasicProgram>();
        this.CreateMap<BasicProgram, BasicProgramDTO>()
            .ForMember(dto => dto.IconUri, map => map.MapFrom(program => program.IconUri));

        this.CreateMap<UserBasicProgramMap, UserBasicProgramMapDTO>();
    }
}
