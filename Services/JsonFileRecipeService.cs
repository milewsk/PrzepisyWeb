using Microsoft.AspNetCore.Hosting;
using PrzepisyWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrzepisyWeb.Services
{
    public class JsonFileRecipeService
    {
        public JsonFileRecipeService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "Recipes.json"); }
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Recipe[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

       /* public void AddRating(string recipeId, int rating)
        {
            var recipes = GetRecipes();

            if (recipes.First(x => x.RecipeID == recipeId).Ratings == null)
            {
                recipes.First(x => x.RecipeID == recipeId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = recipes.First(x => x.RecipeID == recipeId).Ratings.ToList();
                ratings.Add(rating);
                recipes.First(x => x.RecipeID == recipeId).Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Recipe>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    recipes
                );
            }
        }
    */
    }
}
