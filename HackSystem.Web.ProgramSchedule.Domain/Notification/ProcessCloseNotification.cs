using HackSystem.Intermediary.Domain;

namespace HackSystem.Web.ProgramSchedule.Domain.Notification;

public record ProcessCloseNotification : IIntermediaryNotification
{
    public int PID { get; set; }

    public ProcessCloseNotification(int pID)
    {
        this.PID = pID;
    }

    public override string ToString()
        => $"Process Close Message => {this.PID} ID";
}
