using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public class PlannerItemDataService : IPlannerItemDataService
    {
        private readonly HttpClient client;
        public PlannerItemDataService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<PlannerItemDTO>> LoadItems(DateTime? startDate = null, DateTime? endDate = null)
        {
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
            return await client.PutJsonAsync<PlannerItemDTO>($"/api/PlannerItems/{plannerItem.ID}", plannerItem);
        }

        public async Task DeleteItem(int itemID)
        {
            var response = await client.DeleteAsync($"/api/PlannerItems/{itemID}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException();
            }
        }
    }
}
