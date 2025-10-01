# TODO App (Angular + .NET 8)

Simple TODO list: view, add, delete. Angular frontend + .NET Web API backend. In-memory storage.

## Prerequisites
- Node.js 18+ and npm
- .NET SDK 8.0+

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

## HOW-TO (Project Setup Steps)
1. Initialized Git repository in `TodoApp`.
2. Created Angular 20 app (`frontend`) with standalone components and strict settings.
3. Created .NET 8 Web API (TodoApi) with controllers, added Swagger, CORS, and in-memory repository.
4. Implemented `TodosController` with GET/POST/DELETE endpoints.
5. Built Angular `TodosComponent` and `TodoService` using `HttpClient` and signals.
6. Added unit tests:
   - Backend: repository and controller (xUnit + FluentAssertions + Moq).
   - Frontend: service and component (HttpClientTestingModule + Karma/Jasmine).
7. Restructured backend to `backend/src` and `backend/tests`; updated solution mapping.
8. Added READMEs at root, `backend/`, and `frontend/` with run and test instructions.

## Notes
- CORS allows Angular dev origins.
- Data resets on API restart.
