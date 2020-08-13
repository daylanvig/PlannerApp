using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public class PlannerItemDataService : DataService, IPlannerItemDataService
    {
        public PlannerItemDataService(IAuthorizedHttpClientFactory authorizedHttpClientFactory) : base(authorizedHttpClientFactory)
        {
        }

        /// <summary>
        /// Loads planner items.
        /// If no date values are supplied, all items are loaded.
        /// Otherwise items are loaded for the provided date range.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PlannerItemModel>> LoadItems(DateTime? startDate = null, DateTime? endDate = null)
        {
            var client = await GetClient();
            string url = "/api/PlannerItems?";
            if (startDate.HasValue)
            {
                url += $"StartDate={startDate.Value:yyyy-MM-dd}";
                if (endDate.HasValue)
                {
                    url += $"&EndDate={endDate.Value:yyyy-MM-dd}";
                }
            }
            return await client.GetJsonAsync<List<PlannerItemModel>>(url);
        }

        public async Task<IEnumerable<PlannerItemModel>> LoadCompletedItemsByCategoryID(int? id)
        {
            var items = await LoadCompletedItems();
            if(id == 0)
            {
                items = items.Where(i => !i.CategoryID.HasValue);
            }
            else
            {
                items = items.Where(i => i.CategoryID == id);
            }
            return items;
        }

        public async Task<IEnumerable<PlannerItemModel>> LoadCompletedItems()
        {
            var client = await GetClient();
            return await client.GetJsonAsync<List<PlannerItemModel>>("/api/PlannerItems/Completed");
        }

        public async Task<IEnumerable<PlannerItemModel>> LoadOverdueItems()
        {
            var client = await GetClient();
            return await client.GetJsonAsync<List<PlannerItemModel>>("/api/PlannerItems/Overdue");
        }

        public async Task<PlannerItemModel> AddItem(PlannerItemCreateEditModel plannerItem)
        {
            PlannerItemModel createdItem;
            var client = await GetClient();
            try
            {
                createdItem = await client.PostJsonAsync<PlannerItemModel>("/api/PlannerItems", plannerItem);
            }
            catch
            {
                createdItem = null;
            }
            return createdItem;
        }

        public async Task<PlannerItemModel> EditItem(PlannerItemCreateEditModel plannerItem)
        {
            var client = await GetClient();
            return await client.PutJsonAsync<PlannerItemModel>($"/api/PlannerItems/{plannerItem.ID}", plannerItem);
        }

        public async Task DeleteItem(int itemID)
        {
            var client = await GetClient();
            var response = await client.DeleteAsync($"/api/PlannerItems/{itemID}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException();
            }
        }
    }
}
