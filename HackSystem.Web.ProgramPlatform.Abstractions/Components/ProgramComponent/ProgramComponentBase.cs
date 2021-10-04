using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramPlatform.Components.ToastComponent;
using HackSystem.Web.ProgramSchedule.Domain.Entity;
using HackSystem.Web.ProgramSchedule.Domain.Intermediary;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    private ProcessDetail processDetail;

    [Inject]
    IIntermediaryCommandSender CommandSender { get; set; }

    [CascadingParameter(Name = IToastContainer.CascadingParameterName)]
    private Func<IToastContainer> GetDesktopToastContainer { get; set; }

    protected IToastContainer DesktopToastContainer { get => this.GetDesktopToastContainer?.Invoke(); }

    [Parameter]
    public ProcessDetail ProcessDetail { get => processDetail; set => processDetail = value; }

    public ProgramDetail ProgramDetail { get => this.processDetail.ProgramDetail; }

    public virtual void OnClose()
    {
        this.CommandSender.Send(new ProcessDestroyCommand() { ProcessDetail = this.processDetail });
    }

    public abstract void Dispose();
}
