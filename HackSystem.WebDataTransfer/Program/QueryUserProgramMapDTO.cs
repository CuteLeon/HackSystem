namespace HackSystem.WebDataTransfer.Program
{
    public class QueryUserProgramMapDTO
    {
        public QueryBasicProgramDTO Program { get; set; }

        public bool Hide { get; set; }

        public bool PinToDock { get; set; }

        public bool PinToTop { get; set; }

        public string Rename { get; set; }
    }
}
