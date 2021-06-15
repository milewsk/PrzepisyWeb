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
    public class CreateModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;


        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;


        public CreateModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {

                Recipe.Date = DateTime.Now;
                Recipe.Owner = await _userManager.GetUserAsync(User);
                Recipe.OwnerUserName = _userManager.GetUserName(User);

                _context.Recipes.Add(Recipe);
                await _context.SaveChangesAsync();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            return RedirectToPage("/Recipies");
        }

       
    }
}
