namespace HackSystem.WebAPI.Domain.Entity.Identity;

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
