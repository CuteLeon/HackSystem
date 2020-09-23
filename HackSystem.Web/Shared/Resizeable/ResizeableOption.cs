namespace HackSystem.Web.Shared.Resizeable
{
    public class ResizeableOption
    {
        public int Left { get; set; } = 0;

        public int Top { get; set; } = 0;

        public string ResizeTarget { get; set; } = ".position-fixed";

        public int BorderSize { get; set; } = 8;

        public int CornerSize { get; set; } = 16;
    }
}
