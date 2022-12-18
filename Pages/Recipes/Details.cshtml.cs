using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Food_Recipes.Data;
using Food_Recipes.Models;

namespace Food_Recipes.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly Food_Recipes.Data.Food_RecipesContext _context;

        public DetailsModel(Food_Recipes.Data.Food_RecipesContext context)
        {
            _context = context;
        }

      public Recipe Recipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FirstOrDefaultAsync(m => m.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }
            else 
            {
                Recipe = recipe;
            }
            return Page();
        }
    }
}
