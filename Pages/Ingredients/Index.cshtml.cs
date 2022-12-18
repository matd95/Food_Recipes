using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Food_Recipes.Data;
using Food_Recipes.Models;

namespace Food_Recipes.Pages.Ingredients
{
    public class IndexModel : PageModel
    {
        private readonly Food_Recipes.Data.Food_RecipesContext _context;

        public IndexModel(Food_Recipes.Data.Food_RecipesContext context)
        {
            _context = context;
        }

        public IList<Ingredient> Ingredient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Ingredient != null)
            {
                Ingredient = await _context.Ingredient.ToListAsync();
            }
        }
    }
}
