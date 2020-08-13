using Domain.Categories;
using Domain.PlannerItems;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Common
{
    public interface IPlannerContext
    {
        DbSet<PlannerItem> PlannerItem { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
