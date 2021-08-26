using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HackSystem.WebAPI.Model.Map.UserMap;

namespace HackSystem.WebAPI.Model.Program;

public class ProgramBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string IconUri { get; set; }

    public string Name { get; set; }

    [DefaultValue(true)]
    public bool Enabled { get; set; }

    public bool IsSingleton { get; set; }

    public string AssemblyName { get; set; }

    public string TypeName { get; set; }

    public bool Integral { get; set; }

    public virtual IList<UserBasicProgramMap> UserProgramMaps { get; set; }
}
