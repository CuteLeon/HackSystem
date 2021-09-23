namespace HackSystem.DataTransferObjects.Programs;

public class UserBasicProgramMapRequest
{
    public string ProgramId { get; set; }

    public bool? PinToDesktop { get; set; }

    public bool? PinToDock { get; set; }

    public bool? PinToTop { get; set; }

    public string? Rename { get; set; }
}
