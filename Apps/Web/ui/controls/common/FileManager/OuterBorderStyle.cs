using System;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    [Flags]
    public enum OuterBorderStyle
    {
        None = 0x00,
        Top = 0x01,
        Bottom = 0x02,
        Left = 0x04,
        Right = 0x08,
        TopAndBottom = 0x03,
        LeftAndRight = 0x0C,
        AllSides = 0x0F
    }
}
