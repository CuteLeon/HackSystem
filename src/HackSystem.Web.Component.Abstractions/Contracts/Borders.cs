namespace HackSystem.Web.Component.Contracts;

public enum Borders
{
    None = 0b0000,
    Border = 0b1111,
    BorderTop = 0b1000,
    BorderBottom = 0b0100,
    BorderLeft = 0b0010,
    BorderRight = 0b0001,
    BorderNonTop = Border & ~BorderTop,
    BorderNonBottom = Border & ~BorderBottom,
    BorderNonLeft = Border & ~BorderLeft,
    BorderNonRight = Border & ~BorderRight,
    BorderTopBottom = BorderTop | BorderBottom,
    BorderLeftRight = BorderLeft | BorderRight,
    BorderTopLeft = BorderTop | BorderLeft,
    BorderTopRight = BorderTop | BorderRight,
    BorderBottomLeft = BorderBottom | BorderLeft,
    BorderBottomRight = BorderBottom | BorderRight,
}
