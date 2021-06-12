using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Data;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        public DetailsModel(PrzepisyWeb.Data.RecipeContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.categoryID == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
