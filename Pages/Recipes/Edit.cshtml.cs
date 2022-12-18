using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Food_Recipes.Data;
using Food_Recipes.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Food_Recipes.Pages.Recipes
{
    [Authorize(Roles = "Admin")]
    public class EditModel : RecipeIngredientsPageModel
    {
        private readonly Food_Recipes.Data.Food_RecipesContext _context;

        public EditModel(Food_Recipes.Data.Food_RecipesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
.Include(b => b.Category)
.Include(b => b.RecipeIngredients).ThenInclude(b => b.Ingredient)
.AsNoTracking()
.FirstOrDefaultAsync(m => m.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }
            Recipe = recipe;
            PopulateAssignedIngredientData(_context, Recipe);
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID",
"CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedIngredients)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var recipeToUpdate = await _context.Recipe
            .Include(i => i.Category)
            .Include(i => i.RecipeIngredients)
            .ThenInclude(i => i.Ingredient)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (recipeToUpdate == null)
            {
                return NotFound();
            }
            
            if (await TryUpdateModelAsync<Recipe>(
            recipeToUpdate,
            "Recipe",
            i => i.Title,
            i => i.CategoryID))
            {
                UpdateRecipeIngredients(_context, selectedIngredients, recipeToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
            UpdateRecipeIngredients(_context, selectedIngredients, recipeToUpdate);
            PopulateAssignedIngredientData(_context, recipeToUpdate);
            return Page();
        }
    }
}

