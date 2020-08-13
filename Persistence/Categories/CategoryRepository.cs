using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.Categories;
using Persistence.Common;

namespace Persistence.Categories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IPlannerContext context, ITenantService tenantService) : base(context, tenantService)
        {
        }
    }
}
