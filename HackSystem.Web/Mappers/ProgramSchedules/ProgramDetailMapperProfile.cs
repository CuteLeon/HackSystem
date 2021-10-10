using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.Mappers.ProgramSchedules;

public class ProgramDetailMapperProfile : Profile
{
    public ProgramDetailMapperProfile()
    {
        this.CreateMap<ProgramResponse, ProgramDetail>();
    }
}
