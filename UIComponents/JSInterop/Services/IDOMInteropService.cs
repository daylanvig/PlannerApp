using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using UIComponents.JSInterop.Models;

namespace UIComponents.JSInterop.Services
{
    public interface IDOMInteropService
    {
        DOMRect GetBoundingClientRect(ElementReference element);
        void ScrollIntoView(string cssSelector);
    }
}