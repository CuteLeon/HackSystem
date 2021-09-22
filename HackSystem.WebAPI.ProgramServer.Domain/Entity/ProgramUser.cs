using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

namespace HackSystem.WebAPI.ProgramServer.Domain.Entity;

public class ProgramUser
{
    [Key]
    public string Id { get; set; }

    public virtual IList<UserBasicProgramMap> UserProgramMaps { get; set; }
}
