using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Data;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages.Recipes
{
    [Authorize]
    public class FavouriteModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;

        public FavouriteModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

    

       /// public FavouriteRecipe FavouriteRecipe { get; set; }
       

        //zmienić nazwe
        public IList<Recipe> Recipe{ get; set; }
       
     

        public async Task OnGetAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
               // var Query = from r in _context.Recipes where r.favouriteRecipes.    select r;

                foreach(FavouriteRecipe r in _context.FavouriteRecipes)
                {

                    if(_userManager.GetUserId(User) == r.UserID)
                    {
                        
                        
                    }
                }


                Recipe = await _context.Recipes.ToListAsync();
            }
            
        }
    }
}
