// Owner 1: "Juan Pablo Ordonez Gomez" has added 71% of the code in this file
using Mealventory.API.Database;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;

namespace Mealventory.API.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly MealventoryDbContext _context;

        public FoodRepository(MealventoryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FoodItem> GetAll(int userId)
        {
            return _context.FoodItems
                .Where(f => f.UserId == userId)
                .ToList();
        }

        public IEnumerable<FoodItem> GetByUser(int userId)
        {
            return _context.FoodItems
                .Where(f => f.UserId == userId)
                .ToList();
        }

        public FoodItem? GetById(int id, int userId)
        {
            return _context.FoodItems
                .FirstOrDefault(f => f.Id == id && f.UserId == userId);
        }

        public FoodItem Add(FoodItem item)
        {
            _context.FoodItems.Add(item);
            _context.SaveChanges();
            return item;
        }

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