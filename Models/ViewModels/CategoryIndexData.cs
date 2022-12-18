using Food_Recipes.Models;
using System.Security.Policy;

namespace Food_Recipes.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
    }
}
