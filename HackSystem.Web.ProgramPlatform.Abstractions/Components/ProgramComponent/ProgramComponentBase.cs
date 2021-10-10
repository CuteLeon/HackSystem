using HackSystem.Intermediary.Application;
using HackSystem.Web.Component.ToastContainer;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    private ProcessDetail processDetail;

    [Inject]
    protected IIntermediaryCommandSender CommandSender { get; set; }

    [Inject]
    protected IToastHandler ToastHandler { get; set; }

    [Parameter]
    public ProcessDetail ProcessDetail { get => processDetail; set => processDetail = value; }

    public ProgramDetail ProgramDetail { get => this.processDetail.ProgramDetail; }

    public virtual void OnClose()
    {
        this.CommandSender.Send(new ProcessDestroyCommand() { ProcessDetail = this.processDetail });
    }

    public abstract void Dispose();
}
