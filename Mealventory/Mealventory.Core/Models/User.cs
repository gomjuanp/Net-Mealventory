// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using System.Collections.Generic;

namespace Mealventory.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
    }
}