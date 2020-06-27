using Microsoft.EntityFrameworkCore;

namespace PlannerApp.Infrastructure.Data
{
    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options): base(options)
        {

        }
    }
}
