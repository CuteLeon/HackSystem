using System;
using HackSystem.Web.ProgramSDK.ProgramComponent;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.Scheduler.Program.Model
{
    public class ProcessEntity
    {
        public int PID { get; set; }

        public Type ProgramComponentType { get; set; }

        public ProgramComponentBase ProgramComponent { get; set; }

        public RenderFragment ProgramRenderFramgment { get; set; }
    }
}
