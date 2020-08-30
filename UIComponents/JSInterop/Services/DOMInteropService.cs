using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using UIComponents.JSInterop.Models;

namespace UIComponents.JSInterop.Services
{
    public class DOMInteropService : IDOMInteropService
    {
        private readonly IJSInProcessRuntime jsRuntime;
        public DOMInteropService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = (IJSInProcessRuntime)jsRuntime;
        }

        public async Task<DOMRect> GetBoundingClientRect(ElementReference element)
        {
            var box = await jsRuntime.InvokeAsync<DOMRect>("window.customScripts.DOM.getBoundingClientRect", element);
            return box;
        }

        public async Task ScrollIntoView(string cssSelector)
        {
            await jsRuntime.InvokeVoidAsync("window.customScripts.DOM.scrollIntoView", cssSelector);
        }
    }
}
