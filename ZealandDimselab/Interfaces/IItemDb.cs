using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZealandDimselab.Interfaces;
using ZealandDimselab.Models;

namespace ZealandDimselab.Interfaces
{
    public interface IItemDb : IDbService<Item>
    {
        public Task<Item> GetItemWithCategoriesAsync(int id);
        public Task<List<Item>> GetAllItemsWithCategoriesAsync();
        public Task<List<Item>> GetItemsWithCategoryId(int id);



    }
}
