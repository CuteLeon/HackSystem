namespace HackSystem.DataTransferObjects.Programs;

public class UserProgramMapResponse
{
    public ProgramResponse Program { get; set; }

    public bool PinToDesktop { get; set; }

    public bool PinToDock { get; set; }

    public bool PinToTop { get; set; }

    public string Rename { get; set; }
}
