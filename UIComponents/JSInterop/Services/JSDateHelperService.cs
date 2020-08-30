using Microsoft.JSInterop;

namespace UIComponents.JSInterop.Services
{
    public class JSDateHelperService : ScriptInteropService, IJSDateHelperService
    {


        public JSDateHelperService(IJSRuntime jsRuntime) : base(jsRuntime)
        {
        }

        public int GetTimeZoneOffset()
        {
            return InvokeCustom<int>("Date.getTimeZoneOffset");
        }
    }
}
