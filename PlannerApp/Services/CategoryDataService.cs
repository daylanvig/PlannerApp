using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public class CategoryDataService : DataService, ICategoryDataService
    {
        private readonly ICacheService cacheService;

        public CategoryDataService(IAuthorizedHttpClientFactory authorizedHttpClientFactory, ICacheService cacheService) : base(authorizedHttpClientFactory)
        {
            this.cacheService = cacheService;
        }

        public async Task<IEnumerable<CategoryDTO>> LoadCategories()
        {
            var items = await cacheService.GetItem<IEnumerable<CategoryDTO>>(nameof(CategoryDataService));
            if(items == null)
            {
                var client = await GetClient();
                items = await client.GetJsonAsync<List<CategoryDTO>>("/api/Categories");
                await cacheService.SetItem(nameof(CategoryDataService), items);
            }
            return items;
        }

        public async Task<CategoryDTO> AddItem(CategoryDTO category)
        {
            var client = await GetClient();
            CategoryDTO createdCategory;
            try
            {
                createdCategory = await client.PostJsonAsync<CategoryDTO>("/api/Categories", category);
            }
            catch
            {
                createdCategory = null;
            }
            return createdCategory;
        }

        public async Task<CategoryDTO> EditItem(CategoryDTO category)
        {
            var client = await GetClient();
            return await client.PutJsonAsync<CategoryDTO>($"/api/Categories/{category.ID}", category);
        }

        public async Task DeleteItem(int categoryID)
        {
            var client = await GetClient();
            var response = await client.DeleteAsync($"/api/Categories/{categoryID}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException();
            }
        }
    }
}
