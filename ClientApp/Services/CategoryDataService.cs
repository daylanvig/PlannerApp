using Application.Categories.Queries.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public class CategoryDataService : DataService, ICategoryDataService
    {
        private readonly ICacheService cacheService;
        private const string CACHEKEY = nameof(CategoryDataService);

        public CategoryDataService(IAuthorizedHttpClientFactory authorizedHttpClientFactory, ICacheService cacheService) : base(authorizedHttpClientFactory)
        {
            this.cacheService = cacheService;
        }

        private Task<IEnumerable<CategoryModel>> LoadFromCache()
        {
            return cacheService.GetItem<IEnumerable<CategoryModel>>(CACHEKEY);
        }

        private async Task CacheItems(IEnumerable<CategoryModel> categories)
        {
            await cacheService.SetItem(CACHEKEY, categories);
        }

        public async Task<CategoryModel> LoadCategory(int id)
        {
            CategoryModel category = null;
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

        public async Task<IEnumerable<CategoryModel>> LoadCategories()
        {
            var items = await LoadFromCache();
            if(items == null)
            {
                var client = await GetClient();
                items = await client.GetJsonAsync<List<CategoryModel>>("/api/Categories");
                await CacheItems(items);
            }
            return items;
        }

        public async Task<CategoryModel> AddItem(CategoryModel category)
        {
            var client = await GetClient();
            CategoryModel createdCategory;
            try
            {
                createdCategory = await client.PostJsonAsync<CategoryModel>("/api/Categories", category);
                await cacheService.ClearCachedItem(CACHEKEY);
            }
            catch
            {
                createdCategory = null;
            }
            return createdCategory;
        }

        public async Task<CategoryModel> EditItem(CategoryModel category)
        {
            var client = await GetClient();
            await cacheService.ClearCachedItem(CACHEKEY);
            return await client.PutJsonAsync<CategoryModel>($"/api/Categories/{category.ID}", category);
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
