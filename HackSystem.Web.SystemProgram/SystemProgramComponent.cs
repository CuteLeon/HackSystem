using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace HackSystem.Web.SystemProgram
{
    /// <summary>
    /// Renders a form element that cascades an <see cref="SystemProgramComponent"/> to descendants.
    /// </summary>
    public class SystemProgramComponent : ComponentBase
    {
        [Parameter]
        public string Content { get; set; } = "Default System Program Content.";

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "h3");
            builder.AddContent(1, this.Content);
            builder.CloseElement();
        }
    }
}
