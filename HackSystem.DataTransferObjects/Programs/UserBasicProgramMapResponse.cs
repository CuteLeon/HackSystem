namespace HackSystem.DataTransferObjects.Programs;

public class UserBasicProgramMapResponse
{
    public BasicProgramResponse BasicProgram { get; set; }

    public bool PinToDesktop { get; set; }

    public bool PinToDock { get; set; }

    public bool PinToTop { get; set; }

    public string Rename { get; set; }
}
