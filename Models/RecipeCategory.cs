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
        
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }

        
        public int CategoryID { get; set; }
        public Category Category { get; set; }


       

    }
}
