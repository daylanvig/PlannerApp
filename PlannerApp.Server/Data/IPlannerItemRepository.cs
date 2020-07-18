using PlannerApp.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlannerApp.Server.Data
{
    public interface IPlannerItemRepository : IRepository<PlannerItem>
    {
        Task<IReadOnlyList<PlannerItem>> LoadOverdueItems();
    }
}
