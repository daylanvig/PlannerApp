using Application.Categories.Queries.Common;
using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IGetAllCategoriesQuery
    {
        private readonly ICategoryRepository categoryRepository;

        public GetAllCategoriesQuery(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryModel>> Execute()
        {
            return await categoryRepository
                .GetAll()
                .OrderBy(c => c.Description)
                .Select(c => new CategoryModel
                {
                    ID = c.ID,
                    Colour = c.Colour,
                    Description = c.Description
                }).ToListAsync();
        }
    }
}
