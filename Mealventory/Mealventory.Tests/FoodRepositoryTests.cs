using Mealventory.API.Repositories;
using Mealventory.Core.Models;

namespace Mealventory.Tests;

[TestClass]
public class FoodRepositoryTests
{
    [TestMethod]
    public void AddFoodItem_ShouldIncreaseCount()
    {
        var repo = new FoodRepository();

        repo.Add(new FoodItem { Id = 1, Name = "Apple" });

        Assert.AreEqual(1, repo.GetAll().Count());
    }
}
