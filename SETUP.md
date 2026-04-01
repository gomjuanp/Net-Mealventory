# Setup Instructions

Use these steps every time after cloning the repository or pulling changes from the cloud.

1. Run restore:

```bash
dotnet restore
```

2. Ensure SQL Server LocalDB is installed.

3. Apply database migrations:

```bash
dotnet ef database update --project Mealventory.API
```

4. Run the API:

```bash
dotnet run --project Mealventory.API
```
