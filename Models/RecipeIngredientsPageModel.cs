using Microsoft.AspNetCore.Mvc.RazorPages;
using Food_Recipes.Data;

namespace Food_Recipes.Models
{
    public class RecipeIngredientsPageModel : PageModel
    {
        public List<AssignedIngredientData> AssignedIngredientDataList;
        public void PopulateAssignedIngredientData(Food_RecipesContext context,
        Recipe recipe)
        {
            var allIngredients = context.Ingredient;
            var recipeIngredients = new HashSet<int>(
            recipe.RecipeIngredients.Select(c => c.IngredientID)); //
            AssignedIngredientDataList = new List<AssignedIngredientData>();
            foreach (var ing in allIngredients)
            {
                AssignedIngredientDataList.Add(new AssignedIngredientData
                {
                    IngredientID = ing.ID,
                    Name = ing.IngredientName,
                    Assigned = recipeIngredients.Contains(ing.ID)
                });
            }
        }
        public void UpdateRecipeIngredients(Food_RecipesContext context,
        string[] selectedIngredients, Recipe recipeToUpdate)
        {
            if (selectedIngredients == null)
            {
                recipeToUpdate.RecipeIngredients = new List<RecipeIngredient>();
                return;
            }
            var selectedIngredientsHS = new HashSet<string>(selectedIngredients);
            var recipeIngredients = new HashSet<int>
            (recipeToUpdate.RecipeIngredients.Select(c => c.Ingredient.ID));
            foreach (var ing in context.Ingredient)
            {
                if (selectedIngredientsHS.Contains(ing.ID.ToString()))
                {
                    if (!recipeIngredients.Contains(ing.ID))
                    {
                        recipeToUpdate.RecipeIngredients.Add(
                        new RecipeIngredient
                        {
                            RecipeID = recipeToUpdate.ID,
                            IngredientID = ing.ID
                        });
                    }
                }
                else
                {
                    if (recipeIngredients.Contains(ing.ID))
                    {
                        RecipeIngredient courseToRemove
                        = recipeToUpdate
                        .RecipeIngredients
                        .SingleOrDefault(i => i.IngredientID == ing.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
