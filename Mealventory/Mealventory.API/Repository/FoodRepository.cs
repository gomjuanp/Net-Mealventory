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

        public IEnumerable<FoodItem> GetAll()
        {
            return _context.FoodItems.ToList();
        }

        public FoodItem GetById(int id)
        {
            return _context.FoodItems.Find(id);
        }

        public FoodItem Add(FoodItem item)
        {
            _context.FoodItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public FoodItem Update(FoodItem item)
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

        public void Delete(int id)
        {
            var item = _context.FoodItems.Find(id);
            if (item == null) return;

            _context.FoodItems.Remove(item);
            _context.SaveChanges();
        }
    }
}