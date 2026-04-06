// Owner 1: "Daniel Bajenov" has added 100% of the code in this file

// Principal Author: Daniel Bajenov
// Description: Controller exposing endpoints for managing a user's shopping list.
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mealventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Controller for shopping list operations (get, add, delete).
    /// </summary>
    public class ShoppingListController : ControllerBase
    {
        /// <summary>
        /// Repository for shopping list items.
        /// </summary>
        private readonly IShoppingListRepository _shoppingRepo;

        /// <summary>
        /// Repository for food inventory used to check for existing items.
        /// </summary>
        private readonly IFoodRepository _foodRepo;

        /// <summary>
        /// Creates a new <see cref="ShoppingListController"/>.
        /// </summary>
        /// <param name="shoppingRepo">Shopping list repository.</param>
        /// <param name="foodRepo">Food repository.</param>
        public ShoppingListController(IShoppingListRepository shoppingRepo, IFoodRepository foodRepo)
        {
            _shoppingRepo = shoppingRepo;
            _foodRepo = foodRepo;
        }

        /// <summary>
        /// Gets shopping list items for the supplied user.
        /// </summary>
        /// <param name="userId">Owner user id.</param>
        [HttpGet]
        public IActionResult GetByUser([FromQuery] int userId)
        {
            return Ok(_shoppingRepo.GetByUser(userId));
        }

        /// <summary>
        /// Adds an item to the user's shopping list and returns a possible warning
        /// if the item already exists in inventory.
        /// </summary>
        /// <param name="item">The shopping list item to add.</param>
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

        /// <summary>
        /// Deletes an item from the shopping list for the given user.
        /// </summary>
        /// <param name="id">The id of the shopping list item to delete.</param>
        /// <param name="userId">The owner user id.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] int userId)
        {
            _shoppingRepo.Delete(id, userId);
            return NoContent();
        }
    }
}