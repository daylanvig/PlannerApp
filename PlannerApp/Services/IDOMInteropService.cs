using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Models.DOM;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public interface IDOMInteropService
    {
        Task<DOMRect> GetBoundingClientRect(ElementReference element);
        Task ScrollIntoView(string cssSelector);
    }
}