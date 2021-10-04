using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Domain.Entity;
using HackSystem.Web.ProgramSchedule.Domain.Intermediary;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    private ProcessDetail processDetail;

    [Inject]
    IIntermediaryCommandSender CommandSender { get; set; }

    [Parameter]
    public ProcessDetail ProcessDetail { get => processDetail; set => processDetail = value; }

    public ProgramDetail ProgramDetail { get => this.processDetail.ProgramDetail; }

    public virtual void OnClose()
    {
        this.CommandSender.Send(new ProcessDestroyCommand() { ProcessDetail = this.processDetail });
    }

    public abstract void Dispose();
}
