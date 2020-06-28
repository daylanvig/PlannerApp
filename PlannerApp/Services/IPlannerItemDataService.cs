using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public interface IPlannerItemDataService
    {
        Task<IEnumerable<PlannerItemDTO>> LoadItems(DateTime? date = null);
        Task<PlannerItemDTO> AddItem(PlannerItemDTO plannerItem);
        Task DeleteItem(int itemID);
    }
}