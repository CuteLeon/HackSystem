using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.Mappers.ProgramSchedules;

public class UserProgramMapMapperProfile : Profile
{
    public UserProgramMapMapperProfile()
    {
        this.CreateMap<UserProgramMapResponse, UserProgramMap>();
    }
}
