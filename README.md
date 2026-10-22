<<<<<<< HEAD
# PetCare Hub — Smart Pet Care and Wellness Management System

A student-friendly full-stack ASP.NET Core MVC project using C#, Entity Framework Core, SQL Server, HTML, CSS, Bootstrap and JavaScript.

## Features
- Register, login and logout (ASP.NET Core Identity)
- User-specific pet profile CRUD (create, view, edit, delete)
- Appointment request creation and list
- Vaccination records with due-date status
- Responsive dashboard with overall record counts
- SQL Server database through Entity Framework Core

## Requirements
- Visual Studio 2022 with **ASP.NET and web development** workload
- .NET 8 SDK
- SQL Server LocalDB (normally installed with Visual Studio)

## Run on Windows
1. Extract the ZIP.
2. Open `PetCareHub.csproj` in Visual Studio.
3. Open **Tools > NuGet Package Manager > Package Manager Console**.
4. Run:
   ```powershell
   Update-Database
   ```
   If EF commands are not available, run `dotnet tool install --global dotnet-ef` in a terminal, restart the terminal, then use the CLI steps below.
5. Press **F5** or **Ctrl+F5**.

### Alternative: terminal
Open PowerShell in the folder containing `PetCareHub.csproj`:
```powershell
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
If the tool is already installed, skip the install command. Open the localhost URL printed in the terminal.

## Database connection
The default connection is in `appsettings.json`:
`Server=(localdb)\\MSSQLLocalDB;Database=PetCareHubDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True`

For a different SQL Server instance, change `DefaultConnection` to your server name.

## Demo flow
1. Register an account.
2. Add a pet under **My Pets**.
3. Submit an appointment request.
4. Add a vaccination record and show the due-date status.
5. Log out and register another account to demonstrate that each user only sees their own pets and records.

## Notes / project scope
- Appointment requests are saved with `Pending` status. This starter project does not connect to a real veterinary clinic or send email/SMS reminders.
- The dashboard counts are system-wide totals for demonstration; personal pet and medical records are filtered by the logged-in user.
- There is no separate admin dashboard/role seeded in this starter version.
- Do not use this student demo as a medical diagnostic tool.
=======
# PetCare-Hub
Smart Pet Care and Wellness Management System using ASP.NET Core and SQLite
>>>>>>> 157bcc6e0570e72838b67c84ed1244206b8c5214
