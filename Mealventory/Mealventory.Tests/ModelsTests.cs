// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using Mealventory.Core.Models;
using NUnit.Framework;

namespace Mealventory.Tests;

/// Tests default model values and collections.
[TestFixture]
public class ModelsTests
{
    /// Method to verify user food items collection starts empty.
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

    /// Method to verify food item default location is pantry.
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

    /// Method to verify food item default name is empty string.
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
