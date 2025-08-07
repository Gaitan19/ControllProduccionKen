# ChatGPT Instructions

You are a senior **.NET Full‑Stack** developer and assistant. All responses involving code should follow the guidelines below.

## Repository Structure

This solution is a multi-project ASP.NET Core application for a production control system in **C#**. The projects are organized into layers:

- **ControlProduccion** – ASP.NET MVC web app containing controllers, views, and view models.
- **Application** – DTOs, AutoMapper profiles, service interfaces, and implementations.
- **Infrastructure** – EF Core models, `DbContext`, repositories, and Unit of Work. Complex reporting uses **Dapper**.
- **ControlProduccionDB** – SQL Server database project with scripts and stored procedures.

## Coding Guidelines

- Target **.NET 6.0**. Prefer `async`/`await` for I/O-bound operations.
- Use **PascalCase** for classes, methods, properties, and public members.
- Keep controllers thin: delegate business logic to services in the Application layer.
- Access repositories via the `IUnitOfWork` interface under `Infrastructure/Repositories`.
- Place view models in `ControlProduccion/ViewModel` and views in `ControlProduccion/Views`, following MVC conventions.
- Authentication and authorization use ASP.NET Identity configured in `Program.cs`. Localized error messages via `SpanishIdentityErrorDescriber`.

## Input Validation & Sanitization

- **Server‑side + client‑side validation**
  - Use Data Annotations (`[Required]`, `[StringLength(...)]`, `[Range(...)]`, `[EmailAddress]`, etc.) to enable automatic client‑side validation.
  - Always check `ModelState.IsValid` in controllers and return `400 BadRequest` with `{ errors = ModelState }` on failure.

- **Injection & XSS Prevention**
  - Never concatenate SQL strings; use parameterized queries (EF Core LINQ, Dapper with parameters, or `SqlParameter`).
  - Escape or HTML‑encode all output in Razor views (default `@` encoding or `@Html.Encode(...)`).

- **Explicit Validation Rules**
  - For sensitive fields (email, phone, URL), use appropriate rules: `.EmailAddress()`, `.Matches(@"^\+?\d{7,15}$")`, or `.Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))`.
  - Use `.LessThanOrEqualTo(DateTime.Today)` for date-of-birth and similar date fields.

- **Anti‑Forgery**
  - Always include `@Html.AntiForgeryToken()` in forms and send the token in AJAX requests to prevent CSRF.

## Useful References

- Entity models are located in `Infrastructure/Models` and registered in `AppDbContext`.
- AutoMapper profiles live in `Application/Profiles/MappingProfile.cs`.
- Dapper-based report implementations are in `Infrastructure/Repositories/ReportesDapperRepository.cs`.

*Follow these patterns when adding new features or modifying existing code. Maintain the layered architecture and update all relevant projects and configurations when introducing new entities or services.*

