using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class Recipe
    {
        //params

        protected int RecpieID;
        private string Name;
        private string Ingredients;
        private string Description;
        private DateTime Date;


        protected ICollection<RecipeCategory> RecipeCategories;
        //Owner
        private IdentityUser user;

        //Constructors

        public Recipe(string Name, DateTime Date, string Description, string Ingredients)
        {
            setName(Name);
            setDate(Date);
            setDescription(Description);
            setIngredients(Ingredients);

        }


        //setters

        public void setName(string Name)
        {
            this.Name = Name;
        }
        public void setDate(DateTime Time)
        {
            this.Date = Time;
        }
        public void setDescription(string Description)
        {
            this.Description = Description;
        }
        public void setIngredients(string Ingredients)
        {
            this.Ingredients = Ingredients;
        }

        //getters

        public string getName()
        {
            return this.Name;
        }

        public DateTime getDate()
        {
            return this.Date;
        }
        public string getDescription()
        {
            return this.Description;
        }
        public string getIngredients()
        {
            return this.Ingredients;
        }
        public int getID()
        {
            return this.RecpieID;
        }

        public IdentityUser getUser()
        {
            return this.user;
        }
    }
}
