using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace UIComponents.JSInterop.Services
{
    public class JSDateHelperService : IJSDateHelperService
    {
        private readonly IJSInProcessRuntime jsRuntime;

        public JSDateHelperService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = (IJSInProcessRuntime)jsRuntime;
        }

        public int GetTimeZoneOffset()
        {
            return jsRuntime.Invoke<int>("window.customScripts.Date.getTimeZoneOffset");
        }
    }
}
