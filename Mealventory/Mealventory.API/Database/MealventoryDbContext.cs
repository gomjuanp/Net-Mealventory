using Mealventory.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Mealventory.API.Database
{
    public class MealventoryDbContext : DbContext
    {
        public MealventoryDbContext(DbContextOptions<MealventoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; }
    }
}