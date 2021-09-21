using HackSystem.WebAPI.Model.Map.UserMap;

namespace HackSystem.WebAPI.Model.Map;

public class ProgramUser
{
    public ProgramUser()
        : base()
    {
    }

    public string Id { get; set; }

    public virtual IList<UserBasicProgramMap> UserProgramMaps { get; set; }
}
