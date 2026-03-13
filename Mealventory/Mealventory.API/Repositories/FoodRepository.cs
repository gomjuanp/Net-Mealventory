// TODO: This has to be changed to use database

using Mealventory.Core.Models;
using Mealventory.Core.Interfaces;

namespace Mealventory.API.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly List<FoodItem> foods = new();

        public IEnumerable<FoodItem> GetAll() => foods;

        public FoodItem GetById(int id) =>
            foods.FirstOrDefault(f => f.Id == id);

        public FoodItem Add(FoodItem item)
        {
            foods.Add(item);
            return item;
        }

        public FoodItem Update(FoodItem item)
        {
            var existing = GetById(item.Id);

            if (existing == null)
                return null;

            existing.Name = item.Name;
            existing.ExpirationDate = item.ExpirationDate;
            existing.Quantity = item.Quantity;

            return existing;
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
                foods.Remove(item);
        }
    }
}
