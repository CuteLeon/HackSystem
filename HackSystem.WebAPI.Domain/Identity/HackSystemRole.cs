namespace HackSystem.WebAPI.Domain.Identity;

public class HackSystemRole : IdentityRole
{
    public HackSystemRole()
        : base()
    {
    }

    public HackSystemRole(string roleName)
        : base(roleName)
    {
    }
}
