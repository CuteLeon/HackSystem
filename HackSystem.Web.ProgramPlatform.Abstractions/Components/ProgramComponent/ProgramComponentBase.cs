using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Domain.Entity;
using HackSystem.Web.ProgramSchedule.Domain.Intermediary;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    private ProcessDetail processDetail;

    [Inject]
    protected IIntermediaryCommandSender CommandSender { get; set; }

    [Inject]
    protected IIntermediaryNotificationPublisher NotificationPublisher { get; set; }

    [Inject]
    protected IIntermediaryRequestSender RequestSender { get; set; }

    [Inject]
    protected IIntermediaryEventPublisher EventPublisher { get; set; }

    [Parameter]
    public ProcessDetail ProcessDetail { get => processDetail; set => processDetail = value; }

    public ProgramDetail ProgramDetail { get => this.processDetail.ProgramDetail; }

    public virtual void OnClose()
    {
        this.CommandSender.Send(new ProcessDestroyCommand() { ProcessDetail = this.processDetail });
    }

    public abstract void Dispose();
}
