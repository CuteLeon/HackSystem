using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Model.Program;

namespace HackSystem.WebAPI.Model.Map.UserMap
{
    public class UserProgramMap
    {
        public string UserId { get; set; }

        public virtual HackSystemUser User { get; set; }

        public string ProgramId { get; set; }

        public virtual ProgramBase Program { get; set; }

        public bool Hide { get; set; }

        public bool PinToDock { get; set; }

        public bool PinToTop { get; set; }

        public string Rename { get; set; }
    }
}
