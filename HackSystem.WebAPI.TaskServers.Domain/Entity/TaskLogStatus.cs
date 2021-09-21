namespace HackSystem.WebAPI.TaskServers.Domain.Entity;

public enum TaskLogStatus
{
    Pending,
    Allocated,
    Running,
    Cancelled,
    Failed,
    Complete,
}
