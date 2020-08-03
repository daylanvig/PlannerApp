using PlannerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public interface ICategoryService
    {
        Task<IDictionary<CategoryDTO, int>> GetTotalMinutesByCategory();
    }
}