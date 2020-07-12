using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIComponents.Bulma
{
    public class UIComponentBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
        [Parameter]
        public string CssClass { get; set; }
    }
}
