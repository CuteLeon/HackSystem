using System;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.ProgramSDK.ProgramComponent
{
    public abstract class ProgramComponentBase : ComponentBase, IDisposable
    {
        private ProgramEntity programEntity;

        [Parameter]
        public ProgramEntity ProgramEntity { get => programEntity; set => programEntity = value; }

        public abstract void Dispose();
    }
}
