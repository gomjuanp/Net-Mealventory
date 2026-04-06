// Owner 1: "Juan Pablo Ordonez Gomez" has added 71% of the code in this file
// Owner 2: "Daniel Bajenov" has added 29% of the code in this file
// Principal Author: Juan Pablo Ordonez Gomez
// Description: Repository implementation for managing FoodItem persistence.
using Mealventory.API.Database;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;

namespace Mealventory.API.Repositories
{
    /// <summary>
    /// Concrete implementation of <see cref="IFoodRepository"/> using EF Core.
    /// </summary>
    public class FoodRepository : IFoodRepository
    {
        /// <summary>
        /// EF Core DbContext instance.
        /// </summary>
        private readonly MealventoryDbContext _context;

        /// <summary>
        /// Constructs a new <see cref="FoodRepository"/>.
        /// </summary>
        /// <param name="context">DbContext to use for data access.</param>
        public FoodRepository(MealventoryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all food items for the specified user.
        /// </summary>
        /// <param name="userId">User id.</param>
        public IEnumerable<FoodItem> GetAll(int userId)
        {
            return _context.FoodItems
                .Where(f => f.UserId == userId)
                .ToList();
        }

        /// <summary>
        /// Gets food items by user (alias for GetAll).
        /// </summary>
        /// <param name="userId">User id.</param>
        public IEnumerable<FoodItem> GetByUser(int userId)
        {
            return _context.FoodItems
                .Where(f => f.UserId == userId)
                .ToList();
        }

        /// <summary>
        /// Gets a single food item for the given id and user.
        /// </summary>
        /// <param name="id">Food id.</param>
        /// <param name="userId">User id.</param>
        public FoodItem? GetById(int id, int userId)
        {
            return _context.FoodItems
                .FirstOrDefault(f => f.Id == id && f.UserId == userId);
        }

        /// <summary>
        /// Adds and persists a new food item.
        /// </summary>
        /// <param name="item">Food item to add.</param>
        public FoodItem Add(FoodItem item)
        {
            _context.FoodItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        /// <summary>
        /// Updates an existing food item.
        /// </summary>
        /// <param name="item">Updated food item.</param>
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

        /// <summary>
        /// Deletes a food item for the specified user.
        /// </summary>
        /// <param name="id">Id of the item to delete.</param>
        /// <param name="userId">Owner user id.</param>
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