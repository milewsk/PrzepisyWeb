using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrzepisyWeb.Models;


namespace PrzepisyWeb.Pages
{
    public class RecipiesModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipiesModel(PrzepisyWeb.Data.RecipeContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        //foricz ()

        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        public IList<Recipe> SearchList { get; set; }

        public IList<LikeDislikeModel> LikeDislikeList { get; set; }

        public IList<FavouriteRecipe> FavRecipeList { get; set; }

        //kategorie

        public IList<Category> Categories { get; set; }

        public int CategoriesID { get; set; }

        public int CategoryRecipeID { get; set; }

        public List<Recipe> SearchCategegoryRecipe { get; set; }

        public  IActionResult OnGet()
        {
            var GetFullList = from X in _context.Recipes orderby X.Date descending select X;

            var GetLikeDislike = from L in _context.LikeDislikeList select L;

            var FavList = from F in _context.FavouriteRecipes select F;

       

            SearchList =  GetFullList.ToList();

            LikeDislikeList =  GetLikeDislike.ToList();

            FavRecipeList = FavList.ToList();

            return Page();
        }

        
        public ActionResult OnPostAsync(int Like)
        {

            if (ModelState.IsValid)
            {


                // if (Request.Form.Keys.Contains("Search"))
                //{
                if (SearchString != "" && SearchString != null)
                {
                    var ListOfCategory = from z in _context.RecipeCategories where Recipe.RecipeID == z.RecipeID select z.Category;

                    Categories = ListOfCategory.ToList();


                    //wypisa� ID?
                    var Query_2 =(from Q in Categories where Q.CategoryName.Contains(SearchString) select Q.CategoryID).First();

                    CategoriesID = Query_2;
                    //szuka� id przepisu w tablicy po�redniej
                    var Query_3 = (from T in _context.RecipeCategories where T.CategoryID == CategoriesID select T.Recipe);

                    SearchCategegoryRecipe = Query_3.ToList();

                    //szuka� przepis�w z danymi id 

                    //doda� do listy Modelu do wypisana

                    var SearchQuery = from X in _context.Recipes
                                      where (X.Name.Contains(SearchString) ||
                                      X.Owner.UserName.Contains(SearchString) ||
                                      X.Ingredients.Contains(SearchString) ||
                                      X.Description.Contains(SearchString)) 
                                    //  X.RecipeCategories.Contains((from Q in _context.RecipeCategories where  Q.Category == (from E in Categories where E.CategoryName.Contains(SearchString)  select E) select Q).Single())
                                      orderby X.Date descending
                                      select X;
                    // || (from Z in _context.Categories where Z.CategoryName.Contains(SearchString.ToLower()) && (Z.CategoryID = (from Y in _context.RecipeCategories where Y.RecipeID == Recipe.RecipeID select Y.CategoryID)))
                   

                    SearchList = SearchQuery.ToList();
                }
                else
                {
                    var GetFullList = from X in _context.Recipes select X;
                    SearchList = GetFullList.ToList();
                }
                //}

                if (_signInManager.IsSignedIn(User))
                {

                    if (Request.Form.Keys.Contains("Like"))
                    {
                        Recipe =  _context.Recipes.FirstOrDefault(m => m.RecipeID == Like);

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
                            // albo var co� tam taki sam jak obecny i usun��
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

                        //ilo�� wszystkich rekord�w
                        //ilo�� rekrd�w dla danego id gdzie dislike jest true
                        //odj�� od siebie b�dzie liczba lik�w
                        // var AllLikedRecords = (from X in _context.LikeDislikeList where (X.RecipeID == Recipe.RecipeID) && (X.Like == true) select X).Count();

                        // var AllDislikedRecords = (from X in _context.LikeDislikeList where (X.RecipeID == Recipe.RecipeID) && (X.Dislike == true) select X).Count();



                        //zmieni� counter

                    }
                    if (Request.Form.Keys.Contains("Dislike"))
                    {
                        Recipe =  _context.Recipes.FirstOrDefault(m => m.RecipeID == Like);

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
                            
                            // albo var co� tam taki sam jak obecny i usun��
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
