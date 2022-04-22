using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Categories
{
    public class EditCategoryModel : PageModel
    {
        private CategoryService categoryService;
        [BindProperty]
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }

        public EditCategoryModel(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Categories = await categoryService.GetAllCategoriesAsync();
            Category = await categoryService.GetCategoryByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Categories = await categoryService.GetAllCategoriesAsync();
                Category = await categoryService.GetCategoryByIdAsync(id);
                return Page();
            }

            await categoryService.UpdateCategoryAsync(Category.CategoryId, Category);
            return RedirectToPage("/Index");

        }
    }
}
