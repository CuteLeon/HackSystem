using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.TopBar;

public class TopBarView : IComponent
{
    private RenderHandle renderHandle;

    [Parameter]
    public RenderFragment ProgramMenuView { get; set; }

    [Parameter]
    public RenderFragment ProgramStatusView { get; set; }

    public void Attach(RenderHandle renderHandle)
    {
        this.renderHandle = renderHandle;
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        this.ProgramMenuView = parameters.GetValueOrDefault<RenderFragment>(nameof(this.ProgramMenuView), null);
        this.ProgramStatusView = parameters.GetValueOrDefault<RenderFragment>(nameof(this.ProgramStatusView), null);

        return Task.CompletedTask;
    }
}
