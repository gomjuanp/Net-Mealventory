// Owner 1: "Juan Pablo Ordonez Gomez" has added 97% of the code in this file
// Owner 2: "Daniel Bajenov" has added 3% of the code in this file
// Principal Author: Juan Pablo Ordonez Gomez
// Description: Application startup and dependency injection configuration for Mealventory API.
using Mealventory.API.Database;
using Mealventory.API.Repositories;
using Mealventory.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add controller services
builder.Services.AddControllers();

builder.Services.AddOpenApi();

// Register DbContext
builder.Services.AddDbContext<MealventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IShoppingListRepository, ShoppingListRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
