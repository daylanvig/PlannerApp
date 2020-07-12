using System;
using System.Collections.Generic;
using System.Text;

namespace UIComponents.Bulma.Helpers
{
    public enum ComponentColour
    {
        Primary,
        Success,
        Warning,
        Info,
        Danger,
        Hovered,
        Link
    }

    public static class ComponentColourHelper
    {
        public static string ToClass(this ComponentColour colour)
        {
            switch (colour)
            {
                case ComponentColour.Primary:
                    return "is-primary";
                case ComponentColour.Success:
                    return "is-success";
                case ComponentColour.Warning:
                    return "is-warning";
                case ComponentColour.Info:
                    return "is-info";
                case ComponentColour.Danger:
                    return "is-danger";
                case ComponentColour.Link:
                    return "is-link";
                case ComponentColour.Hovered:
                    return "is-hovered";
                default:
                    throw new NotImplementedException();
            }
        }
    }

}
