﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PlannerApp.Client.Models.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
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
            Console.WriteLine(box.Bottom);
            return box;
        }

        public async Task ScrollIntoView(string cssSelector)
        {
            await jsRuntime.InvokeVoidAsync("window.customScripts.scrollIntoView", cssSelector);
        }
    }
}