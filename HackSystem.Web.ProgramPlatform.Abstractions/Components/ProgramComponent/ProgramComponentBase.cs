using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Domain.Entity;
using HackSystem.Web.ProgramSchedule.Domain.Notification;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    [Inject]
    public IIntermediaryNotificationPublisher NotificationPublisher { get; set; }

    private int pID;

    [Parameter]
    public int PID { get => pID; set => pID = value; }

    private ProgramDetail programDetail;

    [Parameter]
    public ProgramDetail ProgramDetail { get => programDetail; set => programDetail = value; }

    public virtual void OnClose()
    {
        this.NotificationPublisher.Publish(new ProcessCloseNotification(this.pID));
    }

    public abstract void Dispose();
}
