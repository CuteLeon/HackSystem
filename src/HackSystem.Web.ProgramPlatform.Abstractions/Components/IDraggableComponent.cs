using Microsoft.JSInterop;

namespace HackSystem.Web.ProgramPlatform.Components;

public interface IDraggableComponent
{
    [JSInvokable]
    void UpdatePosition(double left, double top);
}
