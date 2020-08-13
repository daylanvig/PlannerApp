using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public class CategoryDataService : DataService, ICategoryDataService
    {
        private readonly ICacheService cacheService;
        private const string CACHEKEY = nameof(CategoryDataService);

        public CategoryDataService(IAuthorizedHttpClientFactory authorizedHttpClientFactory, ICacheService cacheService) : base(authorizedHttpClientFactory)
        {
            this.cacheService = cacheService;
        }

        private Task<IEnumerable<CategoryDTO>> LoadFromCache()
        {
            return cacheService.GetItem<IEnumerable<CategoryDTO>>(CACHEKEY);
        }

        private async Task CacheItems(IEnumerable<CategoryDTO> categories)
        {
            await cacheService.SetItem(CACHEKEY, categories);
        }

        public async Task<CategoryDTO> LoadCategory(int id)
        {
            CategoryDTO category = null;
            var items = await LoadFromCache();
            if(items != null)
            {
                category = items.FirstOrDefault(c => c.ID == id);
            }

            if (items == null || category == null)
            {
                await cacheService.ClearCachedItem(CACHEKEY);
                items = await LoadCategories();
                category = items.First(c => c.ID == id);
            }
            return category;
        }

        public async Task<IEnumerable<CategoryDTO>> LoadCategories()
        {
            var items = await LoadFromCache();
            if(items == null)
            {
                var client = await GetClient();
                items = await client.GetJsonAsync<List<CategoryDTO>>("/api/Categories");
                await CacheItems(items);
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
                await cacheService.ClearCachedItem(CACHEKEY);
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
            await cacheService.ClearCachedItem(CACHEKEY);
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
            await cacheService.ClearCachedItem(CACHEKEY);
        }
    }
}
