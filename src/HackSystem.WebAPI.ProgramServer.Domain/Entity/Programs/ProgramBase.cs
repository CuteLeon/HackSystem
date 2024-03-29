﻿using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

namespace HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

public class ProgramBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string Name { get; set; }

    [DefaultValue(true)]
    public bool Enabled { get; set; }

    public bool SingleInstance { get; set; }

    public string EntryAssemblyName { get; set; }

    public string EntryTypeName { get; set; }

    public string? EntryParameter { get; set; }

    public bool Mandatory { get; set; }

    public virtual IList<UserProgramMap> UserProgramMaps { get; set; }
}
