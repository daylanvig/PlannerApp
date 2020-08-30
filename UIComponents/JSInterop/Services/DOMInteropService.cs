using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using UIComponents.JSInterop.Models;

namespace UIComponents.JSInterop.Services
{
    public class DOMInteropService : ScriptInteropService, IDOMInteropService
    {
        
        public DOMInteropService(IJSRuntime jsRuntime) : base(jsRuntime)
        {
            
        }

        public DOMRect GetBoundingClientRect(ElementReference element)
        {
            return InvokeCustom<DOMRect>("DOM.getBoundingClientRect", element);
        }

        public void ScrollIntoView(string cssSelector)
        {
            InvokeCustomVoid("DOM.scrollIntoView", cssSelector);
        }
    }
}
