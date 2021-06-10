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
        
      //  private int Counter = 0;

      // mamy bazkę rokordy z recipeid i userID

      // public int CounterLike { get { return Counter; } set { Counter = value; } }

        
        [Required]
        public int RecipeID { get; set; }
        
        [Required]
        public string UserID { get; set; }

        public Recipe Recipe { get; set; }

        public ApplicationUser User { get; set; }

        [DefaultValue(false)]
        public bool Like { get { return BoolBoxLike; } set { BoolBoxLike = value; } }

        [DefaultValue(false)]
        public bool Dislike { get { return BoolBoxDislike; } set { BoolBoxDislike = value; } }


        public LikeDislikeModel(int recipeID, string userID) 
        {
            UserID = userID;
            RecipeID = recipeID;
           

        }

    }
}
