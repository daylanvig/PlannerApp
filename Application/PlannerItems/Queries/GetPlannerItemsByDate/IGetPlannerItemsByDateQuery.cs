using Application.PlannerItems.Queries.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.PlannerItems.Queries.GetPlannerItemsByDate
{
    public interface IGetPlannerItemsByDateQuery
    {
        Task<IReadOnlyList<PlannerItemModel>> Execute(DateTime? startDate, DateTime? endDate);
    }
}
