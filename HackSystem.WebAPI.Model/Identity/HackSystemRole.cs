using Microsoft.AspNetCore.Identity;

namespace HackSystem.WebAPI.Model.Identity
{
    public class HackSystemRole : IdentityRole
    {
        public int Level { get; set; }
    }
}
