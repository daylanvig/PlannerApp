using PlannerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public interface ICategoryService
    {
        Task<IDictionary<CategoryDTO, int>> GetTotalMinutesByCategory();
    }
}