using HackSystem.Web.ProgramSchedule.Application.Destroyer;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramPlatform.Components.ProgramComponent;

public abstract class ProgramComponentBase : ComponentBase, IDisposable
{
    [Inject]
    public IProcessDestroyer ProcessDestroyer { get; set; }

    private int processID;

    [Parameter]
    public int PID { get => processID; set => processID = value; }

    private ProgramDetail programDetail;

    [Parameter]
    public ProgramDetail ProgramDetail { get => programDetail; set => programDetail = value; }

    public virtual void OnClose()
    {
        this.ProcessDestroyer.DestroyProcess(this.processID);
    }

    public abstract void Dispose();
}
