using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public interface IPlannerItemDataService
    {
        Task<IEnumerable<PlannerItemDTO>> LoadItems(DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<PlannerItemDTO>> LoadCompletedItems();
        Task<IEnumerable<PlannerItemDTO>> LoadOverdueItems();
        Task<PlannerItemDTO> AddItem(PlannerItemDTO plannerItem);
        Task DeleteItem(int itemID);
        Task<PlannerItemDTO> EditItem(PlannerItemDTO plannerItem);
    }
}