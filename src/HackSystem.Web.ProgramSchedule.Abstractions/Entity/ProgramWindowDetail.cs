using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProgramWindowDetail
{
    private ProgramWindowStates windowState = ProgramWindowStates.Normal;
    private SemaphoreSlim modalSemaphore;

    public ProgramWindowDetail(
        string windowId,
        Type programWindowType,
        ProcessDetail processDetail)
    {
        this.WindowId = windowId;
        this.ProgramWindowType = programWindowType;
        this.ProcessDetail = processDetail;
    }

    public ProgramWindowDetail(
        string windowId,
        Type programWindowType,
        ProcessDetail processDetail,
        ProgramWindowDetail? parentWindow)
        : this(windowId, programWindowType, processDetail)
    {
        this.SetParentWindow(parentWindow);
    }

    public string WindowId { get; init; }

    public string Caption { get; set; }

    public Type ProgramWindowType { get; init; }

    public string Top { get; set; } = "5%";

    public string Left { get; set; } = "25%";

    public string Width { get; set; } = "50%";

    public string Height { get; set; } = "75%";

    public bool AllowMinimized { get; set; } = true;

    public bool AllowMaximized { get; set; } = true;

    public bool StickyTopTier { get; set; } = false;

    public int TierIndex { get; set; }

    public ProgramWindowStates LastWindowState { get; protected set; } = ProgramWindowStates.Normal;

    public ProgramWindowStates WindowState
    {
        get => this.windowState;
        set
        {
            if (this.windowState == value) return;
            if (!this.AllowMinimized && value == ProgramWindowStates.Minimized) return;
            if (!this.AllowMaximized && value == ProgramWindowStates.Maximized) return;

            this.LastWindowState = this.WindowState;
            this.windowState = value;
        }
    }

    public bool IsModal { get => this.modalSemaphore is not null; }

    public ProcessDetail ProcessDetail { get; init; }

    public ProgramWindowDetail? ParentWindow { get; protected set; }

    protected void SetParentWindow(ProgramWindowDetail? parentWindow)
    {
        if (this.ParentWindow is not null)
            this.ParentWindow.RemoveChildWindowDetail(this);
        this.ParentWindow = parentWindow;
        if (this.ParentWindow is not null)
            this.ParentWindow.AddChildWindowDetail(this);
    }

    protected Dictionary<string, ProgramWindowDetail> ChildWindowDetails { get; init; } = new();

    public bool TryGetChildWindowDetail(string childWindowId, out ProgramWindowDetail? childWindowDetail)
        => this.ChildWindowDetails.TryGetValue(childWindowId, out childWindowDetail);

    public IEnumerable<ProgramWindowDetail> GetChildWindowDetails()
        => this.ChildWindowDetails.Values.AsEnumerable();

    protected bool AddChildWindowDetail(ProgramWindowDetail childWindowDetail)
        => childWindowDetail.ProcessDetail == this.ProcessDetail &&
            this.ChildWindowDetails.TryAdd(childWindowDetail.WindowId, childWindowDetail);

    protected bool RemoveChildWindowDetail(ProgramWindowDetail childWindowDetail)
        => childWindowDetail.ProcessDetail == this.ProcessDetail &&
            this.ChildWindowDetails.Remove(childWindowDetail.WindowId, out _);
}
