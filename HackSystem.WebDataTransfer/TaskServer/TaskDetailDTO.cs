using System;

namespace HackSystem.WebDataTransfer.TaskServer;

public class TaskDetailDTO
{
    public int TaskID { get; set; }

    public string TaskName { get; set; }

    public bool Enabled { get; set; }

    public DateTime ExecuteDateTime { get; set; }

    public TaskFrequency TaskFrequency { get; set; }

    public TimeSpan AutomaticInterval { get; set; }

    public string ClassName { get; set; }

    public string ProcedureName { get; set; }

    public string Parameters { get; set; }

    public string Category { get; set; }
}
