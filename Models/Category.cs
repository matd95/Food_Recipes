namespace Food_Recipes.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
