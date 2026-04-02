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
            if (item.UserId <= 0)
                return BadRequest("User is required.");

            item.Name = item.Name.Trim();
            item.ExpirationDate = item.ExpirationDate.Date;

            var existing = repository.GetByNameAndExpiration(item.Name, item.ExpirationDate, item.UserId);
            if (existing != null)
            {
                var updated = repository.UpdateQuantity(existing.Id, item.UserId, existing.Quantity + item.Quantity);
                return Ok(updated);
            }

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

        [HttpPatch("{id}/quantity")]
        public ActionResult<FoodItem> PatchQuantity(int id, [FromBody] UpdateFoodQuantityRequest request)
        {
            if (request.UserId <= 0)
                return BadRequest("User is required.");

            var updated = repository.UpdateQuantity(id, request.UserId, request.Quantity);
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
