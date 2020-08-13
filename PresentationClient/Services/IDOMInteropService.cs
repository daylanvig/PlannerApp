using Microsoft.AspNetCore.Components;
using PresentationClient.Models.DOM;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public interface IDOMInteropService
    {
        Task<DOMRect> GetBoundingClientRect(ElementReference element);
        Task ScrollIntoView(string cssSelector);
    }
}