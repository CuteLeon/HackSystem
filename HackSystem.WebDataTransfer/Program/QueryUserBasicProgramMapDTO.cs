namespace HackSystem.WebDataTransfer.Program;

    public class QueryUserBasicProgramMapDTO
    {
        public QueryBasicProgramDTO BasicProgram { get; set; }

        public bool Hide { get; set; }

        public bool PinToDock { get; set; }

        public bool PinToTop { get; set; }

        public string Rename { get; set; }
    }
