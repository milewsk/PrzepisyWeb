using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Models;


namespace PrzepisyWeb.Pages
{
    public class RecipiesModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        public RecipiesModel(PrzepisyWeb.Data.RecipeContext context)
        {
            _context = context;
        }

        
        //foricz ()



        [BindProperty]
        public string SearchString { get; set; }

        public IList<Recipe> SearchList { get; set; }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {

                var SearchQuery = from X in _context.Recipes
                                  where (X.Name.Contains(SearchString) ||
                                  X.Owner.UserName.Contains(SearchString) ||
                                  X.Ingredients.Contains(SearchString) ||
                                  X.Description.Contains(SearchString)) select X;

                SearchList = SearchQuery.ToList();
            }


            return Page();
        }

        public void OnGet()
        {
            var GetFullList = (from X in _context.Recipes  select X).Take(20);

            SearchList = GetFullList.ToList();
        }
    }
}
