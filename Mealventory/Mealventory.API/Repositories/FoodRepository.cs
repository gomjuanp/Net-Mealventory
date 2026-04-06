// Owner 1: "Juan Pablo Ordonez Gomez" has added 71% of the code in this file
using Mealventory.API.Database;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;

namespace Mealventory.API.Repositories
{
    /// Provides data access operations for food inventory items.
    public class FoodRepository : IFoodRepository
    {
        /// Field to store the database context dependency.
        private readonly MealventoryDbContext _context;

        /// Method to create a food repository with required dependencies.
        public FoodRepository(MealventoryDbContext context)
        {
            _context = context;
        }

        /// Method to get all food items for a user.
        public IEnumerable<FoodItem> GetAll(int userId)
        {
            return _context.FoodItems
                .Where(f => f.UserId == userId)
                .ToList();
        }

        /// Method to get food items by user identifier.
        public IEnumerable<FoodItem> GetByUser(int userId)
        {
            return _context.FoodItems
                .Where(f => f.UserId == userId)
                .ToList();
        }

        /// Method to get a food item by item identifier and user identifier.
        public FoodItem? GetById(int id, int userId)
        {
            return _context.FoodItems
                .FirstOrDefault(f => f.Id == id && f.UserId == userId);
        }

        /// Method to add a new food item.
        public FoodItem Add(FoodItem item)
        {
            _context.FoodItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        /// Method to update an existing food item.
        public FoodItem? Update(FoodItem item)
        {
            var existing = _context.FoodItems
                .FirstOrDefault(f => f.Id == item.Id && f.UserId == item.UserId);

            if (existing == null)
                return null;

            existing.Name = item.Name;
            existing.Quantity = item.Quantity;
            existing.ExpirationDate = item.ExpirationDate;
            existing.Location = item.Location;

            _context.SaveChanges();
            return existing;
        }

        /// Method to delete a food item by item identifier and user identifier.
        public void Delete(int id, int userId)
        {
            var item = _context.FoodItems
                .FirstOrDefault(f => f.Id == id && f.UserId == userId);

            if (item == null)
                return;

            _context.FoodItems.Remove(item);
            _context.SaveChanges();
        }
    }
}