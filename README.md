# TODO App (Angular + .NET 9)

Simple TODO list: view, add, delete. Angular frontend + .NET Web API backend. In-memory storage.

## Prerequisites
- Node.js 18+ and npm
- .NET SDK 9.0+

## Run Backend
```
cd backend/src/TodoApi
 dotnet restore
 dotnet run
```
API runs at http://localhost:5000 (Swagger at /swagger in Development).

## Run Frontend
```
cd frontend
 npm install
 npm start
```
App runs at http://localhost:4200 and calls http://localhost:5000.

## Tests
- Backend: `cd backend/tests/TodoApi.Tests && dotnet restore && dotnet test`
- Frontend: `cd frontend && npm test`

## API Endpoints
- GET /api/todos
- POST /api/todos { title: string }
- DELETE /api/todos/{id}
