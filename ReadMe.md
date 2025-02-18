# Library Management System

## Overview

The Library Management System is a web application designed to manage books, categories, and members in a library. It provides functionalities for adding, editing, deleting, and viewing books and members. The application is built using ASP.NET Core MVC and Blazor, targeting .NET 9.

## Features

- Manage Books: Add, edit, delete, and view books.
- Manage Categories: Add, edit, delete, and view categories.
- Manage Members: Add, edit, delete, and view members.
- Responsive design using Bootstrap for a clean and user-friendly interface.

## Architecture

The application follows a layered architecture with the following layers:

1. **Presentation Layer**: This layer includes the Blazor components and Razor views that handle the user interface. It uses Bootstrap for styling and responsiveness.

2. **Controller Layer**: This layer includes the controllers that handle HTTP requests and return appropriate responses. The controllers interact with the services to perform CRUD operations.

3. **Service Layer**: This layer includes the service interfaces and their implementations. Services contain the business logic and interact with the data access layer to perform operations.

4. **Data Access Layer**: This layer includes the data models and the context for interacting with the database. It uses Entity Framework Core for data access.

## Project Structure

- **Controllers**: Contains the controllers for handling HTTP requests.
- **Models**: Contains the data models representing the entities in the application.
- **Services**: Contains the service interfaces and their implementations.
- **Views**: Contains the Razor views for the MVC part of the application.
- **Components**: Contains the Blazor components for the Blazor part of the application.
- **wwwroot**: Contains static files such as CSS, JavaScript, and images.

## Technologies Used

- ASP.NET Core MVC
- Blazor
- Entity Framework Core
- Bootstrap
- Moq (for unit testing)
- xUnit (for unit testing)

## Getting Started

### Prerequisites

- .NET 9 SDK
- Visual Studio 2022 or later

### Installation

1. Clone the repository:
git clone https://github.com/your-repo/library-management-system.git

2. Navigate to the project directory:  
cd library-management-system

3. Restore the dependencies: 
dotnet restore

### Running the Application

1. Build the project:
dotnet build

2. Run the application:
dotnet run

3. Open a web browser and navigate to https://localhost:5001

### Running Tests

1. Navigate to the test project directory:
cd UnitTest

2. Run the tests:
dotnet test

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## Contact

For any questions or suggestions, please contact [taheeronline@gmail.com](mailto:taheeronline@gmail.com).

---

This README provides an overview of the Library Management System, its architecture, and instructions for getting started with the project.