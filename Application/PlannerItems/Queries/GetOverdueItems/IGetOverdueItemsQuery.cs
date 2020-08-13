using Application.PlannerItems.Queries.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.PlannerItems.Queries.GetOverdueItems
{
    public interface IGetOverdueItemsQuery
    {
        Task<IReadOnlyList<PlannerItemModel>> Execute();
    }
}