using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class Category
    {
        public int categoryID { get; set; }

        protected string categoryName;

        protected ICollection<RecipesCategory> RecipesCategories;
    }
}
