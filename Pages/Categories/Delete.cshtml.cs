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
    public class DeleteModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        public DeleteModel(PrzepisyWeb.Data.RecipeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        public IList<RecipeCategory> RecipeCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryID == id);

            var ListOfRecipies = from X in _context.RecipeCategories where X.CategoryID == id select X;

            RecipeCategories = ListOfRecipies.ToList();

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FindAsync(id);

            var ListOfRecipies = from X in _context.RecipeCategories where X.CategoryID == id select X;

            RecipeCategories = ListOfRecipies.ToList();

            if (Category != null)
            {
                foreach (var item in RecipeCategories) 
                {
                    _context.RecipeCategories.Remove(item);
                    
                }
                    _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
