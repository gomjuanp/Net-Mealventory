// Owner 1: "Juan Pablo Ordonez Gomez" has added 77% of the code in this file
// Owner 2: "Daniel Bajenov" has added 23% of the code in this file
// Principal Author: Juan Pablo Ordonez Gomez
// Description: Domain model representing a food inventory item.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealventory.Core.Models
{
    /// <summary>
    /// Represents an inventory item with name, quantity, expiration and location belonging to a user.
    /// </summary>
    public class FoodItem
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the food item.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Expiration date for the item.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Quantity available.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Owner user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property to the owning user.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Location of the item (e.g., Fridge, Pantry).
        /// </summary>
        public string Location { get; set; } = "Pantry";
    }
}