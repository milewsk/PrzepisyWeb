using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrzepisyWeb.Data;
using PrzepisyWeb.Models;

namespace PrzepisyWeb.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly PrzepisyWeb.Data.RecipeContext _context;

        public CreateModel(PrzepisyWeb.Data.RecipeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        [BindProperty]
        public string CategoriesText { get; set; }

        public IList<Category> Categories { get; set; }

        public List<string> CategoryName { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var CategoriesNames = from X in _context.Categories select X.CategoryName;

            CategoryName = CategoriesNames.ToList();

            string TextToLower = CategoriesText.ToLower();
            string[] SplitString = TextToLower.Split(",");


            for (int i = 0; i < SplitString.Length; i++)
            {
                if (CategoryName.Contains(SplitString[i]))
                {
                }
                else
                {
                    Category newCategory = new Category();
                    newCategory.CategoryName = SplitString[i];

                    _context.Categories.Add(newCategory);

                    _context.SaveChanges();

                }
            }

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
