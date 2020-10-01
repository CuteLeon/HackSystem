namespace HackSystem.Web.ProgramSDK.ProgramComponent.ProgramMessages
{
    public class ProgramCloseMessage
    {
        public int PID { get; set; }

        public ProgramCloseMessage(int pID)
        {
            this.PID = pID;
        }
    }
}
