using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;
using ZealandDimselab.Helpers;

namespace ZealandDimselab.Pages.Items
{
    public class AllItemsModel : PageModel
    {
        public List<Item> Items { get; set; }
        private readonly ItemService _itemService;
        public int CategoryId { get; set; }
        public List<Item> Cart { get; set; }

        public AllItemsModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Items = await _itemService.GetAllItemsWithCategoriesAsync();
            CategoryId = 0;
            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategoryAsync(int category)
        {
            if (category == 0) return OnGetAsync().Result;
            CategoryId = category;
            Items = await _itemService.GetItemsWithCategoryIdAsync(category);
            return Page();
        }

        public async Task<IActionResult> OnGetAddToCart(int id)
        {
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (Cart == null) // Check if Cart exists in user cache.
            {
                Cart = new List<Item>
                {
                    await _itemService.GetItemByIdAsync(id)
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            else // If it does, append to that.
            {
                int index = SessionHelper.Exists(Cart, id);
                if (index == -1) // if the item does not exists in the cart, append it.
                {
                    Cart.Add(await _itemService.GetItemByIdAsync(id));
                }
                //else 
                //{
                //    Cart[index].Quantity++;
                //}
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            return RedirectToPage();
        }
    }
}
