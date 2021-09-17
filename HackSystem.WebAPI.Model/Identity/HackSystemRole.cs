namespace HackSystem.WebAPI.Model.Identity;

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

    [DefaultValue(0)]
    public int Level { get; set; }
}
