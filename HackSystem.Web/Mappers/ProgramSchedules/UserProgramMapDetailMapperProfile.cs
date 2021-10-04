using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.Mappers.ProgramSchedules;

public class UserProgramMapDetailMapperProfile : Profile
{
    public UserProgramMapDetailMapperProfile()
    {
        this.CreateMap<UserProgramMapResponse, UserProgramMap>();
    }
}
