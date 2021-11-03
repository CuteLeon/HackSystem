using HackSystem.Web.Component.ToastContainer;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    public ProgramDetail ProgramDetail { get => this.ProcessDetail.ProgramDetail; }

    public ProcessDetail ProcessDetail { get => this.ProgramWindowDetail.ProcessDetail; }

    [CascadingParameter]
    public ProgramWindowDetail ProgramWindowDetail { get; init; }

    [Inject]
    protected IToastHandler ToastHandler { get; init; }

    public ProgramComponentBase() : base()
    {
    }

    public abstract void Dispose();
}
