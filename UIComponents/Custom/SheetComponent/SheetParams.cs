using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIComponents.Custom.SheetComponent
{
    public class SheetParams
    {
        public RenderFragment Body { get; set; }
        public SheetParams() { }
        public SheetParams(Type bodyType)
        {
            Body = new RenderFragment(x =>
            {
                x.OpenComponent(1, bodyType);
                x.CloseComponent();
            });
        }
    }
}
