// Owner 1: "Daniel Bajenov" has added 100% of the code in this file

// Owner 1: "Daniel Bajenov" has added 100% of the code in this file
// Principal Author: Daniel Bajenov
// Description: Repository implementation for managing ShoppingListItem persistence.
using Mealventory.API.Database;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;

namespace Mealventory.API.Repositories
{
    /// <summary>
    /// Concrete implementation of <see cref="IShoppingListRepository"/>.
    /// </summary>
    public class ShoppingListRepository : IShoppingListRepository
    {
        /// <summary>
        /// EF Core DbContext instance.
        /// </summary>
        private readonly MealventoryDbContext _context;

        /// <summary>
        /// Constructs a new <see cref="ShoppingListRepository"/>.
        /// </summary>
        /// <param name="context">DbContext to use for data access.</param>
        public ShoppingListRepository(MealventoryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets shopping list items for a user ordered by name.
        /// </summary>
        /// <param name="userId">User id.</param>
        public IEnumerable<ShoppingListItem> GetByUser(int userId)
        {
            return _context.ShoppingListItems
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Name)
                .ToList();
        }

        /// <summary>
        /// Adds and persists a new shopping list item.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public ShoppingListItem Add(ShoppingListItem item)
        {
            _context.ShoppingListItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes a shopping list item for the given user.
        /// </summary>
        /// <param name="id">Id of the item to delete.</param>
        /// <param name="userId">Owner user id.</param>
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