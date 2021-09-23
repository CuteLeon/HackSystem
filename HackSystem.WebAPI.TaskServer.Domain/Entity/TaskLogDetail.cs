namespace HackSystem.WebAPI.TaskServer.Domain.Entity;

public class TaskLogDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskLogID { get; set; }

    public int TaskID { get; set; }

    public string? Parameters { get; set; }

    public DateTime TriggerDateTime { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime FinishDateTime { get; set; }

    public TaskLogStatus TaskLogStatus { get; set; }

    public string Trigger { get; set; }

    public string? Exception { get; set; }
}
