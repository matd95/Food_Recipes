using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Xml.Linq;

namespace Food_Recipes.Models
{
    public class Recipe
    {
        public int ID { get; set; }

        [Display(Name = "Recipe Title")]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; }
        public int? CategoryID { get; set; }
        public Category? Category { get; set; }
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}
