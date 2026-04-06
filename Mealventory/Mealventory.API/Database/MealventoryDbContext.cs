// Owner 1: "Juan Pablo Ordonez Gomez" has added 95.24% of the code in this file
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

        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.FoodItems)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);

            // Seed Dummy Data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "juan", Email = "juan@gmail.com", PasswordHash = "password123" },
                new User { Id = 2, Username = "daniel", Email = "daniel@hotmail.com", PasswordHash = "password123" }
            );

            modelBuilder.Entity<FoodItem>().HasData(
                new FoodItem { Id = 1, Name = "Milk", ExpirationDate = new DateTime(2026, 4, 15), Quantity = 3, UserId = 1 },
                new FoodItem { Id = 2, Name = "Banana", ExpirationDate = new DateTime(2026, 4, 12), Quantity = 2, UserId = 1 },
                new FoodItem { Id = 3, Name = "Bacon", ExpirationDate = new DateTime(2026, 4, 24), Quantity = 1, UserId = 1 },

                new FoodItem { Id = 4, Name = "Milk", ExpirationDate = new DateTime(2026, 4, 15), Quantity = 24, UserId = 2 },
                new FoodItem { Id = 5, Name = "Banana", ExpirationDate = new DateTime(2026, 4, 12), Quantity = 16, UserId = 2 },
                new FoodItem { Id = 6, Name = "Apple", ExpirationDate = new DateTime(2026, 4, 20), Quantity = 12, UserId = 2 }
            );
        }
    }
}