using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.PlannerItems;
using Persistence.Common;

namespace Persistence.PlannerItems
{
    public class PlannerItemRepository : Repository<PlannerItem>, IPlannerItemRepository
    {
        public PlannerItemRepository(IPlannerContext context, ITenantService tenantService) : base(context, tenantService)
        {
        }
    }
}
