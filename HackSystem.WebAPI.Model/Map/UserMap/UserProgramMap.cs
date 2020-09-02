using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Model.Program;

namespace HackSystem.WebAPI.Model.Map.UserMap
{
    public class UserProgramMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public virtual HackSystemUser User { get; set; }

        public string ProgramId { get; set; }

        public virtual ProgramBase Program { get; set; }
    }
}
