namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProgramDetail
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string AssemblyName { get; set; }

    public string TypeName { get; set; }

    public Type ProgramComponentType { get; set; }

    public int Z_Index { get; set; }

    public string IconUri { get; set; }

    public bool Enabled { get; set; }

    public bool SingleInstance { get; set; }

    public bool Mandatory { get; set; }
}
