using System.ComponentModel.DataAnnotations;

namespace Mealventory.Core.Models
{
    public class UpdateFoodQuantityRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public int UserId { get; set; }
    }
}