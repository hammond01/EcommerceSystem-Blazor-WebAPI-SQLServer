## EMS application with ASP.NET Core

It showcases:

- Blazor WebAssembly
- Minimal APIs
- Using EntityFramework and SQL Server for data access
- OpenAPI
- User management with ASP.NET Core Identity
- Cookie authentication
- JWT authentication
- REST API

#### SETUP Database API

# Northwind and pubs sample databases for Microsoft SQL Server

[DownloadLink](https://github.com/microsoft/sql-server-samples/blob/master/samples/databases/northwind-pubs/instnwnd.sql)

This folder contains scripts to create and load the _Northwind_ (`instnwnd.sql`) and _pubs_ (`instpubs.sql`) sample databases.

These scripts were originally created for SQL Server 2000.

## Before you begin

To run this sample, you need a tool that can run Transact-SQL scripts. You can run the scripts in the following tools:

- **SQL Server Management Studio (SSMS)**. To download SSMS, go to [Download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017).

- **SQL Server Data Tools (SSDT)** or Visual Studio. To download SSDT, or to enable its features in Visual Studio, go to [Download and install SQL Server Data Tools (SSDT) for Visual Studio](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-2017).

## Run the scripts in SSMS

1. Open SSMS.
2. Connect to the target SQL Server.
3. Open the script in a new query window.
4. Run the script.

## Run the scripts in SSDT or Visual Studio

1. Open SSDT or Visual Studio.
2. Open the SQL Server Object Explorer.
3. Connect to the target SQL Server.
4. Open the script in a new query window.
5. Run the script.

## Related links

- [Wide World Importers sample database](https://github.com/Microsoft/sql-server-samples/releases/tag/wide-world-importers-v1.0)

- [AdventureWorks sample databases](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks)

#### SETUP Database Authentication

1. Install the **dotnet-ef** tool: `dotnet tool install dotnet-ef -g`
1. Navigate to the `AuthenticationAPI` folder.
   Run `dotnet ef database update` to create the database.
1. Learn more about [dotnet-ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

#### Config and start project

## System requirements

1. Dotnet 8.0
   [Link download]: (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Config

1. - \*\*Edit appsettings.json file of you
   # In Authentication API

```json
{
  "ConnectionStrings": {
    "SQLServer": "Data Source=(local);Initial Catalog=TS_Auth;User Id=sa;Password=123456; Trustservercertificate=True;"
  }
}
```

    # In Server API

```
    "SQL": "Data Source=(local);Initial Catalog=MyDB;User Id=sa;Password=123456;trustservercertificate=True;",
```

2. - **Visual Studio** - Setup multiple startup projects by right clicking on the solution and selecting Properties. Select `AuthenticationAPI`, `Server` and `Client` as startup projects.
     <img width="591" alt="image" src="https://res.cloudinary.com/dabdclkhv/image/upload/v1706493772/Image/Authentication_su7wjb.png">

3. -**Terminal/CLI** - Open up 2 terminal windows, one in [AuthenticationAPI](EcommerceSystem\TS_ES_EcommerceSystem\AuthenticationAPI),[Server](EcommerceSystem\TS_ES_EcommerceSystem\Server) and the other in [Client](EcommerceSystem\TS_ES_EcommerceSystem\Client) run:
   ```
   dotnet watch run -lp https
   ```
   This will run both applications with the `https` profile.
4. -**Type** - Install the global tool using the following command:
   ```
   dotnet tool install --global Microsoft.Tye --version 0.11.0-alpha.22111.1
   ```
   Run `tye run` in the repository root and navigate to the tye dashboard (usually http://localhost:8000) to see both applications running.

## Optional

### Using the API standalone

The Todo REST API can run standalone as well. You can run the [Server](Server) project and make requests to various endpoints using the Swagger UI (or a client of your choice):
<img width="1200" alt="image" src="https://res.cloudinary.com/dabdclkhv/image/upload/v1706494295/Image/Server_oqhlqt.png">
<img width="1200" alt="image" src="https://res.cloudinary.com/dabdclkhv/image/upload/v1706494408/Image/Authen_v8u8dt.png">
