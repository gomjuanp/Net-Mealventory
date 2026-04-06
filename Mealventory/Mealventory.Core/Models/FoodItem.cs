// Owner 1: "Juan Pablo Ordonez Gomez" has added 71% of the code in this file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealventory.Core.Models
{
    /// Represents a food item tracked in a user's inventory.
    public class FoodItem
    {
        /// Field to store the food item identifier.
        public int Id { get; set; }

        /// Field to store the food item name.
        public string Name { get; set; } = string.Empty;

        /// Field to store the expiration date of the food item.
        public DateTime ExpirationDate { get; set; }

        /// Field to store the quantity of the food item.
        public int Quantity { get; set; }

        /// Field to store the owner user identifier.
        public int UserId { get; set; }

        /// Field to store the navigation reference to the owner user.
        public User? User { get; set; }

        /// Field to store the location where the food item is kept.
        public string Location { get; set; } = "Pantry";
    }
}