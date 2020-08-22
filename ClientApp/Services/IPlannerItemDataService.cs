using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface IPlannerItemDataService
    {
        Task<IEnumerable<PlannerItemModel>> LoadItems(DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<PlannerItemModel>> LoadCompletedItemsByCategoryID(int? id);
        Task<IEnumerable<PlannerItemModel>> LoadCompletedItems();
        Task<IEnumerable<PlannerItemModel>> LoadOverdueItems();
        Task<PlannerItemModel> AddItem(PlannerItemCreateEditModel plannerItem);
        Task DeleteItem(int itemID);
        Task<PlannerItemModel> EditItem(PlannerItemCreateEditModel plannerItem);
    }
}