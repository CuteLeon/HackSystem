using HackSystem.Observer.Message;

namespace HackSystem.Web.ProgramSDK.ProgramComponent.Messages;

    public record ProcessCloseMessage : MessageBase
    {
        public int PID { get; set; }

        public ProcessCloseMessage(int pID)
        {
            this.PID = pID;
        }

        public override string ToString()
            => $"Process Close Message => {this.PID} ID";
    }
