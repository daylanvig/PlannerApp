using Application.Interfaces;
using Domain.Categories;
using Domain.Common;
using Domain.PlannerItems;
using Microsoft.EntityFrameworkCore;
using Persistence.Categories;
using Persistence.Common;
using Persistence.PlannerItems;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class PlannerContext : DbContext, IPlannerContext
    {
        private readonly string tenantID;
        public DbSet<PlannerItem> PlannerItem { get; set; }
        public DbSet<Category> Category { get; set; }
        public PlannerContext(DbContextOptions<PlannerContext> options, ITenantService tenantService) : base(options)
        {
            tenantID = tenantService?.GetTenantID() ?? "";
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries();
            var addedEntities = entities.Where(e => e.State == EntityState.Added && e.Entity.GetType().IsSubclassOf(typeof(Entity)));
            foreach(var addedEntity in addedEntities)
            {
                var entity = (Entity)addedEntity.Entity;
                entity.TenantID = tenantID;
            }
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration(tenantID));
            modelBuilder.ApplyConfiguration(new PlannerItemConfiguration(tenantID));
        }
    }
}
