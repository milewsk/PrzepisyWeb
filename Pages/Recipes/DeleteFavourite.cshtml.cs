using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrzepisyWeb.Data;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages.Recipes
{
    [Authorize]
    public class DeleteFavouriteModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;

        public DeleteFavouriteModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        public FavouriteRecipe FavouriteRecipe { get; set; }
        
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe =  _context.Recipes.FirstOrDefault(m => m.RecipeID == id);

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == null)
            {
                return NotFound();
            }

        //    FavouriteRecipe = await _context.FavouriteRecipes.FindAsync(FavouriteRecipe.RecipeID == id);

            if (_signInManager.IsSignedIn(User))
            {
                FavouriteRecipe DeleteFav = new FavouriteRecipe();

                DeleteFav.RecipeID = (int)id;
                DeleteFav.UserID = _userManager.GetUserId(User);

                _context.FavouriteRecipes.Remove(DeleteFav);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Favourite");
        }
    }
}
