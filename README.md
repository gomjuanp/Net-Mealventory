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
Net-Mealventory/
├── Mealventory/
│   ├── Mealventory.slnx
│   ├── Mealventory.API/
│   ├── Mealventory.Core/
│   └── Mealventory.Tests/
├── README.md
└── SETUP.md
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

The project uses **Entity Framework Core** with **SQL Server (LocalDB)**.

The recommended stack:

* **Entity Framework Core**
* **SQL Server**

### Database setup and maintenance

1. Restore dependencies

```bash
dotnet restore
```

2. Ensure SQL Server LocalDB is installed

3. Apply migrations

```bash
dotnet ef database update --project Mealventory.API
```

4. Keep migrations in source control and avoid manual schema edits

Common EF Core packages in this project:

```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```

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

## 1. Web Interface (Views)

The project still needs a **web interface** where users can submit food items.

Pages to implement:

* Add Food Item
* View Stored Items
* Expiration Dashboard

The frontend should communicate with the API endpoints.

---

## 2. OpenAI Integration

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

## 3. Email Notifications

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

## 4. Security

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

# Next Steps For The Project

Current foundation already in place:

* Basic CRUD
* Repository pattern
* SQL database
* Controller layer

The next phase is focused on improving code quality, architecture, and production readiness.

## Priority Roadmap (In Order)

### 1. Make Methods Async (Professional Standard)

Update repository methods to asynchronous versions.

Example:

```csharp
Task<List<FoodItem>> GetAllAsync();
```

Use EF Core async queries:

```csharp
await _context.FoodItems.ToListAsync();
```

Modern APIs should use async/await for scalability and non-blocking I/O.

### 2. Add DTOs (Very Important)

Do not expose entity models directly from the API.

Create DTOs such as:

```text
DTOs/
  CreateFoodDto
  UpdateFoodDto
  FoodResponseDto
```

Benefits:

* Decouples database models from API contracts
* Gives better control over request/response payloads
* Improves maintainability and versioning

### 3. Add Validation

Add request validation rules, for example:

* Name cannot be empty
* Quantity must be greater than 0
* Expiration date cannot be in the past

Options:

* Data Annotations
* FluentValidation

### 4. Add Proper Error Handling Middleware

Avoid returning raw exceptions.

Create:

```text
Middleware/ExceptionMiddleware.cs
```

Return consistent error responses with a standard error format.

### 5. Add Basic Authentication or JWT (If Required)

If this is a real-world deployment, secure the API.

At minimum, implement authentication so endpoints are not fully public.

### 6. Implement OpenAI Mail Generation

If required by the project scope:

* Auto-generate reminder emails for expiring food
* Add AI-powered suggestions

Create service:

```text
Services/OpenAiService.cs
```

Register and inject it through dependency injection.

### 7. Create the Frontend (If Needed)

Current implementation is backend-only.

Next logical UI options:

* Angular
* Razor Pages
* React

## Project Maturity Levels

Current state:

* Level 2/5 - Functional CRUD API

Target state:

* Level 4/5 - Clean layered architecture, async flows, DTO boundaries, validation, and secure API

## Team Project Safeguards

Before adding major features, ensure:

* Everyone uses the same .NET version
* Migrations are committed to source control
* Nobody edits the database schema manually outside migrations
* Setup instructions are always kept up to date

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

### 3 Navigate to the solution folder

```bash
cd Mealventory
```

---

### 4 Restore dependencies

Visual Studio should restore packages automatically.

If needed:

```bash
dotnet restore
```

---

### 5 Ensure SQL Server LocalDB is installed

---

### 6 Run migrations

```bash
dotnet ef database update --project Mealventory.API
```

---

### 7 Run the API

Press

```
F5
```

or

```bash
dotnet run --project Mealventory.API
```

---

### 8 Test endpoints

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

```bash
git push origin feature/feature-name
```

6. Create a Pull Request
   
```
Open a Pull Request into `develop`.
After review and approval, merge into `develop`.
Use `main` for milestones or stable releases.
```

# Summary

This project implements the backend of a **Smart Food Expiration Management System** using **ASP.NET Core Web API**.

The API will:

* Store food items
* Track expiration
* Suggest recipes
* Notify users before food expires

The next steps include:

* Async repository and service methods
* DTO and validation improvements
* OpenAI integration
* Web interface
* Security
* Notification system
