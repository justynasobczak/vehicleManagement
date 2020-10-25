using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleManagementModels;

namespace VehicleManagementApi.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Category> AddCategory(Category category)
        {
            var result = await _appDbContext.Categories.AddAsync(category);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> DeleteCategory(int categoryId)
        {
            var dbEntry = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (dbEntry != null)
            {
                _appDbContext.Categories.Remove(dbEntry);
                await _appDbContext.SaveChangesAsync();
                return dbEntry;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _appDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var dbEntry = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);
            if (dbEntry != null)
            {
                dbEntry.CategoryName = category.CategoryName;
                dbEntry.WeightFrom = category.WeightFrom;
                dbEntry.WeightUpTo = category.WeightUpTo;
                dbEntry.Icon = category.Icon;

                await _appDbContext.SaveChangesAsync();
                return dbEntry;
            }
            return null;
        }
    }
}
