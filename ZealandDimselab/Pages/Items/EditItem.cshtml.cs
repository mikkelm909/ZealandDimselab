using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Items
{
    public class EditItemModel : PageModel
    {
        private readonly ItemService itemService;
        private readonly CategoryService categoryService;
        private readonly BookingService _bookingService;
        [BindProperty] public Item Item { get; set; }
        public List<Item> Items { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty] public int CategoryId { get; set; }
        [BindProperty] public IFormFile FileUpload { get; set; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditItemModel(ItemService itemService, CategoryService categoryService, BookingService bookingService, IWebHostEnvironment whe)
        {
            this.itemService = itemService;
            this.categoryService = categoryService;
            _bookingService = bookingService;
            _webHostEnvironment = whe;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Items = await itemService.GetAllItemsWithCategoriesAsync();
            Categories = await categoryService.GetAllCategoriesAsync();
            Item = await itemService.GetItemWithCategoriesAsync(id);
            CategoryId = 0;

            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategoryAsync(int id, int category)
        {
            if (category == 0) return RedirectToPage("EditItem", new { id });

            Items = await itemService.GetItemsWithCategoryIdAsync(category);
            Categories = await categoryService.GetAllCategoriesAsync();
            Item = await itemService.GetItemWithCategoriesAsync(id);
            CategoryId = category;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (CategoryId == 0) return await OnGetAsync(Item.Id);
                return await OnGetFilterByCategoryAsync(Item.Id, CategoryId);
            }

            if (FileUpload != null)
            {
                var fileName = Item.Id + "." + FileUpload.ContentType.TrimStart('i', 'm', 'a', 'g', 'e', '/');
                var fileUpload = Path.Combine(_webHostEnvironment.WebRootPath, "images/ItemImages", fileName);
                using (var Fs = new FileStream(fileUpload, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(Fs);
                }

                Item.ImageName = fileName;
            }
            else
            {
                Item.ImageName = (await itemService.GetItemWithCategoriesAsync(Item.Id)).ImageName;
            }

            var bookedItems = await _bookingService.GetAllBookedItemsAsync();
            int matchingId = 0;
            foreach (var bookedItem in bookedItems)
            {
                if (bookedItem.Item.Id == Item.Id && String.Equals(bookedItem.Status, "Not Returned"))
                {
                    matchingId += bookedItem.Quantity;
                }
            }
            Item.Stock = Item.Quantity - matchingId;

            await itemService.UpdateItemAsync(Item.Id, Item);
            return RedirectToPage("AllItems", "FilterByCategory", new { category = CategoryId });
        }
    }
}
