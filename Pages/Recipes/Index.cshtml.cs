using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Food_Recipes.Data;
using Food_Recipes.Models;
using System.Net;

namespace Food_Recipes.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly Food_Recipes.Data.Food_RecipesContext _context;

        public IndexModel(Food_Recipes.Data.Food_RecipesContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get; set; } = default!;
        public RecipeData RecipeD { get; set; }
        public int RecipeID { get; set; }
        public int IngredientID { get; set; }

        public string CategorySort { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? ingredientID, string sortOrder, string searchString)
        {
            RecipeD = new RecipeData();
            CategorySort = String.IsNullOrEmpty(sortOrder) ? "category_asc" : "";
            CurrentFilter = searchString;
            RecipeD.Recipes = await _context.Recipe
            .Include(b => b.Category)
            .Include(b => b.RecipeIngredients)
            .ThenInclude(b => b.Ingredient)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                RecipeD.Recipes = RecipeD.Recipes.Where(s => s.Category.CategoryName.Contains(searchString)
                || s.RecipeIngredients.Any(x => x.Ingredient.IngredientName.Contains(searchString))
                || s.Title.Contains(searchString));
            }
            if (id != null)
            {
                RecipeID = id.Value;
                Recipe recipe = RecipeD.Recipes
                .Where(i => i.ID == id.Value).Single();
                RecipeD.Ingredients = (IEnumerable<Ingredient>)recipe.RecipeIngredients.Select(s => s.Recipe);
            }
            switch (sortOrder)
            {
                case "category_asc":
                    RecipeD.Recipes = RecipeD.Recipes.OrderBy(s =>
                    s.Category.CategoryName);
                    break;
            }
        }
    }
}
