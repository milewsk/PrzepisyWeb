using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class RecipeCategory
    {
        
        public int recipeID { get; set; }
        public Recipe recipe { get; set; }

        
        public int CategoryID { get; set; }
        public Category category { get; set; }


       

    }
}
