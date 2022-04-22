using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Items
{
    public class DeleteItemModel : PageModel
    {
        private readonly ItemService itemService;
        public Item Item { get; set; }
        public List<Item> Items { get; set; }
        [BindProperty] public int CategoryId { get; set; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteItemModel(ItemService itemService, IWebHostEnvironment webHostEnvironment)
        {
            this.itemService = itemService;
            _webHostEnvironment = webHostEnvironment;
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
            if (category == 0) return RedirectToPage("DeleteItem", new { id });

            Item = await itemService.GetItemWithCategoriesAsync(id);
            Items = await itemService.GetItemsWithCategoryIdAsync(category);
            CategoryId = category;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            var image = (await itemService.GetItemByIdAsync(id)).ImageName;
            if (!String.IsNullOrEmpty(image))
            {
                var file = Path.Combine(_webHostEnvironment.WebRootPath, "images/ItemImages", image);
                try
                {
                    System.IO.File.Delete(file);
                }
                catch
                {
                }
            }


            await itemService.DeleteItemAsync(id);
            return RedirectToPage("AllItems", "FilterByCategory", new { category = CategoryId });
        }
    
    }
}
