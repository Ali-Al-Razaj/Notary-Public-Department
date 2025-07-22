# Notary Public Department System

This project is a comprehensive solution for managing documents, people records, registration authorities, notaries, and related entities within a Notary Public Department. It includes a backend API built with .NET 8, a layered architecture with Business Logic and Data Access layers, and a SQL Server database with stored procedures.

## Technologies Used

- **Backend**: .NET 8, C#
- **Database**: Microsoft SQL Server
- **API Documentation**: Swagger / Swashbuckle
- **IDE**: Visual Studio / Visual Studio Code
- **Other**: Microsoft.Data.SqlClient, Microsoft.Extensions.* libraries


## Database
- The database schema is defined in the provided SQL script.

- Stored procedures are separated by table for modularity and maintenance.

- ER diagrams are included in the Untitled Diagram.drawio files for reference.

## Data Access Layer
- Responsible for all interactions with the SQL Server database.

- Implements repository patterns to abstract data storage details.

- Uses ADO.NET and Microsoft.Data.SqlClient to execute stored procedures.

- Returns domain entities to the Business Logic Layer.

- Encapsulates all SQL queries and data transaction logic for maintainability and testability.

## Business Logic Layer
- Contains core domain models and business rules.

- Decouples business logic from API and data access.

- Used by both API Layer and ConsoleAppForTesting.

## API Layer
- Implements RESTful endpoints for managing documents, people, registration authorities, and other entities.

- Controllers are organized by entity type.

- Supports standard CRUD operations.


## Testing
- The ConsoleAppForTesting project allows manual testing of business logic and data access layers.

- It references both Business Logic and Data Access layers to simulate real use cases.
