
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class FavouriteRecipe
    {
        [Required]
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }

        [Required]
        public string UserID { get; set; }

        public ApplicationUser User { get; set; }

        public FavouriteRecipe(int id, string userID)
        {
            RecipeID = id;
            UserID = userID;

        }

        public FavouriteRecipe() { }
    }
}
