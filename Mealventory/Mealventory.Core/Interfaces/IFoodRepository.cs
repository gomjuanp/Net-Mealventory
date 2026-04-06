// Owner 1: "Juan Pablo Ordonez Gomez" has added 77% of the code in this file

using Mealventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealventory.Core.Interfaces
{
    /// Defines operations to manage food inventory items for a specific user.
    public interface IFoodRepository
    {
        /// Method to get all food items for a user.
        IEnumerable<FoodItem> GetAll(int userId);

        /// Method to get food items by user identifier.
        IEnumerable<FoodItem> GetByUser(int userId);

        /// Method to get a food item by item identifier and user identifier.
        FoodItem? GetById(int id, int userId);

        /// Method to add a food item.
        FoodItem Add(FoodItem item);

        /// Method to update an existing food item.
        FoodItem? Update(FoodItem item);

        /// Method to delete a food item by item identifier and user identifier.
        void Delete(int id, int userId);
    }
}