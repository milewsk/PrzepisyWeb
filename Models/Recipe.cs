using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class Recipe
    {
        //params
        [Key]
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }


        public ICollection<RecipeCategory> RecipeCategories { get; set; }

        //Owner
        public IdentityUser Owner { get; set; }

        //Ulubione
        //public ICollection<FavouriteRecipe> favouriteRecipe { get; set; }
        //for identityUser

        //Constructors

        public Recipe(string name, DateTime date, string description, string ingredients)
        {
            Name = name;
            Date = date;
            Description = description;
            Ingredients = ingredients;
        }


    }
}
