using HackSystem.Web.ProgramSDK.ProgramComponent;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.Scheduler.Program.Model;

    public class ProcessDetail
    {
        public int PID { get; set; }

        public DynamicComponent DynamicProgramComponent { get; set; }

        public ProgramEntity ProgramEntity { get; set; }
    }
