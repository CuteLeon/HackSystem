using HackSystem.Observer.Message;

namespace HackSystem.Web.ProgramSDK.ProgramComponent.Messages;

public record ProgramLaunchMessage : MessageBase
{
    public int PID { get; set; }

    public override string ToString()
        => $"Process Launch Message => {this.PID} ID";
}
