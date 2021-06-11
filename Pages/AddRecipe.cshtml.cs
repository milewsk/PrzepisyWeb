using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages
{
    [Authorize]
    public class AddRecipeModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;

        public AddRecipeModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        [BindProperty(SupportsGet = true)]
        public Recipe Recipe { get; set; }

        // POST
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_signInManager.IsSignedIn(User))
            {
                //Tutaj Kondziu bêdê siê musia³ siê Ciebie popytaæ
                Recipe.Name = "Lorem Ipsum";
                Recipe.Ingredients = "Lorem Ipsum";
                Recipe.Description = "Lorem Ipsum";
                Recipe.Date = System.DateTime.Now;
                _context.Recipes.Add(Recipe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
