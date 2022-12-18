namespace Food_Recipes.Models
{
    public class RecipeData
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
