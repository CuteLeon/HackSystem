namespace HackSystem.Web.Component.Contracts;

public enum Borders
{
    None = 0b0000,
    Border = 0b1111,
    BorderTop = 0b1000,
    BorderBottom = 0b0100,
    BorderLeft = 0b0010,
    BorderRight = 0b0001,
    BorderNonTop = 0b0111,
    BorderNonBottom = 0b1011,
    BorderNonLeft = 0b1101,
    BorderNonRight = 0b1110,
    BorderTopBottom = 0b1100,
    BorderLeftRight = 0b0011,
    BorderTopLeft = 0b1010,
    BorderTopRight = 0b1001,
    BorderBottomLeft = 0b0110,
    BorderBottomRight = 0b0101,
}
