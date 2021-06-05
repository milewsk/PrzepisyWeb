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
    public class AddFavouriteModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;
       
        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;

        public AddFavouriteModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult OnGet(Recipe recipe)
        {
            //Przechwycenie podanego przepisu zależy czy będziecie przerzucać obiekt czy 
            // będziecie przerzucać asp-for itp tak to bindproperty
            Recipe = recipe;
            return Page();
        }

        [BindProperty(SupportsGet =true)]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public FavouriteRecipe FavouriteRecipe { get; set; }

        // POST
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_signInManager.IsSignedIn(User))
            {
                FavouriteRecipe.UserID = _userManager.GetUserId(User);
                FavouriteRecipe.RecipeID = Recipe.RecipeID;
                _context.FavouriteRecipes.Add(FavouriteRecipe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
