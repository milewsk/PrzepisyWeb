﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Username { get; set; }

       public ICollection<Recipe> FavouriteRecipes { get; set; }
    }
}
