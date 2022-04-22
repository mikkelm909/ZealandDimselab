using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Items
{
    public class ItemDetailsModel : PageModel
    {
        private readonly ItemService itemService;
        public Item Item { get; set; }
        public List<Item> Items { get; set; }
        public int CategoryId { get; set; }

        public ItemDetailsModel(ItemService itemService)
        {
            this.itemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await itemService.GetItemWithCategoriesAsync(id);
            Items = await itemService.GetAllItemsWithCategoriesAsync();
            CategoryId = 0;
            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategory(int id, int category)
        {
            if (category == 0) return RedirectToPage("ItemDetails", new { id });
            Items = await itemService.GetItemsWithCategoryIdAsync(category);
            Item = await itemService.GetItemWithCategoriesAsync(id);
            CategoryId = category;
            return Page();
        }
    }
}
