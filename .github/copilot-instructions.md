# Copilot / AI agent instructions for this repository

Purpose: quickly bring an AI coding agent up to speed on the codebase layout, conventions, and common developer workflows so suggestions and edits are correct and actionable.

- **Big picture**: this mono-repo contains many small .NET services, console apps, libraries and tests grouped in subfolders (see `code.slnx`). Many projects are lightweight examples of common patterns (WebAPI, console, messaging, EF Core, benchmarks).

- **Shared platform conventions**:
  - Centralized package versions: [Directory.Packages.props](Directory.Packages.props) controls NuGet versions across projects (use existing package IDs; don't change versions without reason).
  - Target framework & defaults: [Directory.Build.props](Directory.Build.props) sets `TargetFramework` to `net9.0`, `ImplicitUsings` and nullable context.
  - Common host/service defaults: the `aspire` helper library exposes `AddServiceDefaults()` and OpenTelemetry/healthcheck wiring in [aspire/aspire.serviceDefaults/Extensions.cs](aspire/aspire.serviceDefaults/Extensions.cs). Many WebAPI projects call `builder.AddServiceDefaults()` early in `Program.cs`.

- **Architecture & patterns to follow when editing/adding code**:
  - Web APIs use minimal hosting (`WebApplication.CreateBuilder`) with `AddOpenApi()`/NSwag and `MapDefaultEndpoints()`; see `chores/chores.webapi/Program.cs` for a canonical example.
  - Authentication: several APIs use Azure AD OAuth2 configuration keys (e.g. `AzureAd:Instance`, `AzureAd:TenantId`, `AzureAd:Audience`, `AzureAd:ApiScope`) — follow the same configuration keys when adding auth-related code.
  - Observability: OpenTelemetry and Serilog are used widely. Middleware often enriches logs/traces (see `CorrelationIdEnrichmentMiddleware.cs` and `EnvEnrichmentMiddleware.cs` in `chores.webapi`). Prefer adding tags/baggage or log scopes for traceable data.
  - Messaging: RabbitMQ/EasyNetQ examples live under `task-practice/*`. They register EasyNetQ via DI and use `RabbitHutch.CreateBus("host=localhost")` in examples — preserve the existing patterns for registration and subscription.
  - MediatR: request/handler and pipeline behavior are used (see `builder.Services.AddMediatR(...)` in several APIs). When adding handlers, register by assembly scanning like existing projects.

- **Build / run / test workflows (examples)**
  - Restore and build the repo root:
    - `dotnet restore` then `dotnet build code.slnx` (or simply `dotnet build` at repo root).
  - Run a WebAPI locally (example):
    - `dotnet run --project chores/chores.webapi`
    - To use the in-memory DB variant used for tests, set `ASPNETCORE_ENVIRONMENT=Testing` (the code checks `IsEnvironment("Testing")` to swap EF provider).
  - Run a console app (example):
    - `dotnet run --project enigma/enigma.console`
  - Run tests: use the specific test project folder to avoid framework mismatch (projects use MSTest, NUnit, and xUnit across the repo):
    - `dotnet test chores/chores.tests/chores.tests.csproj`

- **Files to consult when making changes**
  - Solution manifest: `code.slnx` (top-level grouping of projects)
  - Central settings: `Directory.Packages.props`, `Directory.Build.props`
  - Service defaults & OpenTelemetry: `aspire/aspire.serviceDefaults/Extensions.cs`
  - Typical WebAPI example: `chores/chores.webapi/Program.cs`
  - Middleware examples: `chores/chores.webapi/CorrelationIdEnrichmentMiddleware.cs`, `chores/chores.webapi/EnvEnrichmentMiddleware.cs`
  - Messaging examples: `task-practice/*` (publisher/subscriber/logger)

- **Agent guidance / editing constraints**
  - Keep package versions in sync with `Directory.Packages.props` — prefer adding a PackageReference without changing the central file unless explicitly requested.
  - Preserve logging and observability patterns (OpenTelemetry traces, baggage, and Serilog enrichment) when modifying request/response flows.
  - When editing a project Program.cs, follow the `AddServiceDefaults()` → `AddControllers()` → `AddOpenApi()` pattern for APIs.
  - For database code, respect the `Testing` environment switch which uses an in-memory provider; tests rely on this behavior.
  - Prefer small, focused changes; reference existing examples in the repo rather than introducing new frameworks or radically different conventions.

If any section is unclear or you want additional examples (more code snippets or step-by-step run/debug commands for a specific project), tell me which area to expand and I will iterate. 
