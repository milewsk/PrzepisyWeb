
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Areas.Identity.Data;
using PrzepisyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Data
{
    public class RecipeContext : IdentityDbContext<ApplicationUser>
    {
        public RecipeContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        public DbSet<FavouriteRecipe> FavouriteRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many to many
            modelBuilder.Entity<RecipeCategory>().HasKey(rc => new { rc.recipeID, rc.CategoryID });

            modelBuilder.Entity<RecipeCategory>().HasOne(rc => rc.recipe).WithMany(rc => rc.RecipeCategories).HasForeignKey(rc => rc.recipeID);
            modelBuilder.Entity<RecipeCategory>().HasOne(rc => rc.category).WithMany(c => c.RecipeCategories).HasForeignKey(rc => rc.CategoryID);


            modelBuilder.Entity<FavouriteRecipe>().HasKey(fr => new { fr.RecipeID, fr.Id });

            modelBuilder.Entity<FavouriteRecipe>().HasOne(fr => fr.person).WithMany(u => u.favouriteRecipes).HasForeignKey(fr => fr.Id);
            modelBuilder.Entity<FavouriteRecipe>().HasOne(fr => fr.recipe).WithMany(r => r.favouriteRecipe).HasForeignKey(fr => fr.RecipeID);
            //one to many
           // modelBulider.Entity<Recipe>().HasRequired<ApplicationUser>(r => r.User).WithMany(a => a.FavRecipes).HasForeignKey(r => r.ApplicationUser.Id);
           // modelBuilder.Entity<FavouriteRecipe>().HasKey(fr => new { fr.RecipeID, fr.Id });
           // modelBuilder.Entity<FavouriteRecipe>().HasOne(fr => fr.Recipe).WithMany(f => f.favouriteRecipe).HasForeignKey(fr => fr.RecipeID);
          //dobrze (w modelu usera liste wrzucić)  modelBuilder.Entity<FavouriteRecipe>().HasOne(fr => fr.User).WithMany(f => f.favouriteRecipe).HasForeignKey(fr => fr.Id);
        }

        //zrobić coś takiego tylko połączyć userów z polubieniami

    }
}
