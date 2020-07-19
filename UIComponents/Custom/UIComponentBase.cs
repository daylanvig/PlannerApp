using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace UIComponents.Bulma.Custom
{
    public class UIComponentBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
        // This is allows the normal "class" attribute to be merged with any component classes
        public string CssClass
        {
            get
            {
                if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("class", out object value))
                {
                    return value.ToString();
                }
                return string.Empty;
            }
        }
    }
}
