using HackSystem.Intermediary.Domain;

namespace HackSystem.Web.ProgramSchedule.Domain.Notification;

public record ProgramLaunchNotification : IIntermediaryNotification
{
    public int PID { get; set; }

    public override string ToString()
        => $"Process Launch Message => {this.PID} ID";
}
