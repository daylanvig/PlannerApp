using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PlannerApp.Server.Data.Configurations;
using PlannerApp.Server.Services;
using System.Reflection;

namespace PlannerApp.Server.Data
{
    public class PlannerContext : DbContext
    {
        private readonly string tenantID;
        public PlannerContext(DbContextOptions<PlannerContext> options, ITenantService tenantService) : base(options)
        {
            tenantID = tenantService?.GetTenantID() ?? "";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration(tenantID));
            modelBuilder.ApplyConfiguration(new PlannerItemConfiguration(tenantID));
        }
    }
}
