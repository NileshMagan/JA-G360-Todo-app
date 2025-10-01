# Backend (TodoApi)

ASP.NET Core .NET 9 Web API with in-memory TODOs and Swagger.

## Run
```
cd backend/src/TodoApi
 dotnet restore
 dotnet run
```
Runs at http://localhost:5000 (Swagger at /swagger in Development).

## Tests
```
cd backend/tests/TodoApi.Tests
 dotnet restore
 dotnet test
```

## Structure
- src/TodoApi: API project (net9.0)
- tests/TodoApi.Tests: xUnit tests (net9.0)

## Notes
- CORS allows `http://localhost:4200` for Angular dev.
- Data stored in-memory; restarts reset the list.
