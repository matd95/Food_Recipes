using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Food_Recipes.Data;
using Food_Recipes.Models;
using Food_Recipes.Models.ViewModels;
using System.Security.Policy;

namespace Food_Recipes.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Food_Recipes.Data.Food_RecipesContext _context;

        public IndexModel(Food_Recipes.Data.Food_RecipesContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int RecipeID { get; set; }

        public async Task OnGetAsync(int? id, int? recipeID)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
            .Include(i => i.Recipes)
            .OrderBy(i => i.CategoryName)
            .ToListAsync();
            if (id != null)
            {
                CategoryID = id.Value;
                Category Category = CategoryData.Categories
                .Where(i => i.ID == id.Value).Single();
                CategoryData.Recipes = Category.Recipes;
            }
        }
    }
}
