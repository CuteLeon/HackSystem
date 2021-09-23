using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

namespace HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

public class UserBasicProgramMap
{
    public string UserId { get; set; }

    public virtual ProgramUser ProgramUser { get; set; }

    public string ProgramId { get; set; }

    public virtual BasicProgram BasicProgram { get; set; }

    [DefaultValue(true)]
    public bool PinToDesktop { get; set; }

    [DefaultValue(false)]
    public bool PinToDock { get; set; }

    [DefaultValue(false)]
    public bool PinToTop { get; set; }

    [DefaultValue(null)]
    public string? Rename { get; set; }
}
