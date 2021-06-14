
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Data
{
    public class RecipeContext : IdentityDbContext<ApplicationUser>
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        public DbSet<FavouriteRecipe> FavouriteRecipes { get; set; }

        public DbSet<LikeDislikeModel> LikeDislikeList { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasOne(o => o.Owner)
                .WithMany(r => r.Recipes);

            //many to many Kategorie
            modelBuilder.Entity<RecipeCategory>().HasKey(rc => new { rc.RecipeID, rc.CategoryID });

            modelBuilder.Entity<RecipeCategory>()
                        .HasOne(rc => rc.Recipe)
                        .WithMany(r => r.RecipeCategories)
                        .HasForeignKey(rc => rc.RecipeID);

            modelBuilder.Entity<RecipeCategory>()
                        .HasOne(rc => rc.Category)
                        .WithMany(c => c.RecipeCategories)
                        .HasForeignKey(rc => rc.CategoryID);


            modelBuilder.Entity<LikeDislikeModel>().HasKey(dl => new { dl.RecipeID, dl.UserID });

            modelBuilder.Entity<LikeDislikeModel>()
                        .HasOne(r => r.Recipe)
                        .WithMany(x => x.LikeDislikeList)
                        .HasForeignKey(r => r.RecipeID);

            modelBuilder.Entity<LikeDislikeModel>()
                        .HasOne(r => r.User)
                        .WithMany(u => u.LikeDislikeList)
                        .HasForeignKey(r => r.UserID);
            //Ulubione
            modelBuilder.Entity<FavouriteRecipe>().HasKey(fr => new { fr.RecipeID, fr.UserID });
          
            modelBuilder.Entity<FavouriteRecipe>()
                        .HasOne(fr => fr.Recipe)
                        .WithMany(f => f.favouriteRecipes)
                        .HasForeignKey(fr => fr.RecipeID);

            modelBuilder.Entity<FavouriteRecipe>()
                        .HasOne(fr => fr.User)
                        .WithMany(u => u.favouriteRecipes)
                        .HasForeignKey(fr => fr.UserID);

            //galeria

            modelBuilder.Entity<Image>().HasOne(r => r.Recipe).WithMany(i => i.Images);

        }

        //zrobić coś takiego tylko połączyć userów z polubieniami

    }
}
