using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Data;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        public DetailsModel(PrzepisyWeb.Data.RecipeContext context)
        {
            _context = context;
        }

        public Recipe Recipe { get; set; }

        public ICollection<ImageGallery> Images { get; set; }

        public List<int> CategoryIDs { get; set; }

        public List<Category> Categories { get; set; }

        public List<string> CategoryNames { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           

            Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.RecipeID == id);

            var Query_1 = from X in _context.RecipeCategories where Recipe.RecipeID == X.RecipeID select X.CategoryID;

            Categories = new List<Category>();
            CategoryNames = new List<string>();

            CategoryIDs = Query_1.ToList();

            foreach (var item in CategoryIDs)
            {
                foreach(var cat in _context.Categories)
                {
                    if(item == cat.CategoryID)
                    {
                        Categories.Add(cat);
                    }
                }
            }

            foreach(var cat in Categories)
            {
                CategoryNames.Add(cat.CategoryName);
            }

            CategoryNames.ToList();

            if (Recipe == null)
            {
                return NotFound();
            }

            return Page();
        }

    }
}
