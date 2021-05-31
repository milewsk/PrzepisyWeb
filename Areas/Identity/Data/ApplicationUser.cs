using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //składowa public już jest dziedziczona
        [Key]
        public override string Id { get => base.Id; set => base.Id = value; }
        
        [Required]
        [StringLength(100, ErrorMessage =" Błąd w nazwie użytkownika",MinimumLength =10)]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        public ICollection<FavouriteRecipe> favouriteRecipes { get; set; }
    }
}
