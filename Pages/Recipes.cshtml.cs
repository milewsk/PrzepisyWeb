using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PrzepisyWeb.Models;
using PrzepisyWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrzepisyWeb.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly ILogger<RecipesModel> _logger;
        public JsonFileRecipeService RecipeService;
        public IEnumerable<Recipe> Recipes { get; private set; }

        public RecipesModel(ILogger<RecipesModel> logger, JsonFileRecipeService recipeService)
        {
            _logger = logger;
            RecipeService = recipeService;
        }

        public void OnGet()
        {
            Recipes = RecipeService.GetRecipes();
        }
    }
}
