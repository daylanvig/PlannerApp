using Application.Categories.Queries.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface ICategoryDataService
    {
        Task<CategoryModel> AddItem(CategoryModel category);
        Task DeleteItem(int categoryID);
        Task<CategoryModel> EditItem(CategoryModel category);
        Task<IEnumerable<CategoryModel>> LoadCategories();
        Task<CategoryModel> LoadCategory(int id);
    }
}