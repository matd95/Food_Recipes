using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Food_Recipes.Data;
using Food_Recipes.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Food_Recipes.Pages.Recipes
{
    [Authorize(Roles = "Admin")]
   
    public class DeleteModel : PageModel
    {
        private readonly Food_Recipes.Data.Food_RecipesContext _context;

        public DeleteModel(Food_Recipes.Data.Food_RecipesContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Recipe Recipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.Include(b => b.Category).FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipe.FindAsync(id);

            if (recipe != null)
            {
                Recipe = recipe;
                _context.Recipe.Remove(Recipe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
