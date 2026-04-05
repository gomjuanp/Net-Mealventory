using Mealventory.Core.Models;
using System.Collections.Generic;

namespace Mealventory.Core.Interfaces
{
    public interface IShoppingListRepository
    {
        IEnumerable<ShoppingListItem> GetByUser(int userId);
        ShoppingListItem Add(ShoppingListItem item);
        void Delete(int id, int userId);
    }
}