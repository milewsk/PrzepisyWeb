using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class Category
    {
        [Key]
        public int categoryID { get; set; }

        public string categoryName { get; set; }

        public ICollection<RecipeCategory> RecipeCategories { get; set; }

    }
}
