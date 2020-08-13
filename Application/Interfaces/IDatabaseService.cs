using Microsoft.EntityFrameworkCore;
using Domain.Categories;
using Domain.PlannerItems;

namespace Application.Interfaces
{
    public interface IDatabaseService
    {
        public DbSet<PlannerItem> PlannerItems { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
