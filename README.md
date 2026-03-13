.NET Web API Project

## Project Overview

This project is a **.NET ASP.NET Core Web API application** that simulates the backend of a **Smart Food Scanner system**.

The original concept of the application (from the iOS custom project) was:

* A mobile application scans food items using AI
* The system detects the food item
* The system predicts expiration time
* The system suggests recipes and notifies users before food expires

For this **.NET implementation**, we simplify the input method:

Instead of scanning food using AI, the user will **manually enter food items through a form in a web application**.

Example:

User adds an item:

* Name: Apple
* Expiration Date: 2026-04-01
* Quantity: 3

The system will:

* Store the food item
* Track expiration dates
* Generate notifications
* Suggest recipes (future feature using AI)

The application exposes a **RESTful API** that the web interface will consume.

---

# Tech Stack

Backend Framework

* **ASP.NET Core Web API (.NET 10 or later)**

Language

* **C#**

Architecture

* RESTful API

Database

* **SQL Database (Entity Framework Core)**

Testing

* **MSTest**

External Integration (future implementation)

* **OpenAI API**

---

# Repository Structure

```
SmartFoodScanner
│
├── SmartFoodScanner.Api
│   ├── Controllers
│   ├── Models
│   ├── DTOs
│   ├── Services
│   ├── Data
│   ├── Interfaces
│   └── Program.cs
│
├── SmartFoodScanner.Tests
│
├── README.md
└── .gitignore
```

---

# Current Implementation Status

The initial **API structure has been created**.

Implemented components:

* ASP.NET Core Web API project
* Basic REST architecture
* Project folder organization
* API Controllers structure
* Models for food items
* Service layer structure
* DTO layer
* Testing project structure

Some files contain **TODO comments** indicating where implementation still needs to be completed.

---

# Features (Planned)

Core Features

1. Add food item
2. View stored food items
3. Track expiration dates
4. Send expiration notifications
5. Generate recipe suggestions

---

# Database Integration

Currently the project may contain **temporary or placeholder repository logic**.

This must be replaced with a **real SQL database implementation**.

The recommended stack:

* **Entity Framework Core**
* **SQL Server**

### Steps that must be completed

1. Install EF Core packages

```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```

2. Create the database context

```
Data/
    ApplicationDbContext.cs
```

Example structure:

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<FoodItem> FoodItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
```

3. Configure the connection string in

```
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SmartFoodScannerDB;Trusted_Connection=True;"
}
```

4. Register the DbContext in `Program.cs`

5. Create migrations

```
Add-Migration InitialCreate
Update-Database
```

Any files containing `TODO: Replace with database implementation` must be updated accordingly.

---

# API Endpoints (Expected)

Food Items

```
GET /api/fooditems
GET /api/fooditems/{id}
POST /api/fooditems
PUT /api/fooditems/{id}
DELETE /api/fooditems/{id}
```

Example POST request:

```
POST /api/fooditems
```

Body

```
{
  "name": "Milk",
  "expirationDate": "2026-04-10",
  "quantity": 1
}
```

---

# Testing

Unit tests are located in:

```
SmartFoodScanner.Tests
```

Testing framework:

* **MSTest**

Tests should cover:

* Controllers
* Services
* Business logic

Example test cases:

* Add food item
* Retrieve food items
* Delete food item
* Expiration validation

---

# Remaining Work

The following components still need to be implemented.

## 1. SQL Database Integration

Replace temporary logic with:

* Entity Framework Core
* SQL Server database
* Migrations

Any file marked with **TODO** should be updated.

---

## 2. Web Interface (Views)

The project still needs a **web interface** where users can submit food items.

Pages to implement:

* Add Food Item
* View Stored Items
* Expiration Dashboard

The frontend should communicate with the API endpoints.

---

## 3. OpenAI Integration

The system should generate **recipe suggestions based on available ingredients**.

Example workflow:

1. User stores food items
2. The system gathers available ingredients
3. The API calls **OpenAI**
4. OpenAI generates recipe suggestions

Example service:

```
Services/OpenAiService.cs
```

This feature is **not yet implemented**.

---

## 4. Email Notifications

Users should receive notifications when food items are close to expiration.

Possible implementations:

* SMTP email service
* Background service
* Scheduled tasks

Example:

```
"Your milk expires tomorrow. Consider using it for a recipe."
```

---

## 5. Security

Security features must be added:

* Authentication
* Authorization
* Input validation
* API protection

Recommended implementations:

* JWT authentication
* HTTPS enforcement
* Secure API endpoints

---

# Running the Project

### 1 Clone the repository

```
git clone <repository-url>
```

---

### 2 Open the solution

Open in:

```
Visual Studio 2026
```

---

### 3 Restore dependencies

Visual Studio should restore packages automatically.

If needed:

```
dotnet restore
```

---

### 4 Configure database

Update `appsettings.json` with your SQL Server connection string.

---

### 5 Run migrations

```
Add-Migration InitialCreate
Update-Database
```

---

### 6 Run the API

Press

```
F5
```

or

```
dotnet run
```

---

### 7 Test endpoints

Use:

* Swagger (built-in)
* Postman
* Thunder Client

Example:

```
https://localhost:xxxx/api/fooditems
```

---

# Team Development Guidelines

Before starting work:

1. Pull the latest changes

```
git pull
```

2. Create a new branch (First create an Issue on GitHub and then create/attach a branch to it)

```
git checkout -b feature/feature-name
```

3. Implement the feature

4. Commit changes

```
git commit -m "Implemented feature"
```

5. Push branch

```
git push origin feature/feature-nameMake a Pull request into develop, after someone reviews it goes into develop, then
```

6. Create a Pull Request
   
```
Make a Pull request into develop, after someone reviews it, it goes into develop.
Let's use main for milestones or after big changes in develop.
```

# Summary

This project implements the backend of a **Smart Food Expiration Management System** using **ASP.NET Core Web API**.

The API will:

* Store food items
* Track expiration
* Suggest recipes
* Notify users before food expires

The next steps include:

* Database implementation
* OpenAI integration
* Web interface
* Security
* Notification system
