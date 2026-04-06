// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using Mealventory.Core.Models;
using NUnit.Framework;

namespace Mealventory.Tests;

[TestFixture]
public class ModelsTests
{
    [Test]
    public void User_FoodItemsStartsEmpty()
    {
        // Arrange
        var user = new User();

        // Act
        var foodItems = user.FoodItems;

        // Assert
        Assert.That(foodItems, Is.Not.Null);
        Assert.That(foodItems, Is.Empty);
    }

    [Test]
    public void FoodItem_DefaultLocationIsPantry()
    {
        // Arrange
        var foodItem = new FoodItem();

        // Act
        var location = foodItem.Location;

        // Assert
        Assert.That(location, Is.EqualTo("Pantry"));
    }

    [Test]
    public void FoodItem_DefaultNameIsEmptyString()
    {
        // Arrange
        var foodItem = new FoodItem();

        // Act
        var name = foodItem.Name;

        // Assert
        Assert.That(name, Is.EqualTo(string.Empty));
    }
}
