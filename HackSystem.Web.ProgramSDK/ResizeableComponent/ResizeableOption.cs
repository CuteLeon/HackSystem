namespace HackSystem.Web.ProgramSDK.ResizeableComponent;

    public class ResizeableOption
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public string ResizeTarget { get; set; } = ".position-fixed";

        public int BorderSize { get; set; } = 8;

        public int CornerSize { get; set; } = 16;

        public int Z_Index { get; set; }
    }
