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
    public class MyRecipesModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;

        public MyRecipesModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IList<Recipe> MyRecipes { get; set; }

        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var username = _userManager.GetUserName(User);

                var Query = from x in _context.Recipes
                            where (username == x.OwnerUserName)
                            select x;

                MyRecipes = Query.ToList();
            }
            return Page();
        }
    }
}
