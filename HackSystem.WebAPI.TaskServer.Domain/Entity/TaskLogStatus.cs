namespace HackSystem.WebAPI.TaskServer.Domain.Entity;

public enum TaskLogStatus
{
    Pending,
    Allocated,
    Running,
    Cancelled,
    Failed,
    Complete,
}
