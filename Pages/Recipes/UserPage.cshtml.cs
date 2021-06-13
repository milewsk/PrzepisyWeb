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

namespace PrzepisyWeb.Pages.Recipes
{
    public class UserPageModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;


        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserPageModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        public string UsernameDisplay { get; set; }

        public IList<Recipe> RecipeList { get;set; }

        public IActionResult OnGet(string UserName)
        {
            UsernameDisplay = UserName;

            var UserList = from X in _context.Recipes where X.OwnerUserName == UserName select X;

            RecipeList = UserList.ToList();
            return Page();
        }

        public ActionResult OnPostAsync(int Like, string UserName)
        {

            if (ModelState.IsValid)
            {
                UsernameDisplay = UserName;
                var UserList = from X in _context.Recipes where X.OwnerUserName == UserName select X;


                RecipeList = UserList.ToList();

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
                            // albo var coś tam taki sam jak obecny i usunąć
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

                        //ilość wszystkich rekordów
                        //ilość rekrdów dla danego id gdzie dislike jest true
                        //odjąć od siebie będzie liczba lików
                        // var AllLikedRecords = (from X in _context.LikeDislikeList where (X.RecipeID == Recipe.RecipeID) && (X.Like == true) select X).Count();

                        // var AllDislikedRecords = (from X in _context.LikeDislikeList where (X.RecipeID == Recipe.RecipeID) && (X.Dislike == true) select X).Count();



                        //zmienić counter

                    }
                    if (Request.Form.Keys.Contains("Dislike"))
                    {
                        Recipe = _context.Recipes.FirstOrDefault(m => m.RecipeID == Like);

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

                            // albo var coś tam taki sam jak obecny i usunąć
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
