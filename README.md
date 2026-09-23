# Sales Dashboard

<p align="center">
  <strong>A clear and interactive view of sales performance.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 8">
  <img src="https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core MVC">
  <img src="https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server">
  <img src="https://img.shields.io/badge/Chart.js-Interactive%20Charts-FF6384?style=for-the-badge&logo=chartdotjs&logoColor=white" alt="Chart.js">
</p>

## About

This repository contains the **visualization and reporting layer** of a complete Business Intelligence project focused on sales analysis using the **WideWorldImporters** database.

As part of the broader project, the relevant source tables were selected, prepared through ETL workflows, and loaded into a data warehouse. Analytical stored procedures were then developed to produce the metrics consumed by the dashboards.

This web application, built with **ASP.NET Core MVC**, represents the final layer of the BI pipeline: it queries SQL Server and turns analytical results into clear, interactive visualizations.

The goal is to provide a simple, responsive and practical interface for quickly monitoring sales trends and supporting business decisions.

## Repository Scope

The data warehouse design, ETL workflows and initial data preparation belong to the broader BI project and are not included in this repository. This repository focuses on:

- connecting to SQL Server and the data warehouse;
- calling analytical stored procedures;
- preparing data for presentation;
- building dashboards with ASP.NET Core MVC, Razor and Chart.js;
- providing responsive navigation and indicator presentation.

> **In summary:** the broader BI project covers the complete journey from source data to analysis, while this repository focuses primarily on the application visualization layer.

## Preview

<p align="center">
  <img src="docs/screenshots/dash1.jfif" alt="Sales Dashboard main view" width="48%">
  <img src="docs/screenshots/dash2.jfif" alt="Sales Dashboard indicators and charts" width="48%">
</p>

<p align="center">
  <img src="docs/screenshots/diag.jfif" alt="Sales dashboard data analysis diagram" width="70%">
</p>

## Features

| Area | Available indicators |
| --- | --- |
| Performance | Monthly revenue and quantity sold per month |
| Customers | Top 5 customers by revenue |
| Products | Top 5 products by quantity sold |
| Geography | Sales by country |
| Sales team | Revenue by salesperson |
| Catalog | Revenue by product category |
| Experience | Interactive charts, responsive navigation and Bootstrap interface |

Charts are generated with Chart.js and powered by SQL Server stored procedures, keeping presentation, application logic and data access clearly separated.

## Technology Stack

- **Language and framework:** C#, ASP.NET Core MVC, .NET 8
- **Data:** Microsoft SQL Server, Microsoft.Data.SqlClient
- **Interface:** Razor Views, HTML, CSS, JavaScript
- **UI components:** Bootstrap 5, Bootstrap Icons
- **Visualization:** Chart.js and chartjs-plugin-datalabels
- **Recommended environment:** Visual Studio or Visual Studio Code

## Prerequisites

- .NET 8 SDK
- SQL Server or SQL Server Express
- A `Sales` database
- The stored procedures listed in the [SQL Configuration](#sql-configuration) section

Check the installed SDK version:

```bash
dotnet --version
```

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/USERNAME/dashbordSales.git
   cd dashbordSales
   ```

2. Restore the dependencies:

   ```bash
   dotnet restore
   ```

3. Copy `appsettings.example.json` to `appsettings.Development.json`.

4. Replace `YOUR_SQL_SERVER` with your local or remote SQL Server name. Never commit this development configuration file.

5. Make sure the stored procedures listed in the [SQL Configuration](#sql-configuration) section exist in the `Sales` database.

## Usage

Run the application with:

```bash
dotnet run
```

Then open the URL displayed in the terminal, usually `https://localhost:xxxx` or `http://localhost:xxxx`.

The main dashboard is available at `/Dashboard/Index`.

## SQL Configuration

The dashboard uses the following stored procedures in the `Sales` database:

- `GetTotalSalesPerMonth`
- `GetTotalSalesQuantityPerMonth`
- `GetTop5CustomersBySales`
- `GetTop5ProductsByQuantitySold`
- `GetSalesByCountry`
- `GetSalesBySalesperson`
- `GetSalesByProductCategory`

The connection string is defined locally in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DwhConnection": "Server=YOUR_SQL_SERVER;Database=Sales;Trusted_Connection=True;Encrypt=False;"
  }
}
```

Replace `YOUR_SQL_SERVER` only in your local file. Never publish a real connection string to GitHub.

The complete SQL script is available at [`docs/sql/analytical-procedures.sql`](docs/sql/analytical-procedures.sql). It contains the seven analytical stored procedures, the supporting nonclustered indexes and an example execution command.

## Project Structure

```text
dashbordSales/
├── Controllers/              # MVC controllers and dashboard data access
├── Models/                   # Indicator models and ViewModels
├── Views/                    # Razor views and application layout
├── Pages/                    # Razor pages generated by the ASP.NET template
├── wwwroot/                  # CSS, JavaScript and static libraries
├── docs/                     # Documentation and screenshots
│   └── sql/                  # Analytical stored procedures and indexes
├── Program.cs                # Application entry point and HTTP configuration
├── appsettings.json          # Shared configuration
├── appsettings.example.json  # Configuration template without sensitive data
└── dashbordSales.sln         # Visual Studio solution
```

The root-level `Controllers/`, `Models/` and `Views/` folders follow standard ASP.NET Core MVC conventions. The `docs/` folder contains additional documentation without unnecessarily changing the existing project structure.

## Screenshots

Application screenshots are stored in `docs/screenshots/` and displayed in the [Preview](#preview) gallery.

## Testing and Quality

The solution does not currently include an automated test project.

To verify that the project builds successfully:

```bash
dotnet build
```

## Security and Configuration

- Development configuration files are ignored by Git.
- Passwords, API keys, real connection strings and secrets must never be written to the repository.
- For production, use environment variables, User Secrets or a suitable secrets manager.
- Before each publication, check `git status` and scan tracked files for secrets.

## Author

Project created by **MARIEM** as part of the SID Master's program.

## License

Distributed under the MIT License. See [LICENSE](LICENSE).
