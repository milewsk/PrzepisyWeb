using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class Recipe
    {
        //params

        public int RecpieID { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }


        public ICollection<RecipeCategory> RecipeCategories { get; set; }
        //Owner
        public IdentityUser user { get; set; }

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
