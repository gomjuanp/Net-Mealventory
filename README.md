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

* **NUnit**

