// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
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
        // Arrange
        _repository.AllItems = [new FoodItem { Id = 1, Name = "Apple", UserId = 1 }];

        // Act
        var items = _controller.Get(1).ToList();

        // Assert
        Assert.That(items, Has.Count.EqualTo(1));
        Assert.That(items.Single().Name, Is.EqualTo("Apple"));
    }

    [Test]
    public void GetById_ReturnsOkWhenTheItemExists()
    {
        // Arrange
        var item = new FoodItem { Id = 1, Name = "Apple", UserId = 1 };
        _repository.ItemById = item;

        // Act
        var result = _controller.Get(1, 1);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.SameAs(item));
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public void GetById_ReturnsNotFoundWhenTheItemDoesNotExist()
    {
        // Arrange

        // Act
        var result = _controller.Get(1, 1);

        // Assert
        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Post_ReturnsBadRequestWhenTheNameIsEmpty()
    {
        // Arrange
        var item = new FoodItem { Name = "   ", UserId = 1 };

        // Act
        var result = _controller.Post(item);

        // Assert
        var badRequest = result.Result as BadRequestObjectResult;
        Assert.That(badRequest, Is.Not.Null);
        Assert.That(badRequest!.Value, Is.EqualTo("Food name cannot be empty."));
    }

    [Test]
    public void Post_ReturnsBadRequestWhenAnItemWithTheSameNameAlreadyExists()
    {
        // Arrange
        _repository.AllItems = [new FoodItem { Id = 1, Name = " Apple ", UserId = 1 }];

        // Act
        var result = _controller.Post(new FoodItem { Name = "apple", UserId = 1 });

        // Assert
        var badRequest = result.Result as BadRequestObjectResult;
        Assert.That(badRequest, Is.Not.Null);
        Assert.That(badRequest!.Value, Is.EqualTo("apple already exists in your inventory."));
    }

    [Test]
    public void Post_TrimsTheNameAndReturnsCreatedAtAction()
    {
        // Arrange
        var item = new FoodItem { Name = "  Apple  ", UserId = 1 };

        // Act
        var result = _controller.Post(item);

        // Assert
        var created = result.Result as CreatedAtActionResult;
        Assert.That(created, Is.Not.Null);
        Assert.That(_repository.AddedItem, Is.Not.Null);
        Assert.That(_repository.AddedItem!.Name, Is.EqualTo("Apple"));
        Assert.That(created!.RouteValues!["id"], Is.EqualTo(_repository.AddedItem.Id));
        Assert.That(created.RouteValues["userId"], Is.EqualTo(_repository.AddedItem.UserId));
        Assert.That(created.StatusCode, Is.EqualTo(201));
    }

    [Test]
    public void Put_ReturnsOkWhenTheRepositoryUpdatesTheItem()
    {
        // Arrange
        var updated = new FoodItem { Id = 1, Name = "Apple", UserId = 1 };
        _repository.UpdatedItem = updated;

        // Act
        var result = _controller.Put(updated);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.SameAs(updated));
    }

    [Test]
    public void Put_ReturnsNotFoundWhenTheRepositoryCannotUpdateTheItem()
    {
        // Arrange

        // Act
        var result = _controller.Put(new FoodItem { Id = 1, Name = "Apple", UserId = 1 });

        // Assert
        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Delete_ReturnsNoContentAndCallsTheRepository()
    {
        // Arrange

        // Act
        var result = _controller.Delete(1, 1);

        // Assert
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
        public int DeleteCallCount { get; private set; }
        public int? DeletedId { get; private set; }
        public int? DeletedUserId { get; private set; }

        public IEnumerable<FoodItem> GetAll(int userId) => AllItems.Where(item => item.UserId == userId);

        public IEnumerable<FoodItem> GetByUser(int userId) => AllItems.Where(item => item.UserId == userId);

        public FoodItem? GetById(int id, int userId) => ItemById is { } item && item.Id == id && item.UserId == userId ? item : null;

        public FoodItem Add(FoodItem item)
        {
            AddedItem = item;
            return item;
        }

        public FoodItem? Update(FoodItem item) => UpdatedItem is { } updated && updated.Id == item.Id && updated.UserId == item.UserId ? updated : null;

        public void Delete(int id, int userId)
        {
            DeleteCallCount++;
            DeletedId = id;
            DeletedUserId = userId;
        }
    }
}
