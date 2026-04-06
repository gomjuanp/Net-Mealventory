// Owner 1: "Juan Pablo Ordonez Gomez" has added 88% of the code in this file
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mealventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository repository;

        public FoodController(IFoodRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IEnumerable<FoodItem> Get([FromQuery] int userId)
        {
            return repository.GetAll(userId);
        }

        [HttpGet("{id}")]
        public ActionResult<FoodItem> Get(int id, [FromQuery] int userId)
        {
            var item = repository.GetById(id, userId);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<FoodItem> Post([FromBody] FoodItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                return BadRequest("Food name cannot be empty.");

            var normalizedName = item.Name.Trim().ToLower();

            var existingItems = repository.GetAll(item.UserId);

            if (existingItems.Any(f => f.Name.Trim().ToLower() == normalizedName))
                return BadRequest($"{item.Name} already exists in your inventory.");

            item.Name = item.Name.Trim();

            var created = repository.Add(item);

            return CreatedAtAction(nameof(Get), new { id = created.Id, userId = created.UserId }, created);
        }

        [HttpPut]
        public ActionResult<FoodItem> Put([FromBody] FoodItem item)
        {
            var updated = repository.Update(item);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] int userId)
        {
            repository.Delete(id, userId);
            return NoContent();
        }
    }
}