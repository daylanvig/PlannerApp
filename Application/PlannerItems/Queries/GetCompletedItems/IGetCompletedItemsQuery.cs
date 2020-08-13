using Application.PlannerItems.Queries.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.PlannerItems.Queries.GetCompletedItems
{
    public interface IGetCompletedItemsQuery
    {
        Task<IReadOnlyList<PlannerItemModel>> Execute();
    }
}
