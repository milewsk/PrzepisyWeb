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
    public class AddFavouriteModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;
       
        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public Recipe Recipe { get; set; }


        public AddFavouriteModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var IsFav = from X in _context.FavouriteRecipes where (X.RecipeID == id) && (X.UserID == _userManager.GetUserId(User)) select X;

            if (IsFav.Count() != 0)
            {
                return RedirectToPage("/Recipies");
            }


            if (id == null)
            {
                return NotFound();
            }

            // var RecipeList = from X in _context.Recipes select X;

            Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.RecipeID == id);

            if (Recipe == null)
            {
                return NotFound();
            }

            //Przechwycenie podanego przepisu zależy czy będziecie przerzucać obiekt czy 
            // będziecie przerzucać asp-for itp tak to bindproperty         
            return Page();
        }
      
       public FavouriteRecipe FavRec { get; set; }

        // POST
        public async Task<IActionResult> OnPostAsync()
        {


            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_signInManager.IsSignedIn(User))
            {
              
                //  FavouriteRecipe FavRecipe = new FavouriteRecipe(Recipe.RecipeID, _userManager.GetUserId(User));
                FavRec = new FavouriteRecipe();

                var temp = Recipe.RecipeID;

                FavRec.RecipeID = temp;
                FavRec.UserID = _userManager.GetUserId(User);

                _context.FavouriteRecipes.Add(FavRec);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Favourite");
        }


    }
}
