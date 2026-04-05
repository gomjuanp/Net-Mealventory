using System;
using System.Collections.Generic;
using System.Text;

namespace Mealventory.Core.Models
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public bool IsPurchased { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
