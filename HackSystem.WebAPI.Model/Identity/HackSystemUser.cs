using HackSystem.WebAPI.Model.Map.UserMap;

namespace HackSystem.WebAPI.Model.Identity;

public class HackSystemUser : IdentityUser
{
    public HackSystemUser()
        : base()
    {
    }

    public HackSystemUser(string userName)
        : base(userName)
    {
    }

    [DefaultValue(0)]
    public int Level { get; set; }

    public virtual IList<UserBasicProgramMap> UserProgramMaps { get; set; }
}
