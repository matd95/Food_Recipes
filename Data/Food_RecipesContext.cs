using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Food_Recipes.Models;

namespace Food_Recipes.Data
{
    public class Food_RecipesContext : DbContext
    {
        public Food_RecipesContext (DbContextOptions<Food_RecipesContext> options)
            : base(options)
        {
        }

        public DbSet<Food_Recipes.Models.Recipe> Recipe { get; set; } = default!;

        public DbSet<Food_Recipes.Models.Category> Category { get; set; }

        public DbSet<Food_Recipes.Models.Ingredient> Ingredient { get; set; }
    }
}
