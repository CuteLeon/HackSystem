using HackSystem.Web.Component.ToastContainer;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    [Parameter]
    public ProcessDetail ProcessDetail { get; set; }

    [Parameter]
    public ProgramWindowDetail ProgramWindowDetail { get; set; }

    [Inject]
    protected IToastHandler ToastHandler { get; set; }

    public ProgramComponentBase() : base()
    {
    }

    public abstract void Dispose();
}
