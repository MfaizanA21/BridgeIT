# .NET Web API – Local Development Setup

This repository contains a .NET Web API project. Follow the steps below to set up and run the project locally for development.

## Prerequisites
Ensure the following tools are installed on your machine:

- .NET 8 SDK
- SQL Server (SQL Server Express)
- SQL Server Management Studio (SSMS) – recommended


## Database Setup (SQL Server)

### 1. Install SQL Server
Install SQL Server locally. SQL Server Express is sufficient for local development.

### 2. Enable TCP/IP
TCP/IP must be enabled for SQL Server.

Steps:
1. Open **SQL Server Configuration Manager**
2. Go to  
   `SQL Server Network Configuration → Protocols for SQLEXPRESS`
3. Enable **TCP/IP**
4. Restart the SQL Server service


## Application Configuration

### 1. Create `appsettings.Development.json`

In the root directory (../BridgeITAPIs/BridgeITAPIs) of the project, create the following file:
`appsettings.Development.json`

### 2. Configure Connection String

Add the following configuration and update it if required to match your local SQL Server setup:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=BridgeITDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
### 3. Database Migration

This project uses Entity Framework Core for database management.

Run the following command from the project directory:

`dotnet ef migrations add InitialCreate`

`dotnet ef database update`

This command will:

- Create the database if it does not exist
- Automatically create all required tables

Ensure the correct startup project is selected if running this command from an IDE.

## Running the Application
Start the application using one of the following methods:
- `dotnet build`
- `dotnet watch run`

Or run the project directly from your IDE (Visual Studio, Rider, or VS Code).

### Notes

- Containerization of the database setup and automatic population of some tables for easier development is planned for a future phase.
- Until then, please follow the steps above for local development.

### Happy coding:)
