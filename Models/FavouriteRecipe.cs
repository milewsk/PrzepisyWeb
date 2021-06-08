
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class FavouriteRecipe
    {
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }

        public string UserID { get; set; }

        public ApplicationUser User { get; set; }
    }
}
