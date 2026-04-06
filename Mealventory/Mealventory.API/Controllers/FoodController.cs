// Owner 1: "Juan Pablo Ordonez Gomez" has added 88% of the code in this file
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mealventory.API.Controllers
{
    /// Provides endpoints to manage food inventory items.
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        /// Field to store the food repository dependency.
        private readonly IFoodRepository repository;

        /// Method to create a food controller with required dependencies.
        public FoodController(IFoodRepository repo)
        {
            repository = repo;
        }

        /// Method to get all food items for a user.
        [HttpGet]
        public IEnumerable<FoodItem> Get([FromQuery] int userId)
        {
            return repository.GetAll(userId);
        }

        /// Method to get a specific food item by identifier for a user.
        [HttpGet("{id}")]
        public ActionResult<FoodItem> Get(int id, [FromQuery] int userId)
        {
            var item = repository.GetById(id, userId);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        /// Method to create a new food item.
        [HttpPost]
        public ActionResult<FoodItem> Post([FromBody] FoodItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                return BadRequest("Food name cannot be empty.");

            item.Name = item.Name.Trim();
            item.Location = item.Location.Trim();

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

        /// Method to update an existing food item.
        [HttpPut]
        public ActionResult<FoodItem> Put([FromBody] FoodItem item)
        {
            var updated = repository.Update(item);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        /// Method to delete a food item for a user.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] int userId)
        {
            repository.Delete(id, userId);
            return NoContent();
        }
    }
}