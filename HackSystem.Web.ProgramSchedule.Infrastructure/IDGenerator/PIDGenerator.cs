using HackSystem.Web.ProgramSchedule.IDGenerator;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IDGenerator;

public class PIDGenerator : IPIDGenerator
{
    private int availablePID = 1;
    
    public int GetAvailablePID()
        => Interlocked.Increment(ref availablePID);
}
