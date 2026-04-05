using Mealventory.API.Database;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;

namespace Mealventory.API.Repositories
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly MealventoryDbContext _context;

        public ShoppingListRepository(MealventoryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingListItem> GetByUser(int userId)
        {
            return _context.ShoppingListItems
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Name)
                .ToList();
        }

        public ShoppingListItem Add(ShoppingListItem item)
        {
            _context.ShoppingListItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(int id, int userId)
        {
            var item = _context.ShoppingListItems
                .FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (item != null)
            {
                _context.ShoppingListItems.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}