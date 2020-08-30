using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIComponents.JSInterop.Services
{
    public class ScriptInteropService
    {
        private readonly IJSInProcessRuntime jsRuntime;
        public ScriptInteropService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = (IJSInProcessRuntime)jsRuntime;
        }

        protected T InvokeCustom<T>(string jsFunction, params object[] args)
        {
            return jsRuntime.Invoke<T>($"window.UIComponents.{jsFunction}", args);
        }

        protected void InvokeCustomVoid(string jsFunction, params object[] args)
        {
            jsRuntime.InvokeVoid($"window.UIComponents.{jsFunction}", args);
        }
    }
}
