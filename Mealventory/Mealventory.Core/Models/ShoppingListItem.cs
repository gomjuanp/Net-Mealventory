// Owner 1: "Daniel Bajenov" has added 100% of the code in this file
// Principal Author: Daniel Bajenov
// Description: Domain model representing an item on a user's shopping list.

using System;
using System.Collections.Generic;
using System.Text;

namespace Mealventory.Core.Models
{
    /// <summary>
    /// Represents an item on a user's shopping list.
    /// </summary>
    public class ShoppingListItem
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the shopping list item.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Desired quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Indicates whether the item has been purchased.
        /// </summary>
        public bool IsPurchased { get; set; }

        /// <summary>
        /// Owner user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property to the owning user.
        /// </summary>
        public User? User { get; set; }
    }
}
