// Owner 1: "Juan Pablo Ordonez Gomez" has added 90% of the code in this file
// Owner 2: "Daniel Bajenov" has added 10% of the code in this file
using Mealventory.API.Controllers;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Mealventory.Tests;

[TestFixture]
public class FoodControllerTests
{
    private StubFoodRepository _repository = null!;
    private FoodController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _repository = new StubFoodRepository();
        _controller = new FoodController(_repository);
    }

    [Test]
    public void Get_ReturnsTheItemsFromTheRepository()
    {
        _repository.AllItems =
        [
            new FoodItem { Id = 1, Name = "Apple", UserId = 1 }
        ];

        var items = _controller.Get(1).ToList();

        Assert.That(items, Has.Count.EqualTo(1));
        Assert.That(items.Single().Name, Is.EqualTo("Apple"));
    }

    [Test]
    public void GetById_ReturnsOkWhenTheItemExists()
    {
        var item = new FoodItem { Id = 1, Name = "Apple", UserId = 1 };
        _repository.ItemById = item;

        var result = _controller.Get(1, 1);

        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.SameAs(item));
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public void GetById_ReturnsNotFoundWhenTheItemDoesNotExist()
    {
        var result = _controller.Get(1, 1);

        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Post_ReturnsBadRequestWhenTheNameIsEmpty()
    {
        var item = new FoodItem { Name = "   ", UserId = 1, Location = "Pantry" };

        var result = _controller.Post(item);

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.That(badRequest, Is.Not.Null);
        Assert.That(badRequest!.Value, Is.EqualTo("Food name cannot be empty."));
    }

    [Test]
    public void Post_MergesQuantity_WhenNameExpiryAndLocationMatch()
    {
        _repository.AllItems =
        [
            new FoodItem
            {
                Id = 1,
                Name = " Milk ",
                ExpirationDate = new DateTime(2026, 4, 10),
                Quantity = 2,
                UserId = 1,
                Location = " pantry "
            }
        ];

        var item = new FoodItem
        {
            Name = "milk",
            ExpirationDate = new DateTime(2026, 4, 10, 18, 0, 0),
            Quantity = 3,
            UserId = 1,
            Location = "Pantry"
        };

        var result = _controller.Post(item);

        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);

        Assert.That(_repository.UpdateCallCount, Is.EqualTo(1));
        Assert.That(_repository.LastUpdatedItem, Is.Not.Null);
        Assert.That(_repository.LastUpdatedItem!.Quantity, Is.EqualTo(5));
        Assert.That(_repository.AddedItem, Is.Null);
    }

    [Test]
    public void Post_AddsNewItem_WhenExpiryIsDifferent()
    {
        _repository.AllItems =
        [
            new FoodItem
            {
                Id = 1,
                Name = "Milk",
                ExpirationDate = new DateTime(2026, 4, 10),
                Quantity = 2,
                UserId = 1,
                Location = "Pantry"
            }
        ];

        var item = new FoodItem
        {
            Name = "Milk",
            ExpirationDate = new DateTime(2026, 4, 15),
            Quantity = 1,
            UserId = 1,
            Location = "Pantry"
        };

        var result = _controller.Post(item);

        var created = result.Result as CreatedAtActionResult;
        Assert.That(created, Is.Not.Null);
        Assert.That(_repository.AddedItem, Is.Not.Null);
        Assert.That(_repository.AddedItem!.Name, Is.EqualTo("Milk"));
        Assert.That(_repository.UpdateCallCount, Is.EqualTo(0));
    }

    [Test]
    public void Post_AddsNewItem_WhenLocationIsDifferent()
    {
        _repository.AllItems =
        [
            new FoodItem
            {
                Id = 1,
                Name = "Milk",
                ExpirationDate = new DateTime(2026, 4, 10),
                Quantity = 2,
                UserId = 1,
                Location = "Pantry"
            }
        ];

        var item = new FoodItem
        {
            Name = "Milk",
            ExpirationDate = new DateTime(2026, 4, 10),
            Quantity = 1,
            UserId = 1,
            Location = "Fridge"
        };

        var result = _controller.Post(item);

        var created = result.Result as CreatedAtActionResult;
        Assert.That(created, Is.Not.Null);
        Assert.That(_repository.AddedItem, Is.Not.Null);
        Assert.That(_repository.AddedItem!.Location, Is.EqualTo("Fridge"));
        Assert.That(_repository.UpdateCallCount, Is.EqualTo(0));
    }

    [Test]
    public void Post_TrimsTheNameBeforeAdding()
    {
        var item = new FoodItem
        {
            Name = "  Apple  ",
            UserId = 1,
            Location = "Pantry",
            ExpirationDate = new DateTime(2026, 4, 10)
        };

        var result = _controller.Post(item);

        var created = result.Result as CreatedAtActionResult;
        Assert.That(created, Is.Not.Null);
        Assert.That(_repository.AddedItem, Is.Not.Null);
        Assert.That(_repository.AddedItem!.Name, Is.EqualTo("Apple"));
    }

    [Test]
    public void Put_ReturnsOkWhenTheRepositoryUpdatesTheItem()
    {
        var updated = new FoodItem { Id = 1, Name = "Apple", UserId = 1 };
        _repository.UpdatedItem = updated;

        var result = _controller.Put(updated);

        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.SameAs(updated));
    }

    [Test]
    public void Put_ReturnsNotFoundWhenTheRepositoryCannotUpdateTheItem()
    {
        var result = _controller.Put(new FoodItem { Id = 1, Name = "Apple", UserId = 1 });

        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Delete_ReturnsNoContentAndCallsTheRepository()
    {
        var result = _controller.Delete(1, 1);

        Assert.That(result, Is.TypeOf<NoContentResult>());
        Assert.That(_repository.DeleteCallCount, Is.EqualTo(1));
        Assert.That(_repository.DeletedId, Is.EqualTo(1));
        Assert.That(_repository.DeletedUserId, Is.EqualTo(1));
    }

    private sealed class StubFoodRepository : IFoodRepository
    {
        public IEnumerable<FoodItem> AllItems { get; set; } = Array.Empty<FoodItem>();
        public FoodItem? ItemById { get; set; }
        public FoodItem? UpdatedItem { get; set; }
        public FoodItem? AddedItem { get; private set; }
        public FoodItem? LastUpdatedItem { get; private set; }
        public int UpdateCallCount { get; private set; }
        public int DeleteCallCount { get; private set; }
        public int? DeletedId { get; private set; }
        public int? DeletedUserId { get; private set; }

        public IEnumerable<FoodItem> GetAll(int userId) => AllItems.Where(item => item.UserId == userId);

        public IEnumerable<FoodItem> GetByUser(int userId) => AllItems.Where(item => item.UserId == userId);

        public FoodItem? GetById(int id, int userId) =>
            ItemById is { } item && item.Id == id && item.UserId == userId ? item : null;

        public FoodItem Add(FoodItem item)
        {
            item.Id = item.Id == 0 ? 999 : item.Id;
            AddedItem = item;
            return item;
        }

        public FoodItem? Update(FoodItem item)
        {
            UpdateCallCount++;
            LastUpdatedItem = item;

            if (UpdatedItem is not null && UpdatedItem.Id == item.Id && UpdatedItem.UserId == item.UserId)
                return UpdatedItem;

            return null;
        }

        public void Delete(int id, int userId)
        {
            DeleteCallCount++;
            DeletedId = id;
            DeletedUserId = userId;
        }
    }
}