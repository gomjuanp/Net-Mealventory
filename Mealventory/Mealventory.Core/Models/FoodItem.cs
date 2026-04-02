using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Mealventory.Core.Models
{
    public class FoodItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Food name is required.")]
        [RegularExpression(@".*\S.*", ErrorMessage = "Food name is required.")]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(DateTime), "01/01/1900", "12/31/9999", ErrorMessage = "Expiration date is required.")]
        public DateTime ExpirationDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
