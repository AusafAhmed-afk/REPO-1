# Student Event Management System API (CCP)

This project is a backend RESTful API for managing student events, participant registrations, and feedback, developed as part of the Web Engineering Complex Computing Problem.

## Prerequisites

*   .NET 9.0 SDK (or the version you used)
*   [SQL Server / MySQL - Specify which one and any version info if important]
*   An appropriate IDE like Visual Studio 2022 or VS Code.

## Setup Instructions

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/AusafAhmed-afk/REPO-1.git 
    # Or your new repository link if you chose Option A
    cd REPO-1 # Or into the subfolder if you chose Option B, e.g., cd REPO-1/StudentEventManagementSystemAPI_CCP
    ```

2.  **Configure Database Connection:**
    *   Open the `appsettings.json` file.
    *   Locate the `ConnectionStrings` section.
    *   Update the `DefaultConnection` string to point to your local [SQL Server/MySQL] instance and desired database name. For example:
      ```json
      "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentEventDB_CCP;Trusted_Connection=True;MultipleActiveResultSets=true"
      }
      ```

3.  **Apply Entity Framework Core Migrations:**
    *   Open a terminal or command prompt in the project's root directory (where the `.csproj` file is located).
    *   Run the following command to create/update the database schema:
      ```bash
      dotnet ef database update
      ```

4.  **Run the Application:**
    *   You can run the application using the .NET CLI:
      ```bash
      dotnet run
      ```
    *   Alternatively, open the solution (`.sln` file) in Visual Studio and press F5 or click the "Start" button.

## Accessing the API

*   The API will be listening on (by default):
    *   `https://localhost:7121`
    *   `http://localhost:5253`
*   **Swagger UI (API Documentation & Testing):**
    *   Access Swagger UI by navigating to: `https://localhost:7121/swagger` in your web browser.
