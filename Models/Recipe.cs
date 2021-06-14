using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        //dodać inta do zliczania 

        //params
        [Key]
        public int RecipeID { get; set; }
        [Required(ErrorMessage = "Proszę podać nazwę przepisu")]
        [MaxLength(40)]
        [MinLength(5)]
        public string Name { get; set; }
        [JsonPropertyName("img")]
        public string Image { get; set; }

        [Required(ErrorMessage ="Podaj składniki")]
        [MaxLength(350,ErrorMessage ="Za długa lista składników (max 350 znaków)")]
        [MinLength(10)]
        public string Ingredients { get; set; }
        [Required(ErrorMessage ="Podaj opis przepisu")]
        [MaxLength(500,ErrorMessage ="Za długi opis (max 500 znaków)")]
        [MinLength(10)]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }

        //public ICollection<int> Ratings { get; set; }


        public ICollection<RecipeCategory> RecipeCategories { get; set; }

        //Owner
        public ApplicationUser Owner { get; set; }

        [Required]
        public string OwnerUserName { get; set; }

        //Image base

        public ICollection<Image> Images { get; set; }

        //public string Category { get; set; }

        //Użytkownik ulubione
       // public ApplicationUser FavUser { get; set; }

        //Ulubione
        //dobre podejście
        public ICollection<FavouriteRecipe> favouriteRecipes { get; set; }
        
        //polubienia

        public ICollection<LikeDislikeModel> LikeDislikeList { get; set; }


        private int LikeCounterParam = 0;

        public int LikeCounter { get { return LikeCounterParam; } set { LikeCounterParam = value; } }

        // polubienia i listy użytkowników którzy polublili
       /* public int LikeCounter { get; set; }
        
        [NotMapped]
        public ICollection<ApplicationUser> LikeUsers { get; set; }

        [NotMapped]
        public ICollection<ApplicationUser> DislikeUsers { get; set; }
 */
        //Constructors

        public Recipe(string name, DateTime date, string description, string ingredients)
        {
            Name = name;
            Date = date;
            Description = description;
            Ingredients = ingredients;
        }
        public Recipe()
        { }

        public override string ToString() => JsonSerializer.Serialize<Recipe>(this);

    }
}
