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

## HOW-TO (Project Setup Steps + Commands)
1. Initialize repo and Angular app:
   ```bash
   git init
   npm i -g @angular/cli@latest --yes
   ng new todo-app --routing --style=scss --standalone true --skip-git --directory "frontend" --strict true --ssr false
   ```
2. Create Web API project (framework now net9.0) and add files following `dotnet new webapi --use-controllers` conventions (under `backend/src/TodoApi`).
3. Add Swagger and CORS in `Program.cs`:
   - `builder.Services.AddEndpointsApiExplorer();`
   - `builder.Services.AddSwaggerGen(...)`
   - `app.UseSwagger(); app.UseSwaggerUI();` (Development only)
   - CORS policy `AllowFrontend` for `http://localhost:4200`
4. Implement in-memory repository and controller.
5. Add backend tests (xUnit) under `backend/tests/TodoApi.Tests`.
6. Implement Angular `TodoService` and `TodosComponent`; provide `HttpClient` and route root to component.
7. Restructure backend into `backend/src` and `backend/tests`.
8. Add READMEs for root, backend, and frontend.
9. Commit changes:
   ```bash
   git add -A
   git commit -m "feat: Angular + .NET TODO app with in-memory API, tests, and README"
   git commit -m "chore(backend): move API to backend/src and tests to backend/tests; add READMEs and update solution"
   ```

## Notes
- Swagger is available at `http://localhost:5000/swagger` in Development.
- CORS allows Angular dev origins.
- Data resets on API restart.
