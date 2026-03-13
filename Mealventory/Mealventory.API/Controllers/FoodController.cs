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
        public IEnumerable<FoodItem> Get()
        {
            return repository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<FoodItem> Get(int id)
        {
            var item = repository.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<FoodItem> Post([FromBody] FoodItem item)
        {
            var created = repository.Add(item);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
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
        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            return NoContent();
        }
    }
}
