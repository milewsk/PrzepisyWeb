
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Data
{
    public class RecipeContext :DbContext
    {
        public RecipeContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBulider)
        {
            //many to many
            modelBulider.Entity<RecipeCategory>().HasKey(rc => new { rc.recipeID, rc.CategoryID });

            modelBulider.Entity<RecipeCategory>().HasOne(rc => rc.recipe).WithMany(rc => rc.RecipeCategories).HasForeignKey(rc => rc.recipeID);
            modelBulider.Entity<RecipeCategory>().HasOne(rc => rc.category).WithMany(c => c.RecipeCategories).HasForeignKey(rc => rc.CategoryID);

            //one to many
            modelBulider.Entity<Recipe>().HasRequired<ApplicationUser>(r => r.User).WithMany(a => a.FavRecipes).HasForeignKey(r => r.ApplicationUser.Id);
        }

        //zrobić coś takiego tylko połączyć userów z polubieniami

    }
}
