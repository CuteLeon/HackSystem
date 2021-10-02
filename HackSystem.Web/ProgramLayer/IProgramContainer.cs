using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Domain.Notification;

namespace HackSystem.Web.ProgramLayer
{
    public interface IProgramContainer : IIntermediaryNotificationHandler<ProgramLaunchNotification>, IIntermediaryNotificationHandler<ProcessDisposeNotification>
    {
    }
}
