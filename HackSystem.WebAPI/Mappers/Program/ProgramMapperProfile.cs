using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;
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
