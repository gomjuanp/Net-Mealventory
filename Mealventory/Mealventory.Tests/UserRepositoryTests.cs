// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using Mealventory.API.Database;
using Mealventory.API.Repositories;
using Mealventory.Core.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Mealventory.Tests;

[TestFixture]
public class UserRepositoryTests
{
    private MealventoryDbContext _context = null!;
    private UserRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MealventoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new MealventoryDbContext(options);
        _repository = new UserRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task GetUserByEmailAsync_ReturnsTheMatchingUser()
    {
        // Arrange
        _context.Users.Add(new User { Id = 1, Username = "juan", Email = "juan@example.com", PasswordHash = "hash" });
        await _context.SaveChangesAsync();

        // Act
        var user = await _repository.GetUserByEmailAsync("juan@example.com");

        // Assert
        Assert.That(user, Is.Not.Null);
        Assert.That(user!.Username, Is.EqualTo("juan"));
    }

    [Test]
    public async Task GetUserByEmailAsync_ReturnsNullWhenNoUserMatches()
    {
        // Arrange

        // Act
        var user = await _repository.GetUserByEmailAsync("missing@example.com");

        // Assert
        Assert.That(user, Is.Null);
    }

    [Test]
    public async Task CreateUserAsync_PersistsTheUser()
    {
        // Arrange
        var user = new User { Username = "juan", Email = "juan@example.com", PasswordHash = "hash" };

        // Act
        var created = await _repository.CreateUserAsync(user);

        // Assert
        Assert.That(created, Is.SameAs(user));
        Assert.That(await _context.Users.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetUserByIdAsync_ReturnsTheMatchingUser()
    {
        // Arrange
        _context.Users.Add(new User { Id = 1, Username = "juan", Email = "juan@example.com", PasswordHash = "hash" });
        await _context.SaveChangesAsync();

        // Act
        var user = await _repository.GetUserByIdAsync(1);

        // Assert
        Assert.That(user, Is.Not.Null);
        Assert.That(user!.Email, Is.EqualTo("juan@example.com"));
    }

    [Test]
    public async Task GetUserByIdAsync_ReturnsNullWhenNoUserMatches()
    {
        // Arrange

        // Act
        var user = await _repository.GetUserByIdAsync(1);

        // Assert
        Assert.That(user, Is.Null);
    }
}
