using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PlannerApp.Server.Data
{
    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
