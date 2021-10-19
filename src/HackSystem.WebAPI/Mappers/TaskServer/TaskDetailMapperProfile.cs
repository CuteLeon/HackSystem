using HackSystem.WebAPI.TaskServer.Domain.Entity;
using HackSystem.DataTransferObjects.TaskServer;

namespace HackSystem.WebAPI.Mappers.TaskServer;

public class TaskDetailMapperProfile : Profile
{
    public TaskDetailMapperProfile()
    {
        this.CreateMap<TaskDetail, TaskDetailResponse>();
    }
}
