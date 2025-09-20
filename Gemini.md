## Gemini Project Initialization

This file outlines the project structure and conventions for the e_learning project.

### Project Structure

*   **e_learning**: The main ASP.NET MVC project.
    *   `Controllers`: Contains the application's controllers.
    *   `Views`: Contains the application's views.
    *   `Models`: Contains the application's models.
    *   `App_Start`: Contains configuration files for bundling, filters, and routing.
    *   `Content`: Contains CSS and other static content.
    *   `Scripts`: Contains JavaScript files.
*   **Data_Oracle**: A separate project for data access.
    *   `Context`: Contains the Entity Framework `DbContext`.
    *   `Entities`: Contains the database entities.
    *   `Repositories`: Contains the data repositories.
*   **packages**: Contains the NuGet packages.

### Conventions

*   **Language**: C#
*   **Framework**: ASP.NET MVC
*   **Data Access**: Entity Framework with Oracle
*   **Package Management**: NuGet
*   **Styling**: Bootstrap

### Notes

*   The project is set up to use an Oracle database.
*   The `Data_Oracle` project is responsible for all database interactions.
*   The `e_learning` project is responsible for the application's UI and business logic.
