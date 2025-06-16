# Student Event Management System API (CCP)

This project is a backend RESTful API designed to manage student events, participant registrations, and feedback submissions. It was developed as part of the Web Engineering Complex Computing Problem (CCP) requirements for the BS (SE) program at FEST.

The API is built using ASP.NET Core 9.0, Entity Framework Core 8.0+, and supports [**CHOOSE ONE AND DELETE THE OTHER LINE: SQL Server / MySQL**].

## Key Features Implemented

*   Full CRUD (Create, Read, Update, Delete) operations for Events.
*   Participant registration for events, modeling a many-to-many relationship.
*   Feedback submission (rating and comment) for events, restricted to after the event date.
*   Event searching by name or venue.
*   Event filtering by date or venue, and sorting capabilities.
*   Input validation and standardized HTTP error handling for API responses.
*   Swagger UI integration for interactive API documentation and testing.

## Prerequisites

Before you begin, ensure you have met the following requirements:

*   [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   An instance of [**CHOOSE ONE: SQL Server (e.g., Express, LocalDB) / MySQL**] running.
*   An IDE such as Visual Studio 2022 or Visual Studio Code.
*   Git for cloning the repository.

## Setup Instructions

1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/AusafAhmed-afk/REPO-1.git
    cd REPO-1
    # If your project is inside a subfolder within REPO-1, for example 'StudentEventManagementSystem_CCP',
    # then the command would be: cd REPO-1/StudentEventManagementSystem_CCP
    ```

2.  **Configure Database Connection:**
    *   Open the `appsettings.json` file located in the root of the project.
    *   Locate the `ConnectionStrings` section.
    *   Update the `DefaultConnection` string to point to your local [**CHOOSE ONE: SQL Server / MySQL**] instance.

    **Example for SQL Server (LocalDB):**
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentEventDB_CCP_Ausaf;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    }
    ```
    **Example for MySQL:**
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;Database=student_event_db_ccp_ausaf;Uid=your_mysql_user;Pwd=your_mysql_password;"
    }
    ```
    *   Ensure the database name (e.g., `StudentEventDB_CCP_Ausaf`) is one you want to use; EF Core will create it if it doesn't exist. *(I've added your name to the DB name to make it unique if your teacher runs multiple projects).*

3.  **Apply Entity Framework Core Migrations:**
    *   Open a terminal or command prompt in the project's root directory (the one containing the `.csproj` file, e.g., `StudentEventManagementSystem.csproj`).
    *   Run the following command to create/update the database schema based on the migrations:
      ```bash
      dotnet ef database update
      ```

4.  **Run the Application:**
    *   **Using .NET CLI:**
      In the project's root directory, run:
      ```bash
      dotnet run
      ```
    *   **Using Visual Studio:**
      Open the solution file (`.sln`) in Visual Studio and press `F5` or click the "Start" button.

## Accessing the API

Once the application is running:

*   The API will typically be available at the following base URLs (check your `Properties/launchSettings.json` if different):
    *   `https://localhost:7121`
    *   `http://localhost:5253`

*   **Swagger UI (API Documentation & Testing):**
    You can access the interactive Swagger UI documentation by navigating to the following URL in your web browser:
    `https://localhost:7121/swagger`

## Project Structure

The solution follows a layered architecture to ensure separation of concerns:

*   **/Controllers:** Contains API controllers (e.g., `EventsController.cs`) that handle incoming HTTP requests and route them to appropriate services.
*   **/Models (or /Entities):** Houses the EF Core domain entity classes (e.g., `Event.cs`, `Student.cs`) that represent the database tables.
*   **/DTOs:** Includes Data Transfer Objects used to shape data for API requests and responses, aiding in validation and API contract definition.
*   **/Services:** Contains the business logic layer (e.g., `EventService.cs`), orchestrating operations between controllers and data access.
*   **/Data:** Holds the `ApplicationDbContext.cs` for Entity Framework Core configurations and database interaction.
*   **/Migrations:** Stores EF Core generated database migration files detailing schema changes.
*   `Program.cs`: The main entry point for the application, responsible for configuring services and the HTTP request pipeline.
*   `appsettings.json`: Contains application configuration settings, including database connection strings.

---
