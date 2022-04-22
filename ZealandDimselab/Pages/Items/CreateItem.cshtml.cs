using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Items
{
    public class CreateItemModel : PageModel
    {
        private readonly ItemService itemService;
        private readonly CategoryService categoryService;
        [BindProperty] public Item Item { get; set; }
        public List<Item> Items { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty] public int CategoryId { get; set; }
        [BindProperty] public IFormFile FileUpload { get; set; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateItemModel(ItemService itemService, CategoryService categoryService, IWebHostEnvironment whe)
        {
            this.itemService = itemService;
            this.categoryService = categoryService;
            _webHostEnvironment = whe;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            Items = await itemService.GetAllItemsWithCategoriesAsync();
            Categories = await categoryService.GetAllCategoriesAsync();
            CategoryId = 0;

            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategoryAsync(int category)
        {
            if (category == 0) return RedirectToPage("CreateItem");

            Items = await itemService.GetItemsWithCategoryIdAsync(category);
            Categories = await categoryService.GetAllCategoriesAsync();
            CategoryId = category;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                if (CategoryId == 0) return await OnGetAsync();
                return await OnGetFilterByCategoryAsync(CategoryId);
            }

            await itemService.AddItemAsync(Item);
            Item item = (await itemService.GetAllItems()).Last();

            if (FileUpload != null)
            {
                var fileName = item.Id + "." + FileUpload.ContentType.TrimStart('i','m','a','g','e','/');
                var fileUpload = Path.Combine(_webHostEnvironment.WebRootPath, "images/ItemImages", fileName);
                using (var Fs = new FileStream(fileUpload, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(Fs);
                }

                item.ImageName = fileName;
            }

            await itemService.UpdateItemAsync(item.Id, item);

            if (CategoryId == 0) return RedirectToPage("AllItems");
            if (Item.CategoryId != CategoryId)
                return RedirectToPage("AllItems", "FilterByCategory", new {category = Item.CategoryId});
            return RedirectToPage("AllItems", "FilterByCategory", new { category = CategoryId });
        }
    }
}
