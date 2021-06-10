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
        public string SearchString { get; set; }

        public IList<Recipe> SearchList { get; set; }

        public IList<LikeDislikeModel> LikeDislikeList { get; set; }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {

                var SearchQuery = from X in _context.Recipes
                                  where (X.Name.Contains(SearchString) ||
                                  X.Owner.UserName.Contains(SearchString) ||
                                  X.Ingredients.Contains(SearchString) ||
                                  X.Description.Contains(SearchString)) select X;

                SearchList = SearchQuery.ToList();
            }


            return Page();
        }

        public void OnGet()
        {
            var GetLikeDislike = from L in _context.LikeDislikeList select L;

            var GetFullList = (from X in _context.Recipes  select X).Take(20);

            SearchList = GetFullList.ToList();

            LikeDislikeList = GetLikeDislike.ToList();
        }

        
        public  ActionResult Like(int R)
        {
            var IsCreated = from IS in _context.LikeDislikeList where IS.RecipeID == R select IS;

            var tempLike = (from IS in _context.LikeDislikeList where ((IS.RecipeID == R) || (IS.UserID == _userManager.GetUserId(User))) select IS.Like).Single();

            var tempDislike = (from IS in _context.LikeDislikeList where ((IS.RecipeID == R) || (IS.UserID == _userManager.GetUserId(User))) select IS.Dislike).Single();

            if (IsCreated == null)
            {
                LikeDislikeModel newOne = new LikeDislikeModel(R, _userManager.GetUserId(User));
                newOne.Like = true;
                _context.LikeDislikeList.Add(newOne);
          
            }
            else if(tempLike == true)
            {
               
            }
            else 
            {
                // albo var co� tam taki sam jak obecny i usun��
                LikeDislikeModel ToDelete = new LikeDislikeModel(R, _userManager.GetUserId(User));
                _context.LikeDislikeList.Remove(ToDelete);

                

                ToDelete.Dislike = false;
                ToDelete.Like = true;
                _context.LikeDislikeList.Add(ToDelete);

            }

            //ilo�� wszystkich rekord�w
            //ilo�� rekrd�w dla danego id gdzie dislike jest true
            //odj�� od siebie b�dzie liczba lik�w
            var AllLikedRecords = (from X in _context.LikeDislikeList where (X.RecipeID == R )&& (X.Like == true) select X).Count();

            var AllDislikedRecords = (from X in _context.LikeDislikeList where (X.RecipeID == R) && (X.Dislike == true) select X).Count();



            //zmieni� counter

            return  Page();
        }
    
        public IActionResult Dislike(int R)
        {

            var IsCreated = from IS in _context.LikeDislikeList where IS.RecipeID == R select IS;

            var tempLike = (from IS in _context.LikeDislikeList where ((IS.RecipeID == R) || (IS.UserID == _userManager.GetUserId(User))) select IS.Like).Single();

            var tempDislike = (from IS in _context.LikeDislikeList where ((IS.RecipeID == R) || (IS.UserID == _userManager.GetUserId(User))) select IS.Dislike).Single();

            if (IsCreated == null)
            {
                LikeDislikeModel newOne = new LikeDislikeModel(R, _userManager.GetUserId(User));
                newOne.Like = true;
                _context.LikeDislikeList.Add(newOne);

            }
            else if (tempLike == true)
            {

            }
            else
            {
                // albo var co� tam taki sam jak obecny i usun��
                LikeDislikeModel ToDelete = new LikeDislikeModel(R, _userManager.GetUserId(User));
                _context.LikeDislikeList.Remove(ToDelete);



                ToDelete.Dislike = false;
                ToDelete.Like = true;
                _context.LikeDislikeList.Add(ToDelete);

            }

            //ilo�� wszystkich rekord�w
            //ilo�� rekrd�w dla danego id gdzie dislike jest true
            //odj�� od siebie b�dzie liczba lik�w
            var AllLikedRecords = (from X in _context.LikeDislikeList where (X.RecipeID == R) && (X.Like == true) select X).Count();

            var AllDislikedRecords = (from X in _context.LikeDislikeList where (X.RecipeID == R) && (X.Dislike == true) select X).Count();



            //zmieni� counter

            return Page();
        }
    }

}
