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
    public class CreateCategoryModel : PageModel
    {
        private CategoryService categoryService;
        [BindProperty] 
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }

        public CreateCategoryModel(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await categoryService.GetAllCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await categoryService.GetAllCategoriesAsync();
                return Page();
            }

            await categoryService.AddCategoryAsync(Category);
            return RedirectToPage("/Index");
        }
    }
}
