// Owner 1: "Juan Pablo Ordonez Gomez" has added 95% of the code in this file
// Owner 2: "Daniel Bajenov" has added 5% of the code in this file
// Principal Author: Juan Pablo Ordonez Gomez
// Description: Entity Framework Core DbContext for Mealventory application. Defines DbSets and seeds initial data.
using Mealventory.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Mealventory.API.Database
{
    /// <summary>
    /// EF Core DbContext for the application.
    /// </summary>
    public class MealventoryDbContext : DbContext
    {
        /// <summary>
        /// Constructs the DbContext with the specified options.
        /// </summary>
        /// <param name="options">DbContext options.</param>
        public MealventoryDbContext(DbContextOptions<MealventoryDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Users table.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// FoodItems table.
        /// </summary>
        public DbSet<FoodItem> FoodItems { get; set; }

        /// <summary>
        /// Shopping list items table.
        /// </summary>
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        /// <summary>
        /// Configures the EF Core model and seeds initial data.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
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