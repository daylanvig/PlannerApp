﻿using Microsoft.AspNetCore.Components;
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

        /// <summary>
        /// Loads planner items.
        /// If no date values are supplied, all items are loaded.
        /// Otherwise items are loaded for the provided date range.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
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

        public async Task<IEnumerable<PlannerItemDTO>> LoadCompletedItemsByCategoryID(int? id)
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

        public async Task<IEnumerable<PlannerItemDTO>> LoadCompletedItems()
        {
            var client = await GetClient();
            return await client.GetJsonAsync<List<PlannerItemDTO>>("/api/PlannerItems/Completed");
        }

        public async Task<IEnumerable<PlannerItemDTO>> LoadOverdueItems()
        {
            var client = await GetClient();
            return await client.GetJsonAsync<List<PlannerItemDTO>>("/api/PlannerItems/Overdue");
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
