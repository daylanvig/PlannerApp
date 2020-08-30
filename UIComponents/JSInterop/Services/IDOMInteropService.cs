using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using UIComponents.JSInterop.Models;

namespace UIComponents.JSInterop.Services
{
    public interface IDOMInteropService
    {
        Task<DOMRect> GetBoundingClientRect(ElementReference element);
        Task ScrollIntoView(string cssSelector);
    }
}