namespace HackSystem.Web.ProgramSchedule.Application.Destroyer;

public interface IProcessDestroyer
{
    Task DisposeProcess(int processID);
}
