using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public class CategoryDataService : ICategoryDataService
    {
        private readonly HttpClient client;
        public CategoryDataService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<CategoryDTO>> LoadCategories()
        {
            return await client.GetJsonAsync<List<CategoryDTO>>("/api/Categories");
        }

        public async Task<CategoryDTO> AddItem(CategoryDTO category)
        {
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
            return await client.PutJsonAsync<CategoryDTO>($"/api/Categories/{category.ID}", category);
        }

        public async Task DeleteItem(int categoryID)
        {
            var response = await client.DeleteAsync($"/api/Categories/{categoryID}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException();
            }
        }
    }
}
