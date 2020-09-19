using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.ProgramMenu
{
    public class ProgramMenuView : IComponent
    {
        private RenderHandle renderHandle;

        /// <summary>
        /// The content to which the value should be provided.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public void Attach(RenderHandle renderHandle)
        {
            this.renderHandle = renderHandle;
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            this.ChildContent = parameters.GetValueOrDefault<RenderFragment>(nameof(this.ChildContent), null);
            return Task.CompletedTask;
        }
    }
}
