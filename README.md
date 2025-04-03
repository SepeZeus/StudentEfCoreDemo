# CLEAN Architecture and CQRS with ASP.NET Core 8 and EF Core 8

This project is an ASP.NET Core Web API demonstrating the implementation of Clean Architecture principles for managing sports Players and Teams. It utilizes Entity Framework Core for data persistence, MediatR for implementing the CQRS pattern, and Xunit for testing.

## Overview

The API provides endpoints for performing CRUD (Create, Read, Update, Delete) operations on Player and Team resources. It follows the Clean Architecture pattern, separating concerns into distinct layers: Domain, Application, Infrastructure, and Presentation (API).

## Features

* **Player Management:**
    * Create new players.
    * Retrieve a list of all players.
    * Retrieve a specific player by ID.
    * Update existing player details.
    * Delete players.
* **Team Management:**
    * Create new teams with details like name, sport type, founded date, stadium, and max roster size.
    * Retrieve a list of all teams.
    * Retrieve a specific team by ID (including its players).
    * Update existing team details.
    * Delete teams.
* **Business Logic:**
    * Validates that a player cannot be created or assigned to a team if the team's maximum roster size (`MaxRosterSize`) would be exceeded.

## Technology Stack

* **.NET 8.0**
* **ASP.NET Core 8:** For building the Web API.
* **Entity Framework Core 8:** ORM for data access.
    * `Microsoft.EntityFrameworkCore`
    * `Microsoft.EntityFrameworkCore.SqlServer`: SQL Server provider.
    * `Microsoft.EntityFrameworkCore.Tools`: For EF Core migrations.
* **SQL Server:** Relational database (can be configured for others via EF Core providers).
* **MediatR:** For implementing CQRS pattern (Commands, Queries, Handlers).
* **Swashbuckle:** For API documentation and Swagger UI.
    * `Swashbuckle.AspNetCore`
    * `Swashbuckle.AspNetCore.Swagger`
    * `Swashbuckle.AspNetCore.SwaggerGen`
    * `Swashbuckle.AspNetCore.SwaggerUI`
* **Xunit:** Testing framework.
* **Moq / Moq.EntityFrameworkCore:** For mocking dependencies in unit tests.

## Architecture

This project adheres to the **Clean Architecture** principles:

1.  **Domain Layer (`StudentEfCoreDemo.Domain`):** Contains core business entities (`Player.cs`, `Team.cs`) and domain logic. Has no dependencies on other layers.
2.  **Application Layer (`StudentEfCoreDemo.Application`):** Contains application-specific logic.
    * Defines interfaces for repositories (`IPlayerRepository.cs`, `ITeamRepository.cs`).
    * Includes DTOs (`PlayerDto.cs`, `TeamDto.cs`, `CreateTeamDto.cs`) for data transfer.
    * Implements use cases using MediatR Commands (`CreatePlayerCommand.cs`, `UpdateTeamCommand.cs`, etc.) and Queries (`GetPlayerByIdQuery.cs`, `GetTeamsQuery.cs`, etc.) along with their Handlers.
    * Depends only on the Domain layer.
3.  **Infrastructure Layer (`StudentEfCoreDemo.Infrastructure`):** Handles external concerns like data access and services.
    * Provides concrete implementations of repositories (`PlayerRepository.cs`, `TeamRepository.cs`) using Entity Framework Core.
    * Contains the EF Core DbContext (`StudentContext.cs`).
    * Depends on the Application layer (to implement its interfaces).
4.  **Presentation Layer (`StudentEfCoreDemo.API`):** The entry point of the application.
    * ASP.NET Core Web API project (`Program.cs`).
    * Contains API Controllers (`PlayersController.cs`, `TeamsController.cs`) which orchestrate requests by sending Commands/Queries via MediatR.
    * Depends on the Application layer.

**Dependency Rule:** Dependencies flow inwards (Presentation -> Application -> Domain, Infrastructure -> Application).

## Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* A SQL Server instance (e.g., SQL Server Express, SQL Server Developer Edition, Docker container, Azure SQL Database).
* [Optional] EF Core Tools (Install globally via `dotnet tool install --global dotnet-ef` or use project-local tools).

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/SepeZeus/StudentEfCoreDemo.git
    cd StudentEfCoreDemo
    ```
2.  **Configure Database Connection:**
    * Open `src/StudentEfCoreDemo.API/appsettings.json` (or `appsettings.Development.json`).
    * Update the `ConnectionStrings` section with your SQL Server connection details
3.  **Apply EF Core Migrations:**
    * Run the database update command (this creates the database and schema if they don't exist):
        ```bash
        dotnet ef database update --project StudentEfCoreDemo.Infrastructure --startup-project StudentEfCoreDemo.API
        ```

### Running the Application

1.  **Navigate to the API project directory:**
    ```bash
    cd src/StudentEfCoreDemo.API
    ```
2.  **Run the application:**
    ```bash
    dotnet run
    ```
3.  **Access the API:**
    * The API will typically be available at `https://localhost:7xx /` or `http://localhost:5xx /` (check the console output for the exact URLs). (Notably, it should auto open the Swagger UI in your default web browser.)
    * Access the Swagger UI for interactive documentation and testing at `/swagger`.

4.  **Test the API:**
    * Create a team through the Teams post endpoint (make sure to set the MaxRosterSize to > 0).
    * Cerate a player through the Players post endpoint (make sure to set the TeamId to a valid team).

## Running Tests

1.  **Navigate to the solution root directory or the test project directory.**
2.  **Run the tests using the .NET CLI:**
    ```bash
    dotnet test
    ```
    This command will discover and execute all Xunit tests across the solution.

## API Endpoints

The API exposes endpoints under the `/api` route:

* `/api/players`: Endpoints for Player CRUD operations.
* `/api/teams`: Endpoints for Team CRUD operations.

Refer to the **Swagger UI** (accessible at `/swagger` when the application is running) for detailed information about each endpoint, including HTTP methods, request/response models, and parameters.