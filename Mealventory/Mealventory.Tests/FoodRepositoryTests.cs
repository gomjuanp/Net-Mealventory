// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using Mealventory.API.Database;
using Mealventory.API.Repositories;
using Mealventory.Core.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Mealventory.Tests;

[TestFixture]
public class FoodRepositoryTests
{
    private MealventoryDbContext _context = null!;
    private FoodRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MealventoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new MealventoryDbContext(options);
        _repository = new FoodRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public void GetAll_ReturnsOnlyItemsForTheRequestedUser()
    {
        // Arrange
        _context.FoodItems.AddRange(
            new FoodItem { Id = 1, Name = "Apple", UserId = 1 },
            new FoodItem { Id = 2, Name = "Milk", UserId = 2 },
            new FoodItem { Id = 3, Name = "Banana", UserId = 1 });
        _context.SaveChanges();

        // Act
        var items = _repository.GetAll(1).ToList();

        // Assert
        Assert.That(items, Has.Count.EqualTo(2));
        Assert.That(items.All(item => item.UserId == 1), Is.True);
    }

    [Test]
    public void GetByUser_ReturnsOnlyItemsForTheRequestedUser()
    {
        // Arrange
        _context.FoodItems.AddRange(
            new FoodItem { Id = 1, Name = "Apple", UserId = 1 },
            new FoodItem { Id = 2, Name = "Milk", UserId = 2 },
            new FoodItem { Id = 3, Name = "Banana", UserId = 1 });
        _context.SaveChanges();

        // Act
        var items = _repository.GetByUser(2).ToList();

        // Assert
        Assert.That(items, Has.Count.EqualTo(1));
        Assert.That(items.Single().Name, Is.EqualTo("Milk"));
    }

    [Test]
    public void GetById_ReturnsTheMatchingItemForTheRequestedUser()
    {
        // Arrange
        _context.FoodItems.Add(new FoodItem { Id = 1, Name = "Apple", UserId = 1 });
        _context.SaveChanges();

        // Act
        var item = _repository.GetById(1, 1);

        // Assert
        Assert.That(item, Is.Not.Null);
        Assert.That(item!.Name, Is.EqualTo("Apple"));
    }

    [Test]
    public void GetById_ReturnsNullWhenTheItemDoesNotBelongToTheRequestedUser()
    {
        // Arrange
        _context.FoodItems.Add(new FoodItem { Id = 1, Name = "Apple", UserId = 1 });
        _context.SaveChanges();

        // Act
        var item = _repository.GetById(1, 2);

        // Assert
        Assert.That(item, Is.Null);
    }

    [Test]
    public void Add_PersistsTheItem()
    {
        // Arrange
        var item = new FoodItem { Id = 1, Name = "Apple", UserId = 1 };

        // Act
        var created = _repository.Add(item);

        // Assert
        Assert.That(created, Is.SameAs(item));
        Assert.That(_context.FoodItems.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Update_UpdatesTheExistingItem()
    {
        // Arrange
        _context.FoodItems.Add(new FoodItem
        {
            Id = 1,
            Name = "Apple",
            Quantity = 1,
            Location = "Pantry",
            UserId = 1
        });
        _context.SaveChanges();

        var updated = new FoodItem
        {
            Id = 1,
            Name = "Banana",
            Quantity = 3,
            ExpirationDate = new DateTime(2026, 4, 12),
            Location = "Fridge",
            UserId = 1
        };

        // Act
        var result = _repository.Update(updated);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("Banana"));
        Assert.That(result.Quantity, Is.EqualTo(3));
        Assert.That(result.Location, Is.EqualTo("Fridge"));
        Assert.That(result.ExpirationDate, Is.EqualTo(updated.ExpirationDate));
    }

    [Test]
    public void Update_ReturnsNullWhenTheItemDoesNotExist()
    {
        // Arrange
        var item = new FoodItem { Id = 1, Name = "Banana", UserId = 1 };

        // Act
        var result = _repository.Update(item);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Delete_RemovesTheMatchingItem()
    {
        // Arrange
        _context.FoodItems.Add(new FoodItem { Id = 1, Name = "Apple", UserId = 1 });
        _context.SaveChanges();

        // Act
        _repository.Delete(1, 1);

        // Assert
        Assert.That(_context.FoodItems.Count(), Is.EqualTo(0));
    }

    [Test]
    public void Delete_DoesNothingWhenTheItemDoesNotExist()
    {
        // Arrange
        _context.FoodItems.Add(new FoodItem { Id = 1, Name = "Apple", UserId = 1 });
        _context.SaveChanges();

        // Act
        _repository.Delete(2, 1);

        // Assert
        Assert.That(_context.FoodItems.Count(), Is.EqualTo(1));
    }
}
