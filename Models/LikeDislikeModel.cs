using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class LikeDislikeModel
    {
        
        public string LikeID { get; set; }

        private bool BoolBoxDislike = false;

        private bool BoolBoxLike = false;
     
        [Required]
        public int RecipeID { get; set; }
        
        [Required]
        public string UserID { get; set; }

        public Recipe Recipe { get; set; }

        public ApplicationUser User { get; set; }

        [DefaultValue(0)]
        public bool Like { get { return BoolBoxLike; } set { BoolBoxLike = value; } }

        [DefaultValue(0)]
        public bool Dislike { get { return BoolBoxDislike; } set { BoolBoxDislike = value; } }


        public LikeDislikeModel(int recipeID, string userID) 
        {
            UserID = userID;
            RecipeID = recipeID;      

        }

        public LikeDislikeModel() { }

    }
}
