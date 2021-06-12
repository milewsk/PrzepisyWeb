using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrzepisyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly PrzepisyWeb.Data.RecipeContext _context;
        public IndexModel(ILogger<IndexModel> logger, PrzepisyWeb.Data.RecipeContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Recipe> SearchList { get; set; }


        public IActionResult OnGet()
        {
            var GetFullList = (from X in _context.Recipes orderby X.LikeCounter descending select X).Take(10);

       
            SearchList = GetFullList.ToList();

            return Page();
        }
    }
}
