using Microsoft.EntityFrameworkCore;
using PlannerApp.Server.Models;
using PlannerApp.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Server.Data
{

    public class PlannerItemRepository : Repository<PlannerItem>, IPlannerItemRepository
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public PlannerItemRepository(PlannerContext context, ITenantService tenantService, IDateTimeProvider dateTimeProvider) : base(context, tenantService)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<IReadOnlyList<PlannerItem>> LoadOverdueItems()
        {
            var overdueItems = await items.AsQueryable()
                                    .Where(item => item.CompletionDate == null && item.PlannedEndTime < dateTimeProvider.Now)
                                    .ToListAsync();
            return overdueItems;
        }
    }
}
