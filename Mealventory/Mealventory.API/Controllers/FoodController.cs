// Owner 1: "Juan Pablo Ordonez Gomez" has added 60% of the code in this file
// Owner 2: "Daniel Bajenov" has added 40% of the code in this file

// Principal Author: Juan Pablo Ordonez Gomez
// Description: Controller exposing CRUD endpoints for food inventory items.
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mealventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Controller for managing food items in a user's inventory.
    /// </summary>
    public class FoodController : ControllerBase
    {
        /// <summary>
        /// Repository used to access and manipulate food items.
        /// </summary>
        private readonly IFoodRepository repository;

        /// <summary>
        /// Constructs a new <see cref="FoodController"/>.
        /// </summary>
        /// <param name="repo">The food repository.</param>
        public FoodController(IFoodRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Returns all food items for the supplied user id.
        /// </summary>
        /// <param name="userId">The owner user id.</param>
        [HttpGet]
        public IEnumerable<FoodItem> Get([FromQuery] int userId)
        {
            return repository.GetAll(userId);
        }

        /// <summary>
        /// Returns a specific food item by id for a given user.
        /// </summary>
        /// <param name="id">Food item id.</param>
        /// <param name="userId">Owner user id.</param>
        [HttpGet("{id}")]
        public ActionResult<FoodItem> Get(int id, [FromQuery] int userId)
        {
            var item = repository.GetById(id, userId);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        /// <summary>
        /// Adds a new food item or merges with an existing one when name, location and expiry match.
        /// </summary>
        /// <param name="item">The food item to add.</param>
        [HttpPost]
        public ActionResult<FoodItem> Post([FromBody] FoodItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                return BadRequest("Food name cannot be empty.");

            item.Name = item.Name.Trim();
            item.Location = string.IsNullOrWhiteSpace(item.Location) ? "Pantry" : item.Location.Trim();

            var normalizedName = item.Name.ToLower();
            var normalizedLocation = item.Location.ToLower();
            var normalizedExpiry = item.ExpirationDate.Date;

            var existingItems = repository.GetAll(item.UserId);

            var matchingItem = existingItems.FirstOrDefault(f =>
                f.Name.Trim().ToLower() == normalizedName &&
                f.ExpirationDate.Date == normalizedExpiry &&
                f.Location.Trim().ToLower() == normalizedLocation);

            if (matchingItem != null)
            {
                matchingItem.Quantity += item.Quantity;
                var updated = repository.Update(matchingItem);
                return Ok(updated);
            }

            var created = repository.Add(item);
            return CreatedAtAction(nameof(Get), new { id = created.Id, userId = created.UserId }, created);
        }

        /// <summary>
        /// Updates an existing food item.
        /// </summary>
        /// <param name="item">The updated food item.</param>
        [HttpPut]
        public ActionResult<FoodItem> Put([FromBody] FoodItem item)
        {
            var updated = repository.Update(item);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        /// <summary>
        /// Deletes a food item for a given user.
        /// </summary>
        /// <param name="id">The id of the item to delete.</param>
        /// <param name="userId">The owner user id.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] int userId)
        {
            repository.Delete(id, userId);
            return NoContent();
        }
    }
}