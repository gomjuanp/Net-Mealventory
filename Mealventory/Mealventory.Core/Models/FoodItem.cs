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

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Quantity { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
