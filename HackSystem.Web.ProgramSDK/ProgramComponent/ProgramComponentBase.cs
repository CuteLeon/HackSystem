using System;
using HackSystem.Observer.Publisher;
using HackSystem.Web.ProgramSDK.ProgramComponent.Messages;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.ProgramSDK.ProgramComponent;

    public abstract class ProgramComponentBase : ComponentBase, IDisposable
    {
        // TODO: [LEON] Should modify reference relationship between projects: Reference IProcessDisposer here to dispose process, and publish message in IProcessDisposer
        [Inject]
        public IPublisher<ProcessCloseMessage> ProcessClosePublisher { get; set; }

        private int pID;

        [Parameter]
        public int PID { get => pID; set => pID = value; }

        private ProgramEntity programEntity;

        [Parameter]
        public ProgramEntity ProgramEntity { get => programEntity; set => programEntity = value; }

        public virtual void OnClose()
        {
            this.ProcessClosePublisher.Publish(new ProcessCloseMessage(this.PID));
        }

        public abstract void Dispose();
    }
