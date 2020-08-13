using Application.Categories.Queries.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetAllCategories
{
    public interface IGetAllCategoriesQuery
    {
        Task<IEnumerable<CategoryModel>> Execute();
    }
}