using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Data;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages.Recipes
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        public EditModel(PrzepisyWeb.Data.RecipeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        //3 stringi
        [BindProperty]
        public string Img_1 { get; set; }
        [BindProperty]
        public string Img_2 { get; set; }
        [BindProperty]
        public string Img_3 { get; set; }
        [BindProperty]
        public IList<Image> Images { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.RecipeID == id);

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var temp = Recipe.RecipeID;
            Recipe.Date = DateTime.Now;

            Image Image1 = new Image();
            Image Image2 = new Image();
            Image Image3 = new Image();

            Image1.Url = Img_1;
            Image1.Recipe = Recipe;

            Image2.Url = Img_2;
            Image2.Recipe = Recipe;

            Image3.Url = Img_3;
            Image3.Recipe = Recipe;

            Images.Add(Image1);
            Images.Add(Image2);
            Images.Add(Image3);

            Images.ToList();

            Recipe.Images = Images;



            _context.Attach(Recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.RecipeID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeID == id);
        }
    }
}
