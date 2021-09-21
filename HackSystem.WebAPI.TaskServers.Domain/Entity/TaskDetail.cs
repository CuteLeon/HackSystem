namespace HackSystem.WebAPI.TaskServers.Domain.Entity;

public class TaskDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskID { get; set; }

    public string TaskName { get; set; }

    [DefaultValue(true)]
    public bool Enabled { get; set; }

    public DateTime ExecuteDateTime { get; set; }

    public TimeSpan FirstInterval { get; set; }

    public TimeSpan AutomaticInterval { get; set; }

    public TaskFrequency TaskFrequency { get; set; }

    public bool Reentrant { get; set; }

    public string AssemblyName { get; set; }

    public string ClassName { get; set; }

    public string ProcedureName { get; set; }

    public string Parameters { get; set; }

    public string Category { get; set; }

    public DateTime CreateTime { get; set; }
}
