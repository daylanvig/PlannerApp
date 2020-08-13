using PlannerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public interface ICategoryDataService
    {
        Task<CategoryDTO> AddItem(CategoryDTO category);
        Task DeleteItem(int categoryID);
        Task<CategoryDTO> EditItem(CategoryDTO category);
        Task<IEnumerable<CategoryDTO>> LoadCategories();
        Task<CategoryDTO> LoadCategory(int id);
    }
}