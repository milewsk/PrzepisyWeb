using Microsoft.AspNetCore.Identity;
using PrzepisyWeb.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class FavouriteRecipe
    {
        //recipe
        public int RecipeID { get; set; }

        public Recipe Recipe { get; set; }

        //user
        public ApplicationUser User { get; set; }

       public string Id { get; set; }
    }
}
