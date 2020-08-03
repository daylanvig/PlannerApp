using Microsoft.AspNetCore.Components;

namespace UIComponents.Custom.SheetComponent
{
    public enum SheetSide
    {
        Left, 
        Right
    }

    public class SheetParams
    {
        public SheetSide Side { get; set; } = SheetSide.Right;
        public RenderFragment Body { get; set; }
    }
}
