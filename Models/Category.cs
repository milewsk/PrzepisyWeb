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
        public int CategoryID { get; set; }

        [Required(ErrorMessage ="To pole musi być uzupełnione")]
        [MaxLength(20,ErrorMessage ="Za długa kategoria")]
        [MinLength(2,ErrorMessage ="Za krótka kategoria")]
        public string CategoryName { get; set; }

        public ICollection<RecipeCategory> RecipeCategories { get; set; }

    }
}
