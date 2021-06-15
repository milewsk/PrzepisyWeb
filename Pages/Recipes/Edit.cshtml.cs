using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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


        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;


        public EditModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public string Img_1 { get; set; }
        [BindProperty]
        public string Img_2 { get; set; }
        [BindProperty]
        public string Img_3 { get; set; }
        [BindProperty]
        public IList<ImageGallery> ImageGalleries { get; set; }

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
            Recipe.Owner = await _userManager.GetUserAsync(User);
            Recipe.OwnerUserName = _userManager.GetUserName(User);

            var DeleteLikes = (from X in _context.LikeDislikeList where X.RecipeID == Recipe.RecipeID select X).FirstOrDefault();

            if (DeleteLikes != null)
            {
                _context.LikeDislikeList.Remove(DeleteLikes);
            }
           
            ImageGallery Image2 = new ImageGallery();
            ImageGallery Image3 = new ImageGallery();


         //   var Query_1 = from X in _context.Images

            if((Img_1 !="" || Img_1 != null))
            {
                ImageGallery Image1 = new ImageGallery();
                Image1.Url = Img_1;
                Image1.Recipe = Recipe;
                ImageGalleries.Add(Image1);
            }
          
            Image2.Url = Img_2;
            Image2.Recipe = Recipe;

            Image3.Url = Img_3;
            Image3.Recipe = Recipe;

           
            ImageGalleries.Add(Image2);
            ImageGalleries.Add(Image3);

            ImageGalleries.ToList();

            Recipe.ImagesGallery = ImageGalleries;

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

            return RedirectToPage("./MyRecipes");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeID == id);
        }
    }
}
