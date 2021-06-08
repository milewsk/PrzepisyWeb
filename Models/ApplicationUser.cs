using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //składowa public już jest dziedziczona

        public ICollection<FavouriteRecipe> favouriteRecipes { get; set; }
    }
}
