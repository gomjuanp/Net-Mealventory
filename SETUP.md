# Mealventory – Setup & Run Instructions

## Requirements
- .NET SDK (version 10.0 or later)
- Visual Studio 2026 (recommended) or VS Code
- SQL Server LocalDB (included with Visual Studio)

---

## 1. Open the Project
1. Extract the ZIP file  
2. Open the solution file:  
   Mealventory.sln  

---

## 2. Restore Dependencies

In Visual Studio:
- Right-click the solution → Restore NuGet Packages  

OR using CLI:
dotnet restore

---

## 3. Setup the Database

Open Package Manager Console in Visual Studio  

Set:
- Default Project → Mealventory.API  

Run:
Update-Database

---

## 4. Run the Application

Option A (Recommended – Visual Studio)
1. Right-click the solution → Set Startup Projects  
2. Select Multiple startup projects  
3. Set:  
   - Mealventory.API → Start  
   - Mealventory.Web → Start  
4. Press F5  

Option B (CLI)
Run the following commands:
dotnet build  
dotnet ef database update --project Mealventory.API  
dotnet run --project Mealventory.Web  

---

## 5. Access the Application
The app will open automatically in your browser  

---

## Notes
- Make sure the database is created before running the app  
- If errors occur, rebuild the solution and run Update-Database again