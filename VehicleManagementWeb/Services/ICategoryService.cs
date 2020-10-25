using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagementModels;

namespace VehicleManagementWeb.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<Category> UpdateCategory(Category updatedCategory);
        Task<Category> AddCategory(Category newCategory);
        Task DeleteCategory(int id);
    }
}
