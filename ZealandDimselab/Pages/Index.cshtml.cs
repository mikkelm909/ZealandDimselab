using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Category> Categories { get; set; }
        private CategoryService categoryService;

        public IndexModel(ILogger<IndexModel> logger, CategoryService categoryService)
        {
            _logger = logger;
            this.categoryService = categoryService;
        }

        public async Task OnGetAsync()
        {
            Categories = await categoryService.GetAllCategoriesAsync();
        }
    }
}
