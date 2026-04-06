// Owner 1: "Juan Pablo Ordonez Gomez" has added 92% of the code in this file
// Owner 2: "Daniel Bajenov" has added 8% of the code in this file
// Principal Author: Juan Pablo Ordonez Gomez
// Description: Interface defining operations for food item persistence.


using Mealventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealventory.Core.Interfaces
{
    /// <summary>
    /// Repository interface for managing <see cref="Mealventory.Core.Models.FoodItem"/> entities.
    /// </summary>
    public interface IFoodRepository
    {
        /// <summary>
        /// Gets all food items for a user.
        /// </summary>
        /// <param name="userId">User id.</param>
        IEnumerable<FoodItem> GetAll(int userId);

        /// <summary>
        /// Gets food items by user (alias for GetAll).
        /// </summary>
        /// <param name="userId">User id.</param>
        IEnumerable<FoodItem> GetByUser(int userId);

        /// <summary>
        /// Gets a food item by id for a given user.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <param name="userId">User id.</param>
        FoodItem? GetById(int id, int userId);

        /// <summary>
        /// Adds a new food item.
        /// </summary>
        /// <param name="item">Item to add.</param>
        FoodItem Add(FoodItem item);

        /// <summary>
        /// Updates an existing food item.
        /// </summary>
        /// <param name="item">Updated item.</param>
        FoodItem? Update(FoodItem item);

        /// <summary>
        /// Deletes a food item by id for a user.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <param name="userId">User id.</param>
        void Delete(int id, int userId);
    }
}