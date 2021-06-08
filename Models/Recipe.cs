using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class Recipe
    {
        //params
        [Key]
        public int RecipeID { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        //public ICollection<int> Ratings { get; set; }


        public ICollection<RecipeCategory> RecipeCategories { get; set; }

        //Owner
        public ApplicationUser Owner { get; set; }

        //Użytkownik ulubione
       // public ApplicationUser FavUser { get; set; }

        //Ulubione
        //dobre podejście
        public ICollection<FavouriteRecipe> favouriteRecipes { get; set; }
        

        // polubienia i listy użytkowników którzy polublili
        public int LikeCounter { get; set; }
 /*       [NotMapped]
        public ICollection<String> LikeUsers { get; set; }
        [NotMapped]
        public ICollection<String> DislikeUsers { get; set; }
 */
        //Constructors

        public Recipe(string name, DateTime date, string description, string ingredients)
        {
            Name = name;
            Date = date;
            Description = description;
            Ingredients = ingredients;
        }

        public override string ToString() => JsonSerializer.Serialize<Recipe>(this);

    }
}
