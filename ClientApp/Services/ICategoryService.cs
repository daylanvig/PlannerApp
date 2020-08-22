using Application.Categories.Queries.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface ICategoryService
    {
        Task<IDictionary<CategoryModel, int>> GetTotalMinutesByCategory();
    }
}