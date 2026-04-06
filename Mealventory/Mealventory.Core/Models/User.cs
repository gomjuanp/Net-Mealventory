// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using System.Collections.Generic;

namespace Mealventory.Core.Models
{
    /// Represents an application user and their related inventory items.
    public class User
    {
        /// Field to store the user identifier.
        public int Id { get; set; }

        /// Field to store the username.
        public string Username { get; set; }

        /// Field to store the email address.
        public string Email { get; set; }

        /// Field to store the password hash value.
        public string PasswordHash { get; set; }

        /// Field to store the food items associated with the user.
        public ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
    }
}