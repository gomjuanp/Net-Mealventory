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
            return _context.FoodItems.Where(f => f.UserId == userId).ToList();
        }

        public FoodItem? GetById(int id, int userId)
        {
            return _context.FoodItems.FirstOrDefault(f => f.Id == id && f.UserId == userId);
        }

        public FoodItem? GetByNameAndExpiration(string name, DateTime expirationDate, int userId)
        {
            var normalizedName = name.Trim().ToLower();
            var normalizedDate = expirationDate.Date;

            return _context.FoodItems.FirstOrDefault(f =>
                f.UserId == userId &&
                f.ExpirationDate.Date == normalizedDate &&
                f.Name.ToLower() == normalizedName);
        }

        public FoodItem Add(FoodItem item)
        {
            _context.FoodItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public FoodItem? Update(FoodItem item)
        {
            var existing = _context.FoodItems.Find(item.Id);
            if (existing == null)
                return null;

            existing.Name = item.Name;
            existing.Quantity = item.Quantity;
            existing.ExpirationDate = item.ExpirationDate;

            _context.SaveChanges();
            return existing;
        }

        public FoodItem? UpdateQuantity(int id, int userId, int quantity)
        {
            var existing = _context.FoodItems.FirstOrDefault(f => f.Id == id && f.UserId == userId);
            if (existing == null)
                return null;

            existing.Quantity = quantity;
            _context.SaveChanges();
            return existing;
        }

        public void Delete(int id, int userId)
        {
            var item = _context.FoodItems.FirstOrDefault(f => f.Id == id && f.UserId == userId);
            if (item == null) return;

            _context.FoodItems.Remove(item);
            _context.SaveChanges();
        }
    }
}