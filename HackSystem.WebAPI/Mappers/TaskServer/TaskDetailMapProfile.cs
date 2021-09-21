using HackSystem.WebAPI.TaskServer.Domain.Entity;
using HackSystem.WebDataTransfer.TaskServer;

namespace HackSystem.WebAPI.Mappers.TaskServer;

public class TaskDetailMapProfile : Profile
{
    public TaskDetailMapProfile()
    {
        this.CreateMap<TaskDetail, TaskDetailDTO>();
    }
}
