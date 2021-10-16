using System.Reflection;

namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProgramDetail
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string EntryAssemblyName { get; set; }

    public string EntryTypeName { get; set; }

    public string EntryParameter { get; set; }

    public Type ProgramEntryType { get; set; }

    public MethodInfo ProgramEntryMethod { get; set; }

    public Type? ProgramEntryComponentType { get; set; }

    public string IconUri { get; set; }

    public bool Enabled { get; set; }

    public bool SingleInstance { get; set; }

    public bool Mandatory { get; set; }
}
