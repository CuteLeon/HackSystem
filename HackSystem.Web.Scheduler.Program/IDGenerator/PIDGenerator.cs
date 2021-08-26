namespace HackSystem.Web.Scheduler.Program.IDGenerator;

public class PIDGenerator : IPIDGenerator
{
    private int availablePID = 1;
    
    public int GetAvailablePID()
        => Interlocked.Increment(ref availablePID);
}
