using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrzepisyWeb.Data;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages.Categories
{
    [Authorize]
    public class AddCategoryModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        public AddCategoryModel(PrzepisyWeb.Data.RecipeContext context)
        {
            _context = context;
        }

        //Properties
        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public string CategoriesText { get; set; }

        public IList<RecipeCategory> RecipeCategories { get; set; }

        public IList<Category> Categories { get; set; }

        public List<string> CategoryName { get; set; }

        public IActionResult OnGet(int? id)
        {
           // var RecipeSearch = from X in _context.Recipes where X.RecipeID == id select X;
            var RecipeCat = from X in _context.RecipeCategories select X;
            var CategoriesList = from X in _context.Categories select X;

            var CategoriesNames = from X in _context.Categories select X.CategoryName;

            Recipe = _context.Recipes.FirstOrDefault(m => m.RecipeID == id);

            CategoryName = CategoriesNames.ToList();
            Categories = CategoriesList.ToList();
            RecipeCategories = RecipeCat.ToList();

            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {            
               

                return Page();
            }

            Recipe = _context.Recipes.FirstOrDefault(m => m.RecipeID == id);

            var CategoriesNames = from X in _context.Categories select X.CategoryName;

            CategoryName = CategoriesNames.ToList();

            string TextToLower = CategoriesText.ToLower();
            string[] SplitString = TextToLower.Split(",");

            
           
               for(int i=0; i<SplitString.Length;i++)
                {
                    if (CategoryName.Contains(SplitString[i]))
                    {
                    //do nothing
                    
                    //dodaj do relacji
                    var QueryID = (from X in _context.Categories where X.CategoryName == SplitString[i] select X.CategoryID).FirstOrDefault();

                        //czy istnieje połączenie 
                     
                                var Query = from X in _context.RecipeCategories where (X.RecipeID == Recipe.RecipeID) && (X.CategoryID == QueryID) select X;
                           
                                if(Query.Count() == 0)
                                {
                                    RecipeCategory newCategoryRecipe = new RecipeCategory();
                                    newCategoryRecipe.CategoryID = QueryID;
                                    newCategoryRecipe.RecipeID = Recipe.RecipeID;

                                    _context.RecipeCategories.Add(newCategoryRecipe);
                                    _context.SaveChanges();
                                }
                            

                    }
                    else
                     {
                            Category newCategory = new Category();
                            newCategory.CategoryName = SplitString[i];

                            _context.Categories.Add(newCategory);

                            _context.SaveChanges();


                            var QueryID = (from X in _context.Categories where X.CategoryName == SplitString[i] select X.CategoryID).FirstOrDefault();

                            //czy istnieje połączenie 

                            var Query = from X in _context.RecipeCategories where (X.RecipeID == Recipe.RecipeID) && (X.CategoryID == QueryID) select X;

                            if (Query.Count() == 0)
                            {
                                RecipeCategory newCategoryRecipe = new RecipeCategory();
                                newCategoryRecipe.CategoryID = QueryID;
                                newCategoryRecipe.RecipeID = Recipe.RecipeID;

                                _context.RecipeCategories.Add(newCategoryRecipe);
                                _context.SaveChanges();
                            }

                    }
                }

          






             _context.SaveChanges();

            return RedirectToPage("/Recipes/MyRecipes");
        }
    }
}
