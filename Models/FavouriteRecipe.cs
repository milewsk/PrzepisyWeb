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

        public Recipe recipe { get; set; }

        //user
        public ApplicationUser person { get; set; }

       
        public string Id { get; set; }
    }
}
