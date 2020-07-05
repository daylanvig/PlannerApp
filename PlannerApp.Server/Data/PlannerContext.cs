using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PlannerApp.Server.Data.Configurations;
using PlannerApp.Server.Models;
using PlannerApp.Server.Services;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PlannerApp.Server.Data
{
    public class PlannerContext : DbContext
    {
        private readonly string tenantID;
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
                entity.tenantID = tenantID;
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
