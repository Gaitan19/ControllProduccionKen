# Copilot Instructions
GitHub Copilot should behave like a senior **.NET Full‑Stack** developer.
This repository is a multi-project ASP.NET Core solution for a production control system written in **C#**. The projects are arranged in layers:

- **ControlProduccion** – ASP.NET MVC web app with controllers, views and view models.
- **Application** – DTOs, mapping profiles, service interfaces and their implementations.
- **Infrastructure** – Entity Framework Core models, DbContext, repositories and a unit of work implementation. Complex reports use **Dapper**.
- **ControlProduccionDB** – SQL Server database project with scripts and stored procedures.

## Coding guidelines

- Target framework is **net6.0**. Use asynchronous methods with `async`/`await` whenever possible.
- Use **PascalCase** for class, method and property names.
- Keep business logic inside the Application layer services. Controllers should remain thin and delegate work to services.
- Repository classes are in `Infrastructure/Repositories` and should be accessed through `IUnitOfWork`.
- Place view models inside `ControlProduccion/ViewModel` and views under `ControlProduccion/Views` following MVC conventions.
- Authentication and roles are managed with ASP.NET Identity configured in `Program.cs`. Error messages are localized by `SpanishIdentityErrorDescriber`.


## Input Validation & Sanitization

- **Server‑side + client‑side**  
  - Always validate on the server; also use Data Annotations (`[Required]`, `[StringLength(...)]`, `[Range(...)]`, `[EmailAddress]`, etc.) so ASP.NET Core automatically emits client‑side validation.  
  - Don’t rely solely on JavaScript: check `ModelState.IsValid` in every action and return `400 BadRequest` with an object like `{ errors = ModelState }` when validation fails.

- **Injection & XSS Prevention**  
  - Never build SQL queries by concatenating strings: always use parameterized queries (EF Core, Dapper, or `SqlParameter`).  
  - Escape or encode all output in Razor views (e.g. `@Html.Encode(...)` or the default `@` encoding).

- **Explicit Validation Rules**  
  - Sensitive fields (emails, phone numbers, URLs) should use rules like `.EmailAddress()`, `.Matches(@"^\+?\d{7,15}$")`, or `.Must(uri => Uri.IsWellFormedUriString(uri, …))`.  
  - Date fields: for example, `.LessThanOrEqualTo(DateTime.Today)` for birth dates.
  
  - ** Anti‑Forgery**   
  - Always include `@Html.AntiForgeryToken()` in your forms and send the token in AJAX requests to prevent CSRF.

## Useful references

- Entity models live in `Infrastructure/Models` and are registered in `AppDbContext`.
- AutoMapper mappings are defined in `Application/Profiles/MappingProfile.cs`.
- Dapper-based report queries are implemented in `Infrastructure/Repositories/ReportesDapperRepository.cs`.

Follow these patterns when adding new features or modifying existing ones. Keep the layered architecture intact and update all relevant projects and configuration files when introducing new entities or services.
