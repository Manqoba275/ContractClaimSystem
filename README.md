# Contract Claim System

An ASP.NET Core MVC coursework prototype for viewing monthly lecturer claims through lecturer, programme coordinator, and academic manager dashboards. It uses Entity Framework Core 8 and SQL Server.

## Current status

The repository includes claim models, database seed records, dashboard views, and read-only dashboard queries. It is **not a complete claim submission or approval system**:

- Views link or submit to `ClaimsController` actions, but that controller is absent. Claim submission, document handling, and status changes need implementation.
- Dashboard role selection is not authentication or role-based access control.
- Several manager report actions display placeholder alerts, and claim details include placeholder content.
- Database query failures are logged and can produce an empty dashboard; an empty table does not establish that the database connection works.

Use synthetic data while developing this prototype.

## Requirements

- A .NET SDK capable of targeting .NET 8, plus the ASP.NET Core 8 runtime to run the application.
- SQL Server. The checked-in development connection uses Windows SQL Server LocalDB: `(localdb)\mssqllocaldb`.
- Access to NuGet for the first package restore.

## Run locally

From the repository root:

```powershell
dotnet restore ContractClaimSystem.sln
dotnet build ContractClaimSystem.sln --no-restore
dotnet run --project ContractClaimSystem/ContractClaimSystem.csproj --launch-profile http
```

Open `http://localhost:5056`. The `http` launch profile selects the Development environment. For local HTTPS, configure a trusted development certificate with `dotnet dev-certs https --trust` and use `--launch-profile https` instead; its HTTPS address is `https://localhost:7012`.

If LocalDB is unavailable, override the connection for the current PowerShell session before running:

```powershell
$env:ConnectionStrings__DefaultConnection = 'Server=YOUR_SERVER;Database=ContractClaimSystem;Integrated Security=true;Encrypt=true'
```

Use connection settings appropriate to your own SQL Server and its certificate. Do not commit database credentials.

At startup, `Program.cs` calls `EnsureCreated()`. A new database receives four example claims defined in `Data/AppDbContext.cs`. This is not a migration workflow for updating an existing database schema. Connection failures are logged; inspect the terminal when dashboards show no claims.

## Project layout

| Path | Purpose |
| --- | --- |
| `ContractClaimSystem/Program.cs` | MVC, SQL Server, session, routing, and database startup configuration |
| `ContractClaimSystem/Data/AppDbContext.cs` | Claim mapping and seed data |
| `ContractClaimSystem/Models/Claim.cs` | Claim fields and status enum |
| `ContractClaimSystem/Controllers/HomeController.cs` | Dashboard queries |
| `ContractClaimSystem/Views/Home/` | Role selection and dashboard pages |
| `ContractClaimSystem/wwwroot/` | Static styles, scripts, and vendor libraries |

## Verification and next steps

`dotnet build ContractClaimSystem.sln` verifies compilation. There is no automated test project in this repository. A successful build does not verify SQL Server connectivity or the incomplete claim actions.

For a manual smoke check, start with an accessible development database, open each dashboard, and verify the four seeded claims on a newly created database. Review terminal logs for database errors.

Before treating this as a complete application:

1. Implement and test the missing claim actions, including validation and document handling.
2. Add authentication and enforce role permissions on the server.
3. Test allowed claim-status transitions and rejected requests.
4. Replace placeholder reports and details with database-backed results.
5. Introduce database migrations and integration tests.
