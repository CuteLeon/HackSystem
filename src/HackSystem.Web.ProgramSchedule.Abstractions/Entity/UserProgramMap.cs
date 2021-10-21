namespace HackSystem.Web.ProgramSchedule.Entity;

public class UserProgramMap
{
    public UserProgramMap(ProgramDetail program)
    {
        this.Program = program;
    }

    public ProgramDetail Program { get; init; }

    public bool PinToDesktop { get; set; }

    public bool PinToDock { get; set; }

    public bool PinToTop { get; set; }

    public string? Rename { get; set; }
}
