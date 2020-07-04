using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public class PlannerItemDataService : DataService, IPlannerItemDataService
    {


        public PlannerItemDataService(IAuthorizedHttpClientFactory authorizedHttpClientFactory) : base(authorizedHttpClientFactory)
        {
        }

        public async Task<IEnumerable<PlannerItemDTO>> LoadItems(DateTime? startDate = null, DateTime? endDate = null)
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
            return await client.GetJsonAsync<List<PlannerItemDTO>>(url);
        }

        public async Task<PlannerItemDTO> AddItem(PlannerItemDTO plannerItem)
        {
            PlannerItemDTO createdItem;
            var client = await GetClient();
            try
            {
                createdItem = await client.PostJsonAsync<PlannerItemDTO>("/api/PlannerItems", plannerItem);
            }
            catch
            {
                createdItem = null;
            }
            return createdItem;
        }

        public async Task<PlannerItemDTO> EditItem(PlannerItemDTO plannerItem)
        {
            var client = await GetClient();
            return await client.PutJsonAsync<PlannerItemDTO>($"/api/PlannerItems/{plannerItem.ID}", plannerItem);
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
