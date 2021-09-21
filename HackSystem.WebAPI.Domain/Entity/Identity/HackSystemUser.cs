namespace HackSystem.WebAPI.Domain.Entity.Identity;

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
    public int ExperienceLevel { get; set; }

    [DefaultValue(0)]
    public int ExperiencePoints { get; set; }
}
