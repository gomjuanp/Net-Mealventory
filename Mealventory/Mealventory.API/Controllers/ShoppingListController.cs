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
            var inventoryItems = _foodRepo.GetByUser(item.UserId);

            var existing = inventoryItems.FirstOrDefault(x =>
                x.Name.ToLower() == item.Name.ToLower());

            var created = _shoppingRepo.Add(item);

            if (existing != null)
            {
                return Ok(new
                {
                    item = created,
                    warning = $"{item.Name} is already in your {existing.Location.ToLower()}."
                });
            }

            return Ok(new
            {
                item = created,
                warning = (string?)null
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