namespace HackSystem.Web.ProgramSDK.ProgramComponent.Messages
{
    public class ProcessCloseMessage
    {
        public int PID { get; set; }

        public ProcessCloseMessage(int pID)
        {
            this.PID = pID;
        }
    }
}
