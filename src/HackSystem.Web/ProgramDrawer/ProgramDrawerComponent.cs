using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.Domain.Intermediary;
using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramDrawer;

public partial class ProgramDrawerComponent : IAsyncDisposable
{
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.programMapEventHandler.EventRaised += OnProgramMapEventHandle;
    }

    private async void OnProgramMapEventHandle(object sender, UserProgramMapEvent args)
    {
        var request = args.UserProgramMap;
        if (!this.UserProgramMaps.TryGetValue(request.ProgramId, out var programMap)) return;

        if (request.PinToDock.HasValue)
        {
            programMap.PinToDock = request.PinToDock.Value;
        }

        this.StateHasChanged();
    }

    public void ClearProgramDrawer()
    {
        this.UserProgramMaps.Clear();
        this.StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task LoadProgramDrawer(IEnumerable<UserProgramMap> maps)
    {
        foreach (var map in maps) this.UserProgramMaps.Add(map.Program.Id, map);
        this.StateHasChanged();
    }

    public async Task OnIconSelect(ProgramIconEventArgs args)
    {
        var programDetail = args.UserProgramMap.Program;
        this.logger.LogInformation($"Double click to launch program: {programDetail.Name}");
        await this.publisher.SendRequest(new ProgramLaunchRequest(programDetail));
    }

    public async Task OnIconContextMenu(ProgramIconEventArgs args)
    {
        var programDetail = args.UserProgramMap.Program;
        this.logger.LogInformation($"Show program context menu: {programDetail.Name}");
        await this.IconContextMenuComponent.Show(args.UserProgramMap, args.Position!.Value);
    }

    public async Task OnMenuRunProgram(UserProgramMap programMap)
    {
        var programDetail = programMap.Program;
        this.logger.LogInformation($"Menu click to launch program: {programDetail.Name}");
        await this.publisher.SendRequest(new ProgramLaunchRequest(programDetail));
    }

    public async Task OnMenuTogglePinToDock(UserProgramMap programMap)
    {
        var programDetail = programMap.Program;
        this.logger.LogInformation($"Menu click to toggle program pin to dock: {programDetail.Name}");
        var programMapCommand = new UserProgramMapCommand(new UserProgramMapRequest()
        {
            ProgramId = programDetail.Id,
            PinToDock = !programMap.PinToDock,
        });
        await this.publisher.SendCommand(programMapCommand);
    }

    public async ValueTask DisposeAsync()
    {
        this.programMapEventHandler.EventRaised -= OnProgramMapEventHandle;
    }
}
