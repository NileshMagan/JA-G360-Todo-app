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

## HOW-TO

### Frontend (Angular)
1. Install Angular CLI (optional):
   ```bash
   npm i -g @angular/cli@latest --yes
   ```
2. Scaffold app (done for you):
   ```bash
   ng new todo-app --routing --style=scss --standalone true --skip-git --directory "frontend" --strict true --ssr false
   ```
3. Add Material and animations (already integrated):
   - Dependencies: `@angular/material`, animations provided in `app.config.ts` via `provideAnimations()`
   - Theme configured in `src/styles.scss`
4. Dev server:
   ```bash
   cd frontend
   npm install
   npm start
   ```

### Backend (ASP.NET Core)
1. Create solution and projects (shown here for a fresh setup):
   ```bash
   dotnet new sln -n TodoApp
   dotnet new webapi -n TodoApi -f net9.0 --use-controllers
   dotnet new xunit -n TodoApi.Tests -f net9.0
   ```
2. Move to structured layout (optional but recommended):
   ```bash
   mkdir -p backend/src backend/tests
   mv TodoApi backend/src/TodoApi
   mv TodoApi.Tests backend/tests/TodoApi.Tests
   ```
3. Add to solution and reference tests to API:
   ```bash
   dotnet sln TodoApp.sln add backend/src/TodoApi/TodoApi.csproj backend/tests/TodoApi.Tests/TodoApi.Tests.csproj
   dotnet add backend/tests/TodoApi.Tests/TodoApi.Tests.csproj reference backend/src/TodoApi/TodoApi.csproj
   ```
4. Enable Swagger, caching, pagination, and CORS in `Program.cs` and controllers (done).
5. Set dev URL via launch settings (port 5000): `backend/src/TodoApi/Properties/launchSettings.json`
6. Restore and run:
   ```bash
   cd backend/src/TodoApi
   dotnet restore
   dotnet run
   ```
7. Run tests:
   ```bash
   cd ../../tests/TodoApi.Tests
   dotnet restore
   dotnet test
   ```

## SEO
- Semantic HTML structure and heading (`h1`) present.
- Mobile-friendly viewport meta tag.
- Fast, client-side rendering with lean bundle; Material icons loaded via CDN.
- Caching: API uses response caching on list endpoint, improving repeat load times.
- Pagination prevents overfetching, improving performance metrics.
- Descriptive page title set in `index.html`.
- Optional SSR/Prerender could be added (Angular SSR) for further SEO; omitted for brevity.

## Notes
- Swagger is available at `http://localhost:5000/swagger` in Development.
- CORS allows Angular dev origins.
- Data resets on API restart.
