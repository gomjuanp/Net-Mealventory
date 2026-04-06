// Owner 1: "Daniel Bajenov" has added 100% of the code in this file

// Principal Author: Daniel Bajenov
// Description: Unit tests for the ShoppingListRepository.

using Mealventory.API.Database;
using Mealventory.API.Repositories;
using Mealventory.Core.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Mealventory.Tests;

[TestFixture]
public class ShoppingListRepositoryTests
{
    private MealventoryDbContext _context = null!;
    private ShoppingListRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MealventoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new MealventoryDbContext(options);
        _repository = new ShoppingListRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public void GetByUser_ReturnsOnlyMatchingUsersItems_OrderedByName()
    {
        _context.ShoppingListItems.AddRange(
            new ShoppingListItem { Id = 1, Name = "Banana", UserId = 1 },
            new ShoppingListItem { Id = 2, Name = "Apple", UserId = 1 },
            new ShoppingListItem { Id = 3, Name = "Milk", UserId = 2 });
        _context.SaveChanges();

        var items = _repository.GetByUser(1).ToList();

        Assert.That(items, Has.Count.EqualTo(2));
        Assert.That(items[0].Name, Is.EqualTo("Apple"));
        Assert.That(items[1].Name, Is.EqualTo("Banana"));
    }

    [Test]
    public void Add_PersistsTheItem()
    {
        var item = new ShoppingListItem { Id = 1, Name = "Milk", Quantity = 1, UserId = 1 };

        var created = _repository.Add(item);

        Assert.That(created, Is.SameAs(item));
        Assert.That(_context.ShoppingListItems.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Delete_RemovesMatchingItem()
    {
        _context.ShoppingListItems.Add(new ShoppingListItem { Id = 1, Name = "Milk", UserId = 1 });
        _context.SaveChanges();

        _repository.Delete(1, 1);

        Assert.That(_context.ShoppingListItems.Count(), Is.EqualTo(0));
    }

    [Test]
    public void Delete_DoesNothingWhenItemDoesNotExist()
    {
        _context.ShoppingListItems.Add(new ShoppingListItem { Id = 1, Name = "Milk", UserId = 1 });
        _context.SaveChanges();

        _repository.Delete(2, 1);

        Assert.That(_context.ShoppingListItems.Count(), Is.EqualTo(1));
    }
}