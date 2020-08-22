using ClientApp.Models.DOM;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ClientApp.Services
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
            var box = await jsRuntime.InvokeAsync<DOMRect>("window.customScripts.getBoundingClientRect", element);
            return box;
        }

        public async Task ScrollIntoView(string cssSelector)
        {
            await jsRuntime.InvokeVoidAsync("window.customScripts.scrollIntoView", cssSelector);
        }
    }
}
