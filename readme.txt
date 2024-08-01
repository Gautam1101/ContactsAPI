Overview

The Contacts API is a backend service designed to manage contact information. 
It allows CRUD (Create, Read, Update, Delete) operations on contact records.

Setup Instructions

1. Clone the Repository:

   Open your terminal and run the following command to clone the repository:
   git clone https://github.com/yourusername/contacts-api.git
   Then navigate into the project directory:
   cd contacts-api

2. Install .NET SDK:

   Make sure you have the .NET SDK installed on your system. You can download and install it from the .NET website if it's not already installed.

3. Build the API:

   Run the following command to build the project:
   dotnet build

4. Run the API:

   Start the API with the command:
   dotnet run

   The API will be available at https://localhost:7278.

How to Run the Application

1. Ensure .NET SDK is Installed:

   Verify that the .NET SDK is installed by running:
   dotnet --version

2. Start the API:

   After navigating to the contacts-api directory and building the project, start the API with:
   dotnet run

   You can access the API at https://localhost:7278. (might be different localhost for your pc)

Design Decisions and Application Structure

- Technology Stack:
  - .NET Core: Chosen for its robustness, cross-platform capabilities, and ease of development.
  - ASP.NET Core Web API: Provides the framework to build the RESTful services.

- Application Structure:
  - Controllers: Manage HTTP requests and responses. The primary controller handles CRUD operations for contacts.
  - Models: Define the data structure of contact entities.
  - Data Access Layer: Implements the repository pattern to interact with the database.
  - Configuration: Manages application settings and connection strings.

- Design Decisions:
  - RESTful API Design: Ensures the API is stateless and adheres to standard HTTP methods (GET, POST, PUT, DELETE).
  - Separation of Concerns: Utilizes different layers (controllers, services, and data access) to maintain clear boundaries and enhance maintainability.
  - Error Handling: Centralized error handling to provide consistent error responses to clients.

