using HackSystem.Web.ProgramSchedule.Application.Disposer;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    [Inject]
    public IProcessDisposer ProcessDisposer { get; set; }

    private int processID;

    [Parameter]
    public int PID { get => processID; set => processID = value; }

    private ProgramDetail programDetail;

    [Parameter]
    public ProgramDetail ProgramDetail { get => programDetail; set => programDetail = value; }

    public virtual void OnClose()
    {
        this.ProcessDisposer.DisposeProcess(this.processID);
    }

    public abstract void Dispose();
}
