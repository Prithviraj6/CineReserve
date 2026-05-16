# Architecture Guide — Clean Architecture (.NET)

## Overview

This project uses **Clean Architecture** with 4 layers. Each layer has strict rules about what it can reference.

```
┌─────────────────────────────────────┐
│            MyApp.API                │  ← Entry point, controllers, middleware
├─────────────────────────────────────┤
│        MyApp.Infrastructure         │  ← EF Core, repos, services (implementations)
├─────────────────────────────────────┤
│         MyApp.Application           │  ← Interfaces, DTOs, validators, mappings
├─────────────────────────────────────┤
│           MyApp.Domain              │  ← Entities, enums, base classes (pure C#)
└─────────────────────────────────────┘
```

## Dependency Rule

> Inner layers NEVER reference outer layers.

| Layer | Can reference |
|---|---|
| Domain | Nothing |
| Application | Domain only |
| Infrastructure | Application + Domain |
| API | Infrastructure + Application |

---

## Layer Responsibilities

### Domain (`MyApp.Domain`)
- `BaseEntity.cs` — Id, CreatedAt, UpdatedAt, IsDeleted
- Domain entities (plain C# classes, no EF or framework dependencies)
- Enums

**Rule:** Zero NuGet packages. Pure .NET only.

---

### Application (`MyApp.Application`)
- **Interfaces** — contracts for repositories and services (e.g. `IUserRepository`, `IAuthService`)
- **DTOs** — data shapes for API input/output (never expose entities directly)
- **Validators** — FluentValidation validators for every request DTO
- **Mappings** — AutoMapper profiles (entity ↔ DTO)
- **Common** — `ApiResponse<T>`, `PaginatedResult<T>`
- **Settings** — strongly-typed config classes (e.g. `JwtSettings`)

**Rule:** No EF, no HTTP, no BCrypt. Only contracts and shapes.

---

### Infrastructure (`MyApp.Infrastructure`)
- **AppDbContext** — EF Core context + entity configurations
- **Repositories** — implementations of repository interfaces
- **Services** — implementations of service interfaces (Auth, JWT, etc.)
- **Seed** — DataSeeder (no auto-migration)
- **Extensions** — `ServiceCollectionExtensions` (all DI registrations)

**Rule:** All EF, BCrypt, JWT implementation lives here.

---

### API (`MyApp.API`)
- **Controllers** — thin, just call service, return `ApiResponse<T>`
- **Middleware** — `ExceptionHandlingMiddleware`
- **Program.cs** — app setup and pipeline

**Rule:** No business logic in controllers. No direct DB access.

---

## Key Patterns

### Soft Delete
Never delete records. Set `IsDeleted = true`.
EF global query filters automatically hide soft-deleted records.

```csharp
modelBuilder.Entity<MyEntity>().HasQueryFilter(e => !e.IsDeleted);
```

### Generic Repository
All repos extend `GenericRepository<T>` which provides:
`GetByIdAsync`, `GetAllAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`, `SaveChangesAsync`

Custom queries go in specific repository implementations.

### Consistent API Response
Every endpoint returns:
```json
{
  "success": true,
  "message": "...",
  "data": { ... },
  "statusCode": 200
}
```

### JWT Authentication
- Access token only (no refresh tokens)
- Claims: UserId, Email, FullName, Role
- `ICurrentUserService` extracts identity from `HttpContext`

### Exception → HTTP Mapping
`ExceptionHandlingMiddleware` maps:
- `KeyNotFoundException` → 404
- `UnauthorizedAccessException` → 403
- `InvalidOperationException` → 400
- `ArgumentException` → 400
- Everything else → 500

---

## Project References

```
MyApp.API          → MyApp.Infrastructure, MyApp.Application
MyApp.Infrastructure → MyApp.Application, MyApp.Domain
MyApp.Application  → MyApp.Domain
MyApp.Domain       → (nothing)
MyApp.Tests        → MyApp.Infrastructure, MyApp.Application
```
