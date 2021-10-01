namespace HackSystem.Web.ProgramSchedule.Application.Disposer;

public interface IProcessDisposer
{
    Task DisposeProcess(int processID);
}
