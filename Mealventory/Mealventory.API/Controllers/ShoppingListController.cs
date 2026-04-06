// Owner 1: "Daniel Bajenov" has added 100% of the code in this file

using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mealventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListRepository _shoppingRepo;
        private readonly IFoodRepository _foodRepo;

        public ShoppingListController(IShoppingListRepository shoppingRepo, IFoodRepository foodRepo)
        {
            _shoppingRepo = shoppingRepo;
            _foodRepo = foodRepo;
        }

        [HttpGet]
        public IActionResult GetByUser([FromQuery] int userId)
        {
            return Ok(_shoppingRepo.GetByUser(userId));
        }

        [HttpPost]
        public IActionResult Add([FromBody] ShoppingListItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                return BadRequest("Item name cannot be empty.");

            item.Name = item.Name.Trim();

            var inventoryItems = _foodRepo.GetByUser(item.UserId);

            var existing = inventoryItems.FirstOrDefault(x =>
                x.Name.Trim().ToLower() == item.Name.ToLower());

            var created = _shoppingRepo.Add(item);

            string? warning = null;

            if (existing != null)
            {
                var location = string.IsNullOrWhiteSpace(existing.Location)
                    ? "inventory"
                    : existing.Location.ToLower();

                warning = $"{item.Name} was added to your shopping list, but it is already in your {location}.";
            }

            return Ok(new
            {
                item = created,
                warning
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] int userId)
        {
            _shoppingRepo.Delete(id, userId);
            return NoContent();
        }
    }
}