﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public ICollection<RecipeCategory> RecipeCategories { get; set; }

    }
}
