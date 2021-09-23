using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;
using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.WebAPI.Mappers.Programs;

public class ProgramMapperProfile : Profile
{
    public ProgramMapperProfile()
    {
        this.CreateMap<BasicProgramResponse, BasicProgram>();
        this.CreateMap<BasicProgram, BasicProgramResponse>();
        this.CreateMap<UserBasicProgramMap, UserBasicProgramMapResponse>();
    }
}
