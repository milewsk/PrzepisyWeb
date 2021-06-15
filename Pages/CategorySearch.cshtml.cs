﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Data;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages
{
    public class CategorySearchModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;


        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategorySearchModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

    
        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        public IList<Recipe> SearchList { get; set; }

        //Szukanie po kategorii

        public int CategoriesID { get; set; }

        public int CategoryRecipeID { get; set; }

        public List<Recipe> SearchCategegoryRecipe { get; set; }

        //like / ulubione

        public IList<LikeDislikeModel> LikeDislikeList { get; set; }

        public IList<FavouriteRecipe> FavRecipeList { get; set; }


        public IActionResult OnGet()
        {
            var GetFullList = from X in _context.Recipes orderby X.Date descending select X;

            var GetLikeDislike = from L in _context.LikeDislikeList select L;

            var FavList = from F in _context.FavouriteRecipes select F;

            var HiddenList = from Z in _context.Recipes where Z.RecipeID == 0 select Z;

            SearchCategegoryRecipe = GetFullList.ToList();

         

            LikeDislikeList = GetLikeDislike.ToList();

            FavRecipeList = FavList.ToList();

            return Page();
        }

        public ActionResult OnPostAsync(int Like, int Dislike)
        {

            if (ModelState.IsValid)
            {


                // if (Request.Form.Keys.Contains("Search"))
                //{
                if (SearchString != "" && SearchString != null)
                {
                    //wypisaæ ID?
                    var Query_2 = (from Q in _context.Categories where Q.CategoryName.Contains(SearchString.ToLower()) select Q.CategoryID).FirstOrDefault();

                    CategoriesID = Query_2;
                    //szukaæ id przepisu w tablicy poœredniej
                    var Query_3 = (from T in _context.RecipeCategories where T.CategoryID == CategoriesID select T.Recipe);

                    SearchCategegoryRecipe = Query_3.ToList();

                }
                else
                {
                    var GetFullList = from X in _context.Recipes orderby X.Date select X;
                    SearchCategegoryRecipe = GetFullList.ToList();
                }
                //}

                if (_signInManager.IsSignedIn(User))
                {

                    if (Request.Form.Keys.Contains("Like"))
                    {
                        Recipe = _context.Recipes.FirstOrDefault(m => m.RecipeID == Like);

                        var IsCreated = from IS in _context.LikeDislikeList where (IS.RecipeID == Recipe.RecipeID) && (IS.UserID == _userManager.GetUserId(User)) select IS;


                        if (IsCreated.Count() == 0)
                        {
                            var newOne = new LikeDislikeModel();
                            newOne.RecipeID = Recipe.RecipeID;
                            newOne.UserID = _userManager.GetUserId(User);
                            newOne.Like = true;
                            _context.LikeDislikeList.Add(newOne);
                            Recipe.LikeCounter++;
                            _context.SaveChanges();

                        }


                        var tempLike = from IS in _context.LikeDislikeList where (IS.RecipeID == Recipe.RecipeID) && (IS.UserID == _userManager.GetUserId(User)) && (IS.Like == true) select IS;

                        var tempDislike = from IS in _context.LikeDislikeList where (IS.RecipeID == Recipe.RecipeID) && (IS.UserID == _userManager.GetUserId(User)) && (IS.Dislike == true) select IS;

                        if (tempLike.Count() > 0)
                        {

                        }
                        else
                        {
                            // albo var coœ tam taki sam jak obecny i usun¹æ
                            LikeDislikeModel ToDelete = new LikeDislikeModel();
                            ToDelete.UserID = _userManager.GetUserId(User);
                            ToDelete.RecipeID = Recipe.RecipeID;
                            Recipe.LikeCounter++;
                            _context.LikeDislikeList.Remove(ToDelete);
                            _context.SaveChanges();


                            ToDelete.Dislike = false;
                            ToDelete.Like = true;
                            Recipe.LikeCounter++;
                            _context.LikeDislikeList.Add(ToDelete);

                            _context.SaveChanges();

                        }

                    }
                    if (Request.Form.Keys.Contains("Dislike"))
                    {
                        Recipe = _context.Recipes.FirstOrDefault(m => m.RecipeID == Dislike);

                        var IsCreated = from IS in _context.LikeDislikeList where (IS.RecipeID == Recipe.RecipeID) && (IS.UserID == _userManager.GetUserId(User)) select IS;


                        if (IsCreated.Count() == 0)
                        {
                            var newOne = new LikeDislikeModel();
                            newOne.RecipeID = Recipe.RecipeID;
                            newOne.UserID = _userManager.GetUserId(User);
                            newOne.Dislike = true;
                            _context.LikeDislikeList.Add(newOne);
                            Recipe.LikeCounter--;
                            _context.SaveChanges();

                        }


                        var tempLike = from IS in _context.LikeDislikeList where (IS.RecipeID == Recipe.RecipeID) && (IS.UserID == _userManager.GetUserId(User)) && (IS.Like == true) select IS;

                        var tempDislike = from IS in _context.LikeDislikeList where (IS.RecipeID == Recipe.RecipeID) && (IS.UserID == _userManager.GetUserId(User)) && (IS.Dislike == true) select IS;

                        if (tempDislike.Count() > 0)
                        {

                        }
                        else
                        {

                            // albo var coœ tam taki sam jak obecny i usun¹æ
                            LikeDislikeModel ToDelete = new LikeDislikeModel();
                            ToDelete.RecipeID = Recipe.RecipeID;
                            ToDelete.UserID = _userManager.GetUserId(User);
                            _context.LikeDislikeList.Remove(ToDelete);
                            Recipe.LikeCounter--;
                            _context.SaveChanges();




                            ToDelete.Dislike = true;
                            ToDelete.Like = false;
                            _context.LikeDislikeList.Add(ToDelete);
                            Recipe.LikeCounter--;
                            _context.SaveChanges();

                        }


                    }
                }

            }
            return Page();
        }
    }
}