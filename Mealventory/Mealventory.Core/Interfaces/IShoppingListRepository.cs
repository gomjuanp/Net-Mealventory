// Owner 1: "Daniel Bajenov" has added 100% of the code in this file

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