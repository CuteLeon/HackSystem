using HackSystem.WebAPI.TaskServers.Domain.Entity;
using HackSystem.WebDataTransfer.TaskServer;

namespace HackSystem.WebAPI.Mappers.TaskServer;

public class TaskDetailMapProfile : Profile
{
    public TaskDetailMapProfile()
    {
        this.CreateMap<TaskDetail, TaskDetailDTO>();
    }
}
