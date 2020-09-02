using System.Collections.Generic;
using HackSystem.WebAPI.Model.Map.UserMap;
using Microsoft.AspNetCore.Identity;

namespace HackSystem.WebAPI.Model.Identity
{
    public class HackSystemUser : IdentityUser
    {
        public int Level { get; set; }

        public virtual IList<UserProgramMap> UserProgramMaps { get; set; }
    }
}
