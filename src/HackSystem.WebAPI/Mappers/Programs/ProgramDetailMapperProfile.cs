using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;
using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.WebAPI.Mappers.Programs;

public class ProgramDetailMapperProfile : Profile
{
    public ProgramDetailMapperProfile()
    {
        this.CreateMap<ProgramResponse, ProgramDetail>();
        this.CreateMap<ProgramDetail, ProgramResponse>();
        this.CreateMap<UserProgramMap, UserProgramMapResponse>();
    }
}
