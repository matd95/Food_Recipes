namespace Food_Recipes.Models
{
    public class RecipeIngredient
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
