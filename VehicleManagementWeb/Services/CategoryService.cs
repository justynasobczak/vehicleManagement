using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using VehicleManagementModels;

namespace VehicleManagementWeb.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Category> AddCategory(Category newCategory)
        {
            return await _httpClient.PostJsonAsync<Category>($"api/category", newCategory);
        }

        public async Task DeleteCategory(int id)
        {
            await _httpClient.DeleteAsync($"api/category/{id}");
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _httpClient.GetJsonAsync<Category[]>("api/category");
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _httpClient.GetJsonAsync<Category>($"api/category/{id}");
        }

        public async Task<Category> UpdateCategory(Category updatedCategory)
        {
            return await _httpClient.PutJsonAsync<Category>($"api/category", updatedCategory);
        }
    }
}
