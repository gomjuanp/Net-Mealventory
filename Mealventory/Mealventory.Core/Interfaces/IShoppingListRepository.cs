// Owner 1: "Daniel Bajenov" has added 100% of the code in this file
// Principal Author: Daniel Bajenov
// Description: Interface defining shopping list repository operations.

using Mealventory.Core.Models;
using System.Collections.Generic;

namespace Mealventory.Core.Interfaces
{
    /// <summary>
    /// Repository interface for managing <see cref="Mealventory.Core.Models.ShoppingListItem"/> entities.
    /// </summary>
    public interface IShoppingListRepository
    {
        /// <summary>
        /// Gets shopping list items for a user.
        /// </summary>
        /// <param name="userId">User id.</param>
        IEnumerable<ShoppingListItem> GetByUser(int userId);

        /// <summary>
        /// Adds a new shopping list item.
        /// </summary>
        /// <param name="item">Item to add.</param>
        ShoppingListItem Add(ShoppingListItem item);

        /// <summary>
        /// Deletes a shopping list item for a user.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <param name="userId">User id.</param>
        void Delete(int id, int userId);
    }
}