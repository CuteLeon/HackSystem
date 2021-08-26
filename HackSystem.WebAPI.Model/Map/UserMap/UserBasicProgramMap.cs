using System.ComponentModel;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Model.Program;

namespace HackSystem.WebAPI.Model.Map.UserMap;

    public class UserBasicProgramMap
    {
        public string UserId { get; set; }

        public virtual HackSystemUser User { get; set; }

        public string ProgramId { get; set; }

        public virtual BasicProgram BasicProgram { get; set; }

        [DefaultValue(false)]
        public bool Hide { get; set; }

        [DefaultValue(false)]
        public bool PinToDock { get; set; }

        [DefaultValue(false)]
        public bool PinToTop { get; set; }

        [DefaultValue(null)]
        public string Rename { get; set; }
    }
