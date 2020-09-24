using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.ProgramSDK.ProgramComponent
{
    public class ProgramComponentBase : ComponentBase
    {
        private ProgramEntity programEntity;
        [Parameter]
        public ProgramEntity ProgramEntity { get => programEntity; set => programEntity = value; }
    }
}
