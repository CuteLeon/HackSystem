using System.Collections.Generic;
using System.ComponentModel;
using HackSystem.WebAPI.Model.Map.UserMap;
using Microsoft.AspNetCore.Identity;

namespace HackSystem.WebAPI.Model.Identity
{
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

        public virtual IList<UserProgramMap> UserProgramMaps { get; set; }
    }
}
