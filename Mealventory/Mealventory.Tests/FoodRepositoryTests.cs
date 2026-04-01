using Mealventory.API.Repositories;
using Mealventory.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Mealventory.Tests;

[TestClass]
public class FoodRepositoryTests
{
    [TestMethod]
    public void AddFoodItem_ShouldIncreaseCount()
    {
        var options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<Mealventory.API.Database.MealventoryDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new Mealventory.API.Database.MealventoryDbContext(options);
        var repo = new FoodRepository(context);

        repo.Add(new FoodItem { Id = 1, Name = "Apple", UserId = 1 });

        Assert.AreEqual(1, repo.GetAll(1).Count());
    }
}
