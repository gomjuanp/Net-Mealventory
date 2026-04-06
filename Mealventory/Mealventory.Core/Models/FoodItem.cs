// Owner 1: "Juan Pablo Ordonez Gomez" has added 77% of the code in this file
// Owner 2: "Daniel Bajenov" has added 23% of the code in this file

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealventory.Core.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }

        public User? User { get; set; }

        public string Location { get; set; } = "Pantry";
    }
}