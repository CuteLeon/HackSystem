namespace HackSystem.Web.ProgramSchedule.Application.Destroyer;

public interface IProcessDestroyer
{
    Task DestroyProcess(int processID);
}
