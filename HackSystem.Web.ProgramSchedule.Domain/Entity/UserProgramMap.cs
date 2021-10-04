namespace HackSystem.Web.ProgramSchedule.Domain.Entity;

public class UserProgramMap
{
    public ProgramDetail Program { get; set; }

    public bool PinToDesktop { get; set; }

    public bool PinToDock { get; set; }

    public bool PinToTop { get; set; }

    public string Rename { get; set; }
}
