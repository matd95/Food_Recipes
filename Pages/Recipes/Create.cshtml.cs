using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Food_Recipes.Data;
using Food_Recipes.Models;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Food_Recipes.Pages.Recipes
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : RecipeIngredientsPageModel
    {
        private readonly Food_Recipes.Data.Food_RecipesContext _context;

        public CreateModel(Food_Recipes.Data.Food_RecipesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID",
"CategoryName");
            var recipe = new Recipe();
            recipe.RecipeIngredients = new List<RecipeIngredient>();
            PopulateAssignedIngredientData(_context, recipe);
            return Page();
        }
    

        [BindProperty]
        public Recipe Recipe { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedIngredients)
        {
            var newRecipe = new Recipe();
            if (selectedIngredients != null)
            {
                newRecipe.RecipeIngredients = new List<RecipeIngredient>();
                foreach (var ing in selectedIngredients)
                {
                    var ingToAdd = new RecipeIngredient
                    {
                        IngredientID = int.Parse(ing)
                    };
                    newRecipe.RecipeIngredients.Add(ingToAdd);
                }
            }
            Recipe.RecipeIngredients = newRecipe.RecipeIngredients;
            _context.Recipe.Add(Recipe);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

            PopulateAssignedIngredientData(_context, newRecipe);
            return Page();
        }
}
}
