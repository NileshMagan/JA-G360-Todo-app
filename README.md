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

## HOW-TO (Project Setup Steps + Commands)
1. Initialize repo and Angular app:
   ```bash
   git init
   npm i -g @angular/cli@latest --yes
   ng new todo-app --routing --style=scss --standalone true --skip-git --directory "frontend" --strict true --ssr false
   ```
2. Create .NET 8 Web API (local SDK not available in this env, files added manually following `dotnet new webapi --use-controllers` conventions). Project files are under `backend/src/TodoApi`.
3. Add Swagger and CORS in `Program.cs`:
   - `builder.Services.AddEndpointsApiExplorer();`
   - `builder.Services.AddSwaggerGen(...)`
   - `app.UseSwagger(); app.UseSwaggerUI();` (Development only)
   - CORS policy `AllowFrontend` for `http://localhost:4200`
4. Implement in-memory repository and controller:
   - `InMemoryTodoRepository` implementing `ITodoRepository`
   - `TodosController` with `GET`, `POST`, `DELETE`
5. Add backend tests (xUnit): `backend/tests/TodoApi.Tests`
   - `InMemoryTodoRepositoryTests`
   - `TodosControllerTests`
6. Angular frontend wiring:
   - `TodoService` using `HttpClient` to `http://localhost:5000/api/todos`
   - `TodosComponent` with add/delete and list rendering
   - Provide `HttpClient` in `app.config.ts`
   - Route `''` to `TodosComponent`
7. Restructure backend:
   ```bash
   # Move API and tests
   # (performed via file operations; final structure below)
   backend/src/TodoApi
   backend/tests/TodoApi.Tests
   ```
8. Create READMEs for root, backend, and frontend.
9. Commit changes:
   ```bash
   git add -A
   git commit -m "feat: Angular + .NET 8 TODO app with in-memory API, tests, and README"
   git commit -m "chore(backend): move API to backend/src and tests to backend/tests; add READMEs and update solution"
   ```

## Notes
- Swagger is available at `http://localhost:5000/swagger` in Development.
- CORS allows Angular dev origins.
- Data resets on API restart.
