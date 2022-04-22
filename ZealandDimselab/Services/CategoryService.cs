using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Interfaces;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class CategoryService
    {
        private readonly IDbService<Category> dbService;
        public CategoryService(IDbService<Category> dbService)
        {
            this.dbService = dbService;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return (await dbService.GetObjectsAsync()).ToList();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await dbService.GetObjectByKeyAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await dbService.AddObjectAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await dbService.DeleteObjectAsync(await GetCategoryByIdAsync(id));
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            category.CategoryId = id;
            await dbService.UpdateObjectAsync(category);
        }
    }
}
