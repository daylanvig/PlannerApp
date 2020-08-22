using Microsoft.AspNetCore.Components;
using ClientApp.Models.DOM;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface IDOMInteropService
    {
        Task<DOMRect> GetBoundingClientRect(ElementReference element);
        Task ScrollIntoView(string cssSelector);
    }
}