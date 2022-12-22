using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Xml.Linq;

namespace Food_Recipes.Models
{
    public class Recipe
    {
        public int ID { get; set; }

        [Display(Name = "Recipe Name")]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; }
        public int? CategoryID { get; set; }
        [Display(Name = "Category")]
        public Category? Category { get; set; }
        [Display(Name = "Ingredients")]
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
        [Display(Name = "Instructions")]
        public string Instructions { get; set; }
    }
}
