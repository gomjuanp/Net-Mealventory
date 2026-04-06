// Owner 1: "Daniel Bajenov" has added 100% of the code in this file

// Principal Author: Daniel Bajenov
// Description: Unit tests for the ShoppingListController.

using Mealventory.API.Controllers;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Reflection;

namespace Mealventory.Tests;

[TestFixture]
public class ShoppingListControllerTests
{
    private StubShoppingListRepository _shoppingRepo = null!;
    private StubFoodRepository _foodRepo = null!;
    private ShoppingListController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _shoppingRepo = new StubShoppingListRepository();
        _foodRepo = new StubFoodRepository();
        _controller = new ShoppingListController(_shoppingRepo, _foodRepo);
    }

    [Test]
    public void GetByUser_ReturnsOkWithItems()
    {
        _shoppingRepo.Items =
        [
            new ShoppingListItem { Id = 1, Name = "Milk", UserId = 1 }
        ];

        var result = _controller.GetByUser(1) as OkObjectResult;

        Assert.That(result, Is.Not.Null);
        var items = result!.Value as IEnumerable<ShoppingListItem>;
        Assert.That(items, Is.Not.Null);
        Assert.That(items!.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Add_ReturnsBadRequestWhenNameIsEmpty()
    {
        var result = _controller.Add(new ShoppingListItem { Name = "   ", UserId = 1 });

        var badRequest = result as BadRequestObjectResult;
        Assert.That(badRequest, Is.Not.Null);
        Assert.That(badRequest!.Value, Is.EqualTo("Item name cannot be empty."));
    }

    [Test]
    public void Add_ReturnsNullWarningWhenItemIsNotInInventory()
    {
        var result = _controller.Add(new ShoppingListItem
        {
            Name = "  Apple  ",
            Quantity = 1,
            UserId = 1
        }) as OkObjectResult;

        Assert.That(result, Is.Not.Null);

        var warning = result!.Value!.GetType().GetProperty("warning", BindingFlags.Public | BindingFlags.Instance)!.GetValue(result.Value);
        Assert.That(warning, Is.Null);

        Assert.That(_shoppingRepo.AddedItem, Is.Not.Null);
        Assert.That(_shoppingRepo.AddedItem!.Name, Is.EqualTo("Apple"));
    }

    [Test]
    public void Add_ReturnsWarningWhenItemAlreadyExistsInInventory()
    {
        _foodRepo.Items =
        [
            new FoodItem
            {
                Id = 1,
                Name = "Milk",
                UserId = 1,
                Location = "Pantry"
            }
        ];

        var result = _controller.Add(new ShoppingListItem
        {
            Name = "Milk",
            Quantity = 1,
            UserId = 1
        }) as OkObjectResult;

        Assert.That(result, Is.Not.Null);

        var warning = result!.Value!.GetType().GetProperty("warning", BindingFlags.Public | BindingFlags.Instance)!.GetValue(result.Value) as string;
        Assert.That(warning, Is.EqualTo("Milk was added to your shopping list, but it is already in your pantry."));
    }

    [Test]
    public void Add_InventoryCheckIsCaseInsensitive()
    {
        _foodRepo.Items =
        [
            new FoodItem
            {
                Id = 1,
                Name = "milk",
                UserId = 1,
                Location = "Fridge"
            }
        ];

        var result = _controller.Add(new ShoppingListItem
        {
            Name = "MILK",
            Quantity = 1,
            UserId = 1
        }) as OkObjectResult;

        Assert.That(result, Is.Not.Null);

        var warning = result!.Value!.GetType().GetProperty("warning", BindingFlags.Public | BindingFlags.Instance)!.GetValue(result.Value) as string;
        Assert.That(warning, Is.EqualTo("MILK was added to your shopping list, but it is already in your fridge."));
    }

    [Test]
    public void Add_UsesInventoryWhenLocationIsBlank()
    {
        _foodRepo.Items =
        [
            new FoodItem
            {
                Id = 1,
                Name = "Milk",
                UserId = 1,
                Location = "   "
            }
        ];

        var result = _controller.Add(new ShoppingListItem
        {
            Name = "Milk",
            Quantity = 1,
            UserId = 1
        }) as OkObjectResult;

        Assert.That(result, Is.Not.Null);

        var warning = result!.Value!.GetType().GetProperty("warning", BindingFlags.Public | BindingFlags.Instance)!.GetValue(result.Value) as string;
        Assert.That(warning, Is.EqualTo("Milk was added to your shopping list, but it is already in your inventory."));
    }

    [Test]
    public void Delete_ReturnsNoContentAndCallsRepository()
    {
        var result = _controller.Delete(7, 1);

        Assert.That(result, Is.TypeOf<NoContentResult>());
        Assert.That(_shoppingRepo.DeleteCallCount, Is.EqualTo(1));
        Assert.That(_shoppingRepo.DeletedId, Is.EqualTo(7));
        Assert.That(_shoppingRepo.DeletedUserId, Is.EqualTo(1));
    }

    private sealed class StubShoppingListRepository : IShoppingListRepository
    {
        public IEnumerable<ShoppingListItem> Items { get; set; } = Array.Empty<ShoppingListItem>();
        public ShoppingListItem? AddedItem { get; private set; }
        public int DeleteCallCount { get; private set; }
        public int? DeletedId { get; private set; }
        public int? DeletedUserId { get; private set; }

        public IEnumerable<ShoppingListItem> GetByUser(int userId) => Items.Where(x => x.UserId == userId);

        public ShoppingListItem Add(ShoppingListItem item)
        {
            item.Id = item.Id == 0 ? 999 : item.Id;
            AddedItem = item;
            return item;
        }

        public void Delete(int id, int userId)
        {
            DeleteCallCount++;
            DeletedId = id;
            DeletedUserId = userId;
        }
    }

    private sealed class StubFoodRepository : IFoodRepository
    {
        public IEnumerable<FoodItem> Items { get; set; } = Array.Empty<FoodItem>();

        public IEnumerable<FoodItem> GetAll(int userId) => Items.Where(x => x.UserId == userId);
        public IEnumerable<FoodItem> GetByUser(int userId) => Items.Where(x => x.UserId == userId);
        public FoodItem? GetById(int id, int userId) => Items.FirstOrDefault(x => x.Id == id && x.UserId == userId);
        public FoodItem Add(FoodItem item) => item;
        public FoodItem? Update(FoodItem item) => item;
        public void Delete(int id, int userId) { }
    }
}