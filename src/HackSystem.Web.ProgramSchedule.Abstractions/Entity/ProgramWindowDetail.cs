using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProgramWindowDetail
{
    private ProgramWindowStates windowState = ProgramWindowStates.Normal;

    public ProgramWindowDetail(
        string windowId,
        Type programWindowType,
        ProcessDetail processDetail)
    {
        this.WindowId = windowId;
        this.ProgramWindowType = programWindowType;
        this.ProcessDetail = processDetail;
    }

    public string WindowId { get; init; }

    public string Caption { get; set; }

    public Type ProgramWindowType { get; init; }

    public string Top { get; set; } = "5%";

    public string Left { get; set; } = "25%";

    public string Width { get; set; } = "50%";

    public string Height { get; set; } = "75%";

    public int TierIndex { get; set; }

    public ProgramWindowStates LastWindowState { get; protected set; } = ProgramWindowStates.Normal;

    public ProgramWindowStates WindowState
    {
        get => this.windowState;
        set
        {
            if (this.windowState == value) return;
            this.LastWindowState = this.WindowState;
            this.windowState = value;
        }
    }

    public ProcessDetail ProcessDetail { get; init; }
}
