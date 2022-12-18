namespace Food_Recipes.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string IngredientName { get; set; }
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}
